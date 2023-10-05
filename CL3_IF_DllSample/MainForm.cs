using CL3_IF_DllSample.SettingRWForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Thorlabs.MotionControl.DeviceManagerCLI;
using Thorlabs.MotionControl.GenericMotorCLI.AdvancedMotor;
using Thorlabs.MotionControl.GenericMotorCLI.KCubeMotor;
using Thorlabs.MotionControl.GenericMotorCLI.Settings;
using Thorlabs.MotionControl.GenericMotorCLI.ControlParameters;
using Thorlabs.MotionControl.IntegratedStepperMotorsCLI;
using Thorlabs.MotionControl.KCube.DCServoCLI;

using System.Text.RegularExpressions;
using Sharer;
using System.IO.Ports;
using SERIAL_RX_TX;
using System.Reflection;
using System.Collections;

namespace CL3_IF_DllSample
{
    public partial class MainForm : Form
    {

        private readonly DeviceData[] _deviceData;
        private readonly Label[] _deviceStatusLabels;

        private const int MaxRequestDataLength = 512000;

        private CL3IF_MEASUREMENT_DATA[] _trendData = new CL3IF_MEASUREMENT_DATA[MaxMeasurementDataCountPerTime];
        private uint _trendIndex;
        private uint _trendReceivedDataCount;

        // Maximum number of sequential acquired data - 10 seconds at sampling cycle 100us(fastest)
        private const int MaxSequenceMeasurementData = 100000;

        // Maximum number of acquired data per time
        private const int MaxMeasurementDataCountPerTime = 8000;

        //Analog output allocation value when no out assignment
        private const int AnalogOutAllocationNotUsed = 0;

        private CL3IF_MEASUREMENT_DATA[] _sequenceTrendData = new CL3IF_MEASUREMENT_DATA[MaxSequenceMeasurementData];
        private bool _isTrending = false;
        private System.Threading.Thread _threadTrend;
        private uint _sequenceTrendIndex;
        private int _sequenceTrendReceivedDataCount;

        private const int MaxLightWaveData = NativeMethods.CL3IF_LIGHT_WAVE_DATA_LENGTH * NativeMethods.CL3IF_MAX_LIGHT_WAVE_COUNT;
        private ushort[] _lightWaveData;

        private CL3IF_MEASUREMENT_DATA[] _storageData = new CL3IF_MEASUREMENT_DATA[MaxMeasurementDataCountPerTime];
        private uint _storageIndex;
        private uint _storageReceivedDataCount;

        private CL3IF_MEASUREMENT_DATA[] _sequenceStorageData = new CL3IF_MEASUREMENT_DATA[MaxSequenceMeasurementData];
        private bool _isStoraging = false;
        private System.Threading.Thread _threadStorage;
        private uint _sequenceStorageIndex;
        private int _sequenceStorageReceivedDataCount;

        private const string SaveCsvFileFilter = "CSV file(*.txt)|*.txt";
        private const string CsvSeparator = "\t";

        private const string SaveBinFileFilter = "BIN file(*.bin)|*.bin";

        decimal a = 0.0m;
        private Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller;
        private Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY;
        private Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ;
        private decimal heightData;

        private SerialPort port = new SerialPort("COM12", 9600, Parity.None, 8, StopBits.One);
        private SerialComPort serialPort;
        private string receivedData;
        private bool dataReady = false;
        private System.Windows.Forms.Timer receivedDataTimer;

        private double incrementX = 0;
        private decimal previousValue = 0m;


        private decimal kp = 1m;
        private decimal ki = 0.00001m;
        private decimal kd = 0.00001m;
        private decimal setpoint = 0.0m;
        private decimal integralTerm = 0.0m;
        private decimal prevError = 0.0m;
        private decimal error = 0.0m;
        private decimal proportionalTerm = 0.0m;
        private decimal derivativeTerm = 0.0m;
        private decimal controlSignal = 0.0m;


        public static ArrayList myListArcs = new ArrayList();
        public static ArrayList coordinateList = new ArrayList();
        private ArrayList arrayListVelocity = new ArrayList();

        private double distancePerDegree = 0;
        private string VelXY = "";
        private decimal posnX = 0m;
        private decimal posnY = 0m;


        /*        private LongTravelStage controller = LongTravelStage.CreateLongTravelStage("45252174");
                private KCubeDCServo controllerY;
                private LongTravelStage controllerZ;
        */
        // private decimal positionRelative = 0m;

        // Thread thread1 = new Thread(() => CountDown("Timer #1"));
        //Thread thread2 = new Thread(() => CountUp("Timer #2"));

        private int CurrentDeviceId
        {
            get { return GetSelectedDeviceId(); }
        }

        //=======================================================













        //=======================================================


        #region Initialize

        public MainForm()
        {

            InitializeComponent();
            serialPort = new SerialComPort();
            serialPort.RegisterReceiveCallback(ReceiveDataHandler);
            receivedDataTimer = new System.Windows.Forms.Timer();
            receivedDataTimer.Interval = 25;   // 25 ms
            receivedDataTimer.Tick += new EventHandler(ReceivedDataTimerTick);
            receivedDataTimer.Start();

            _deviceData = new DeviceData[NativeMethods.CL3IF_MAX_DEVICE_COUNT];
            _deviceStatusLabels = new Label[] { _labelDeviceStatus0, _labelDeviceStatus1, _labelDeviceStatus2 };

            for (int i = 0; i < NativeMethods.CL3IF_MAX_DEVICE_COUNT; i++)
            {
                _deviceData[i] = new DeviceData();
            }

            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";
            Console.WriteLine(mainThread.Name);

            //test();

            // Thread thread1 = new Thread(() => CountDown("Timer #1"));
            // Thread thread2 = new Thread(() => CountUp("Timer #2",controller));

            //-----------------------------------------------------------------------------------

            //private const int ankit = 559;


            // thread1.Start();
            //thread2.Start();

            // Console.WriteLine(mainThread.Name + " is complete!");
            //SerialPort port = new SerialPort(textBox13.Text, 9600, Parity.None, 8, StopBits.One);  
            port.Open();

            (controller, controllerY, controllerZ) = initializeXYZ();


            // decimal  x =
            /*      Console.WriteLine(controller.Position);
                  Console.WriteLine(controllerY.Position);
                  Console.WriteLine(controllerZ.Position);*/


            InitializeAllDeviceStatement();

            InitializeComboBox();
        }

        private void ReceiveDataHandler(string data)
        {
            if (dataReady)
            {
                Console.WriteLine("Received data was thrown away because line buffer not emptied");
            }
            else
            {
                dataReady = true;
                receivedData = data;
            }
        }

        private void ReceivedDataTimerTick(object sender, EventArgs e)
        {
            if (dataReady)
            {
                dataReady = false;
                Console.WriteLine(receivedData);
            }
        }

        public void Move_Down(String name, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller)
        {
            // VelocityParameters() sd = controller.mov
            VelocityParameters velPars = controller.GetVelocityParams();

            velPars.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velPars.Acceleration = Convert.ToDecimal(textBox14.Text);
            // controllerY.

            controller.SetVelocityParams(velPars);
            decimal positionInitial = controller.Position;
            port.Write("A");


            if (!controller.IsDeviceBusy)
            {
                controller.MoveRelative(0, Convert.ToDecimal(textBox16.Text), 0);
            }
            //sing Stopwatch;
            //  Stopwatch stopwatch = new Stopwatch();
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            
            while (true)
            {
                //Keyence_thread();
                decimal newPosX = controller.Position;

                //System.Console.WriteLine("Moving...");
                // System.Console.WriteLine(newPosX);
                //System.Console.WriteLine(controller.Status.IsMoving);
                //System.Console.WriteLine("posn X: {0}", position);

                if (controller.Status.IsMoving == false)
                {

                    //System.Console.WriteLine("Moved SuccessfullyX!! {0}", (newPosX == Convert.ToDecimal(textBox3.Text)));

                }

                if ((newPosX == Convert.ToDecimal(textBox16.Text) + positionInitial))
                {
                    //System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                    port.Write("B");
                    break;
                }

            }
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("Timer #1 is complete!");
        }
        public void Move_Up(String name, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller)
        {
            VelocityParameters velPars = controller.GetVelocityParams();

            velPars.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velPars.Acceleration = Convert.ToDecimal(textBox14.Text);
            // controllerY.

            controller.SetVelocityParams(velPars);
            decimal positionInitial = controller.Position;


            //decimal x = System.DateTime.Now.Millisecond;
        
            port.Write("A");
            if (!controller.IsDeviceBusy)
            {
                controller.MoveRelative(0, -1 * Convert.ToDecimal(textBox16.Text), 0);
            }

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            while (true)
            {
                //Keyence_thread();
                decimal newPosX = controller.Position;

                //System.Console.WriteLine("Moving...");
                //System.Console.WriteLine(newPosX);
                //System.Console.WriteLine(controller.Status.IsMoving);
                //System.Console.WriteLine("posn X: {0}", position);

                if (controller.Status.IsMoving == false)
                {

                    //System.Console.WriteLine("Moved SuccessfullyX!! {0}", (newPosX == Convert.ToDecimal(textBox3.Text)));

                }

                if ((newPosX == -1 * Convert.ToDecimal(textBox16.Text) + positionInitial))
                {
                    //System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                    port.Write("B");
                    break;
                }

            }
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("Timer #2 is complete!");
        }

        public void Move_Clock(String name, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY)
        {
            VelocityParameters velParsY = controllerY.GetVelocityParams();

            velParsY.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velParsY.Acceleration = Convert.ToDecimal(textBox14.Text);

            controllerY.SetVelocityParams(velParsY);
            decimal positionInitial = controllerY.DevicePosition;
            //port.Write("A");
            if (!controllerY.IsDeviceBusy)
            {
                port.Write("A");
                controllerY.MoveRelative(0, Convert.ToDecimal(textBox3.Text), 0);
            }
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();


            while (true)
            {
                //Keyence_thread();
                decimal newPosY = controllerY.DevicePosition;

                System.Console.WriteLine("Moving...");
                System.Console.WriteLine(newPosY);
                System.Console.WriteLine(controllerY.Status.IsMoving);
                //System.Console.WriteLine("posn X: {0}", position);

                if (controllerY.Status.IsMoving == false)
                {

                    System.Console.WriteLine("Moved SuccessfullyY!! {0}", (Decimal.ToInt32(newPosY) == Decimal.ToInt32(Convert.ToDecimal(textBox3.Text))));

                }

                if ((Decimal.ToInt32(newPosY) == Decimal.ToInt32(Convert.ToDecimal(textBox3.Text) + positionInitial)))
                {
                    System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                    port.Write("B");
                    break;
                }

            }


            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine(controllerY.DevicePosition);
        }

        public void Move_AntiClock(String name, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY)
        {
            // controllerY.MoveTo(-5m, 0);
            VelocityParameters velParsY = controllerY.GetVelocityParams();

            velParsY.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velParsY.Acceleration = Convert.ToDecimal(textBox14.Text);
            // controllerY.

            controllerY.SetVelocityParams(velParsY);
            decimal positionInitial = controllerY.DevicePosition;
            //port.Write("A");
            if (!controllerY.IsDeviceBusy)
            {
                port.Write("A");
                controllerY.MoveRelative(0, -1 * Convert.ToDecimal(textBox3.Text), 0);
            }
            //controllerY.MoveRelative(0, 5m, 0);

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            while (true)
            {
                //Keyence_thread();
                decimal newPosY = controllerY.DevicePosition;

                System.Console.WriteLine("Moving...");
                System.Console.WriteLine(newPosY);
                System.Console.WriteLine(controllerY.Status.IsMoving);
                //System.Console.WriteLine("posn X: {0}", position);

                if (controllerY.Status.IsMoving == false)
                {

                    System.Console.WriteLine("Moved SuccessfullyY!! {0}", (Decimal.ToInt32(newPosY) == Decimal.ToInt32(Convert.ToDecimal(textBox3.Text))));

                }

                if ((Decimal.ToInt32(newPosY) == Decimal.ToInt32(-1 * Convert.ToDecimal(textBox3.Text) + positionInitial)))
                {
                    System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                    port.Write("B");
                    break;
                }

            }

            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("Timer #4 is complete!");
        }
        private void InitializeComboBox()
        {
            textBox3.ReadOnly = false;
            textBox3.Text = "5";

            textBox4.ReadOnly = false;
            textBox4.Text = "10";

            _comboBoxGetLightWaveformHead.SelectedIndex = 0;

            _comboBoxAutoZeroSingleOutNo.SelectedIndex = 0;
            _comboBoxAutoZeroSingleOnOff.SelectedIndex = 0;
            _comboBoxAutoZeroMultiOnOff.SelectedIndex = 0;
            _comboBoxAutoZeroGroupOnOff.SelectedIndex = 0;

            _comboBoxResetOutNo.SelectedIndex = 0;

            _comboBoxTimingSingleOutNo.SelectedIndex = 0;
            _comboBoxTimingSingleOnOff.SelectedIndex = 0;
            _comboBoxTimingMultiOutValue.SelectedIndex = 0;
            _comboBoxTimingOutGroupValue.SelectedIndex = 0;

            _comboBoxSwitchProgramProgramNo.SelectedIndex = 0;
            _comboBoxLockPanelOnOff.SelectedIndex = 0;

            _comboBoxSelectedIndex.SelectedIndex = 0;

            _comboBoxSetLaserControlValue.SelectedIndex = 0;
            _comboBoxSetMeasureEnableValue.SelectedIndex = 0;

            _comboBoxLightIntensityTuning.SelectedIndex = 0;
            _comboBoxCalibration.SelectedIndex = 0;

            _comboBoxSettingProgramNo.SelectedIndex = 0;
            _comboBoxSettingHead.SelectedIndex = 0;
            _comboBoxSettingOut.SelectedIndex = 0;

            _comboBoxTargetProgramNo.SelectedIndex = 0;
        }

        private void InitializeAllDeviceStatement()
        {
            for (int i = 0; i < _deviceData.Length; i++)
            {
                _deviceData[i].Status = DeviceStatus.NoConnection;
                _deviceStatusLabels[i].Text = _deviceData[i].GetStatusString();
            }
        }

        #endregion


        #region initiaizeXYZ controllers
        public (Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage) initializeXYZ()
        {

            {
                /*
                                try
                                {
                                    DeviceManagerCLI.BuildDeviceList();
                                }

                                catch (Exception)
                                {
                                    Console.WriteLine("Faillll");
                                   // return;

                                }

                                List<string> serialNumbers = DeviceManagerCLI.GetDeviceList();
                                List<string> serialNumbersPrefix = DeviceManagerCLI.GetDeviceList(LongTravelStage.DevicePrefix);


                                if (serialNumbers.Count > 0)
                                {
                                    Console.WriteLine(serialNumbers[0]);
                                    Console.WriteLine(serialNumbers[1]);

                                }
                                else
                                {
                                    Console.WriteLine("No connected Devices");
                                   // return;

                                }
                                String serialNoX = "45252174";
                                String serialNoY = "27260648";
                                String serialNoZ = "45252164";

                                LongTravelStage controller = LongTravelStage.CreateLongTravelStage(serialNoX);
                                //Console.WriteLine(controller.GetType());
                                KCubeDCServo controllerY = KCubeDCServo.CreateKCubeDCServo(serialNoY);
                                LongTravelStage controllerZ = LongTravelStage.CreateLongTravelStage(serialNoZ);

                                if (controller != null)
                                {
                                    controller.Connect(serialNoX);
                                    controller.GetMotorConfiguration(serialNoX, DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);

                                }

                                if (controllerY != null)
                                {
                                    controllerY.Connect(serialNoY);
                                    controllerY.GetMotorConfiguration(serialNoY, DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);

                                }

                                if (controllerZ != null)
                                {
                                    controllerZ.Connect(serialNoZ);
                                    controllerZ.GetMotorConfiguration(serialNoZ, DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);

                                }

                                // Wait for the controller settings to initialize - timeout 5000ms
                                if (!controller.IsSettingsInitialized())
                                {
                                    try
                                    {
                                        Console.WriteLine("Settings init start");

                                        controller.WaitForSettingsInitialized(5000);

                                        Console.WriteLine("Settings init finish");

                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Settings failed to initialize");
                                    }
                                }

                                if (!controllerY.IsSettingsInitialized())
                                {
                                    try
                                    {
                                        Console.WriteLine("Settings init start");

                                        controllerY.WaitForSettingsInitialized(5000);

                                        Console.WriteLine("Settings init finish");

                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Settings failed to initialize");
                                    }
                                }

                                if (!controllerZ.IsSettingsInitialized())
                                {
                                    try
                                    {
                                        Console.WriteLine("Settings init start");

                                        controllerZ.WaitForSettingsInitialized(5000);

                                        Console.WriteLine("Settings init finish");

                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Settings failed to initialize");
                                    }
                                }

                                // Start the controller polling
                                // The polling loop requests regular status requests to the motor to ensure the program keeps track of the controller. 
                                controller.StartPolling(250);
                                // Needs a delay so that the current enabled state can be obtained
                                Thread.Sleep(500);
                                // Enable the channel otherwise any move is ignored 
                                controller.EnableDevice();
                                Thread.Sleep(500);
                                controllerY.StartPolling(250);
                                Thread.Sleep(500);
                                controllerY.EnableDevice();
                                // Needs a delay to give time for the controller to be enabled
                                Thread.Sleep(500);
                                controllerZ.StartPolling(250);
                                Thread.Sleep(500);
                                controllerZ.EnableDevice();
                                // Needs a delay to give time for the controller to be enabled
                                Thread.Sleep(500);

                                // Call LoadMotorConfiguration on the controller to initialize the DeviceUnitConverter object required for real world unit parameters
                                //  - loads configuration information into channel
                                MotorConfiguration motorConfiguration = controller.LoadMotorConfiguration(serialNoX);
                                MotorConfiguration motorConfigurationY = controllerY.LoadMotorConfiguration(serialNoY);
                                MotorConfiguration motorConfigurationZ = controllerZ.LoadMotorConfiguration(serialNoZ);

                                // Not used directly in example but illustrates how to obtain controller settings
                                ThorlabsIntegratedStepperMotorSettings currentDeviceSettings = controller.MotorDeviceSettings as ThorlabsIntegratedStepperMotorSettings;
                                ThorlabsIntegratedStepperMotorSettings currentDeviceSettingsZ = controllerZ.MotorDeviceSettings as ThorlabsIntegratedStepperMotorSettings;

                                // Display info about controller
                                DeviceInfo deviceInfo = controller.GetDeviceInfo();
                                Console.WriteLine("controller {0} = {1}", deviceInfo.SerialNumber, deviceInfo.Name);

                                DeviceInfo deviceInfoZ = controllerZ.GetDeviceInfo();
                                Console.WriteLine("controller {0} = {1}", deviceInfoZ.SerialNumber, deviceInfoZ.Name);

                                //--------------------------------------------------
                                // The API requires stage type to be specified
                                motorConfigurationY.DeviceSettingsName = "PRM1Z8";

                                // Get the device unit converter
                                motorConfigurationY.UpdateCurrentConfiguration();

                                // Not used directly in example but illustrates how to obtain device settings
                                KCubeDCMotorSettings currentDeviceSettingsY = controllerY.MotorDeviceSettings as KCubeDCMotorSettings;

                                // Updates the motor controller with the selected settings
                                controllerY.SetSettings(currentDeviceSettingsY, true, false);

                                // Display info about device
                                DeviceInfo deviceInfoY = controllerY.GetDeviceInfo();
                                Console.WriteLine("Device {0} = {1}", deviceInfoY.SerialNumber, deviceInfoY.Name);


                                //--------------------------------------------------


                                bool homed = controller.Status.IsHomed;
                                Console.WriteLine(homed);

                                decimal position = 2000.00m;
                                double diameter = 28.64; // input value mm
                                double circumference = 2 * Math.PI * (diameter / 2);
                                double distancePerDegree = ((circumference) / 360); //mm per degree
                                decimal distanceToMove = 80m; // input value is always less than circumfernce
                                decimal positionY = distanceToMove / (decimal)distancePerDegree;
                                decimal velocity = 15m;

                                bool home = false;
                                bool movement = controller.Status.IsInMotion;
                                //Console.WriteLine("movementt " + movement);

                                Decimal newPosX = controller.Position;
                                Decimal newPosY = controllerY.Position;

                                //Console.WriteLine("controller Moved to {0}", newPos);

                                int counter = 0;

                                Console.WriteLine(newPosY);

                                VelocityParameters velPars = controller.GetVelocityParams();
                                VelocityParameters velParsY = controllerY.GetVelocityParams();

                          *//*      velPars.MaxVelocity = 40m;
                                velParsY.MaxVelocity = 190m;
                                velPars.Acceleration = 90m;
                                velParsY.Acceleration = 120m;
                                controller.SetVelocityParams(velPars); //SetHomeVelocity //isDeviceBusy //isDeviceAvailable
                                controllerY.SetVelocityParams(velParsY);*//*

                                //controllerY.Home(9000);*/
                // return (controller, controllerY, controllerZ);
            }

            {

                try
                {
                    DeviceManagerCLI.BuildDeviceList();
                }

                catch (Exception)
                {
                    Console.WriteLine("Faillll");
                    // return;

                }

                List<string> serialNumbers = DeviceManagerCLI.GetDeviceList();
                List<string> serialNumbersPrefix = DeviceManagerCLI.GetDeviceList(LongTravelStage.DevicePrefix);


                if (serialNumbers.Count > 0)
                {
                    Console.WriteLine(serialNumbers[0]);
                    Console.WriteLine(serialNumbers[1]);

                }
                else
                {
                    Console.WriteLine("No connected Devices");
                    // return;

                }
                String serialNoX = "45252174";
                String serialNoY = "27260648";
                String serialNoZ = "45252164";

                LongTravelStage controller = LongTravelStage.CreateLongTravelStage(serialNoX);
                LongTravelStage controllerZ = LongTravelStage.CreateLongTravelStage(serialNoZ);
                KCubeDCServo controllerY = KCubeDCServo.CreateKCubeDCServo(serialNoY);

                if (controller != null)
                {
                    controller.Connect(serialNoX);
                    controller.GetMotorConfiguration(serialNoX, DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);

                }

                if (controllerY != null)
                {
                    controllerY.Connect(serialNoY);
                    //controllerY.GetMotorConfiguration(serialNoY, DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);

                }

                if (controllerZ != null)
                {
                    controllerZ.Connect(serialNoZ);
                    controllerZ.GetMotorConfiguration(serialNoZ, DeviceConfiguration.DeviceSettingsUseOptionType.UseDeviceSettings);

                }

                // Wait for the controller settings to initialize - timeout 5000ms
                if (!controller.IsSettingsInitialized())
                {
                    try
                    {
                        Console.WriteLine("Settings init start");

                        controller.WaitForSettingsInitialized(5000);

                        Console.WriteLine("Settings init finish");

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Settings failed to initialize");
                    }
                }

                if (!controllerY.IsSettingsInitialized())
                {
                    try
                    {
                        Console.WriteLine("Settings init start");

                        controllerY.WaitForSettingsInitialized(5000);

                        Console.WriteLine("Settings init finish");

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Settings failed to initialize");
                    }
                }

                if (!controllerZ.IsSettingsInitialized())
                {
                    try
                    {
                        Console.WriteLine("Settings init start");

                        controllerZ.WaitForSettingsInitialized(5000);

                        Console.WriteLine("Settings init finish");

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Settings failed to initialize");
                    }
                }

                // Start the controller polling
                // The polling loop requests regular status requests to the motor to ensure the program keeps track of the controller. 
                controller.StartPolling(250);
                // Needs a delay so that the current enabled state can be obtained
                Thread.Sleep(500);
                // Enable the channel otherwise any move is ignored 
                controller.EnableDevice();
                Thread.Sleep(500);
                controllerY.StartPolling(250);
                Thread.Sleep(500);
                controllerY.EnableDevice();
                // Needs a delay to give time for the controller to be enabled
                Thread.Sleep(500);
                controllerZ.StartPolling(250);
                Thread.Sleep(500);
                controllerZ.EnableDevice();
                Thread.Sleep(500);

                // Call LoadMotorConfiguration on the controller to initialize the DeviceUnitConverter object required for real world unit parameters
                //  - loads configuration information into channel
                MotorConfiguration motorConfiguration = controller.LoadMotorConfiguration(serialNoX);
                MotorConfiguration motorConfigurationY = controllerY.LoadMotorConfiguration(serialNoY);
                MotorConfiguration motorConfigurationZ = controllerZ.LoadMotorConfiguration(serialNoZ);


                // Not used directly in example but illustrates how to obtain controller settings
                ThorlabsIntegratedStepperMotorSettings currentDeviceSettings = controller.MotorDeviceSettings as ThorlabsIntegratedStepperMotorSettings;
                ThorlabsIntegratedStepperMotorSettings currentDeviceSettingsZ = controllerZ.MotorDeviceSettings as ThorlabsIntegratedStepperMotorSettings;


                // Display info about controller
                DeviceInfo deviceInfo = controller.GetDeviceInfo();
                Console.WriteLine("controller {0} = {1}", deviceInfo.SerialNumber, deviceInfo.Name);

                DeviceInfo deviceInfoZ = controllerZ.GetDeviceInfo();
                Console.WriteLine("controller {0} = {1}", deviceInfoZ.SerialNumber, deviceInfoZ.Name);

                //--------------------------------------------------
                // The API requires stage type to be specified
                motorConfigurationY.DeviceSettingsName = "PRM1Z8";

                // Get the device unit converter
                motorConfigurationY.UpdateCurrentConfiguration();

                // Not used directly in example but illustrates how to obtain device settings
                KCubeDCMotorSettings currentDeviceSettingsY = controllerY.MotorDeviceSettings as KCubeDCMotorSettings;

                // Updates the motor controller with the selected settings
                controllerY.SetSettings(currentDeviceSettingsY, true, false);

                // Display info about device
                DeviceInfo deviceInfoY = controllerY.GetDeviceInfo();
                Console.WriteLine("Device {0} = {1}", deviceInfoY.SerialNumber, deviceInfoY.Name);


                //--------------------------------------------------


                bool homed = controller.Status.IsHomed;
                Console.WriteLine(homed);

                decimal position = 0.00m;
                double diameter = 29.00; // input value mm
                double circumference = 2 * Math.PI * (diameter / 2);
                double distancePerDegree = ((circumference) / 360); //mm per degree
                decimal distanceToMove = 80m; // input value is always less than circumfernce
                decimal positionY = distanceToMove / (decimal)distancePerDegree;
                decimal velocity = 15m;

                bool home = false;
                bool movement = controller.Status.IsInMotion;
                //Console.WriteLine("movementt " + movement);

                Decimal newPosX = controller.Position;
                Decimal newPosY = controllerY.Position;
                //Console.WriteLine("controller Moved to {0}", newPos);

                int counter = 0;
                Console.WriteLine(newPosY);

                VelocityParameters velParsZ = controllerZ.GetVelocityParams();
                velParsZ.MinVelocity = 60m;
                velParsZ.MaxVelocity = 60m;
                velParsZ.Acceleration = 120m; //0
                controllerZ.SetVelocityParams(velParsZ);
                controllerZ.SetBacklash(0);

                return (controller, controllerY, controllerZ);
            }


            // timer3.Start();
        }

