using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thorlabs.MotionControl.DeviceManagerCLI;
using Thorlabs.MotionControl.GenericMotorCLI.ControlParameters;
using Thorlabs.MotionControl.IntegratedStepperMotorsCLI;
using Thorlabs.MotionControl.KCube.DCServoCLI;

namespace CL3_IF_DllSample
{
    /// <summary>
    /// Cleaned / modernized MainForm.
    /// - Removes tight while(true) polling loops; uses async/await + polling with delay & tolerance.
    /// - Wraps device initialization and shutdown; avoids hard-coded serials by reading TextBoxes (fallback to constants).
    /// - SerialPort safety: try/catch on open, DataReceived -> ConcurrentQueue, UI timer drains queue.
    /// - Consolidates repeated move code into helpers.
    /// - Adds CancellationTokenSource to stop background tasks cleanly.
    /// - Uses decimal tolerance comparisons instead of exact equality.
    /// </summary>
    public partial class MainForm : Form
    {
        // === Constants / Defaults ===
        private const int PollMs = 25;
        private const decimal PositionTolerance = 0.001m; // 1 µm tolerance (adjust for your stage units)
        private const string DefaultSerialX = "45252174";
        private const string DefaultSerialTheta = "27260648";
        private const string DefaultSerialZ = "45252164";
        private const string DefaultComPort = "COM12";

        // === Motion Controllers ===
        private LongTravelStage _x;      // Linear X
        private KCubeDCServo _theta;     // Rotary theta
        private LongTravelStage _z;      // Linear Z

        // === Serial (Pump or MCU) ===
        private readonly SerialPort _serial = new SerialPort(DefaultComPort, 9600, Parity.None, 8, StopBits.One);
        private readonly ConcurrentQueue<string> _serialQueue = new ConcurrentQueue<string>();
        private readonly System.Windows.Forms.Timer _serialDrainTimer = new System.Windows.Forms.Timer();

        // === Background control ===
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public MainForm()
        {
            InitializeComponent();
            InitializeSerial();
            _ = InitializeStagesAsync(_cts.Token);
        }