        #endregion

        #region Communication control

        private void _buttonOpenUsbCommunication_Click(object sender, EventArgs e)
        {
            timer3.Start();
            timer4.Start();
            int returnCode = NativeMethods.CL3IF_OpenUsbCommunication(CurrentDeviceId, 5000);

            OutputLogMessage("OpenUsbCommunication", returnCode);

            SetDeviceStatement(returnCode, DeviceStatus.Usb);
            _deviceStatusLabels[CurrentDeviceId].Text = _deviceData[CurrentDeviceId].GetStatusString();
        }

        private void _buttonOpenEthernetCommunication_Click(object sender, EventArgs e)
        {
            CL3IF_ETHERNET_SETTING ethernetSetting = new CL3IF_ETHERNET_SETTING();
            ethernetSetting.ipAddress = new byte[4];
            if (!byte.TryParse(_textBoxFirstSegment.Text, out ethernetSetting.ipAddress[0]))
            {
                MessageBox.Show(this, "ipAddress firstSegment is Invalid Value");
                return;
            }
            if (!byte.TryParse(_textBoxSecondSegment.Text, out ethernetSetting.ipAddress[1]))
            {
                MessageBox.Show(this, "ipAddress secondSegment is Invalid Value");
                return;
            }
            if (!byte.TryParse(_textBoxThirdSegment.Text, out ethernetSetting.ipAddress[2]))
            {
                MessageBox.Show(this, "ipAddress thirdSegment is Invalid Value");
                return;
            }
            if (!byte.TryParse(_textBoxFourthSegment.Text, out ethernetSetting.ipAddress[3]))
            {
                MessageBox.Show(this, "ipAddress fourthSegment is Invalid Value");
                return;
            }
            if (!ushort.TryParse(_textBoxPortNo.Text, out ethernetSetting.portNo))
            {
                MessageBox.Show(this, "ipAddress Port number is Invalid Value");
                return;
            }

            ethernetSetting.reserved = new byte[2];
            ethernetSetting.reserved[0] = 0x00;
            ethernetSetting.reserved[1] = 0x00;
            int returnCode = NativeMethods.CL3IF_OpenEthernetCommunication(CurrentDeviceId, ref ethernetSetting, 10000);

            OutputLogMessage("OpenEthernetCommunication", returnCode);

            SetDeviceStatement(returnCode, DeviceStatus.Ethernet);
            _deviceData[CurrentDeviceId].EthernetSetting = ethernetSetting;
            _deviceStatusLabels[CurrentDeviceId].Text = _deviceData[CurrentDeviceId].GetStatusString();
        }

        private void SetDeviceStatement(int returnCode, DeviceStatus status)
        {
            if (returnCode == NativeMethods.CL3IF_RC_OK)
            {
                _deviceData[CurrentDeviceId].Status = status;
            }
            else
            {
                _deviceData[CurrentDeviceId].Status = DeviceStatus.NoConnection;
            }
        }

        private void _buttonCloseCommunication_Click(object sender, EventArgs e)
        {
            CommunicationClose(CurrentDeviceId);
        }

        private void CommunicationClose(int deviceId)
        {
            int returnCode = NativeMethods.CL3IF_CloseCommunication(deviceId);

            OutputLogMessage("CloseCommunication", returnCode);

            _deviceData[deviceId].Status = DeviceStatus.NoConnection;
            _deviceStatusLabels[deviceId].Text = _deviceData[deviceId].GetStatusString();
        }

        private int GetSelectedDeviceId()
        {
            foreach (Control control in _pnlDeviceId.Controls)
            {
                RadioButton radioButton = control as RadioButton;
                if ((radioButton == null) || (!radioButton.Checked)) continue;

                return Convert.ToInt32(radioButton.Tag);
            }
            return -1;
        }
        #endregion

        #region Measurement

        private void _buttonGetMeasurementData_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {
                while (true)
                {
                    CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                    measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[NativeMethods.CL3IF_MAX_OUT_COUNT];

                    int returnCode = NativeMethods.CL3IF_GetMeasurementData(CurrentDeviceId, pin.Pointer);

                    //Console.WriteLine(measurementData.outMeasurementData[1].measurementValue.ToString());


                    if (returnCode != NativeMethods.CL3IF_RC_OK)
                    {
                        OutputLogMessage("GetMeasurementData", returnCode);
                        return;
                    }

                    measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(CL3IF_ADD_INFO));
                    int readPosition = Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                    for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                    {
                        measurementData.outMeasurementData[i] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                        readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                    }
                    /*
                                    List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                                    loggingProperties.Add(new LoggingProperty("triggerCount", measurementData.addInfo.triggerCount.ToString()));
                                    loggingProperties.Add(new LoggingProperty("pulseCount", measurementData.addInfo.pulseCount.ToString()));
                                    for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                                    {
                                        string outNumber = "[OUT" + (i + 1) + "]";
                                        loggingProperties.Add(new LoggingProperty(outNumber + "measurementValue", measurementData.outMeasurementData[i].measurementValue.ToString()));
                                        loggingProperties.Add(new LoggingProperty(outNumber + "valueInfo", ((CL3IF_VALUE_INFO)measurementData.outMeasurementData[i].valueInfo).ToString()));
                                        loggingProperties.Add(new LoggingProperty(outNumber + "judgeResult", measurementData.outMeasurementData[i].judgeResult.ToString()));
                                    }
                    */
                    OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString());