        #region --- Init / Dispose ---
        private void InitializeSerial()
        {
            try
            {
                _serial.NewLine = "\n";
                _serial.DataReceived += (s, e) =>
                {
                    try
                    {
                        string line = _serial.ReadExisting();
                        if (!string.IsNullOrWhiteSpace(line)) _serialQueue.Enqueue(line);
                    }
                    catch { /* swallow */ }
                };
                _serial.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open {_serial.PortName}: {ex.Message}", "Serial Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _serialDrainTimer.Interval = 50;
            _serialDrainTimer.Tick += (s, e) =>
            {
                while (_serialQueue.TryDequeue(out var msg))
                {
                    // Replace with your logging UI method
                    Console.WriteLine($"[SERIAL] {msg.Trim()}");
                }
            };
            _serialDrainTimer.Start();
        }

        private async Task InitializeStagesAsync(CancellationToken ct)
        {
            try
            {
                await Task.Run(() => DeviceManagerCLI.BuildDeviceList(), ct);

                // Read serials from textboxes if present; fallback to defaults
                string sx = SafeText(_textBoxSerialX, DefaultSerialX);
                string st = SafeText(_textBoxSerialTheta, DefaultSerialTheta);
                string sz = SafeText(_textBoxSerialZ, DefaultSerialZ);

                _x = LongTravelStage.CreateLongTravelStage(sx);
                _theta = KCubeDCServo.CreateKCubeDCServo(st);
                _z = LongTravelStage.CreateLongTravelStage(sz);

                // Connect & initialize
                ConnectAndInitLongTravel(_x, sx);
                ConnectAndInitKCube(_theta, st);
                ConnectAndInitLongTravel(_z, sz);

                // Example Z velocity setup (keep your existing UI bindings if needed)
                var velZ = _z.GetVelocityParams();
                velZ.MinVelocity = 0m;
                velZ.MaxVelocity = 60m;
                velZ.Acceleration = 120m;
                _z.SetVelocityParams(velZ);
                _z.SetBacklash(0m);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"Stage init failed: {ex.Message}", "Stages", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ConnectAndInitLongTravel(LongTravelStage stage, string serial)
        {
            if (stage == null) throw new InvalidOperationException("Stage not created");
            stage.Connect(serial);
            stage.GetMotorConfiguration(serial, Thorlabs.MotionControl.GenericMotorCLI.Settings.DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);
            if (!stage.IsSettingsInitialized())
            {
                stage.WaitForSettingsInitialized(5000);
            }
            stage.StartPolling(250);
            Thread.Sleep(500);
            stage.EnableDevice();
            Thread.Sleep(500);
            stage.LoadMotorConfiguration(serial);
        }

        private static void ConnectAndInitKCube(KCubeDCServo cube, string serial)
        {
            if (cube == null) throw new InvalidOperationException("K-Cube not created");
            cube.Connect(serial);
            if (!cube.IsSettingsInitialized())
            {
                cube.WaitForSettingsInitialized(5000);
            }
            cube.StartPolling(250);
            Thread.Sleep(500);
            cube.EnableDevice();
            Thread.Sleep(500);
            var mc = cube.LoadMotorConfiguration(serial);
            mc.DeviceSettingsName = "PRM1Z8"; // theta stage
            mc.UpdateCurrentConfiguration();
            var s = cube.MotorDeviceSettings as Thorlabs.MotionControl.KCube.DCServoCLI.Settings.KCubeDCMotorSettings;
            cube.SetSettings(s, true, false);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                _cts.Cancel();
                _serialDrainTimer?.Stop();
                if (_serial.IsOpen) _serial.Close();

                ShutdownStage(_x);
                ShutdownStage(_z);
                ShutdownCube(_theta);
            }
            catch { /* ignore */ }
        }

        private static void ShutdownStage(LongTravelStage stg)
        {
            if (stg == null) return;
            try
            {
                stg.StopPolling();
                stg.DisableDevice();
                stg.Disconnect(true);
            }
            catch { /* ignore */ }
        }

        private static void ShutdownCube(KCubeDCServo cube)
        {
            if (cube == null) return;
            try
            {
                cube.StopPolling();
                cube.DisableDevice();
                cube.Disconnect(true);
            }
            catch { /* ignore */ }
        }
        #endregion

        #region --- Helpers ---
        private static string SafeText(TextBox tb, string fallback) => (tb != null && !string.IsNullOrWhiteSpace(tb.Text)) ? tb.Text.Trim() : fallback;

        private static decimal SafeDec(TextBox tb, decimal fallback)
        {
            if (tb == null) return fallback;
            return decimal.TryParse(tb.Text, out var v) ? v : fallback;
        }

        private void SafeSerialWrite(string s)
        {
            try { if (_serial.IsOpen) _serial.Write(s); }
            catch { /* ignore */ }
        }

        private static bool IsAtTarget(decimal current, decimal target, decimal tol) => Math.Abs(current - target) <= tol;

        private static async Task WaitWhileMovingAsync(Func<bool> isMoving, CancellationToken ct)
        {
            // Polls device flag with short delays
            while (isMoving())
            {
                await Task.Delay(PollMs, ct);
            }
        }
        #endregion

        #region --- Unified Move APIs ---
        private async Task MoveRelativeXAsync(decimal delta, CancellationToken ct)
        {
            var vel = _x.GetVelocityParams();
            vel.MaxVelocity = SafeDec(textBox4, vel.MaxVelocity);
            vel.Acceleration = SafeDec(textBox14, vel.Acceleration);
            _x.SetVelocityParams(vel);

            var start = _x.Position;
            var target = start + delta;

            SafeSerialWrite("A");
            if (!_x.IsDeviceBusy) _x.MoveRelative(0, delta, 0);

            await WaitWhileMovingAsync(() => _x.Status.IsMoving, ct);

            // Final tolerance check (controller may stop near target)
            if (IsAtTarget(_x.Position, target, PositionTolerance))
            {
                SafeSerialWrite("B");
            }
        }

        private async Task MoveRelativeThetaAsync(decimal delta, CancellationToken ct)
        {
            var vel = _theta.GetVelocityParams();
            vel.MaxVelocity = SafeDec(textBox4, vel.MaxVelocity);
            vel.Acceleration = SafeDec(textBox14, vel.Acceleration);
            _theta.SetVelocityParams(vel);

            var start = _theta.DevicePosition;
            var target = start + delta;

            SafeSerialWrite("A");
            if (!_theta.IsDeviceBusy) _theta.MoveRelative(0, delta, 0);

            await WaitWhileMovingAsync(() => _theta.Status.IsMoving, ct);

            if (IsAtTarget(_theta.DevicePosition, target, PositionTolerance))
            {
                SafeSerialWrite("B");
            }
        }
        #endregion

        #region --- UI Event Handlers (cleaned) ---
        private async void buttonMoveDown_Click(object sender, EventArgs e)
        {
            // Move Z+ (down) by value from textBox16
            decimal dz = SafeDec(textBox16, 1m);
            try { await MoveRelativeXAsync(dz, _cts.Token); }
            catch (OperationCanceledException) { }
        }

        private async void buttonMoveUp_Click(object sender, EventArgs e)
        {
            decimal dz = SafeDec(textBox16, 1m);
            try { await MoveRelativeXAsync(-dz, _cts.Token); }
            catch (OperationCanceledException) { }
        }

        private async void buttonThetaClockwise_Click(object sender, EventArgs e)
        {
            decimal dth = SafeDec(textBox3, 5m);
            try { await MoveRelativeThetaAsync(dth, _cts.Token); }
            catch (OperationCanceledException) { }
        }

        private async void buttonThetaAntiClockwise_Click(object sender, EventArgs e)
        {
            decimal dth = SafeDec(textBox3, 5m);
            try { await MoveRelativeThetaAsync(-dth, _cts.Token); }
            catch (OperationCanceledException) { }
        }
        #endregion

        #region --- Sensor / PID (placeholder hooks) ---
        // Keep your existing CL3IF_... P/Invoke & methods. You can call heightZLoop() from a timer safely.
        private void timerPid_Tick(object sender, EventArgs e)
        {
            try
            {
                heightZLoop();
                // Trim chart series safely here (as in your code) without blocking UI
            }
            catch { /* ignore single tick errors */ }
        }

        // existing method in your project
        private void heightZLoop()
        {
            // NOTE: keep your original PID computation here.
            // This placeholder exists only to show where your existing logic continues to run.
        }
        #endregion
    }
}