                    // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                    //Console.WriteLine("GetMeasurementData", returnCode, loggingProperties);
                }
            }
        }

        private void _buttonGetTrendIndex_Click(object sender, EventArgs e)
        {
            uint index = 0;
            int returnCode = NativeMethods.CL3IF_GetTrendIndex(CurrentDeviceId, out index);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("index", index.ToString()) };
            OutputLogMessage("GetTrendIndex", returnCode, loggingProperties);

            if (returnCode != NativeMethods.CL3IF_RC_OK) return;
            _textBoxTrendIndex.Text = index.ToString();
        }

        private void _buttonGetTrendData_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {
                uint index = 0;
                if (!uint.TryParse(_textBoxTrendIndex.Text, out index))
                {
                    MessageBox.Show(this, "index is Invalid Value");
                    return;
                }
                uint requestDataCount = 0;
                if (!uint.TryParse(_textBoxTrendRequestDataCount.Text, out requestDataCount) || requestDataCount > MaxMeasurementDataCountPerTime)
                {
                    MessageBox.Show(this, "requestDataCount is Invalid Value");
                    return;
                }

                uint nextIndex = 0;
                uint obtainedDataCount = 0;
                CL3IF_OUTNO outTarget = 0;
                int returnCode = NativeMethods.CL3IF_GetTrendData(CurrentDeviceId, index, requestDataCount, out nextIndex, out obtainedDataCount, out outTarget, pin.Pointer);

                List<int> outTargetList = ConvertOutTargetList(outTarget);
                List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                loggingProperties.Add(new LoggingProperty("targetOut", CreateTargetOutSequence(outTargetList)));
                loggingProperties.Add(new LoggingProperty("nextIndex", nextIndex.ToString()));
                loggingProperties.Add(new LoggingProperty("obtainedDataCount", obtainedDataCount.ToString()));
                OutputLogMessage("GetTrendData", returnCode, loggingProperties);

                if (returnCode != NativeMethods.CL3IF_RC_OK) return;
                _trendIndex = (uint)index;
                _trendReceivedDataCount = (uint)obtainedDataCount;
                _trendData = new CL3IF_MEASUREMENT_DATA[MaxMeasurementDataCountPerTime];
                int readPosition = 0;
                for (uint i = 0; i < obtainedDataCount; i++)
                {
                    CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                    measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[outTargetList.Count];

                    measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_ADD_INFO));
                    readPosition += Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                    for (int j = 0; j < outTargetList.Count; j++)
                    {
                        measurementData.outMeasurementData[j] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                        readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                    }
                    _trendData[i] = measurementData;
                }
            }
        }

        private string CreateTargetOutSequence(IList<int> outTargetList)
        {
            StringBuilder targetOut = new StringBuilder();
            for (int i = 0; i < outTargetList.Count; i++)
            {
                if (0 < i)
                {
                    targetOut.Append(",");
                }

                targetOut.Append(outTargetList[i].ToString());
            }
            return targetOut.ToString();
        }

        private List<int> ConvertOutTargetList(CL3IF_OUTNO outTarget)
        {
            byte mask = 1;
            List<int> outList = new List<int>();
            for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
            {
                if (((ushort)outTarget & mask) != 0)
                {
                    outList.Add(i + 1);
                }
                mask = (byte)(mask << 1);
            }
            return outList;
        }

        private void _buttonSaveAsTrendData_Click(object sender, EventArgs e)
        {
            if (_trendReceivedDataCount <= 0)
            {
                MessageBox.Show(this, "No Trend Data");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveCsvFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (StreamWriter fileStream = new StreamWriter(dialog.FileName, false, Encoding.GetEncoding("ASCII")))
                    {
                        for (uint i = 0; i < _trendReceivedDataCount; i++)
                        {
                            CL3IF_MEASUREMENT_DATA currentTrendData = _trendData[i];
                            StringBuilder logMessage = new StringBuilder();
                            logMessage.Append((_trendIndex + i).ToString());
                            logMessage.Append(CsvSeparator + currentTrendData.addInfo.triggerCount);
                            logMessage.Append(CsvSeparator + currentTrendData.addInfo.pulseCount);
                            for (int j = 0; j < currentTrendData.outMeasurementData.Length; j++)
                            {
                                logMessage.Append(CsvSeparator + currentTrendData.outMeasurementData[j].measurementValue);
                            }
                            fileStream.WriteLine(logMessage.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetTrendData " + exception.GetType() + Environment.NewLine);
                }
            }
        }

        private void _buttonStartGettingData_Click(object sender, EventArgs e)
        {
            if (_isTrending)
            {
                StopTrendProcess();
                return;
            }

            StopStorageProcess();
            _buttonStartGettingData.Text = "Stop getting data";
            _sequenceTrendReceivedDataCount = 0;
            _sequenceTrendIndex = 0;
            _isTrending = true;
            SynchronizationContext context = System.Threading.SynchronizationContext.Current;
            _threadTrend = new System.Threading.Thread(ContinuouslyExecuteTrendProcess);
            _threadTrend.Start(context);
        }

        private void _buttonTrendContinuouslySave_Click(object sender, EventArgs e)
        {
            if (_sequenceTrendReceivedDataCount <= 0)
            {
                MessageBox.Show(this, "No Trend Continuously Data");
                return;
            }

            StopTrendProcess();
            StopStorageProcess();
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveCsvFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (StreamWriter fileStream = new StreamWriter(dialog.FileName, false, Encoding.GetEncoding("ASCII")))
                    {
                        for (int i = 0; i < _sequenceTrendReceivedDataCount; i++)
                        {
                            CL3IF_MEASUREMENT_DATA currentTrendData = _sequenceTrendData[i];
                            StringBuilder logMessage = new StringBuilder();
                            logMessage.Append((_sequenceTrendIndex + i).ToString());
                            logMessage.Append(CsvSeparator + currentTrendData.addInfo.triggerCount);
                            logMessage.Append(CsvSeparator + currentTrendData.addInfo.pulseCount);

                            for (int j = 0; j < currentTrendData.outMeasurementData.Length; j++)
                            {
                                logMessage.Append(CsvSeparator + currentTrendData.outMeasurementData[j].measurementValue);
                            }
                            fileStream.WriteLine(logMessage.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetTrendDataContinuously " + exception.GetType() + Environment.NewLine);
                }
            }
        }

        private void ContinuouslyExecuteTrendProcess(object state)
        {
            CL3IF_MEASUREMENT_DATA[] trendDataList = new CL3IF_MEASUREMENT_DATA[MaxSequenceMeasurementData];
            byte[] buffer = new byte[MaxRequestDataLength];

            // Get trend index
            uint index = 0;
            uint indexGet = 0;
            int returnCodeTrendIndex = NativeMethods.CL3IF_GetTrendIndex(CurrentDeviceId, out index);
            this.Invoke((MethodInvoker)(() =>
            {
                List<LoggingProperty> loggingTrendIndexProperties = new List<LoggingProperty>() { new LoggingProperty("index", index.ToString()) };
                OutputLogMessage("GetTrendIndex", returnCodeTrendIndex, loggingTrendIndexProperties);
            }));
            if (returnCodeTrendIndex != NativeMethods.CL3IF_RC_OK)
            {
                StopTrendProcess();
                _threadTrend.Abort();
                return;
            }

            indexGet = index;
            _sequenceTrendIndex = indexGet;

            // Get trend data continuously
            while (_isTrending)
            {
                uint nextIndex = 0;
                uint obtainedDataCount = 0;
                int returnCodeTrendData = 0;
                CL3IF_OUTNO outTarget = 0;
                using (PinnedObject pin = new PinnedObject(buffer))
                {
                    returnCodeTrendData = NativeMethods.CL3IF_GetTrendData(CurrentDeviceId, indexGet, MaxMeasurementDataCountPerTime, out nextIndex, out obtainedDataCount, out outTarget, pin.Pointer);

                    if (returnCodeTrendData != NativeMethods.CL3IF_RC_OK)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            OutputLogMessage("GetTrendData", returnCodeTrendData, new List<LoggingProperty>());
                        }));
                        StopTrendProcess();
                        break;
                    }

                    indexGet = nextIndex;
                    List<int> outTargetList = ConvertOutTargetList(outTarget);
                    int readPosition = 0;
                    int trendDataCount = 0;
                    for (uint i = 0; i < obtainedDataCount; i++)
                    {
                        if (MaxSequenceMeasurementData <= i + _sequenceTrendReceivedDataCount)
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                OutputLogMessage("GetTrendData", returnCodeTrendData, new List<LoggingProperty>());
                            }));
                            StopTrendProcess();
                            break;
                        }

                        CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                        measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[outTargetList.Count];
                        measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_ADD_INFO));
                        readPosition += Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                        for (int j = 0; j < outTargetList.Count; j++)
                        {
                            measurementData.outMeasurementData[j] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                            readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                        }

                        trendDataList[i + _sequenceTrendReceivedDataCount] = measurementData;
                        trendDataCount++;
                    }
                    _sequenceTrendReceivedDataCount += trendDataCount;

                }
                this.Invoke((MethodInvoker)(() =>
                {
                    List<LoggingProperty> loggingTrendDataProperties = new List<LoggingProperty>() { };
                    loggingTrendDataProperties.Add(new LoggingProperty("nextIndex", nextIndex.ToString()));
                    loggingTrendDataProperties.Add(new LoggingProperty("obtainedDataCount", obtainedDataCount.ToString()));
                    OutputLogMessage("GetTrendData", returnCodeTrendData, loggingTrendDataProperties);

                    _textBoxGettingDataCount.Text = _sequenceTrendReceivedDataCount.ToString();
                }));

                System.Threading.Thread.Sleep(50);
            }
            _sequenceTrendData = trendDataList;
            _threadTrend.Abort();
        }

        private void StopTrendProcess()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                _buttonStartGettingData.Text = "Start getting data";
                _isTrending = false;
            }));
        }

        private void _buttonGetTerminalStatus_Click(object sender, EventArgs e)
        {
            ushort inputTerminalStatus = 0;
            ushort outputTerminalStatus = 0;
            int returnCode = NativeMethods.CL3IF_GetTerminalStatus(CurrentDeviceId, out inputTerminalStatus, out outputTerminalStatus);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("inputTerminalStatus", inputTerminalStatus.ToString()));
            loggingProperties.Add(new LoggingProperty("outputTerminalStatus", outputTerminalStatus.ToString()));
            OutputLogMessage("GetTerminalStatus", returnCode, loggingProperties);
        }

        private void _buttonGetLightWaveform_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[MaxRequestDataLength];
            byte headNo;
            if (!byte.TryParse(_comboBoxGetLightWaveformHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "HEAD is Invalid Value");
                return;
            }

            CL3IF_PEAKNO peakNo = 0;
            peakNo |= _checkBoxGetLightWaveformPeakNo1.Checked ? CL3IF_PEAKNO.CL3IF_PEAKNO_01 : (CL3IF_PEAKNO)0;
            peakNo |= _checkBoxGetLightWaveformPeakNo2.Checked ? CL3IF_PEAKNO.CL3IF_PEAKNO_02 : (CL3IF_PEAKNO)0;
            peakNo |= _checkBoxGetLightWaveformPeakNo3.Checked ? CL3IF_PEAKNO.CL3IF_PEAKNO_03 : (CL3IF_PEAKNO)0;
            peakNo |= _checkBoxGetLightWaveformPeakNo4.Checked ? CL3IF_PEAKNO.CL3IF_PEAKNO_04 : (CL3IF_PEAKNO)0;
            using (PinnedObject pin = new PinnedObject(buffer))
            {
                int returnCode = NativeMethods.CL3IF_GetLightWaveform(CurrentDeviceId, headNo, peakNo, pin.Pointer);

                OutputLogMessage("GetLightWaveform", returnCode);

                if (returnCode != NativeMethods.CL3IF_RC_OK) return;
                _lightWaveData = new ushort[MaxLightWaveData];
                int readPosition = 0;
                for (int i = 0; i < MaxLightWaveData; i++)
                {
                    _lightWaveData[i] = (ushort)(Marshal.ReadInt16(pin.Pointer + readPosition));
                    readPosition += sizeof(ushort);
                }
            }
        }

        private void _buttonSaveLightWave_Click(object sender, EventArgs e)
        {
            if (_lightWaveData == null || _lightWaveData.Length <= 0)
            {
                MessageBox.Show(this, "No LightWave Data");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveCsvFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (StreamWriter fileStream = new StreamWriter(dialog.FileName, false, Encoding.GetEncoding("ASCII")))
                    {
                        for (int i = 0; i < MaxLightWaveData; i++)
                        {
                            fileStream.WriteLine(_lightWaveData[i]);
                        }
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetLightWave " + exception.GetType() + Environment.NewLine);
                }
            }
        }

        private void _buttonSwitchProgram_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSwitchProgramProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "programNo is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_SwitchProgram(CurrentDeviceId, programNo);

            OutputLogMessage("SwitchProgram", returnCode);
        }

        private void _buttonGetProgramNo_Click(object sender, EventArgs e)
        {
            byte programNo;
            int returnCode = NativeMethods.CL3IF_GetProgramNo(CurrentDeviceId, out programNo);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("programNo", programNo.ToString()) };
            OutputLogMessage("GetProgramNo", returnCode, loggingProperties);
        }

        private void _buttonLockPanel_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_LockPanel(CurrentDeviceId, _comboBoxLockPanelOnOff.SelectedIndex == 0);

            OutputLogMessage("LockPanel", returnCode);
        }

        private void _buttonAutoZeroSingle_Click(object sender, EventArgs e)
        {
            byte outNo;
            if (!byte.TryParse(_comboBoxAutoZeroSingleOutNo.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_AutoZeroSingle(CurrentDeviceId, outNo, _comboBoxAutoZeroSingleOnOff.SelectedIndex == 0);

            OutputLogMessage("AutoZeroSingle", returnCode);
        }

        private void _buttonAutoZeroMulti_Click(object sender, EventArgs e)
        {
            CL3IF_OUTNO outNo = 0;
            outNo |= _checkBoxAutoZeroMultiOutNo1.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_01 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo2.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_02 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo3.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_03 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo4.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_04 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo5.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_05 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo6.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_06 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo7.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_07 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxAutoZeroMultiOutNo8.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_08 : (CL3IF_OUTNO)0;
            int returnCode = NativeMethods.CL3IF_AutoZeroMulti(CurrentDeviceId, outNo, _comboBoxAutoZeroMultiOnOff.SelectedIndex == 0);

            OutputLogMessage("AutoZeroMulti", returnCode);
        }

        private void _buttonAutoZeroGroup_Click(object sender, EventArgs e)
        {
            CL3IF_ZERO_GROUP group = 0;
            group |= _checkBoxAutoZeroGroup1.Checked ? CL3IF_ZERO_GROUP.CL3IF_ZERO_GROUP_01 : (CL3IF_ZERO_GROUP)0;
            group |= _checkBoxAutoZeroGroup2.Checked ? CL3IF_ZERO_GROUP.CL3IF_ZERO_GROUP_02 : (CL3IF_ZERO_GROUP)0;
            int returnCode = NativeMethods.CL3IF_AutoZeroGroup(CurrentDeviceId, group, _comboBoxAutoZeroGroupOnOff.SelectedIndex == 0);

            OutputLogMessage("AutoZeroGroup", returnCode);
        }

        private void _buttonTimingSingle_Click(object sender, EventArgs e)
        {
            byte outNo;
            if (!byte.TryParse(_comboBoxTimingSingleOutNo.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_TimingSingle(CurrentDeviceId, outNo, _comboBoxTimingSingleOnOff.SelectedIndex == 0);

            OutputLogMessage("TimingSingle", returnCode);
        }

        private void _buttonTimingMulti_Click(object sender, EventArgs e)
        {
            CL3IF_OUTNO outNo = 0;
            outNo |= _checkBoxTimingOut1.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_01 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut2.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_02 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut3.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_03 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut4.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_04 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut5.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_05 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut6.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_06 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut7.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_07 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxTimingOut8.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_08 : (CL3IF_OUTNO)0;
            int returnCode = NativeMethods.CL3IF_TimingMulti(CurrentDeviceId, outNo, _comboBoxTimingMultiOutValue.SelectedIndex == 0);

            OutputLogMessage("TimingMulti", returnCode);
        }

        private void _buttonTimingGroup_Click(object sender, EventArgs e)
        {
            CL3IF_TIMING_GROUP group = 0;
            group |= _checkBoxTimingOutGroup1.Checked ? CL3IF_TIMING_GROUP.CL3IF_TIMING_GROUP_01 : (CL3IF_TIMING_GROUP)0;
            group |= _checkBoxTimingOutGroup2.Checked ? CL3IF_TIMING_GROUP.CL3IF_TIMING_GROUP_02 : (CL3IF_TIMING_GROUP)0;
            int returnCode = NativeMethods.CL3IF_TimingGroup(CurrentDeviceId, group, _comboBoxTimingOutGroupValue.SelectedIndex == 0);

            OutputLogMessage("TimingGroup", returnCode);
        }

        private void _buttonResetSingle_Click(object sender, EventArgs e)
        {
            byte outNo;
            if (!byte.TryParse(_comboBoxResetOutNo.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_ResetSingle(CurrentDeviceId, outNo);

            OutputLogMessage("ResetSingle", returnCode);
        }

        private void _buttonResetMulti_Click(object sender, EventArgs e)
        {
            CL3IF_OUTNO outNo = 0;
            outNo |= _checkBoxResetOut1.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_01 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut2.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_02 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut3.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_03 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut4.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_04 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut5.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_05 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut6.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_06 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut7.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_07 : (CL3IF_OUTNO)0;
            outNo |= _checkBoxResetOut8.Checked ? CL3IF_OUTNO.CL3IF_OUTNO_08 : (CL3IF_OUTNO)0;
            int returnCode = NativeMethods.CL3IF_ResetMulti(CurrentDeviceId, outNo);

            OutputLogMessage("ResetMulti", returnCode);
        }

        private void _buttonResetGroup_Click(object sender, EventArgs e)
        {
            CL3IF_RESET_GROUP group = 0;
            group |= _checkBoxResetOutGroup1.Checked ? CL3IF_RESET_GROUP.CL3IF_RESET_GROUP_01 : (CL3IF_RESET_GROUP)0;
            group |= _checkBoxResetOutGroup2.Checked ? CL3IF_RESET_GROUP.CL3IF_RESET_GROUP_02 : (CL3IF_RESET_GROUP)0;
            int returnCode = NativeMethods.CL3IF_ResetGroup(CurrentDeviceId, group);

            OutputLogMessage("ResetGroup", returnCode);
        }

        #endregion

        #region Set/Get settings

        private void _buttonSetSamplingCycle_Click(object sender, EventArgs e)
        {
            using (SamplingCycleForm form = new SamplingCycleForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetSamplingCycle(CurrentDeviceId, programNo, (CL3IF_SAMPLINGCYCLE)form.SelectSamplingCycle);

                OutputLogMessage("SetSamplingCycle", returnCode);
            }
        }

        private void _buttonGetSamplingCycle_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            CL3IF_SAMPLINGCYCLE samplingCycle;
            int returnCode = NativeMethods.CL3IF_GetSamplingCycle(CurrentDeviceId, programNo, out samplingCycle);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("samplingCycle", samplingCycle.ToString()) };
            OutputLogMessage("GetSamplingCycle", returnCode, loggingProperties);
        }

        private void _buttonSetMutualInterferencePrevention_Click(object sender, EventArgs e)
        {
            using (MutualInterferencePreventionForm form = new MutualInterferencePreventionForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetMutualInterferencePrevention(CurrentDeviceId, programNo, form.OnOff, form.Group);

                OutputLogMessage("SetMutualInterferencePrevention", returnCode);
            }
        }

        private void _buttonGetMutualInterferencePrevention_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            bool onOff;
            ushort group;
            int returnCode = NativeMethods.CL3IF_GetMutualInterferencePrevention(CurrentDeviceId, programNo, out onOff, out group);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("onOff", onOff ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("group", Convert.ToString(group, 2).PadLeft(NativeMethods.CL3IF_MAX_HEAD_COUNT, '0')));
            OutputLogMessage("GetMutualInterferencePrevention", returnCode, loggingProperties);
        }

        private void _buttonSetAmbientLightFilter_Click(object sender, EventArgs e)
        {
            using (AmbientLightFilterForm form = new AmbientLightFilterForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetAmbientLightFilter(CurrentDeviceId, programNo, form.OnOff);

                OutputLogMessage("SetAmbientLightFilter", returnCode);
            }
        }

        private void _buttonGetAmbientLightFilter_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            bool onOff;
            int returnCode = NativeMethods.CL3IF_GetAmbientLightFilter(CurrentDeviceId, programNo, out onOff);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("onOff", onOff ? "ON" : "OFF") };
            OutputLogMessage("GetAmbientLightFilter", returnCode, loggingProperties);
        }

        private void _buttonSetJudgmentOutput_Click(object sender, EventArgs e)
        {
            using (JudgmentOutputForm form = new JudgmentOutputForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetJudgmentOutput(CurrentDeviceId, programNo, form.JudgmentOutput);

                OutputLogMessage("SetJudgmentOutput", returnCode);
            }
        }

        private void _buttonGetJudgmentOutput_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            byte[] buffer = new byte[MaxRequestDataLength];

            using (PinnedObject pin = new PinnedObject(buffer))
            {
                int returnCode = NativeMethods.CL3IF_GetJudgmentOutput(CurrentDeviceId, programNo, pin.Pointer);

                List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                {
                    string outNumber = "[OUT" + (i + 1) + "]";
                    CL3IF_JUDGMENT_OUTPUT info = (CL3IF_JUDGMENT_OUTPUT)Marshal.PtrToStructure(pin.Pointer + i * Marshal.SizeOf(typeof(CL3IF_JUDGMENT_OUTPUT)), typeof(CL3IF_JUDGMENT_OUTPUT));
                    loggingProperties.Add(new LoggingProperty(outNumber + "logic", ((CL3IF_LOGIC)info.logic).ToString()));
                    loggingProperties.Add(new LoggingProperty(outNumber + "strobe", ((CL3IF_STROBE)info.strobe).ToString()));
                    loggingProperties.Add(new LoggingProperty(outNumber + "hi", Convert.ToString(info.hi, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')));
                    loggingProperties.Add(new LoggingProperty(outNumber + "go", Convert.ToString(info.go, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')));
                    loggingProperties.Add(new LoggingProperty(outNumber + "lo", Convert.ToString(info.lo, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')));
                }

                OutputLogMessage("GetJudgmentOutput", returnCode, loggingProperties);
            }
        }

        private void _buttonSetStorageNumber_Click(object sender, EventArgs e)
        {
            using (StorageNumberForm form = new StorageNumberForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetStorageNumber(CurrentDeviceId, programNo, form.Overwrite, form.StorageNumber);

                OutputLogMessage("SetStorageNumber", returnCode);
            }
        }

        private void _buttonGetStorageNumber_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            byte overwrite;
            uint storageNumber;
            int returnCode = NativeMethods.CL3IF_GetStorageNumber(CurrentDeviceId, programNo, out overwrite, out storageNumber);

            const byte Overridable = 1;
            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("overwrite", overwrite == Overridable ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("storageNumber", storageNumber.ToString()));
            OutputLogMessage("GetStorageNumber", returnCode, loggingProperties);
        }

        private void _buttonSetStorageTiming_Click(object sender, EventArgs e)
        {
            using (StorageTimingForm form = new StorageTimingForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                byte storageTiming = form.StorageTiming;
                CL3IF_STORAGETIMING_PARAM param = new CL3IF_STORAGETIMING_PARAM();
                if ((CL3IF_STORAGETIMING)storageTiming == CL3IF_STORAGETIMING.CL3IF_STORAGETIMING_MEASUREMENT)
                {
                    param.paramMeasurement.storageCycle = form.StorageCycle;
                }
                else if ((CL3IF_STORAGETIMING)storageTiming == CL3IF_STORAGETIMING.CL3IF_STORAGETIMING_JUDGMENT)
                {
                    param.paramJudgment.logic = form.Judgment.logic;
                    param.paramJudgment.hi = form.Judgment.hi;
                    param.paramJudgment.go = form.Judgment.go;
                    param.paramJudgment.lo = form.Judgment.lo;
                }

                int returnCode = NativeMethods.CL3IF_SetStorageTiming(CurrentDeviceId, programNo, storageTiming, param);

                OutputLogMessage("SetStorageTiming", returnCode);
            }
        }

        private void _buttonGetStorageTiming_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            byte storageTiming;
            CL3IF_STORAGETIMING_PARAM param = new CL3IF_STORAGETIMING_PARAM();

            int returnCode = NativeMethods.CL3IF_GetStorageTiming(CurrentDeviceId, programNo, out storageTiming, out param);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            if (returnCode != NativeMethods.CL3IF_RC_OK)
            {
                OutputLogMessage("GetStorageTiming", returnCode, loggingProperties);
                return;
            }

            loggingProperties.Add(new LoggingProperty("storageTiming", ((CL3IF_STORAGETIMING)storageTiming).ToString()));
            if ((CL3IF_STORAGETIMING)storageTiming == CL3IF_STORAGETIMING.CL3IF_STORAGETIMING_MEASUREMENT)
            {
                loggingProperties.Add(new LoggingProperty("storageCycle", param.paramMeasurement.storageCycle.ToString()));
            }
            else if ((CL3IF_STORAGETIMING)storageTiming == CL3IF_STORAGETIMING.CL3IF_STORAGETIMING_JUDGMENT)
            {
                loggingProperties.Add(new LoggingProperty("logic", ((CL3IF_LOGIC)(param.paramJudgment.logic)).ToString()));
                loggingProperties.Add(new LoggingProperty("hi", Convert.ToString((byte)param.paramJudgment.hi, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')));
                loggingProperties.Add(new LoggingProperty("go", Convert.ToString((byte)param.paramJudgment.go, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')));
                loggingProperties.Add(new LoggingProperty("lo", Convert.ToString((byte)param.paramJudgment.lo, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')));
            }

            OutputLogMessage("GetStorageTiming", returnCode, loggingProperties);
        }

        private void _buttonSetStorageTarget_Click(object sender, EventArgs e)
        {
            using (StorageTargetForm form = new StorageTargetForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetStorageTarget(CurrentDeviceId, programNo, form.OutNo);

                OutputLogMessage("SetStorageTarget", returnCode);
            }
        }

        private void _buttonGetStorageTarget_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            CL3IF_OUTNO outNo;
            int returnCode = NativeMethods.CL3IF_GetStorageTarget(CurrentDeviceId, programNo, out outNo);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("outNo", Convert.ToString((byte)outNo, 2).PadLeft(NativeMethods.CL3IF_MAX_OUT_COUNT, '0')) };
            OutputLogMessage("GetStorageTarget", returnCode, loggingProperties);
        }

        private void _buttonSetAnalogOutAllocation_Click(object sender, EventArgs e)
        {
            using (AnalogOutAllocationForm form = new AnalogOutAllocationForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetAnalogOutAllocation(CurrentDeviceId, programNo, form.Ch1Out, form.Ch2Out, form.Ch3Out, form.Ch4Out);

                OutputLogMessage("SetAnalogOutAllocation", returnCode);
            }
        }

        private void _buttonGetAnalogOutAllocation_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }

            byte ch1Out, ch2Out, ch3Out, ch4Out;
            int returnCode = NativeMethods.CL3IF_GetAnalogOutAllocation(CurrentDeviceId, programNo, out ch1Out, out ch2Out, out ch3Out, out ch4Out);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>();
            loggingProperties.Add(new LoggingProperty("ch1Out", ch1Out == AnalogOutAllocationNotUsed ? "Not used" : "OUT" + ch1Out));
            loggingProperties.Add(new LoggingProperty("ch2Out", ch2Out == AnalogOutAllocationNotUsed ? "Not used" : "OUT" + ch2Out));
            loggingProperties.Add(new LoggingProperty("ch3Out", ch3Out == AnalogOutAllocationNotUsed ? "Not used" : "OUT" + ch3Out));
            loggingProperties.Add(new LoggingProperty("ch4Out", ch4Out == AnalogOutAllocationNotUsed ? "Not used" : "OUT" + ch4Out));
            OutputLogMessage("GetAnalogOutAllocation", returnCode, loggingProperties);
        }

        private void _buttonSetMedianFilter_Click(object sender, EventArgs e)
        {
            using (MedianFilterForm form = new MedianFilterForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetMedianFilter(CurrentDeviceId, programNo, headNo, (CL3IF_MEDIANFILTER)form.MedianFilter);

                OutputLogMessage("SetMedianFilter", returnCode);
            }
        }

        private void _buttonGetMedianFilter_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            CL3IF_MEDIANFILTER medianFilter;
            int returnCode = NativeMethods.CL3IF_GetMedianFilter(CurrentDeviceId, programNo, headNo, out medianFilter);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("medianFilter", medianFilter.ToString()) };
            OutputLogMessage("GetMedianFilter", returnCode, loggingProperties);
        }

        private void _buttonSetThreshold_Click(object sender, EventArgs e)
        {
            using (ThresholdForm form = new ThresholdForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetThreshold(CurrentDeviceId, programNo, headNo, (CL3IF_MODE)form.Mode, form.Value);

                OutputLogMessage("SetThreshold", returnCode);
            }
        }

        private void _buttonGetThreshold_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            CL3IF_MODE mode;
            byte value;
            int returnCode = NativeMethods.CL3IF_GetThreshold(CurrentDeviceId, programNo, headNo, out mode, out value);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("mode", mode.ToString()));
            loggingProperties.Add(new LoggingProperty("value", value.ToString()));
            OutputLogMessage("GetThreshold", returnCode, loggingProperties);
        }

        private void _buttonSetMask_Click(object sender, EventArgs e)
        {
            using (MaskForm form = new MaskForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetMask(CurrentDeviceId, programNo, headNo, form.OnOff, form.Position1, form.Position2);

                OutputLogMessage("SetMask", returnCode);
            }
        }

        private void _buttonGetMask_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            bool onOff;
            int position1;
            int position2;
            int returnCode = NativeMethods.CL3IF_GetMask(CurrentDeviceId, programNo, headNo, out onOff, out position1, out position2);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("onOff", onOff ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("position1", position1.ToString()));
            loggingProperties.Add(new LoggingProperty("position2", position2.ToString()));
            OutputLogMessage("GetMask", returnCode, loggingProperties);
        }

        private void _buttonSetLightIntensityControl_Click(object sender, EventArgs e)
        {
            using (LightIntensityControlForm form = new LightIntensityControlForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetLightIntensityControl(CurrentDeviceId, programNo, headNo, (CL3IF_MODE)form.Mode, form.UpperLimit, form.LowerLimit);

                OutputLogMessage("SetLightIntensityControl", returnCode);
            }
        }

        private void _buttonGetLightIntensityControl_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            CL3IF_MODE mode;
            byte upperLimit;
            byte lowerLimit;
            int returnCode = NativeMethods.CL3IF_GetLightIntensityControl(CurrentDeviceId, programNo, headNo, out mode, out upperLimit, out lowerLimit);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("mode", mode.ToString()));
            loggingProperties.Add(new LoggingProperty("upperLimit", upperLimit.ToString()));
            loggingProperties.Add(new LoggingProperty("lowerLimit", lowerLimit.ToString()));
            OutputLogMessage("GetLightIntensityControl", returnCode, loggingProperties);
        }

        private void _buttonSetPeakShapeFilter_Click(object sender, EventArgs e)
        {
            using (PeakShapeFilterForm form = new PeakShapeFilterForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetPeakShapeFilter(CurrentDeviceId, programNo, headNo, form.OnOff, (CL3IF_INTENSITY)form.Intensity);

                OutputLogMessage("SetPeakShapeFilter", returnCode);
            }
        }

        private void _buttonGetPeakShapeFilter_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            bool onOff;
            CL3IF_INTENSITY intensity;
            int returnCode = NativeMethods.CL3IF_GetPeakShapeFilter(CurrentDeviceId, programNo, headNo, out onOff, out intensity);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("onOff", onOff ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("intensity", intensity.ToString()));
            OutputLogMessage("GetPeakShapeFilter", returnCode, loggingProperties);
        }

        private void _buttonSetLightIntensityIntegration_Click(object sender, EventArgs e)
        {
            using (LightIntensityIntegrationForm form = new LightIntensityIntegrationForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetLightIntensityIntegration(CurrentDeviceId, programNo, headNo, (CL3IF_INTEGRATION_NUMBER)form.IntegrationNumber);

                OutputLogMessage("SetLightIntensityIntegration", returnCode);
            }
        }

        private void _buttonGetLightIntensityIntegration_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            CL3IF_INTEGRATION_NUMBER integrationNumber;
            int returnCode = NativeMethods.CL3IF_GetLightIntensityIntegration(CurrentDeviceId, programNo, headNo, out integrationNumber);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("integrationNumber", integrationNumber.ToString()) };
            OutputLogMessage("GetLightIntensityIntegration", returnCode, loggingProperties);
        }

        private void _buttonSetMeasurementPeaks_Click(object sender, EventArgs e)
        {
            using (MeasurementPeaksForm form = new MeasurementPeaksForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetMeasurementPeaks(CurrentDeviceId, programNo, headNo, form.Peaks);

                OutputLogMessage("SetMeasurementPeaks", returnCode);
            }
        }

        private void _buttonGetMeasurementPeaks_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            byte peaks;
            int returnCode = NativeMethods.CL3IF_GetMeasurementPeaks(CurrentDeviceId, programNo, headNo, out peaks);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("peaks", peaks.ToString()) };
            OutputLogMessage("GetMeasurementPeaks", returnCode, loggingProperties);
        }

        private void _buttonSetCheckingNumberOfPeaks_Click(object sender, EventArgs e)
        {
            using (CheckingNumberOfPeaksForm form = new CheckingNumberOfPeaksForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetCheckingNumberOfPeaks(CurrentDeviceId, programNo, headNo, form.OnOff);

                OutputLogMessage("SetCheckingNumberOfPeaks", returnCode);
            }
        }

        private void _buttonGetCheckingNumberOfPeaks_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            bool onOff;
            int returnCode = NativeMethods.CL3IF_GetCheckingNumberOfPeaks(CurrentDeviceId, programNo, headNo, out onOff);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("onOff", onOff ? "ON" : "OFF") };
            OutputLogMessage("GetCheckingNumberOfPeaks", returnCode, loggingProperties);
        }

        private void _buttonSetMultiLightIntensityControl_Click(object sender, EventArgs e)
        {
            using (MultiLightIntensityControlForm form = new MultiLightIntensityControlForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetMultiLightIntensityControl(CurrentDeviceId, programNo, headNo, form.OnOff);

                OutputLogMessage("SetMultiLightIntensityControl", returnCode);
            }
        }

        private void _buttonGetMultiLightIntensityControl_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            bool onOff;
            int returnCode = NativeMethods.CL3IF_GetMultiLightIntensityControl(CurrentDeviceId, programNo, headNo, out onOff);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("onOff", onOff ? "ON" : "OFF") };
            OutputLogMessage("GetMultiLightIntensityControl", returnCode, loggingProperties);
        }

        private void _buttonSetRefractiveIndexCorrection_Click(object sender, EventArgs e)
        {
            using (RefractiveIndexCorrectionForm form = new RefractiveIndexCorrectionForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                CL3IF_MATERIAL layer1 = (CL3IF_MATERIAL)form.Layer1;
                CL3IF_MATERIAL layer2 = (CL3IF_MATERIAL)form.Layer2;
                CL3IF_MATERIAL layer3 = (CL3IF_MATERIAL)form.Layer3;
                int returnCode = NativeMethods.CL3IF_SetRefractiveIndexCorrection(CurrentDeviceId, programNo, headNo, form.OnOff, layer1, layer2, layer3);

                OutputLogMessage("SetRefractiveIndexCorrection", returnCode);
            }
        }

        private void _buttonGetRefractiveIndexCorrection_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            bool onOff;
            CL3IF_MATERIAL layer1, layer2, layer3;
            int returnCode = NativeMethods.CL3IF_GetRefractiveIndexCorrection(CurrentDeviceId, programNo, headNo, out onOff, out layer1, out layer2, out layer3);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("onOff", onOff ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("layer1", layer1.ToString()));
            loggingProperties.Add(new LoggingProperty("layer2", layer2.ToString()));
            loggingProperties.Add(new LoggingProperty("layer3", layer3.ToString()));
            OutputLogMessage("GetRefractiveIndexCorrection", returnCode, loggingProperties);
        }

        private void _buttonSetQuadProcessing_Click(object sender, EventArgs e)
        {
            using (QuadProcessingForm form = new QuadProcessingForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetQuadProcessing(CurrentDeviceId, programNo, headNo, (CL3IF_QUADPROCESSING)form.Processing, form.ValidPoints);

                OutputLogMessage("SetQuadProcessing", returnCode);
            }
        }

        private void _buttonGetQuadProcessing_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            CL3IF_QUADPROCESSING quadProcessing;
            byte quadValidPoint;
            int returnCode = NativeMethods.CL3IF_GetQuadProcessing(CurrentDeviceId, programNo, headNo, out quadProcessing, out quadValidPoint);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("quadProcessing", quadProcessing.ToString()));
            loggingProperties.Add(new LoggingProperty("quadValidPoint", quadValidPoint.ToString()));
            OutputLogMessage("GetQuadProcessing", returnCode, loggingProperties);
        }

        private void _buttonSetHighSensitivity_Click(object sender, EventArgs e)
        {
            using (HighSensitivityForm form = new HighSensitivityForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte headNo;
                if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
                {
                    MessageBox.Show(this, "Target HEAD is Invalid Value");
                    return;
                }

                CL3IF_HIGH_SENSITIVITY highSensitivity;
                highSensitivity.highSensitivityOnOff = form.HighSensitivityOnOff;
                highSensitivity.highSensitivityStrength = form.HighSensitivityStrength;
                highSensitivity.thresholdValueFractionalPoint = form.ThresholdValueFractionalPoint;
                int returnCode = NativeMethods.CL3IF_SetHighSensitivity(CurrentDeviceId, programNo, headNo, highSensitivity);

                OutputLogMessage("SetHighSensitivity", returnCode);
            }
        }

        private void _buttonGetHighSensitivity_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte headNo;
            if (!byte.TryParse(_comboBoxSettingHead.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            CL3IF_HIGH_SENSITIVITY highSensitivity;
            int returnCode = NativeMethods.CL3IF_GetHighSensitivity(CurrentDeviceId, programNo, headNo, out highSensitivity);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("highSensitivityOnOff", highSensitivity.highSensitivityOnOff ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("highSensitivityStrength", highSensitivity.highSensitivityStrength.ToString()));
            loggingProperties.Add(new LoggingProperty("thresholdValueFractionalPoint", highSensitivity.thresholdValueFractionalPoint.ToString()));
            OutputLogMessage("GetHighSensitivity", returnCode, loggingProperties);
        }

        private void _buttonSetMeasurementMethod_Click(object sender, EventArgs e)
        {
            using (MeasurementMethodForm form = new MeasurementMethodForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                CL3IF_MEASUREMENTMETHOD method = (CL3IF_MEASUREMENTMETHOD)form.Method;
                CL3IF_MEASUREMENTMETHOD_PARAM param = new CL3IF_MEASUREMENTMETHOD_PARAM();
                switch (method)
                {
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_DISPLACEMENT:
                        param.paramDisplacement.headNo = (byte)form.TargetHead1;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_DISPLACEMENT_FOR_TRANSPARENT:
                        param.paramDisplacementForTransparent.headNo = (byte)form.TargetHead1;
                        param.paramDisplacementForTransparent.peak = (byte)form.Peak1;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_THICKNESS_FOR_TRANSPARENT:
                        param.paramThicknessForTransparent.headNo = (byte)form.TargetHead1;
                        param.paramThicknessForTransparent.peak1 = (byte)form.Peak1;
                        param.paramThicknessForTransparent.peak2 = (byte)form.Peak2;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_THICKNESS_2HEADS:
                        param.paramThickness2Heads.headNo1 = (byte)form.TargetHead1;
                        param.paramThickness2Heads.headNo2 = (byte)form.TargetHead2;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_HEIGHTDIFFERENCE_2HEADS:
                        param.paramHeightDifference2Heads.headNo1 = (byte)form.TargetHead1;
                        param.paramHeightDifference2Heads.headNo2 = (byte)form.TargetHead2;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_FORMULA:
                        param.paramFormula.factorA = form.FactorA;
                        param.paramFormula.factorB = form.FactorB;
                        param.paramFormula.factorC = form.FactorC;
                        param.paramFormula.targetOutX = form.TargetOutX;
                        param.paramFormula.targetOutY = form.TargetOutY;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_AVERAGE:
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_PEAK_TO_PEAK:
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_MAX:
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_MIN:
                        param.paramOutOperation.targetOut = form.TargetOutNo;
                        break;
                    case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_NO_CALCULATION:
                        break;
                }

                int returnCode = NativeMethods.CL3IF_SetMeasurementMethod(CurrentDeviceId, programNo, outNo, method, param);

                OutputLogMessage("SetMeasurementMethod", returnCode);
            }
        }

        private void _buttonGetMeasurementMethod_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            CL3IF_MEASUREMENTMETHOD method;
            CL3IF_MEASUREMENTMETHOD_PARAM param = new CL3IF_MEASUREMENTMETHOD_PARAM();
            int returnCode = NativeMethods.CL3IF_GetMeasurementMethod(CurrentDeviceId, programNo, outNo, out method, out param);

            if (returnCode != NativeMethods.CL3IF_RC_OK)
            {
                OutputLogMessage("GetMeasurementMethod", returnCode);
                return;
            }

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("method", method.ToString()));
            const int HeadNumberCorrection = 1;
            switch (method)
            {
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_DISPLACEMENT:
                    loggingProperties.Add(new LoggingProperty("headNo", (param.paramDisplacement.headNo + HeadNumberCorrection).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_DISPLACEMENT_FOR_TRANSPARENT:
                    loggingProperties.Add(new LoggingProperty("headNo", (param.paramDisplacementForTransparent.headNo + HeadNumberCorrection).ToString()));
                    loggingProperties.Add(new LoggingProperty("peak", ((CL3IF_TRANSPARENTPEAK)param.paramDisplacementForTransparent.peak).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_THICKNESS_FOR_TRANSPARENT:
                    loggingProperties.Add(new LoggingProperty("headNo", (param.paramThicknessForTransparent.headNo + HeadNumberCorrection).ToString()));
                    loggingProperties.Add(new LoggingProperty("peak1", ((CL3IF_TRANSPARENTPEAK)param.paramThicknessForTransparent.peak1).ToString()));
                    loggingProperties.Add(new LoggingProperty("peak2", ((CL3IF_TRANSPARENTPEAK)param.paramThicknessForTransparent.peak2).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_THICKNESS_2HEADS:
                    loggingProperties.Add(new LoggingProperty("headNo1", (param.paramThickness2Heads.headNo1 + HeadNumberCorrection).ToString()));
                    loggingProperties.Add(new LoggingProperty("headNo2", (param.paramThickness2Heads.headNo2 + HeadNumberCorrection).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_HEIGHTDIFFERENCE_2HEADS:
                    loggingProperties.Add(new LoggingProperty("headNo1", (param.paramHeightDifference2Heads.headNo1 + HeadNumberCorrection).ToString()));
                    loggingProperties.Add(new LoggingProperty("headNo2", (param.paramHeightDifference2Heads.headNo2 + HeadNumberCorrection).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_FORMULA:
                    const int OutNumberCorrection = 1;
                    loggingProperties.Add(new LoggingProperty("factorA", param.paramFormula.factorA.ToString()));
                    loggingProperties.Add(new LoggingProperty("factorB", param.paramFormula.factorB.ToString()));
                    loggingProperties.Add(new LoggingProperty("factorC", param.paramFormula.factorC.ToString()));
                    loggingProperties.Add(new LoggingProperty("targetOutX", (param.paramFormula.targetOutX + OutNumberCorrection).ToString()));
                    loggingProperties.Add(new LoggingProperty("targetOutY", (param.paramFormula.targetOutY + OutNumberCorrection).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_AVERAGE:
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_PEAK_TO_PEAK:
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_MAX:
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_MIN:
                    loggingProperties.Add(new LoggingProperty("targetOut", ((CL3IF_OUTNO)param.paramOutOperation.targetOut).ToString()));
                    break;
                case CL3IF_MEASUREMENTMETHOD.CL3IF_MEASUREMENTMETHOD_NO_CALCULATION:
                    break;
            }

            OutputLogMessage("GetMeasurementMethod", returnCode, loggingProperties);
        }

        private void _buttonSetScaling_Click(object sender, EventArgs e)
        {
            using (ScalingForm form = new ScalingForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetScaling(CurrentDeviceId, programNo, outNo, form.InputValue1, form.OutputValue1, form.InputValue2, form.OutputValue2);

                OutputLogMessage("SetScaling", returnCode);
            }
        }

        private void _buttonGetScaling_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int inputValue1, outputValue1, inputValue2, outputValue2;
            int returnCode = NativeMethods.CL3IF_GetScaling(CurrentDeviceId, programNo, outNo, out inputValue1, out outputValue1, out inputValue2, out outputValue2);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("inputValue1", inputValue1.ToString()));
            loggingProperties.Add(new LoggingProperty("outputValue1", outputValue1.ToString()));
            loggingProperties.Add(new LoggingProperty("inputValue2", inputValue2.ToString()));
            loggingProperties.Add(new LoggingProperty("outputValue2", outputValue2.ToString()));
            OutputLogMessage("GetScaling", returnCode, loggingProperties);
        }

        private void _buttonSetOffset_Click(object sender, EventArgs e)
        {
            using (OffsetForm form = new OffsetForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetOffset(CurrentDeviceId, programNo, outNo, form.Offset);

                OutputLogMessage("SetOffset", returnCode);
            }
        }

        private void _buttonGetOffset_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int offset;
            int returnCode = NativeMethods.CL3IF_GetOffset(CurrentDeviceId, programNo, outNo, out offset);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("offset", offset.ToString()) };
            OutputLogMessage("GetOffset", returnCode, loggingProperties);
        }

        private void _buttonSetTolerance_Click(object sender, EventArgs e)
        {
            using (ToleranceForm form = new ToleranceForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetTolerance(CurrentDeviceId, programNo, outNo, form.UpperLimit, form.LowerLimit, form.Hysteresis);

                OutputLogMessage("SetTolerance", returnCode);
            }
        }

        private void _buttonGetTolerance_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int upperLimit, lowerLimit, hysteresis;
            int returnCode = NativeMethods.CL3IF_GetTolerance(CurrentDeviceId, programNo, outNo, out upperLimit, out lowerLimit, out hysteresis);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("upperLimit", upperLimit.ToString()));
            loggingProperties.Add(new LoggingProperty("lowerLimit", lowerLimit.ToString()));
            loggingProperties.Add(new LoggingProperty("hysteresis", hysteresis.ToString()));
            OutputLogMessage("GetTolerance", returnCode, loggingProperties);
        }

        private void _buttonSetFilter_Click(object sender, EventArgs e)
        {
            using (FilterForm form = new FilterForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetFilter(CurrentDeviceId, programNo, outNo, (CL3IF_FILTERMODE)form.FilterMode, form.FilterParam);

                OutputLogMessage("SetFilter", returnCode);
            }
        }

        private void _buttonGetFilter_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            CL3IF_FILTERMODE mode;
            ushort filterParameter;
            int returnCode = NativeMethods.CL3IF_GetFilter(CurrentDeviceId, programNo, outNo, out mode, out filterParameter);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            if (CL3IF_FILTERMODE.CL3IF_FILTERMODE_MOVING_AVERAGE == mode)
            {
                loggingProperties.Add(new LoggingProperty("mode", mode.ToString()));
                loggingProperties.Add(new LoggingProperty("filterParameter", ((CL3IF_FILTERPARAM_AVERAGE)filterParameter).ToString()));
            }
            else if (CL3IF_FILTERMODE.CL3IF_FILTERMODE_HIGHPASS == mode || CL3IF_FILTERMODE.CL3IF_FILTERMODE_LOWPASS == mode)
            {
                loggingProperties.Add(new LoggingProperty("mode", mode.ToString()));
                loggingProperties.Add(new LoggingProperty("filterParameter", ((CL3IF_FILTERPARAM_CUTOFF)filterParameter).ToString()));
            }

            OutputLogMessage("GetFilter", returnCode, loggingProperties);
        }

        private void _buttonSetHold_Click(object sender, EventArgs e)
        {
            using (HoldForm form = new HoldForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                CL3IF_HOLDMODE_PARAM param = new CL3IF_HOLDMODE_PARAM();
                CL3IF_HOLDMODE mode = (CL3IF_HOLDMODE)form.HoldMode;
                if (mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_PEAK || mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_BOTTOM || mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_PEAK_TO_PEAK ||
                    mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_SAMPLE || mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_AVERAGE)
                {
                    param.paramHold.updateCondition = form.UpdateCondition;
                    param.paramHold.numberOfSamplings = form.NumberOfSamplings;
                }
                else if (mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_AUTOPEAK || mode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_AUTOBOTTOM)
                {
                    param.paramAutoHold.level = form.Level;
                    param.paramAutoHold.hysteresis = form.Hysteresis;
                }

                int returnCode = NativeMethods.CL3IF_SetHold(CurrentDeviceId, programNo, outNo, mode, param);

                OutputLogMessage("SetHold", returnCode);
            }
        }

        private void _buttonGetHold_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            CL3IF_HOLDMODE holdMode;
            CL3IF_HOLDMODE_PARAM param;
            int returnCode = NativeMethods.CL3IF_GetHold(CurrentDeviceId, programNo, outNo, out holdMode, out param);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("holdMode", holdMode.ToString()));

            if (holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_PEAK || holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_BOTTOM || holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_PEAK_TO_PEAK ||
                holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_SAMPLE || holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_AVERAGE)
            {
                loggingProperties.Add(new LoggingProperty("updateCondition", ((CL3IF_UPDATECONDITION)param.paramHold.updateCondition).ToString()));
                loggingProperties.Add(new LoggingProperty("numberOfSamplings", param.paramHold.numberOfSamplings.ToString()));
            }
            else if (holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_AUTOPEAK || holdMode == CL3IF_HOLDMODE.CL3IF_HOLDMODE_AUTOBOTTOM)
            {
                loggingProperties.Add(new LoggingProperty("level", param.paramAutoHold.level.ToString()));
                loggingProperties.Add(new LoggingProperty("hysteresis", param.paramAutoHold.hysteresis.ToString()));
            }

            OutputLogMessage("GetHold", returnCode, loggingProperties);
        }

        private void _buttonSetInvalidDataProcessing_Click(object sender, EventArgs e)
        {
            using (InvalidDataProcessingForm form = new InvalidDataProcessingForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetInvalidDataProcessing(CurrentDeviceId, programNo, outNo, form.InvalidationNumber, form.RecoveryNumber);

                OutputLogMessage("SetInvalidDataProcessing", returnCode);
            }
        }

        private void _buttonGetInvalidDataProcessing_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            ushort invalidationNumber, recoveryNumber;
            int returnCode = NativeMethods.CL3IF_GetInvalidDataProcessing(CurrentDeviceId, programNo, outNo, out invalidationNumber, out recoveryNumber);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("invalidationNumber", invalidationNumber.ToString()));
            loggingProperties.Add(new LoggingProperty("recoveryNumber", recoveryNumber.ToString()));
            OutputLogMessage("GetInvalidDataProcessing", returnCode, loggingProperties);
        }

        private void _buttonSetDisplayUnit_Click(object sender, EventArgs e)
        {
            using (DisplayUnitForm form = new DisplayUnitForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetDisplayUnit(CurrentDeviceId, programNo, outNo, (CL3IF_DISPLAYUNIT)form.DisplayUnit);

                OutputLogMessage("SetDisplayUnit", returnCode);
            }
        }

        private void _buttonGetDisplayUnit_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            CL3IF_DISPLAYUNIT displayUnit;
            int returnCode = NativeMethods.CL3IF_GetDisplayUnit(CurrentDeviceId, programNo, outNo, out displayUnit);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("displayUnit", displayUnit.ToString()) };
            OutputLogMessage("GetDisplayUnit", returnCode, loggingProperties);
        }

        private void _buttonSetTerminalAllocation_Click(object sender, EventArgs e)
        {
            using (TerminalAllocationForm form = new TerminalAllocationForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetTerminalAllocation(CurrentDeviceId, programNo, outNo, (CL3IF_TIMINGRESET)form.TimingReset, (CL3IF_ZERO)form.Zero);

                OutputLogMessage("SetTerminalAllocation", returnCode);
            }
        }

        private void _buttonGetTerminalAllocation_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            CL3IF_TIMINGRESET timingReset;
            CL3IF_ZERO zero;
            int returnCode = NativeMethods.CL3IF_GetTerminalAllocation(CurrentDeviceId, programNo, outNo, out timingReset, out zero);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("timingReset", timingReset.ToString()));
            loggingProperties.Add(new LoggingProperty("zero", zero.ToString()));
            OutputLogMessage("GetTerminalAllocation", returnCode, loggingProperties);
        }

        private void _buttonSetAnalogOutputScaling_Click(object sender, EventArgs e)
        {
            using (AnalogOutputScalingForm form = new AnalogOutputScalingForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                byte programNo;
                if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }
                byte outNo;
                if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
                {
                    MessageBox.Show(this, "Target OUT is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetAnalogOutputScaling(CurrentDeviceId, programNo, outNo, form.InputValue1, form.OutputValue1, form.InputValue2, form.OutputValue2);

                OutputLogMessage("SetAnalogOutputScaling", returnCode);
            }
        }

        private void _buttonGetAnalogOutputScaling_Click(object sender, EventArgs e)
        {
            byte programNo;
            if (!byte.TryParse(_comboBoxSettingProgramNo.SelectedIndex.ToString(), out programNo))
            {
                MessageBox.Show(this, "Target program is Invalid Value");
                return;
            }
            byte outNo;
            if (!byte.TryParse(_comboBoxSettingOut.SelectedIndex.ToString(), out outNo))
            {
                MessageBox.Show(this, "Target OUT is Invalid Value");
                return;
            }

            int inputValue1, outputValue1, inputValue2, outputValue2;
            int returnCode = NativeMethods.CL3IF_GetAnalogOutputScaling(CurrentDeviceId, programNo, outNo, out inputValue1, out outputValue1, out inputValue2, out outputValue2);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("inputValue1", inputValue1.ToString()));
            loggingProperties.Add(new LoggingProperty("outputValue1", outputValue1.ToString()));
            loggingProperties.Add(new LoggingProperty("inputValue2", inputValue2.ToString()));
            loggingProperties.Add(new LoggingProperty("outputValue2", outputValue2.ToString()));
            OutputLogMessage("GetAnalogOutputScaling", returnCode, loggingProperties);
        }
        #endregion

        #region Set/Get settings(batch)

        private void _buttonGetSettings_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveBinFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;

                byte[] settingList = new byte[NativeMethods.CL3IF_ALL_SETTINGS_DATA_LENGTH];
                int returnCode;
                using (PinnedObject pin = new PinnedObject(settingList))
                {
                    returnCode = NativeMethods.CL3IF_GetSettings(CurrentDeviceId, pin.Pointer);
                }

                if (returnCode != NativeMethods.CL3IF_RC_OK)
                {
                    OutputLogMessage("GetSettings", returnCode);
                    return;
                }

                try
                {
                    using (FileStream filestream = new FileStream(dialog.FileName, FileMode.Create))
                    {
                        filestream.Write(settingList, 0, settingList.Length);
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetSettings " + exception.GetType() + Environment.NewLine);
                    return;
                }

                OutputLogMessage("GetSettings", returnCode);
            }
        }

        private void _buttonSetSettings_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                byte[] settingList = new byte[NativeMethods.CL3IF_ALL_SETTINGS_DATA_LENGTH];
                using (FileStream filestream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    filestream.Read(settingList, 0, NativeMethods.CL3IF_ALL_SETTINGS_DATA_LENGTH);
                }
                int returnCode = NativeMethods.CL3IF_SetSettings(CurrentDeviceId, settingList);

                OutputLogMessage("SetSettings", returnCode);
            }
        }

        private void _buttonGetProgram_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveBinFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;

                byte programNo;
                if (!byte.TryParse(_comboBoxTargetProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                byte[] settingList = new byte[NativeMethods.CL3IF_PROGRAM_SETTINGS_DATA_LENGTH];
                int returnCode;
                using (PinnedObject pin = new PinnedObject(settingList))
                {
                    returnCode = NativeMethods.CL3IF_GetProgram(CurrentDeviceId, programNo, pin.Pointer);
                }

                if (returnCode != NativeMethods.CL3IF_RC_OK)
                {
                    OutputLogMessage("GetProgram", returnCode);
                    return;
                }

                try
                {
                    using (FileStream filestream = new FileStream(dialog.FileName, FileMode.Create))
                    {
                        filestream.Write(settingList, 0, settingList.Length);
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetProgram " + exception.GetType() + Environment.NewLine);
                    return;
                }

                OutputLogMessage("GetProgram", returnCode);
            }
        }

        private void _buttonSetProgram_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                byte[] settingList = new byte[NativeMethods.CL3IF_PROGRAM_SETTINGS_DATA_LENGTH];
                using (FileStream filestream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    filestream.Read(settingList, 0, NativeMethods.CL3IF_PROGRAM_SETTINGS_DATA_LENGTH);
                }
                byte programNo;
                if (!byte.TryParse(_comboBoxTargetProgramNo.SelectedIndex.ToString(), out programNo))
                {
                    MessageBox.Show(this, "Target program is Invalid Value");
                    return;
                }

                int returnCode = NativeMethods.CL3IF_SetProgram(CurrentDeviceId, programNo, settingList);

                OutputLogMessage("SetProgram", returnCode);
            }
        }


        private void _buttonGetEncoderSetting_Click(object sender, EventArgs e)
        {
            CL3IF_ENCODER_SETTING encoderSetting = new CL3IF_ENCODER_SETTING();
            int returnCode = NativeMethods.CL3IF_GetEncoder(CurrentDeviceId, out encoderSetting);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
            loggingProperties.Add(new LoggingProperty("encoderOnOff", encoderSetting.encoderOnOff ? "ON" : "OFF"));
            loggingProperties.Add(new LoggingProperty("operationMode", ((CL3IF_ENCODER_OPERATING_MODE)encoderSetting.operatingMode).ToString()));
            loggingProperties.Add(new LoggingProperty("enterMode", ((CL3IF_ENCODER_ENTER_MODE)encoderSetting.enterMode).ToString()));
            loggingProperties.Add(new LoggingProperty("decimationPoint", encoderSetting.decimationPoint.ToString()));
            loggingProperties.Add(new LoggingProperty("detectionEdge", ((CL3IF_ENCODER_DETECTION_EDGE)encoderSetting.detectionEdge).ToString()));
            loggingProperties.Add(new LoggingProperty("minInputTime", ((CL3IF_ENCODER_MIN_INPUT_TIME)encoderSetting.minInputTime).ToString()));
            loggingProperties.Add(new LoggingProperty("pulseCountOffsetDetectionLogic",
                ((CL3IF_ENCODER_PULSE_COUNT_OFFSET_DETECTION_LOGIC)encoderSetting.pulseCountOffsetDetectionLogic).ToString()));
            loggingProperties.Add(new LoggingProperty("presetValue", encoderSetting.presetValue.ToString()));
            OutputLogMessage("GetEncoder", returnCode, loggingProperties);
        }

        private void _buttonSetEncoderSetting_Click(object sender, EventArgs e)
        {
            using (EncoderForm form = new EncoderForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                CL3IF_ENCODER_SETTING encoderSetting = new CL3IF_ENCODER_SETTING();
                encoderSetting.encoderOnOff = form.EncoderOnOff;
                encoderSetting.operatingMode = form.OperatingMode;
                encoderSetting.enterMode = form.EnterMode;
                encoderSetting.decimationPoint = form.DecimationPoint;
                encoderSetting.detectionEdge = form.DetectionEdge;
                encoderSetting.minInputTime = form.MinInputTime;
                encoderSetting.pulseCountOffsetDetectionLogic = form.PulseCountOffsetDetectionLogic;
                encoderSetting.presetValue = form.PresetValue;

                int returnCode = NativeMethods.CL3IF_SetEncoder(CurrentDeviceId, ref encoderSetting);

                OutputLogMessage("SetEncoder", returnCode);
            }
        }
        #endregion

        #region Data Storage/Other

        private void _buttonStartStorage_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_StartStorage(CurrentDeviceId);

            OutputLogMessage("StartStorage", returnCode);
        }

        private void _buttonStopStorage_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_StopStorage(CurrentDeviceId);

            OutputLogMessage("StopStorage", returnCode);
        }

        private void _buttonClearStorageData_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_ClearStorageData(CurrentDeviceId);

            OutputLogMessage("ClearStorageData", returnCode);
        }

        private void _buttonGetStorageIndex_Click(object sender, EventArgs e)
        {
            uint index = 0;
            CL3IF_SELECTED_INDEX selectedIndex = (CL3IF_SELECTED_INDEX)_comboBoxSelectedIndex.SelectedIndex;
            int returnCode = NativeMethods.CL3IF_GetStorageIndex(CurrentDeviceId, selectedIndex, out index);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("index", index.ToString()) };
            OutputLogMessage("GetStorageIndex", returnCode, loggingProperties);

            if (returnCode != NativeMethods.CL3IF_RC_OK) return;
            _textBoxGetStorageDataIndex.Text = index.ToString();
        }

        private void _buttonGetStorageData_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {
                uint index = 0;
                if (!uint.TryParse(_textBoxGetStorageDataIndex.Text, out index))
                {
                    MessageBox.Show(this, "index is Invalid Value");
                    return;
                }
                uint requestDataCount = 0;
                if (!uint.TryParse(_textBoxGetStorageDataReadCount.Text, out requestDataCount) || requestDataCount > MaxMeasurementDataCountPerTime)
                {
                    MessageBox.Show(this, "requestDataCount is Invalid Value");
                    return;
                }

                uint nextIndex = 0;
                uint obtainedDataCount = 0;
                CL3IF_OUTNO outTarget = 0;
                int returnCode = NativeMethods.CL3IF_GetStorageData(CurrentDeviceId, index, requestDataCount, out nextIndex, out obtainedDataCount, out outTarget, pin.Pointer);

                List<int> outTargetList = ConvertOutTargetList(outTarget);
                List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                loggingProperties.Add(new LoggingProperty("targetOut", CreateTargetOutSequence(outTargetList)));
                loggingProperties.Add(new LoggingProperty("nextIndex", nextIndex.ToString()));
                loggingProperties.Add(new LoggingProperty("obtainedDataCount", obtainedDataCount.ToString()));
                OutputLogMessage("GetStorageData", returnCode, loggingProperties);

                if (returnCode != NativeMethods.CL3IF_RC_OK) return;
                _storageIndex = (uint)index;
                _storageReceivedDataCount = (uint)obtainedDataCount;
                _storageData = new CL3IF_MEASUREMENT_DATA[MaxMeasurementDataCountPerTime];
                int readPosition = 0;
                for (int i = 0; i < obtainedDataCount; i++)
                {
                    CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                    measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[outTargetList.Count];

                    measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_ADD_INFO));
                    readPosition += Marshal.SizeOf(typeof(CL3IF_ADD_INFO));

                    for (int j = 0; j < outTargetList.Count; j++)
                    {
                        measurementData.outMeasurementData[j] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                        readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                    }
                    _storageData[i] = measurementData;
                }
            }
        }

        private void _buttonStorageSave_Click(object sender, EventArgs e)
        {
            if (_storageReceivedDataCount <= 0)
            {
                MessageBox.Show(this, "No Storage Data");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveCsvFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (StreamWriter fileStream = new StreamWriter(dialog.FileName, false, Encoding.GetEncoding("ASCII")))
                    {
                        for (uint i = 0; i < _storageReceivedDataCount; i++)
                        {
                            CL3IF_MEASUREMENT_DATA currentStorageData = _storageData[i];
                            StringBuilder logMessage = new StringBuilder();
                            logMessage.Append((_storageIndex + i).ToString());
                            logMessage.Append(CsvSeparator + currentStorageData.addInfo.triggerCount);
                            logMessage.Append(CsvSeparator + currentStorageData.addInfo.pulseCount);
                            for (int j = 0; j < currentStorageData.outMeasurementData.Length; j++)
                            {
                                logMessage.Append(CsvSeparator + currentStorageData.outMeasurementData[j].measurementValue);
                            }
                            fileStream.WriteLine(logMessage.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetStorageData " + exception.GetType() + Environment.NewLine);
                }
            }
        }

        private void _buttonStorageContinuously_Click(object sender, EventArgs e)
        {
            if (_isStoraging)
            {
                StopStorageProcess();
                return;
            }

            StopTrendProcess();
            _buttonStorageContinuously.Text = "Stop getting data";
            _sequenceStorageReceivedDataCount = 0;
            _sequenceStorageIndex = 0;
            _isStoraging = true;
            SynchronizationContext context = System.Threading.SynchronizationContext.Current;
            _threadStorage = new System.Threading.Thread(ContinuouslyExecuteStorageProcess);
            _threadStorage.Start(context);
        }

        private void _buttonStorageContinuouslySave_Click(object sender, EventArgs e)
        {
            if (_sequenceStorageReceivedDataCount <= 0)
            {
                MessageBox.Show(this, "No Storage Continuously Data");
                return;
            }

            StopTrendProcess();
            StopStorageProcess();

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = SaveCsvFileFilter;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (StreamWriter fileStream = new StreamWriter(dialog.FileName, false, Encoding.GetEncoding("ASCII")))
                    {
                        for (int i = 0; i < _sequenceStorageReceivedDataCount; i++)
                        {
                            CL3IF_MEASUREMENT_DATA currentStorageData = _sequenceStorageData[i];
                            StringBuilder logMessage = new StringBuilder();
                            logMessage.Append((_sequenceStorageIndex + i).ToString());
                            logMessage.Append(CsvSeparator + currentStorageData.addInfo.triggerCount);
                            logMessage.Append(CsvSeparator + currentStorageData.addInfo.pulseCount);
                            for (int j = 0; j < currentStorageData.outMeasurementData.Length; j++)
                            {
                                logMessage.Append(CsvSeparator + currentStorageData.outMeasurementData[j].measurementValue);
                            }
                            fileStream.WriteLine(logMessage.ToString());
                        }
                    }
                }
                catch (Exception exception)
                {
                    OutputLogMessage("GetStorageDataContinuously " + exception.GetType() + Environment.NewLine);
                }
            }
        }

        private void ContinuouslyExecuteStorageProcess(object state)
        {
            CL3IF_MEASUREMENT_DATA[] storageDataList = new CL3IF_MEASUREMENT_DATA[MaxSequenceMeasurementData];
            byte[] buffer = new byte[MaxRequestDataLength];

            // Get storage index
            uint index = 0;
            CL3IF_SELECTED_INDEX selectedIndex = CL3IF_SELECTED_INDEX.CL3IF_SELECTED_INDEX_NEWEST;
            int returnCodeStorageIndex = NativeMethods.CL3IF_GetStorageIndex(CurrentDeviceId, selectedIndex, out index);

            this.Invoke((MethodInvoker)(() =>
           {
               List<LoggingProperty> loggingTrendIndexProperties = new List<LoggingProperty>() { new LoggingProperty("index", index.ToString()) };
               OutputLogMessage("GetStorageIndex", returnCodeStorageIndex, loggingTrendIndexProperties);
           }));

            if (returnCodeStorageIndex != NativeMethods.CL3IF_RC_OK)
            {
                StopStorageProcess();
                _threadStorage.Abort();
                return;
            }

            uint indexGet = index;
            _sequenceStorageIndex = indexGet;

            // Get storage data continuously
            while (_isStoraging)
            {
                uint nextIndex = 0;
                uint obtainedDataCount = 0;
                int returnCodeStorageData = 0;
                CL3IF_OUTNO outTarget = 0;
                using (PinnedObject pin = new PinnedObject(buffer))
                {
                    returnCodeStorageData = NativeMethods.CL3IF_GetStorageData(CurrentDeviceId, indexGet, MaxMeasurementDataCountPerTime, out nextIndex, out obtainedDataCount, out outTarget, pin.Pointer);

                    if (nextIndex == 0 || returnCodeStorageData != NativeMethods.CL3IF_RC_OK)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            OutputLogMessage("GetStorageData", returnCodeStorageData, new List<LoggingProperty>());
                        }));
                        StopStorageProcess();
                        break;
                    }

                    indexGet = nextIndex;
                    List<int> outTargetList = ConvertOutTargetList(outTarget);
                    int readPosition = 0;
                    int storageDataCount = 0;
                    for (int i = 0; i < obtainedDataCount; i++)
                    {
                        if (MaxSequenceMeasurementData <= i + _sequenceStorageReceivedDataCount)
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                OutputLogMessage("GetStorageData", returnCodeStorageData, new List<LoggingProperty>());
                            }));
                            StopStorageProcess();
                            break;
                        }

                        CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                        measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[outTargetList.Count];
                        measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_ADD_INFO));
                        readPosition += Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                        for (int j = 0; j < outTargetList.Count; j++)
                        {
                            measurementData.outMeasurementData[j] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                            readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                        }

                        storageDataList[i + _sequenceStorageReceivedDataCount] = measurementData;
                        storageDataCount++;
                    }
                    _sequenceStorageReceivedDataCount += storageDataCount;
                }

                this.Invoke((MethodInvoker)(() =>
                {
                    List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                    loggingProperties.Add(new LoggingProperty("nextIndex", nextIndex.ToString()));
                    loggingProperties.Add(new LoggingProperty("obtainedDataCount", obtainedDataCount.ToString()));
                    OutputLogMessage("GetStorageData", returnCodeStorageData, loggingProperties);
                    _textBoxStorageDataCount.Text = _sequenceStorageReceivedDataCount.ToString();
                }));

                System.Threading.Thread.Sleep(50);
            }
            _sequenceStorageData = storageDataList;
            _threadStorage.Abort();
        }

        private void StopStorageProcess()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                _buttonStorageContinuously.Text = "Start getting data";
                _isStoraging = false;
            }));
        }

        private void _buttonGetPulseCount_Click(object sender, EventArgs e)
        {
            int pulseCount;
            int returnCode = NativeMethods.CL3IF_GetPulseCount(CurrentDeviceId, out pulseCount);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("pulseCount", pulseCount.ToString()) };
            OutputLogMessage("GetPulseCount", returnCode, loggingProperties);
        }

        private void _buttonResetPulseCount_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_ResetPulseCount(CurrentDeviceId);

            OutputLogMessage("ResetPulseCount", returnCode);
        }

        private void _buttonLightControl_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_LightControl(CurrentDeviceId, _comboBoxSetLaserControlValue.SelectedIndex == 0);

            OutputLogMessage("LightControl", returnCode);
        }

        private void _buttonMeasurementControl_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_MeasurementControl(CurrentDeviceId, _comboBoxSetMeasureEnableValue.SelectedIndex == 0);

            OutputLogMessage("MeasurementControl", returnCode);
        }

        private void _buttonStartLightIntensityTuning_Click(object sender, EventArgs e)
        {
            byte headNo;
            if (!byte.TryParse(_comboBoxLightIntensityTuning.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_StartLightIntensityTuning(CurrentDeviceId, headNo);

            OutputLogMessage("StartLightIntensityTuning", returnCode);
        }

        private void _buttonStopLightIntensityTuning_Click(object sender, EventArgs e)
        {
            byte headNo;
            if (!byte.TryParse(_comboBoxLightIntensityTuning.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_StopLightIntensityTuning(CurrentDeviceId, headNo);

            OutputLogMessage("StopLightIntensityTuning", returnCode);
        }

        private void _buttonCancelLightIntensityTuning_Click(object sender, EventArgs e)
        {
            byte headNo;
            if (!byte.TryParse(_comboBoxLightIntensityTuning.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_CancelLightIntensityTuning(CurrentDeviceId, headNo);

            OutputLogMessage("CancelLightIntensityTuning", returnCode);
        }

        private void _buttonStartCalibration_Click(object sender, EventArgs e)
        {
            byte headNo;
            if (!byte.TryParse(_comboBoxCalibration.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_StartCalibration(CurrentDeviceId, headNo);

            OutputLogMessage("StartCalibration", returnCode);
        }

        private void _buttonStopCalibration_Click(object sender, EventArgs e)
        {
            byte headNo;
            if (!byte.TryParse(_comboBoxCalibration.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_StopCalibration(CurrentDeviceId, headNo);

            OutputLogMessage("StopCalibration", returnCode);
        }

        private void _buttonCancelCalibration_Click(object sender, EventArgs e)
        {
            byte headNo;
            if (!byte.TryParse(_comboBoxCalibration.SelectedIndex.ToString(), out headNo))
            {
                MessageBox.Show(this, "Target HEAD is Invalid Value");
                return;
            }

            int returnCode = NativeMethods.CL3IF_CancelCalibration(CurrentDeviceId, headNo);

            OutputLogMessage("CancelCalibration", returnCode);
        }

        private void _buttonTransitToMeasurementMode_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_TransitToMeasurementMode(CurrentDeviceId);

            OutputLogMessage("TransitToMeasurementMode", returnCode);
        }

        private void _buttonTransitToSettingMode_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_TransitToSettingMode(CurrentDeviceId);

            OutputLogMessage("TransitToSettingMode", returnCode);
        }

        private void _buttonGetSystemConfiguration_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {
                byte deviceCount;
                int returnCode = NativeMethods.CL3IF_GetSystemConfiguration(CurrentDeviceId, out deviceCount, pin.Pointer);

                List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("deviceCount", deviceCount.ToString()) };
                if (returnCode == NativeMethods.CL3IF_RC_OK)
                {
                    for (int i = 0; i < deviceCount; i++)
                    {
                        ushort deviceType = (ushort)Marshal.PtrToStructure(pin.Pointer + i * sizeof(int), typeof(ushort));
                        loggingProperties.Add(new LoggingProperty("deviceType", ((CL3IF_DEVICETYPE)deviceType).ToString()));
                    }
                }
                OutputLogMessage("GetSystemConfiguration", returnCode, loggingProperties);
            }
        }

        private void _buttonReturnToFactoryDefaultSetting_Click(object sender, EventArgs e)
        {
            int returnCode = NativeMethods.CL3IF_ReturnToFactoryDefaultSetting(CurrentDeviceId);

            OutputLogMessage("ReturnToFactoryDefaultSetting", returnCode);
        }

        private void _buttonGetVersion_Click(object sender, EventArgs e)
        {
            CL3IF_VERSION_INFO versionInfo = NativeMethods.CL3IF_GetVersion();

            const string Separator = ".";
            StringBuilder version = new StringBuilder();
            version.Append(versionInfo.majorNumber.ToString());
            version.Append(Separator + versionInfo.minorNumber);
            version.Append(Separator + versionInfo.revisionNumber);
            version.Append(Separator + versionInfo.buildNumber);

            List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { new LoggingProperty("version", version.ToString()) };
            OutputLogMessage("GetVersion", NativeMethods.CL3IF_RC_OK, loggingProperties);
        }

        #endregion

        #region Display log message

        private void OutputLogMessage(string methodName, int returnCode)
        {
            OutputLogMessage(methodName, returnCode, Enumerable.Empty<LoggingProperty>());
        }

        private void OutputLogMessage(string methodName, int returnCode, IEnumerable<LoggingProperty> loggingProperties)
        {
            string result = returnCode == NativeMethods.CL3IF_RC_OK ? "OK" : "NG(" + returnCode + ")";
            _listboxLog.Items.Add(methodName + " " + result);
            if (returnCode == NativeMethods.CL3IF_RC_OK)
            {
                foreach (LoggingProperty property in loggingProperties)
                {
                    _listboxLog.Items.Add("    " + property.Name + ":" + property.Value);
                }
            }
            RotateLog();
        }

        private void OutputLogMessage(string logMessage)
        {
            _listboxLog.Items.Add(logMessage);
            RotateLog();
        }

        private void RotateLog()
        {
            const int MaxLineCount = 1000;
            if (_listboxLog.Items.Count > MaxLineCount)
            {
                int unnecessaryLineCount = _listboxLog.Items.Count - MaxLineCount;
                for (int i = 0; i < unnecessaryLineCount; i++)
                {
                    _listboxLog.Items.RemoveAt(0);
                }
            }

            _listboxLog.TopIndex = _listboxLog.Items.Count - 1;
        }

        private void _buttonClearLog_Click(object sender, EventArgs e)
        {
            _listboxLog.Items.Clear();
        }

        private void _listboxLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string selectedItemText = "";
                for (int i = 0; i < _listboxLog.SelectedItems.Count; i++)
                {
                    if (i != 0)
                    {
                        selectedItemText += Environment.NewLine;
                    }
                    selectedItemText += _listboxLog.SelectedItems[i];
                }
                Clipboard.SetText(selectedItemText);
            }
        }

        #endregion

        #region Form close

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopTrendProcess();
            StopStorageProcess();
            for (int i = 0; i < NativeMethods.CL3IF_MAX_DEVICE_COUNT; i++)
            {
                CommunicationClose(i);
            }
        }

        #endregion

        private void _groupBoxZero_Enter(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Keyence_thread();
            {
                /* Thread thread3 = new Thread(() => dataaa());
                 thread3.Start();

                 dataaa();

                 void dataaa()
                 {

                     byte[] buffer = new byte[MaxRequestDataLength];
                     using (PinnedObject pin = new PinnedObject(buffer))
                     {
                         while (true)
                         {
                             CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                             measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[NativeMethods.CL3IF_MAX_OUT_COUNT];

                             int returnCode = NativeMethods.CL3IF_GetMeasurementData(CurrentDeviceId, pin.Pointer);

                             //Console.WriteLine(measurementData.outMeasurementData[1].measurementValue.ToString());


                             if (returnCode != NativeMethods.CL3IF_RC_OK)
                             {
                                 // OutputLogMessage("GetMeasurementData", returnCode);
                                 return;
                             }

                             // measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(CL3IF_ADD_INFO));
                             int readPosition = Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                             //for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                             {
                                 measurementData.outMeasurementData[0] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                                 // readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                             }

                             List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                             loggingProperties.Add(new LoggingProperty("triggerCount", measurementData.addInfo.triggerCount.ToString()));
                             loggingProperties.Add(new LoggingProperty("pulseCount", measurementData.addInfo.pulseCount.ToString()));
                             for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                             {
                                 string outNumber = "[OUT" + (i + 1) + "]";
                                 loggingProperties.Add(new LoggingProperty(outNumber + "measurementValue", measurementData.outMeasurementData[i].measurementValue.ToString()));
                                 loggingProperties.Add(new LoggingProperty(outNumber + "valueInfo", ((CL3IF_VALUE_INFO)measurementData.outMeasurementData[i].valueInfo).ToString()));
                                 loggingProperties.Add(new LoggingProperty(outNumber + "judgeResult", measurementData.outMeasurementData[i].judgeResult.ToString()));
                             }

                             this.Invoke((MethodInvoker)(() => OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString())));

                             // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                             //Console.WriteLine(measurementData.outMeasurementData[0].measurementValue.ToString());
                         }
                     }
                 }*/
            }

        }

        private void button2_Click(object sender, EventArgs e, IGenericAdvancedMotor controller, decimal position)
        {
            // controller.DisableDevice();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {


        }


        public static void Move_Method_Home(IGenericAdvancedMotor controller, decimal position)
        {

            try
            {
                Console.WriteLine("Moving controllerX to {0}", position);
                controller.MoveTo(position, 0);

                /*                Console.WriteLine("Moving controllerY to {0}", positionY);
                                controllerY.MoveTo(positionY,0);*/

                // bool homed = controller.Status.IsHomed;
                // Console.WriteLine("Home " + homed);

                //controller.MoveTo(14, 0);

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to move to position");
                // Console.ReadKey();
                return;
            }
            Console.WriteLine("controller Moved");
        }


        public static void Clockwise_Relative_Move_Method(IGenericAdvancedMotor controller, decimal position)
        {
            try
            {
                Console.WriteLine("Moving controller to {0}", position);
                //controller.MoveTo(controller.DevicePosition + 5, 0);              

                controller.MoveRelative(0, 5, 0);
                // Console.WriteLine("Moving controller to {0}", positionY);
                //controller.MoveTo(14, 0);

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to move to position");
                // Console.ReadKey();
                return;
            }
            Console.WriteLine("controller Moved");
        }

        public static void AntiClockwise_Relative_Move_Method(IGenericAdvancedMotor controller, decimal position)
        {
            try
            {
                Console.WriteLine("Moving controller to {0}", position);
                //controller.MoveTo(controller.DevicePosition - 5, 0);

                controller.MoveRelative(0, -5, 0);
                // Console.WriteLine("Moving controller to {0}", positionY);
                //controller.MoveTo(14, 0);

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to move to position");
                // Console.ReadKey();
                return;
            }
            Console.WriteLine("controller Moved");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //   openFileDialog1.ShowDialog();
            System.Diagnostics.Process.Start(@"C:\Users\Ankit Shah\Desktop\LIMRDesigner\LIMR_Bioprinter.application");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Move_downthread();

        }

        private void button5_Click(object sender, EventArgs e, Thread thread1)
        {
            //thread1.Start();
        }

        private void testt(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ, SerialPort port)
        {
            {
                /* 
                 String serialNoX = "45252174";
                 String serialNoY = "27260648";
                 String serialNoZ = "45252164";

                 // Start the controller polling
                 // The polling loop requests regular status requests to the motor to ensure the program keeps track of the controller. 
                 controller.StartPolling(250);
                 // Needs a delay so that the current enabled state can be obtained
                 Thread.Sleep(500);
                 // Enable the channel otherwise any move is ignored 
                 controller.EnableDevice();
                 Thread.Sleep(500);
                 controllerY.StartPolling(250);
                 Thread.Sleep(500);
                 controllerY.EnableDevice();
                 // Needs a delay to give time for the controller to be enabled
                 Thread.Sleep(500);

                 // Call LoadMotorConfiguration on the controller to initialize the DeviceUnitConverter object required for real world unit parameters
                 //  - loads configuration information into channel
                 MotorConfiguration motorConfiguration = controller.LoadMotorConfiguration(serialNoX);
                 MotorConfiguration motorConfigurationY = controllerY.LoadMotorConfiguration(serialNoY);

                 // Not used directly in example but illustrates how to obtain controller settings
                 ThorlabsIntegratedStepperMotorSettings currentDeviceSettings = controller.MotorDeviceSettings as ThorlabsIntegratedStepperMotorSettings;

                 // Display info about controller
                 DeviceInfo deviceInfo = controller.GetDeviceInfo();
                 Console.WriteLine("controller {0} = {1}", deviceInfo.SerialNumber, deviceInfo.Name);

                 //--------------------------------------------------
                 // The API requires stage type to be specified
                 motorConfigurationY.DeviceSettingsName = "PRM1Z8";

                 // Get the device unit converter
                 motorConfigurationY.UpdateCurrentConfiguration();

                 // Not used directly in example but illustrates how to obtain device settings
                 KCubeDCMotorSettings currentDeviceSettingsY = controllerY.MotorDeviceSettings as KCubeDCMotorSettings;

                 // Updates the motor controller with the selected settings
                 controllerY.SetSettings(currentDeviceSettingsY, true, false);

                 // Display info about device
                 DeviceInfo deviceInfoY = controllerY.GetDeviceInfo();
                 Console.WriteLine("Device {0} = {1}", deviceInfoY.SerialNumber, deviceInfoY.Name);*/


                //--------------------------------------------------
            }

            bool homed = controller.Status.IsHomed;
            Console.WriteLine(homed);

            decimal position = 0.00m;
            double diameter = 29.00; // input value mm
            double circumference = 2 * Math.PI * (diameter / 2);
            double distancePerDegree = ((circumference) / 360); //mm per degree
            decimal distanceToMove = 80m; // input value is always less than circumfernce
            decimal positionY = distanceToMove / (decimal)distancePerDegree;
            decimal velocity = 15m;

            bool home = false;
            bool movement = controller.Status.IsInMotion;
            //Console.WriteLine("movementt " + movement);

            Decimal newPosX = controller.Position;
            Decimal newPosY = controllerY.Position;
            //Console.WriteLine("controller Moved to {0}", newPos);

            int counter = 0;
            Console.WriteLine(newPosY);


            //Keyence_thread();
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\Ankit Shah\Desktop\gcodefile1.txt"))
            {
                System.Console.WriteLine(line);
                counter++;

                //System.Console.WriteLine(newPosX + "\t"+ newPosY);


                /*                if (line.Contains("G: 00"))
                                {
                                    var numbers = Regex.Matches(line, @"\d+\.*\d*").OfType<Match>().Select(m => decimal.Parse(m.Value)).ToArray();

                                    if (velocity != 0)
                                    {
                                        VelocityParameters velPars = controller.GetVelocityParams();
                                        VelocityParameters velParsY = controllerY.GetVelocityParams();
                                        //velPars.MaxVelocity = 20m;
                                        // velParsY.MaxVelocity = 100m;
                                        velPars.Acceleration = 5m;
                                        velParsY.Acceleration = 40m;
                                        controller.SetHomingVelocity(5);
                                        controllerY.SetHomingVelocity(100);
                                        controller.SetVelocityParams(velPars); //SetHomeVelocity //isDeviceBusy //isDeviceAvailable
                                        controllerY.SetVelocityParams(velParsY);

                                        //controller.SetVelocityParams(velPars);
                                        // controllerY.SetVelocityParams(velParsY);
                                    }

                                    Home_Method1(controller, controllerY);
                                    Console.WriteLine("Home Method Called");
                                    Console.Write(numbers[1] + "\t");
                                    Console.WriteLine(numbers[2]);

                                    //while (newPosY != 0 && newPosX != 0)
                                    while (true)
                                    {
                                        newPosX = controller.Position;
                                        newPosY = controllerY.Position;
                                        System.Console.WriteLine("Homing");
                                        System.Console.WriteLine(newPosX + "\t" + newPosY);
                                        System.Console.WriteLine(controller.Status.IsHoming);
                                        System.Console.WriteLine(controllerY.Status.IsHoming);

                                        if (newPosX == 0 && newPosY == 0)
                                        {
                                            System.Console.WriteLine("Homed Successfully!!");
                                            break;
                                        }

                                    }
                                }*/
                bool flagA = false;
                bool flagB = false;
                //Keyence_thread();

                if (line.Contains("G: 01"))
                {
                    var numbers = Regex.Matches(line, @"\d+\.*\d*").OfType<Match>().Select(m => decimal.Parse(m.Value)).ToArray();
                    Console.WriteLine("Move Method Called");
                    Console.Write(numbers[1] + "\t");
                    Console.WriteLine(numbers[2]);

                    position = numbers[1];
                    distanceToMove = numbers[2]; // input value is always less than circumfernce
                    positionY = (System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000);//distanceToMove / (decimal)distancePerDegree; //(System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000)


                    if (velocity != 0)
                    {
                        VelocityParameters velPars = controller.GetVelocityParams();
                        VelocityParameters velParsY = controllerY.GetVelocityParams();
                        velPars.MaxVelocity = 40m;
                        velParsY.MaxVelocity = 190m;
                        velPars.Acceleration = 90m;
                        velParsY.Acceleration = 120m;
                        controller.SetVelocityParams(velPars); //SetHomeVelocity //isDeviceBusy //isDeviceAvailable
                        controllerY.SetVelocityParams(velParsY);
                    }

                    Console.WriteLine("accX {0}", controller.GetVelocityParams().Acceleration);
                    Console.WriteLine("accY {0}", controllerY.GetVelocityParams().Acceleration);

                    Console.WriteLine("velX {0}", controller.GetVelocityParams().MaxVelocity);
                    Console.WriteLine("velY {0}", controllerY.GetVelocityParams().MaxVelocity);



                    Move_Method1(controller, controllerY, position, positionY);
                    // port.Write("A");


                    while (true)
                    {

                        newPosX = controller.Position;
                        newPosY = controllerY.Position;
                        System.Console.WriteLine("Moving...");
                        System.Console.WriteLine(newPosX + "\t" + newPosY);
                        System.Console.WriteLine(controller.Status.IsMoving);
                        System.Console.WriteLine(controllerY.Status.IsMoving);
                        System.Console.WriteLine("posn X: {0}", position);
                        System.Console.WriteLine("posn Y: {0}", positionY);

                        if (controllerY.Status.IsMoving == false)
                        {

                            System.Console.WriteLine("Moved SuccessfullyY!! {0}", (newPosY == positionY));
                            flagA = true;
                        }

                        if (controller.Status.IsMoving == false)
                        {

                            System.Console.WriteLine("Moved SuccessfullyX!! {0}", (newPosX == position));
                            flagB = true;
                        }

                        if ((newPosX == position) && (newPosY == positionY))
                        {
                            System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                            //port.Write("B");
                            break;
                        }

                    }
                }

                //-------------------------------------------------


                if (line.Contains("G: 00"))
                {
                    var numbers = Regex.Matches(line, @"\d+\.*\d*").OfType<Match>().Select(m => int.Parse(m.Value)).ToArray();
                    position = 0.001m;
                    positionY = 0.001m;
                    if (velocity != 0)
                    {
                        VelocityParameters velPars = controller.GetVelocityParams();
                        VelocityParameters velParsY = controllerY.GetVelocityParams();
                        //velPars.MaxVelocity = 20m;
                        // velParsY.MaxVelocity = 100m;
                        velPars.Acceleration = 5m;
                        velParsY.Acceleration = 40m;
                        controller.SetHomingVelocity(5);
                        controllerY.SetHomingVelocity(100);
                        controller.SetVelocityParams(velPars); //SetHomeVelocity //isDeviceBusy //isDeviceAvailable
                        controllerY.SetVelocityParams(velParsY);

                        //controller.SetVelocityParams(velPars);
                        // controllerY.SetVelocityParams(velParsY);
                    }

                    Move_Method1(controller, controllerY, position, positionY);
                    Console.WriteLine("Home Method Called");
                    Console.Write(numbers[1] + "\t");
                    Console.WriteLine(numbers[2]);

                    //while (newPosY != 0 && newPosX != 0)
                    while (true)
                    {
                        newPosX = controller.Position;
                        newPosY = controllerY.Position;
                        System.Console.WriteLine("Homing");
                        System.Console.WriteLine(newPosX + "\t" + newPosY);
                        System.Console.WriteLine(controller.Status.IsHoming);
                        System.Console.WriteLine(controllerY.Status.IsHoming);

                        if ((newPosX == position) && (newPosY == positionY))
                        {
                            System.Console.WriteLine("Homed Successfully!!");
                            break;
                        }

                    }
                }

                //--------------------------------------------------
                //Thread.Sleep(500);


                /*if (line.Contains("G: 02"))
               {
                   var numbers = Regex.Matches(line, @"\d+").OfType<Match>().Select(m => int.Parse(m.Value)).ToArray();
                   Console.WriteLine("Move Method Called");
                   Console.Write(numbers[1] + "\t");
                   Console.WriteLine(numbers[2]);

                   position = numbers[1];
                   distanceToMove = numbers[2]; // input value is always less than circumfernce
                   positionY = distanceToMove / (decimal)distancePerDegree;


                   if (velocity != 0)
                   {
                       VelocityParameters velPars = controller.GetVelocityParams();
                       VelocityParameters velParsY = controllerY.GetVelocityParams();
                       velPars.MaxVelocity = velocity;
                       controller.SetVelocityParams(velPars);
                       controllerY.SetVelocityParams(velParsY);
                   }

                   Move_Method1(controller, controllerY, position, positionY);

               }*/

            }
            System.Console.WriteLine("There were {0} lines.", counter);

            //Home_Method1(controller, controllerY);
            // or 
            //Home_Method2(controller);


            // If a position is requested
            /* if (position != 0)
             {
                 // Update velocity if required using real world methods
                 if (velocity != 0)
                 {
                     VelocityParameters velPars = controller.GetVelocityParams();
                     VelocityParameters velParsY = controllerY.GetVelocityParams();
                     velPars.MaxVelocity = velocity;
                     controller.SetVelocityParams(velPars);
                     controllerY.SetVelocityParams(velParsY);
                 }

                 // Move_Method1(controller, controllerY,position,positionY);
                 //Relative_Move_Method1(controller, controllerY, position, positionY);
                 // or
                 //Move_Method2(controller, position);

                 //Decimal newPos = controller.Position;
                 //Console.WriteLine("controller Moved to {0}", newPos);
             }*/

            /*            controller.StopPolling();
                        controller.Disconnect(true);  //.DisableDevice()
                        controllerY.StopPolling();
                        controllerY.Disconnect(true);*/


        }

        public static void Move_Method1(IGenericAdvancedMotor controller, IGenericAdvancedMotor controllerY, decimal position, decimal positionY)
        {
            try
            {
                Console.WriteLine("Moving controllerX to {0}", position);
                controller.MoveTo(position, 0);

                Console.WriteLine("Moving controllerY to {0}", positionY);
                controllerY.MoveTo(positionY, 0);

                // bool homed = controller.Status.IsHomed;
                // Console.WriteLine("Home " + homed);

                //controller.MoveTo(14, 0);

            }
            catch (Exception)
            {
                Console.WriteLine("Failed to move to position");
                // Console.ReadKey();
                return;
            }
            Console.WriteLine("controller Moved");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Move_upthread();
            //downthread();
            // OutputLogMessage("aaa");
        }

        public void Move_upthread()
        {
            Thread thread1 = new Thread(() => Move_Up("Timer #1", controller));
            thread1.Start();
            //Counter();
        }

        public void Move_downthread()
        {
            Thread thread2 = new Thread(() => Move_Down("Timer #3", controller));
            thread2.Start();
            //Counter();
        }

        public void Move_Clockthread()
        {
            Thread thread3 = new Thread(() => Move_Clock("Timer #1", controllerY));
            thread3.Start();
            //Counter();
        }

        public void Move_AntiClockthread()
        {
            Thread thread4 = new Thread(() => Move_AntiClock("Timer #3", controllerY));
            thread4.Start();
            //Counter();
        }

        public void Keyence_thread()
        {
            //Thread thread5 = new Thread(() => KeyenceData());
            //thread5.Start();

            timer4.Stop();

            timer1.Start();
            // timer3.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            /*            textBox6.Text = Math.Round(controller.Position, 3).ToString();
                        textBox7.Text = Math.Round(controllerY.Position, 3).ToString();
                        textBox8.Text = Math.Round(controllerZ.Position, 3).ToString();
                        label15.Text = (heightData).ToString();*/

            heightZLoop();
            // Thread.Sleep(200);

            if ((chart1.Series[0].Points.Count > 100) && (chart1.Series[1].Points.Count > 100))
            {
                chart1.Series[0].Points.RemoveAt(0);
                chart1.Series[1].Points.RemoveAt(0);
            }

            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;
            chart1.ChartAreas[0].AxisX.Maximum = 100;

            //chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].YValue;
            chart1.ChartAreas[0].RecalculateAxesScale();


            // incrementX += 1;

        }


        public void heightZLoop()
        {

/*            VelocityParameters velParsZ = controllerZ.GetVelocityParams();
            velParsZ.MinVelocity = 60m;
            velParsZ.MaxVelocity = 60m;
            velParsZ.Acceleration = 120m; //0
            controllerZ.SetVelocityParams(velParsZ);
*/


            // string filePath = "C:/Users/Ankit Shah/Desktop/PIDDoutput.txt";
            /*
                        decimal kp = 1m;
                        decimal ki = 0.00001m;
                        decimal kd = 0.00001m;
                        decimal setpoint = 0.0m;
                        decimal integralTerm = 0.0m;
                        decimal prevError = 0.0m;
                        decimal error = 0.0m;
                        decimal proportionalTerm = 0.0m;
                        decimal derivativeTerm = 0.0m;
                        decimal controlSignal = 0.0m;*/

            //Console.WriteLine("hereeeeba");
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {

                //Console.WriteLine("yooooo");
                CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[NativeMethods.CL3IF_MAX_OUT_COUNT];

                int returnCode = NativeMethods.CL3IF_GetMeasurementData(CurrentDeviceId, pin.Pointer);

                //Console.WriteLine(measurementData.outMeasurementData[1].measurementValue.ToString());


                if (returnCode != NativeMethods.CL3IF_RC_OK)
                {
                    // OutputLogMessage("GetMeasurementData", returnCode);
                    return;
                }

                // measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(CL3IF_ADD_INFO));
                int readPosition = Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                //for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                {
                    measurementData.outMeasurementData[0] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                    // readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                }

                //  this.Invoke((MethodInvoker)(() => OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString())));

                // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                //Console.WriteLine(measurementData.outMeasurementData[0].measurementValue.ToString());
                heightData = Convert.ToDecimal(measurementData.outMeasurementData[0].measurementValue.ToString()) / 10000;
                //Console.WriteLine(heightData);
                this.Invoke((MethodInvoker)(() => OutputLogMessage(heightData.ToString())));

                pid();

                //=================================
                // decimal sensorValue = heightData;  // Replace with your actual sensor reading code

                // Declare a variable to store the previous sensor value
                // decimal previousValue = 0m;  // Initialize with an initial value

                // Check if the current sensor value is within the specified range of the previous value
                /*              if (Math.Round(Math.Abs(sensorValue - previousValue),3) <= 0.020m)
                              {
                                  // Do nothing or any other desired action
                                 // Console.WriteLine("Sensor value is outside the acceptable range.");

                              }
                              else
                              {
                                  // Perform some action when the sensor value is outside the desired range
                                  heightData = sensorValue;
                                  pid();
                                  // Add code here for the action you want to take
                              }

                              // Update the previous value with the current sensor value for the next iteration
                              previousValue = sensorValue;

                              */
                //========================================

                // this.Invoke((MethodInvoker)(() => textBox6.Text = controller.Position.ToString()));
                //this.Invoke((MethodInvoker)(() => textBox7.Text = controllerY.Position.ToString()));
                // this.Invoke((MethodInvoker)(() => textBox8.Text = controllerZ.Position.ToString()));

                void pid()
                {


                    {

                        //using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                        //using (var streamWriter = new StreamWriter(fileStream))

                        {

                            if (heightData != -99.9999m)
                            {
                                error = setpoint - heightData;

                                // Calculate the proportional term
                                proportionalTerm = kp * error;

                                // Calculate the integral term
                                integralTerm += ki * error;

                                // Calculate the derivative term
                                derivativeTerm = kd * (error - prevError);

                                // Update the previous error
                                prevError = error;

                                // Calculate the control signal
                                controlSignal = proportionalTerm + integralTerm + derivativeTerm;

                                decimal sensorValue = heightData;

                                if (Math.Round(Math.Abs(sensorValue - previousValue), 3) >= decimal.Parse(textBox5.Text))
                                //if (Math.Round(Math.Abs(error), 3) >= 0.100m)
                                {
                                    if (!controllerZ.IsDeviceBusy)
                                    {
                                        controllerZ.MoveTo(decimal.Parse(textBox1.Text) + Math.Round(controlSignal, 3), 0);
                                        //controllerZ.MoveRelative(0,Math.Round(controlSignal, 3),0);

                                    }

                                }

                                previousValue = sensorValue;

                                Console.WriteLine(DateTime.Now.Hour.ToString() + "/" + DateTime.Now.Minute.ToString() + "/" + DateTime.Now.Second.ToString() + "." + DateTime.Now.Millisecond.ToString() + " X: " + textBox6.Text + " Y: " + textBox7.Text + " Z: " + textBox8.Text + " H: " + Math.Round(heightData, 3) + " P: " + Math.Round(controlSignal, 3));
                                //streamWriter.WriteLine(heightData + " " + controlSignal);


                                // this.Invoke((MethodInvoker)(() => chart1.Series["PID_Data"].Points.AddY(controlSignal)));
                                //this.Invoke((MethodInvoker)(() => chart1.Series["Laser"].Points.AddY(heightData)));

                                chart1.Series["PID_Data"].Points.AddY(controlSignal);
                                chart1.Series["Laser"].Points.AddY(heightData);

                                //streamWriter.Close();


                            }
                        }
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {
                //Console.WriteLine("yooooo");
                CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[NativeMethods.CL3IF_MAX_OUT_COUNT];

                int returnCode = NativeMethods.CL3IF_GetMeasurementData(CurrentDeviceId, pin.Pointer);

                //Console.WriteLine(measurementData.outMeasurementData[1].measurementValue.ToString());


                if (returnCode != NativeMethods.CL3IF_RC_OK)
                {
                    // OutputLogMessage("GetMeasurementData", returnCode);
                    return;
                }

                // measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(CL3IF_ADD_INFO));
                int readPosition = Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                //for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                {
                    measurementData.outMeasurementData[0] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                    // readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                }
                /*
                                List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                                loggingProperties.Add(new LoggingProperty("triggerCount", measurementData.addInfo.triggerCount.ToString()));
                                loggingProperties.Add(new LoggingProperty("pulseCount", measurementData.addInfo.pulseCount.ToString()));
                                for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                                {
                                    string outNumber = "[OUT" + (i + 1) + "]";
                                    loggingProperties.Add(new LoggingProperty(outNumber + "measurementValue", measurementData.outMeasurementData[i].measurementValue.ToString()));
                                    loggingProperties.Add(new LoggingProperty(outNumber + "valueInfo", ((CL3IF_VALUE_INFO)measurementData.outMeasurementData[i].valueInfo).ToString()));
                                    loggingProperties.Add(new LoggingProperty(outNumber + "judgeResult", measurementData.outMeasurementData[i].judgeResult.ToString()));
                                }
                */
                // this.Invoke((MethodInvoker)(() => OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString())));

                // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                //Console.WriteLine(measurementData.outMeasurementData[0].measurementValue.ToString());
                heightData = Convert.ToDecimal(measurementData.outMeasurementData[0].measurementValue.ToString()) / 10000;
                //Console.WriteLine(heightData);
                this.Invoke((MethodInvoker)(() => OutputLogMessage(heightData.ToString())));


                // this.Invoke((MethodInvoker)(() => textBox6.Text = controller.Position.ToString()));
                //this.Invoke((MethodInvoker)(() => textBox7.Text = controllerY.Position.ToString()));
                // this.Invoke((MethodInvoker)(() => textBox8.Text = controllerZ.Position.ToString()));
            }
        }


        public void KeyenceData()
        {
            //string filePath = "C: /Users/Ankit Shah/Desktop/gcodefile1.txt";

            VelocityParameters velParsZ = controllerZ.GetVelocityParams();
            velParsZ.MaxVelocity = 60m;
            velParsZ.Acceleration = 120m;
            controllerZ.SetVelocityParams(velParsZ);

            //Console.WriteLine("hereeeeba");
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {

                while (true)
                {
                    //Console.WriteLine("yooooo");
                    CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                    measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[NativeMethods.CL3IF_MAX_OUT_COUNT];

                    int returnCode = NativeMethods.CL3IF_GetMeasurementData(CurrentDeviceId, pin.Pointer);

                    //Console.WriteLine(measurementData.outMeasurementData[1].measurementValue.ToString());


                    if (returnCode != NativeMethods.CL3IF_RC_OK)
                    {
                        // OutputLogMessage("GetMeasurementData", returnCode);
                        return;
                    }

                    // measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(CL3IF_ADD_INFO));
                    int readPosition = Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                    //for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                    {
                        measurementData.outMeasurementData[0] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                        // readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                    }
                    /*
                                    List<LoggingProperty> loggingProperties = new List<LoggingProperty>() { };
                                    loggingProperties.Add(new LoggingProperty("triggerCount", measurementData.addInfo.triggerCount.ToString()));
                                    loggingProperties.Add(new LoggingProperty("pulseCount", measurementData.addInfo.pulseCount.ToString()));
                                    for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                                    {
                                        string outNumber = "[OUT" + (i + 1) + "]";
                                        loggingProperties.Add(new LoggingProperty(outNumber + "measurementValue", measurementData.outMeasurementData[i].measurementValue.ToString()));
                                        loggingProperties.Add(new LoggingProperty(outNumber + "valueInfo", ((CL3IF_VALUE_INFO)measurementData.outMeasurementData[i].valueInfo).ToString()));
                                        loggingProperties.Add(new LoggingProperty(outNumber + "judgeResult", measurementData.outMeasurementData[i].judgeResult.ToString()));
                                    }
                    */
                    // this.Invoke((MethodInvoker)(() => OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString())));

                    // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                    //Console.WriteLine(measurementData.outMeasurementData[0].measurementValue.ToString());
                    heightData = Convert.ToDecimal(measurementData.outMeasurementData[0].measurementValue.ToString()) / 10000;
                    //Console.WriteLine(heightData);
                    this.Invoke((MethodInvoker)(() => OutputLogMessage(heightData.ToString())));


                    // this.Invoke((MethodInvoker)(() => textBox6.Text = controller.Position.ToString()));
                    //this.Invoke((MethodInvoker)(() => textBox7.Text = controllerY.Position.ToString()));
                    // this.Invoke((MethodInvoker)(() => textBox8.Text = controllerZ.Position.ToString()));

                    if (!controllerZ.IsDeviceBusy)
                    {
                        Thread threadZ = new Thread(() => MoveZ_PID(controller, controllerZ, heightData));
                        threadZ.Start();

                    }





                    /*
                                        if ((heightData <= 0.00m) && (heightData != -99.9999m))
                                        {
                                            if (!controllerZ.IsDeviceBusy)
                                            {
                                                decimal abc = Math.Round(129.369240m + heightData, 3);
                                                controllerZ.MoveTo(abc, 0);
                                                Console.WriteLine("print: " + abc);
                                            }
                                            *//*try
                                            {
                                                decimal abc = Math.Round(129.369240m + heightData, 3);
                                                controllerZ.MoveTo(abc, 0);
                                                Console.WriteLine("print: " + abc);

                                                // Console.WriteLine(129.369240m + heightData);
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Failed to move to position");
                                                // Console.ReadKey();
                                                return;
                                            }*//*
                                        }

                                        else if ((heightData >= 0.00m) && (heightData != -99.9999m))
                                        {
                                            if (!controllerZ.IsDeviceBusy)
                                            {
                                                decimal abc = Math.Round(129.369240m - heightData, 3);
                                                controllerZ.MoveTo(abc, 0);
                                                Console.WriteLine("print: " + abc);
                                            }

                                           *//* try
                                            {
                                                decimal abc = Math.Round(129.369240m - heightData, 3);
                                                controllerZ.MoveTo(abc, 0);
                                                Console.WriteLine("print: " + abc);
                                                // Console.WriteLine(129.369240m - heightData);

                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Failed to move to position");
                                                // Console.ReadKey();
                                                return;
                                            }*//*
                                        }
                    */






                    /* if(!(heightData >= -0.09m) && (heightData <= 0.09m))
                     {
                         try
                         {
                            // Console.WriteLine("Moving controllerX to {0}", position);
                             controllerZ.MoveTo(6.4m, 0);

                         }
                         catch (Exception)
                         {
                             Console.WriteLine("Failed to move to position");
                             // Console.ReadKey();
                             return;
                         }
                     }*/


                }
            }
        }

        private void MoveLaser(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ, decimal heightData)
        {

            while (controllerZ.Position - heightData != 129.369m)
            {
                if ((heightData > 0.00m) && (heightData != -99.9999m))
                {
                    if (!controllerZ.IsDeviceBusy)
                    {
                        controllerZ.MoveJog(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Backward, 0);
                    }
                }

                if ((heightData < 0.00m) && (heightData != -99.9999m))
                {
                    if (!controllerZ.IsDeviceBusy)
                    {
                        controllerZ.MoveJog(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Forward, 0);
                    }
                }
            }


            if ((heightData > 0.00m) && (heightData != -99.9999m))
            {
                while (controllerZ.Position != 129.369m - heightData)
                {
                    if (!controllerZ.IsDeviceBusy)
                    {
                        controllerZ.MoveJog(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Backward, 0);
                    }
                }
                controllerZ.Stop(0);
            }

            if ((heightData < 0.00m) && (heightData != -99.9999m))
            {
                while (controllerZ.Position != 129.369m + heightData)
                {
                    if (!controllerZ.IsDeviceBusy)
                    {
                        controllerZ.MoveJog(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Forward, 0);
                    }
                }
                controllerZ.Stop(0);
            }

        }

        private void zzzzz()
        {
            double amplitude = 1.0;
            double frequency = 1.0 / 10.0; // 1 cycle per unit
            double x = 0.0;
            /*            while (!controllerZ.IsDeviceBusy)
                        {
                            controllerZ.MoveTo(50m, 0);
                        }*/
            //controller.SetVelocityParams(5, 10);
            controllerZ.SetVelocityParams(20, 20);
            while (true)
            {
                /*                if (!controller.IsDeviceBusy && controllerZ.Position == 100)
                                {

                                    controller.MoveTo(0, 0);

                                }*/
                decimal y = (Decimal)(amplitude * Math.Sin(2 * Math.PI * frequency * x));
                Console.WriteLine("x = {0}, y = {1}", x, y);
                x += 0.1; // Step size of 0.1 units

                if (y < 0)
                {
                    if (!controllerZ.IsDeviceBusy)
                    {
                        controllerZ.MoveTo(50m + y, 0);
                    }
                    /*try
                    {
                        controllerZ.MoveTo(50m + y, 0);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to move to position");
                        // Console.ReadKey();
                        return;
                    }*/
                }

                if (y > 0)
                {
                    if (!controllerZ.IsDeviceBusy)
                    {
                        controllerZ.MoveTo(50m - y, 0);
                    }
                    /*try
                    {
                        controllerZ.MoveTo(50m - y, 0);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to move to position");
                        // Console.ReadKey();
                        return;
                    }*/
                }
                Console.WriteLine(controllerZ.Position);
                /*   if (!controller.IsDeviceBusy && controllerZ.Position == 0)
                   {
                       controller.MoveTo(100, 0);
                   }*/

            }

        }

        private void MoveZ_PID(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ, decimal heightData)
        {
            string filePath = "C:/Users/Ankit Shah/Desktop/PIDDoutput.txt";

            decimal kp = 0.1m;
            decimal ki = 0.5m;
            decimal kd = 0.2m;
            decimal setpoint = 0.0m;
            decimal integralTerm = 0.0m;
            decimal prevError = 0.0m;
            decimal error = 0.0m;
            decimal proportionalTerm = 0.0m;
            decimal derivativeTerm = 0.0m;
            decimal controlSignal = 0.0m;

            // if (!File.Exists(filePath))
            {

                //using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                //using (var streamWriter = new StreamWriter(fileStream))

                {
                    if (heightData != -99.9999m)
                    {
                        error = setpoint - heightData;

                        // Calculate the proportional term
                        proportionalTerm = kp * error;

                        // Calculate the integral term
                        integralTerm += ki * error;

                        // Calculate the derivative term
                        derivativeTerm = kd * (error - prevError);

                        // Update the previous error
                        prevError = error;

                        // Calculate the control signal
                        controlSignal = proportionalTerm + integralTerm + derivativeTerm;


                        if (!controllerZ.IsDeviceBusy)
                        {
                            controllerZ.MoveTo(Math.Round(130.761m + controlSignal, 2), 0);

                        }
                        Console.WriteLine(controllerZ.Position + "   " + heightData + "   " + controlSignal);
                        //streamWriter.WriteLine(heightData + " " + controlSignal);


                        // this.Invoke((MethodInvoker)(() => chart1.Series["PID_Data"].Points.AddY(controlSignal)));
                        //this.Invoke((MethodInvoker)(() => chart1.Series["Laser"].Points.AddY(heightData)));

                        // chart1.Series["PID_Data"].Points.AddY(controlSignal);
                        // chart1.Series["Laser"].Points.AddY(heightData);

                        //streamWriter.Close();


                    }
                }
            }

        }








        private void MoveZ(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ, decimal heightData)
        {
            VelocityParameters velParsZ = controllerZ.GetVelocityParams();
            velParsZ.MaxVelocity = 60m;
            velParsZ.Acceleration = 120m;
            controllerZ.SetVelocityParams(velParsZ);

            /*
                        if (heightData > 0)
                        {
                            while (heightData != 0)
                            {
                                controllerZ.MoveContinuousAtVelocity(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Forward, 5);
                                // controllerZ.JogContinuous(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Forward);
                            }
                        }

                        if (heightData < 0)
                        {
                            while (heightData != 0)
                            {
                                controllerZ.MoveContinuousAtVelocity(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Backward, 5);
                                // controllerZ.JogContinuous(Thorlabs.MotionControl.GenericMotorCLI.MotorDirection.Backward);
                                //controllerZ.JogContinuous(0);
                            }
                        }

                    */
            //=============================================================================
            /*            while (true)  /// thread ma halde naya function banayera
                        {

                            newPosX = controller.Position;
                            newPosY = controllerY.Position;

                            //this.Invoke((MethodInvoker)(() => textBox6.Text = newPosX.ToString()));
                            // this.Invoke((MethodInvoker)(() => textBox7.Text = newPosY.ToString()));
                            // this.Invoke((MethodInvoker)(() => textBox8.Text = controllerZ.Position.ToString()));

                            System.Console.WriteLine(newPosX + "\t" + newPosY);


                            if ((newPosX == position) && (newPosY == positionY))
                            {
                                System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                                port.Write("B");
                                //printer();
                                //Console.ReadKey();
                                break;
                            }


                        }
            */

            //------------------
            decimal abc = 0m;
            decimal newPosZ = 0.0m;

            decimal currentZHeight = controllerZ.Position;

            if (!((Math.Round(currentZHeight - heightData, 2) >= 130.66m) && (Math.Round(currentZHeight - heightData, 2) <= 130.86m)))
            {
                abc = Math.Round(currentZHeight - heightData, 2);
                if (!controllerZ.IsDeviceBusy)
                {
                    controllerZ.MoveTo(abc, 0);
                }
                // Console.WriteLine(abc);
            }
            while (true)
            {
                newPosZ = controllerZ.Position;

                if (newPosZ == abc)
                {
                    System.Console.WriteLine("ZZZdoneeeeeeeeeeeeeeeeeeeeeee");

                    break;
                }
            }


            /*            if ((heightData <= -1.5m) && (heightData != -99.9999m))
                        {
                            decimal abc = 129.369240m + Math.Round(heightData, 3);
                            controllerZ.MoveTo(abc, 0);

                        }

                        else if ((heightData >= 2m) && (heightData != -99.9999m))
                        {
                            decimal abc = 129.369240m - Math.Round(heightData, 3);
                            controllerZ.MoveTo(abc, 0);

                        }*/
            //------------------------
            /* if ((heightData <= -1.5m) && (heightData != -99.9999m))
             {
                 try
                 {
                     //controllerZ.MoveTo(89.477m + heightData, 0);
                     decimal abc = 129.369240m + Math.Round(heightData,3);
                     Console.WriteLine("print: " + abc);

                    // Console.WriteLine(129.369240m + heightData);
                 }
                 catch (Exception)
                 {
                     Console.WriteLine("Failed to move to position");
                     // Console.ReadKey();
                     return;
                 }
             }

             else if ((heightData >= 2m) && (heightData != -99.9999m))
             {
                 try
                 {
                     decimal abc = 129.369240m - heightData;
                     Console.WriteLine("print: " + abc);
                     //controllerZ.MoveTo(89.477m - heightData, 0);
                     // Console.WriteLine(129.369240m - heightData);

                 }
                 catch (Exception)
                 {
                     Console.WriteLine("Failed to move to position");
                     // Console.ReadKey();
                     return;
                 }
             }*/

            //----------------------------------------------------------------------------------------------------

            /*            else if ((heightData == -99.9999m))
                        {
                            try
                            {
                                controllerZ.MoveTo(10, 0);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Failed to move to position");
                                // Console.ReadKey();
                                return;
                            }
                        }*/

            /* if (!(heightData >= -0.09m) && (heightData <= 0.09m) && (heightData != -99.9999m))
             {
                 try
                 {
                     // Console.WriteLine("Moving controllerX to {0}", position);
                    *//* VelocityParameters velParsZ = controllerZ.GetVelocityParams();
                     velParsZ.MaxVelocity = 60m;
                     velParsZ.Acceleration = 120m;
                     controllerZ.SetVelocityParams(velParsZ);*//*
                     controllerZ.MoveTo(5.4m-heightData, 0);

                 }
                 catch (Exception)
                 {
                     Console.WriteLine("Failed to move to position");
                     // Console.ReadKey();
                     return;
                 }
             }*/
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Move_Clockthread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Move_AntiClockthread();
            // Console.WriteLine("yhehehehehehehehehe"+heightData);
        }

        private Boolean printRunning = false;

        private void button8_Click(object sender, EventArgs e)
        {
            //Keyence_thread();
            //testt(controller, controllerY, controllerZ,port);
            // print(controller, controllerY, controllerZ);

            /*            Thread threadPrint = new Thread(() => print(controller, controllerY, controllerZ));
                        threadPrint.Start();*/
            printRunning = true;
            printDesign();

        }

        private void printDesign()
        {
            timer5.Start();
            Thread threadPrint = new Thread(() => designPrintTest(controller, controllerY, controllerZ));
            threadPrint.Start();

        }

        private void print(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ)
        {
            Decimal newPosX = controller.Position;
            Decimal newPosY = controllerY.Position;
            Decimal position = 0.00m;
            Decimal positionY = 0.00m;
            double diameter = 8.25; // input value mm  //textBox2.text;
            double circumference = 2 * Math.PI * (diameter / 2);
            double distancePerDegree = ((circumference) / 360); //mm per degree
            decimal distanceToMove = 0m; // input value is always less than circumfernce
            //decimal positionY = distanceToMove / (decimal)distancePerDegree;

            Console.WriteLine(positionY);

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Ankit Shah\Desktop\gcodefile1.txt");

            // Display the file contents by using a foreach loop
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                // Console.WriteLine("\t" + line);
                // Console.Read();

                if (line.Contains("G: 01"))
                {
                    var numbers = Regex.Matches(line, @"\d+\.*\d*").OfType<Match>().Select(m => decimal.Parse(m.Value)).ToArray();
                    //Console.WriteLine("Move Method Called");
                    Console.Write(numbers[1] + "\t");
                    Console.WriteLine(numbers[2]);

                    position = numbers[1];
                    distanceToMove = numbers[2]; // input value is always less than circumfernce
                    positionY = (System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000);//distanceToMove / (decimal)distancePerDegree; //(System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000)



                    // Console.ReadKey();
                    /*
                                        decimal X_slice = 0;
                                        decimal Y_slice = 0;


                                        decimal delta_x = (position - newPosX) / 10m;
                                        decimal delta_y = (positionY - newPosY) / 10m;

                                        //Console.WriteLine("value: {0} {1} ", delta_x, delta_y);

                                        for (int i = 1; i <= 10; i++)
                                        {

                                            X_slice = newPosX + delta_x * i;
                                            Y_slice = newPosY + delta_y * i;

                                            //Console.WriteLine("(" + X_slice + "," + Y_slice + ")");
                                            //printer((System.Math.Ceiling((X_slice) * 1000) / 1000) , (System.Math.Ceiling((Y_slice) * 1000) / 1000));
                                        }*/




                    printer(position, positionY);

                    //Thread.Sleep(500);
                    //controllerY.MoveTo(positionY, 0);
                    //controller.MoveTo(position, 0);


                    /*                    while (controller.Status.IsMoving && controllerY.Status.IsMoving)
                                        {
                                            newPosX = controller.Position;
                                            newPosY = controllerY.Position;

                                            System.Console.WriteLine(newPosX + "\t" + newPosY);

                                        }*/


                    while (true)  /// thread ma halde naya function banayera
                    {

                        newPosX = controller.Position;
                        newPosY = controllerY.Position;


                        System.Console.WriteLine(newPosX + "\t" + newPosY);


                        if ((newPosX == position) && (newPosY == positionY))
                        {
                            System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                            port.Write("B");
                            //printer();
                            //Console.ReadKey();
                            break;
                        }


                    }
                }

                // }
            }

            void printer(decimal X_slice, decimal Y_slice)
            {
                Thread.Sleep(150);
                port.Write("A");
                controllerY.MoveTo(positionY, 0);
                controller.MoveTo(position, 0);
            }
        }

        private void designPrint(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ)
        {

            double diameter = double.Parse(textBox2.Text); // input value mm  //textBox2.text;
            double circumference = 2 * Math.PI * (diameter / 2);
            double distancePerDegree = ((circumference) / 360);
            Decimal position = 0m;
            Decimal positionY = 0m;
            Decimal distanceToMove = 0m;
            Decimal newPosX = controller.Position;
            Decimal newPosY = controllerY.Position;

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Ankit Shah\Desktop\designFile2.txt");

            foreach (string line in lines)
            {
                coordinateList.Add(line);
            }

            foreach (string elements in coordinateList)
            {
                //   Console.WriteLine(elements);
                string[] parts = elements.Split(',');

                if (parts.Length == 2)
                {
                    if (decimal.TryParse(parts[0], out decimal xCoordinate) && decimal.TryParse(parts[1], out decimal yCoordinate))
                    {

                        position = decimal.Round(xCoordinate, 3);
                        distanceToMove = yCoordinate; // input value is always less than circumfernce
                                                      // positionY = decimal.Round((System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000),3);
                        positionY = (System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000);
                        printer(position, positionY);

                        while (true)  /// thread ma halde naya function banayera 
                        {
                            // (controller.IsDeviceBusy && controllerY.IsDeviceBusy)
                            /*newPosX = decimal.Round(controller.Position,3);
                            newPosY = decimal.Round(controllerY.Position,3);*/
                            newPosX = controller.Position;
                            newPosY = controllerY.Position;
                            System.Console.WriteLine(newPosX + "\t" + newPosY + "\t" + positionY);

                            if ((newPosX == position) && (newPosY == positionY))
                            {
                                // if ((controller.IsDeviceBusy && controllerY.IsDeviceBusy)) 
                                System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                                port.Write("B");
                                break;
                            }
                        }
                    }
                }
            }


            void printer(decimal X_slice, decimal Y_slice)
            {
                controllerY.StopImmediate();
                controller.StopImmediate();
                Thread.Sleep(10);
                port.Write("A");
                controllerY.MoveTo(positionY, 0);
                controller.MoveTo(position, 0);
                Thread.Sleep(10);



                /*                //controller.TargetPosition;
                                controller.RequestStatus();
                                controller.RequestPosition();
                                //controller.DevicePosition;*/



            }

            coordinateList.Clear();
        }
        private void designPrintTest(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ)
        {

            //-------------------------------------------------
            decimal velX = 0m;
            decimal velY = 0m;
            string[] partsXY = VelXY.Split(',');

            if (partsXY.Length == 2)
            {
                if (decimal.TryParse(partsXY[0], out decimal xVelocity) && decimal.TryParse(partsXY[1], out decimal yVelocity))
                {
                    // Console.WriteLine(xCoordinate + "\t" + yCoordinate);
                    velX = xVelocity;
                    velY = yVelocity;
                }
            }

            Console.WriteLine(velX +"   "+ velY);

            controller.SetBacklash(0);
            controllerZ.SetBacklash(0);

            VelocityParameters velPars = controller.GetVelocityParams();
            velPars.MaxVelocity = velX;
            velPars.Acceleration = 60;//decimal.Parse(textBox14.Text);
            controller.SetVelocityParams(velPars);

            VelocityParameters velParsY = controllerY.GetVelocityParams();
            velParsY.MaxVelocity = velY;
            velParsY.Acceleration = 60;//decimal.Parse(textBox14.Text);
            controllerY.SetVelocityParams(velParsY); 

            //--------------------------------------------------

            Decimal position = 0m;
            Decimal positionY = 0m;
            Decimal distanceToMove = 0m;
           // var watch = new System.Diagnostics.Stopwatch();

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Ankit Shah\Desktop\designFile2.txt");

            foreach (string line in lines)
            {
                coordinateList.Add(line);
            }

            
            foreach (string elements in coordinateList)
            {
                //   Console.WriteLine(elements);
                string[] parts = elements.Split(',');

                if (parts.Length == 2 && printRunning == true)
                {
                    if (decimal.TryParse(parts[0], out decimal xCoordinate) && decimal.TryParse(parts[1], out decimal yCoordinate))
                    {
                        
                            position = decimal.Round(xCoordinate, 3);
                            distanceToMove = yCoordinate; // input value is always less than circumfernce
                            positionY = decimal.Round((System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000), 3);

                            posnX = position;
                            posnY = positionY;


                        //port.Write("A");
                        //watch.Start();

                        while (true)  /// thread ma halde naya function banayera 
                        {
                            // (controller.IsDeviceBusy && controllerY.IsDeviceBusy)
                            /*newPosX = decimal.Round(controller.Position,3);
                            newPosY = decimal.Round(controllerY.Position,3);*/

                            //newPosX = controller.Position;
                            //newPosY = controllerY.Position;

                            //System.Console.WriteLine(newPosX + "\t" + newPosY + "\t" + positionY);

                            if (!controller.IsDeviceBusy && !controllerY.IsDeviceBusy)
                            {
                                //Console.WriteLine()
                                printer(position, positionY);
                                break;
                            }

                            /*                          if ((newPosX == position) && (newPosY == positionY))
                                                      {
                                                     // if ((controller.IsDeviceBusy && controllerY.IsDeviceBusy)) 
                                                          System.Console.WriteLine("doneeeeeeeeeeeeeeeeeeeeeee");
                                                          port.Write("B");
                                                          break;
                                                      }*/
                        }

                    }
                }
            }
            //Thread.Sleep(1000);
            //port.Write("B");

            void printer(decimal X_slice, decimal Y_slice)
            {
                // controllerY.StopImmediate();
                //controller.StopImmediate();
                Thread.Sleep(100);
                port.Write("A");
                controllerY.MoveTo(positionY, 0);
                controller.MoveTo(position, 0);

                Thread.Sleep(10);

            }

            coordinateList.Clear();
        }

        private static ArrayList line(double x_start, double y_start, double x_end, double y_end, double Resolution)
        {
            ArrayList myList = new ArrayList();


            double X_slice = 0;
            double Y_slice = 0;

            double delta_xx = (x_end - x_start);
            double delta_yy = (y_end - y_start);

            double delta_x = (x_end - x_start) / Resolution;
            double delta_y = (y_end - y_start) / Resolution;

            myListArcs.Add(x_start + "," + y_start);


            if (delta_xx == 0 || delta_yy == 0)
            {

                // X_slice = x_start;
                //Y_slice = y_start;

                // X_slice = x_end;
                // Y_slice = y_end;

                // Console.WriteLine("(" + X_slice + "," + Y_slice + ")");

                // myListArcs.Add(x_start + "," + y_start + "\n" + x_end + "," + y_end);

                // myListArcs.Add(x_start + "," + y_start);
                myListArcs.Add(x_end + "," + y_end);

                // Console.WriteLine(myList[0]);
                //  Console.WriteLine("herebaaa222");
            }

            else
            {


                //Console.WriteLine("value: {0} {1} ", delta_x, delta_y);

                for (int i = 1; i <= Resolution; i++)
                {

                    X_slice = x_start + delta_x * i;
                    Y_slice = y_start + delta_y * i;

                    myListArcs.Add(X_slice + "," + Y_slice);
                    //Console.WriteLine(myList[i-1]);

                    // Console.WriteLine("(" + X_slice + "," + Y_slice + ")");
                    // Console.WriteLine("herebaaa");


                }
            }

            return myList;
        }


        private static double atan3(double dy, double dx)
        {
            double a = Math.Atan2(dy, dx);

            if (a < 0)
            {
                a = (Math.PI * 2.0) + a;
            }
            return a;
        }

        private static ArrayList arcs(double cx, double cy, double sx, double sy, double x, double y, int dir)
        {
            // ArrayList myListArcs = new ArrayList();


            double dx = sx - cx;
            double dy = sy - cy;

            //get radius
            double radius = Math.Sqrt(dx * dx + dy * dy);

            //find angle of arc (sweep)
            double angle1 = atan3(dy, dx);
            double angle2 = atan3(y - cy, x - cx);

            double theta = angle2 - angle1;
            //Console.WriteLine((theta * 180) / Math.PI);


            if (dir < 0 && theta < 0)
            {
                angle2 += 2 * Math.PI;
            }//clockwise
            else if (dir > 0 && theta > 0)
            {
                angle1 += 2 * Math.PI;
            }//CCW

            theta = angle2 - angle1;
            // Console.WriteLine((theta * 180) / Math.PI);

            // get length of arc
            // float circ=PI*2.0*radius;
            // float len=theta*circ/(PI*2.0);
            // simplifies to
            double len = Math.Abs(theta) * radius;
            //Console.WriteLine(len);

            double MM_PER_SEGMENT = 1;

            int i, segments = (int)(Math.Ceiling(len * MM_PER_SEGMENT));

            //Console.WriteLine(segments);
            double nx, ny, angle3, scale;



            for (i = 0; i < segments; ++i)
            {
                // interpolate around the arc
                scale = ((double)i) / ((double)segments);

                angle3 = (theta * scale) + angle1;
                nx = cx + Math.Cos(angle3) * radius;
                ny = cy + Math.Sin(angle3) * radius;

                //Console.WriteLine(scale);
                //Console.WriteLine(angle3);
                lineArc(sx, sy, nx, ny, 10);
                //  Console.WriteLine("(" + nx+", "+ny +")");

                // ArrayList myArrayList = line(sx, sy, nx, ny, 10);
                //myListArcs.Add(myArrayList[0]);
                myListArcs.Add(nx + "," + ny);

                sx = nx;
                sy = ny;
                // send it to the planner
                // line(nx, ny);
            }
            lineArc(sx, sy, x, y, 10);

            return myListArcs;
        }

        private static ArrayList lineArc(double x_start, double y_start, double x_end, double y_end, double Resolution)
        {
            ArrayList myList = new ArrayList();


            double X_slice = 0;
            double Y_slice = 0;

            double delta_xx = (x_end - x_start);
            double delta_yy = (y_end - y_start);

            double delta_x = (x_end - x_start) / Resolution;
            double delta_y = (y_end - y_start) / Resolution;

            //myListArcs.Add(x_start + "," + y_start);

            if (delta_xx == 0 || delta_yy == 0)
            {

                // X_slice = x_start;
                //Y_slice = y_start;

                // X_slice = x_end;
                // Y_slice = y_end;

                //  Console.WriteLine("(" + X_slice + "," + Y_slice + ")");

                // myListArcs.Add(x_start + "," + y_start + "\n" + x_end + "," + y_end);

                myListArcs.Add(x_end + "," + y_end);
                // Console.WriteLine(myList[0]);
                //  Console.WriteLine("herebaaa222");
            }

            else
            {


                //Console.WriteLine("value: {0} {1} ", delta_x, delta_y);

                for (int i = 1; i <= Resolution; i++)
                {

                    X_slice = x_start + delta_x * i;
                    Y_slice = y_start + delta_y * i;

                    myListArcs.Add(X_slice + "," + Y_slice);
                    //Console.WriteLine(myList[i-1]);

                    //   Console.WriteLine("(" + X_slice + "," + Y_slice + ")");
                    // Console.WriteLine("herebaaa");


                }
            }

            return myList;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Decimal a = 0.0m;
            Console.WriteLine(textBox3.Text);
            //a = Convert.ToDecimal(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(textBox4.Text);
        }
        private void Move_HomeX(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, decimal position)
        {
            while (true)
            {
                decimal newPosX = controller.Position;
                //decimal newPosY = controllerY.Position;
                System.Console.WriteLine("Homing");
                System.Console.WriteLine(newPosX);
                System.Console.WriteLine(controller.Status.IsHoming);
                //System.Console.WriteLine(controllerY.Status.IsHoming);

                if ((newPosX == position))
                {
                    System.Console.WriteLine("Homed Successfully!!");
                    break;
                }
            }
        }

        private void Move_HomeY(Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY, decimal positionY)
        {
            while (true)
            {
                //decimal newPosX = controller.Position;
                decimal newPosY = controllerY.Position;
                System.Console.WriteLine("Homing");
                System.Console.WriteLine(newPosY);
                System.Console.WriteLine(controller.Status.IsHoming);
                //System.Console.WriteLine(controllerY.Status.IsHoming);

                if ((newPosY == positionY))
                {
                    System.Console.WriteLine("Homed Successfully!!");
                    break;
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {

            if (!controller.IsDeviceBusy)
            {
                controller.MoveTo(20,0);
            }
/*            decimal position = 0.001m;
            //decimal positionY = 0.001m;
            if (Convert.ToDecimal(textBox4.Text) != 0)
            {
                VelocityParameters velPars = controller.GetVelocityParams();
                //VelocityParameters velParsY = controllerY.GetVelocityParams();
                //velPars.MaxVelocity = 20m;
                // velParsY.MaxVelocity = 100m;
                velPars.Acceleration = 5m;
                // velParsY.Acceleration = 40m;
                controller.SetHomingVelocity(5);
                // controllerY.SetHomingVelocity(100);
                controller.SetVelocityParams(velPars); //SetHomeVelocity //isDeviceBusy //isDeviceAvailable
                //controllerY.SetVelocityParams(velParsY);

                //controller.SetVelocityParams(velPars);
                // controllerY.SetVelocityParams(velParsY);
            }

            Move_Method_Home(controller, position);
            Console.WriteLine("Home Method Called");

            Thread thread5 = new Thread(() => Move_HomeX(controller, position));
            thread5.Start();

            //while (newPosY != 0 && newPosX != 0)*/

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (!controllerY.IsDeviceBusy)
            {
                controllerY.MoveTo(0, 0);
            }

            /*            //decimal position = 0.001m;
                        decimal positionY = 0.001m;
                        *//* if (Convert.ToDecimal(textBox4.Text) != 0)
                         {
                            // VelocityParameters velPars = controller.GetVelocityParams();
                             VelocityParameters velParsY = controllerY.GetVelocityParams();
                             //velPars.MaxVelocity = 20m;
                             velParsY.MaxVelocity = 100m;
                             //velPars.Acceleration = 5m;
                             velParsY.Acceleration = 40m;
                             controllerY.SetHomingVelocity(5);
                             // controllerY.SetHomingVelocity(100);
                             controller.SetVelocityParams(velParsY); //SetHomeVelocity //isDeviceBusy //isDeviceAvailable
                             //controllerY.SetVelocityParams(velParsY);

                             //controller.SetVelocityParams(velPars);
                             // controllerY.SetVelocityParams(velParsY);
                         }*//*

                        Move_Method_Home(controllerY, positionY);
                        Console.WriteLine("Home Method Called");

                        Thread thread5 = new Thread(() => Move_HomeY(controllerY, positionY));
                        thread5.Start();
            */
            //while (newPosY != 0 && newPosX != 0)

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void _groupBoxCommunicationControl_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!controllerZ.IsDeviceBusy)
            {
                controllerZ.MoveTo(50, 0);
            }
            // zzzzz();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        private void purgeThread()
        {
            if (serialPort.IsOpen())
            {
                string message = textBox11.Text + "\r\n";
                serialPort.SendLine(message);
                Console.WriteLine(message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            port.Write("A");
            //Thread threadPurge = new Thread(() => purgeThread());
            //threadPurge.Start();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            port.Write("B");
        }
        private void button13_Click(object sender, EventArgs e)
        {
            serialPort.Open("COM3", "9600", "8", "None", "One");

        }

        private void button14_Click(object sender, EventArgs e)
        {
            string message = "set volume " + textBox9.Text + "\r\n";
            serialPort.SendLine(message);
            Console.WriteLine(message);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string message = "set rate " + textBox10.Text + "\r\n";
            serialPort.SendLine(message);
            Console.WriteLine(message);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string syringe = comboBox1.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void MoveZ_radius(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ, decimal heightData)
        {
            VelocityParameters velParsZ = controllerZ.GetVelocityParams();
            velParsZ.MaxVelocity = 60m;
            velParsZ.Acceleration = 120m;
            controllerZ.SetVelocityParams(velParsZ);

            if ((heightData <= -1.5m) && (heightData != -99.9999m))
            {
                try
                {
                    controllerZ.MoveTo(120m + heightData, 0);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to move to position");
                    // Console.ReadKey();
                    return;
                }
            }

            else if ((heightData >= 1.5m) && (heightData != -99.9999m))
            {
                try
                {
                    controllerZ.MoveTo(120 - heightData, 0);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to move to position");
                    // Console.ReadKey();
                    return;
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //90-105
            timer1.Stop();
            timer2.Start();
            decimal scanHeight = 140m;
            decimal abc = 0m;
            decimal newPosZ = 0.0m;
            decimal currentZHeight = controllerZ.Position;

            List<decimal> nums = new List<decimal>() { 40m, 60m, 40m, 60m, 40m, 60m };

            List<decimal> heightList = new List<decimal>();


            //timer1.Stop();

            if (!controllerZ.IsDeviceBusy)
            {
                controller.MoveTo(nums[0], 0);
                controllerZ.MoveTo(scanHeight, 0);
            }

            while (true)
            {
                newPosZ = controllerZ.Position;

                if (newPosZ == scanHeight)
                {
                    System.Console.WriteLine("Xdoneeeeeeeeeeeeeeeeeeeeeee: ");

                    break;
                }
            }



            foreach (var i in nums)
            {
                if (!controller.IsDeviceBusy)
                {
                    controller.MoveTo(i, 0);
                }

                while (true)
                {
                    heightList.Add(heightData);

                    newPosZ = controller.Position;

                    if (newPosZ == i)
                    {
                        System.Console.WriteLine("Xdoneeeeeeeeeeeeeeeeeeeeeee: " + i);

                        break;
                    }
                }
            }


            /*            List<decimal> peakHeights = FindPeakHeights(heightList);

                        Console.WriteLine("Peak Heights:");
                        foreach (decimal peak in peakHeights)
                        {
                            Console.WriteLine(peak);
                        }*/


            decimal peakHeight = FindMostRepeatedPeakHeight(heightList);

            if (peakHeight != decimal.MinValue)
            {
                Console.WriteLine($"The peak height repeated the most number of times is: {peakHeight}");
            }
            else
            {
                Console.WriteLine("There is no peak in the list.");
            }


            timer2.Stop();
            timer1.Start();

        }

        static decimal FindMostRepeatedPeakHeight(List<decimal> numbers)
        {
            if (numbers.Count < 3)
            {
                // At least 3 elements are required to have a peak
                return decimal.MinValue;
            }

            Dictionary<decimal, int> heightCount = new Dictionary<decimal, int>();

            for (int i = 1; i < numbers.Count - 1; i++)
            {
                decimal current = numbers[i];
                decimal prev = numbers[i - 1];
                decimal next = numbers[i + 1];

                if (current > prev && current > next)
                {
                    if (heightCount.ContainsKey(current))
                    {
                        heightCount[current]++;
                    }
                    else
                    {
                        heightCount[current] = 1;
                    }
                }
            }

            decimal mostRepeatedPeakHeight = decimal.MinValue;
            int maxCount = 0;

            foreach (KeyValuePair<decimal, int> pair in heightCount)
            {
                if (pair.Value > maxCount)
                {
                    maxCount = pair.Value;
                    mostRepeatedPeakHeight = pair.Key;
                }
            }

            return mostRepeatedPeakHeight;
        }


        static List<decimal> FindPeakHeights(List<decimal> numbers)
        {
            List<decimal> peakHeights = new List<decimal>();

            if (numbers.Count < 3)
            {
                // A peak requires at least three elements
                return peakHeights;
            }

            for (int i = 1; i < numbers.Count - 1; i++)
            {
                decimal current = numbers[i];
                decimal prev = numbers[i - 1];
                decimal next = numbers[i + 1];

                if (current > prev && current > next)
                {
                    peakHeights.Add(current);
                }
            }

            return peakHeights;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            port.Write("B");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!controllerZ.IsDeviceBusy)
            {
                controllerZ.MoveRelative(0, -1 * Convert.ToDecimal(textBox17.Text), 0);
            }
        }


        private void button20_Click(object sender, EventArgs e)
        {
            if (!controllerZ.IsDeviceBusy)
            {
                controllerZ.MoveRelative(0, 1 * Convert.ToDecimal(textBox17.Text), 0);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            double diameter = double.Parse(textBox2.Text); // input value mm  //textBox2.text;
            double circumference = 2 * Math.PI * (diameter / 2);
            double distancePerDegree = ((circumference) / 360);
            Decimal position = 0m;
            Decimal positionY = 0m;
            Decimal distanceToMove = 0m;

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Ankit Shah\Desktop\designFile2.txt");

            string[] parts = lines[0].Split(',');

            if (parts.Length == 2)
            {
                if (decimal.TryParse(parts[0], out decimal xCoordinate) && decimal.TryParse(parts[1], out decimal yCoordinate))
                {

                     position = decimal.Round(xCoordinate, 3);
                     distanceToMove = yCoordinate; // input value is always less than circumfernce
                     positionY = decimal.Round((System.Math.Ceiling((distanceToMove / (decimal)distancePerDegree) * 1000) / 1000), 3);

                    if (!controller.IsDeviceBusy && !controllerY.IsDeviceBusy)
                    {
                        //string xy = lines[0];

                        controller.MoveTo(position, 0);
                        controllerY.MoveTo(positionY, 0);
                    }
                }
            }
        
        }

        private void button22_Click(object sender, EventArgs e)
        {
            decimal currentX = controller.Position;
            decimal currentY = controllerY.Position;

            VelocityParameters velPars = controller.GetVelocityParams();
            velPars.MaxVelocity = decimal.Parse(textBox4.Text);

            velPars.Acceleration = decimal.Parse(textBox11.Text);

            controller.SetVelocityParams(velPars);

            Console.WriteLine(System.DateTime.Now + "\t" + velPars.MaxVelocity + "\t" + velPars.ToString() + "\t" + velPars.Acceleration);
            if (!controller.IsDeviceBusy) 
            { controller.MoveTo(decimal.Parse(textBox12.Text), 0); }

            decimal x = System.DateTime.Now.Millisecond;
            decimal y = System.DateTime.Now.Millisecond;

            Console.WriteLine(" "+x.ToString() + " " +  y.ToString() +"   " + (y- x).ToString());


            /*     Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
                 Thread.Sleep(10);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
                 Thread.Sleep(1000);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);

                 //controller.Stop(200);
                 controller.StopImmediate();
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);

                 Thread.Sleep(10);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
                 Thread.Sleep(10);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);

                 Console.WriteLine(System.DateTime.Now);
                 controller.MoveTo(20m, 0);

                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
                 Thread.Sleep(10);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
                 Thread.Sleep(1000);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);

                 //controller.Stop(200);
                 controller.StopImmediate();

                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);

                 Thread.Sleep(10);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
                 Thread.Sleep(10);
                 Console.WriteLine(System.DateTime.Now + "t" + controller.Position + "\t" + controller.IsDeviceBusy);
     */

            /* VelocityParameters velPars = controllerY.GetVelocityParams();
             velPars.MaxVelocity = Convert.ToDecimal(3);

             velPars.Acceleration = 5m;

             controllerY.SetVelocityParams(velPars);

             Console.WriteLine(System.DateTime.Now + "\t" + velPars.MaxVelocity + "\t" + velPars.ToString() + "\t" + velPars.Acceleration);
             controllerY.MoveTo(20m, 0);

             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);
             Thread.Sleep(10);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);
             Thread.Sleep(1000);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);

             //controller.Stop(200);
             controllerY.StopImmediate();
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);

             Thread.Sleep(10);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);
             Thread.Sleep(10);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);

             Console.WriteLine(System.DateTime.Now);
             controllerY.MoveTo(20m, 0);

             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);
             Thread.Sleep(10);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);
             Thread.Sleep(1000);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);

             //controller.Stop(200);
             controllerY.StopImmediate();

             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);

             Thread.Sleep(10);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);
             Thread.Sleep(10);
             Console.WriteLine(System.DateTime.Now + "t" + controllerY.Position + "\t" + controllerY.IsDeviceBusy);*/
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            printRunning = false;
            controller.StopImmediate();
            controllerY.StopImmediate();
            controllerZ.StopImmediate();
            timer1.Stop();
            timer4.Start();
            port.Write("B");
        }

        private static bool _taskComplete;
        private static ulong _taskID;


        public static void CommandCompleteFunction(ulong taskID)
        {
            if ((_taskID > 0) && (_taskID == taskID))
            {
                _taskComplete = true;
            }
        }
        public static void Move_Method2(IGenericAdvancedMotor controller, decimal position)
        {

            /*
                        Console.WriteLine("Moving Device to {0}", position);
                        _taskComplete = false;
                        _taskID = controller.MoveTo(position, CommandCompleteFunction);
                        while (!_taskComplete)
                        {
                            Thread.Sleep(500);
                            StatusBase status = controller.Status;
                            Console.WriteLine("Device Moving {0}", status.Position);

                            // Will need some timeout functionality;
                        }
                        Console.WriteLine("Device Moved");*/
        }


        private void _pnlDeviceId_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            //Console.WriteLine("heyyy tulsi");
            textBox6.Text = controller.Position.ToString();
            textBox7.Text = controllerY.Position.ToString();
            textBox8.Text = controllerZ.Position.ToString();
            label15.Text = (heightData).ToString();
            //Console.WriteLine("heyyy tulsi");

            
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

            VelocityParameters velParsZ = controllerZ.GetVelocityParams();
            velParsZ.MinVelocity = 60m;
            velParsZ.MaxVelocity = 60m;
            velParsZ.Acceleration = 120m; //0
            controllerZ.SetVelocityParams(velParsZ);

            textBox1.Text = controllerZ.Position.ToString();

            setpoint = heightData;


            // string filePath = "C:/Users/Ankit Shah/Desktop/PIDDoutput.txt";
            /*
                        decimal kp = 1m;
                        decimal ki = 0.00001m;
                        decimal kd = 0.00001m;
                        decimal setpoint = 0.0m;
                        decimal integralTerm = 0.0m;
                        decimal prevError = 0.0m;
                        decimal error = 0.0m;
                        decimal proportionalTerm = 0.0m;
                        decimal derivativeTerm = 0.0m;
                        decimal controlSignal = 0.0m;*/

            //Console.WriteLine("hereeeeba");
            byte[] buffer = new byte[MaxRequestDataLength];
            using (PinnedObject pin = new PinnedObject(buffer))
            {

                //Console.WriteLine("yooooo");
                CL3IF_MEASUREMENT_DATA measurementData = new CL3IF_MEASUREMENT_DATA();
                measurementData.outMeasurementData = new CL3IF_OUTMEASUREMENT_DATA[NativeMethods.CL3IF_MAX_OUT_COUNT];

                int returnCode = NativeMethods.CL3IF_GetMeasurementData(CurrentDeviceId, pin.Pointer);

                //Console.WriteLine(measurementData.outMeasurementData[1].measurementValue.ToString());


                if (returnCode != NativeMethods.CL3IF_RC_OK)
                {
                    // OutputLogMessage("GetMeasurementData", returnCode);
                    return;
                }

                // measurementData.addInfo = (CL3IF_ADD_INFO)Marshal.PtrToStructure(pin.Pointer, typeof(CL3IF_ADD_INFO));
                int readPosition = Marshal.SizeOf(typeof(CL3IF_ADD_INFO));
                //for (int i = 0; i < NativeMethods.CL3IF_MAX_OUT_COUNT; i++)
                {
                    measurementData.outMeasurementData[0] = (CL3IF_OUTMEASUREMENT_DATA)Marshal.PtrToStructure(pin.Pointer + readPosition, typeof(CL3IF_OUTMEASUREMENT_DATA));
                    // readPosition += Marshal.SizeOf(typeof(CL3IF_OUTMEASUREMENT_DATA));
                }

                //  this.Invoke((MethodInvoker)(() => OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString())));

                // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                //Console.WriteLine(measurementData.outMeasurementData[0].measurementValue.ToString());
                heightData = Convert.ToDecimal(measurementData.outMeasurementData[0].measurementValue.ToString()) / 10000;
                //Console.WriteLine(heightData);
                this.Invoke((MethodInvoker)(() => OutputLogMessage(heightData.ToString())));
            }
        

                }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            double diameter = double.Parse(textBox2.Text); // input value mm  //textBox2.text;
            double circumference = 2 * Math.PI * (diameter / 2);
            distancePerDegree = ((circumference) / 360);

            double perMmDegree = 1 / distancePerDegree;

            //Console.Writeline(perMmDegree);
            Console.WriteLine(perMmDegree);

            double v2_max = 25; // Maximum linear velocity in mm/s
            double v1_max = 10; // Maximum rotational velocity in degrees/s



            // Calculate the valid range for v1
            double v1_min = 0.2;
            double v1 = 0;
            double v2 = 0;

            double time = 1;


            comboBox2.Items.Clear();
            arrayListVelocity.Clear();

            while (v1 <= v1_max)
            {
                // v2 = (angularDistanceInDegrees * v1 ) / linearDistanceInMM;

                v1 = 1 / time;
                v2 = perMmDegree / time;

                if (v2 <= v2_max)
                {
                    //Console.WriteLine("Time: " + time + "\t" + "      VelX: " + Math.Round(v1, 2) + "\t" + "            VelY: " + Math.Round(v2, 2));
                    string comboVal = "Time: " + time + "\t" + "      VelX: " + Math.Round(v1, 2) + "\t" + "            VelY: " + Math.Round(v2, 2);
                    comboBox2.Items.Add(comboVal);
                    arrayListVelocity.Add(Math.Round(v1, 2) + "," + Math.Round(v2, 2));
                }

                time += 0.1; // Increment v1 by 1 degree/s

                if (v1 < v1_min)
                {
                    break;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            VelXY = arrayListVelocity[comboBox2.SelectedIndex].ToString();

        }

        private void timer5_Tick(object sender, EventArgs e)
        {
           // Console.WriteLine(decimal.Round(decimal.Parse(textBox6.Text), 3)+"  "+ posnX);
            if (decimal.Round(decimal.Parse(textBox6.Text), 3) == posnX && decimal.Round(decimal.Parse(textBox7.Text), 3) == posnY)
            {
               // Console.WriteLine("yoooo");
                port.Write("B");
                //timer5.Stop();
                //watch.Stop();

                //Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }

            if (coordinateList.Count == 0 && decimal.Round(decimal.Parse(textBox6.Text), 3) == posnX && decimal.Round(decimal.Parse(textBox7.Text), 3) == posnY)
            {
                printRunning = false;
                timer5.Stop();
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
    }
}

