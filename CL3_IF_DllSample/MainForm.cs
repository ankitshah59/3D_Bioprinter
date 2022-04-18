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

            _deviceData = new DeviceData[NativeMethods.CL3IF_MAX_DEVICE_COUNT];
            _deviceStatusLabels = new Label[] { _labelDeviceStatus0, _labelDeviceStatus1, _labelDeviceStatus2 };

            for (int i = 0; i < NativeMethods.CL3IF_MAX_DEVICE_COUNT; i++)
            {
                _deviceData[i] = new DeviceData();
            }

            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";
            Console.WriteLine(mainThread.Name);

            // testt();

            // Thread thread1 = new Thread(() => CountDown("Timer #1"));
            // Thread thread2 = new Thread(() => CountUp("Timer #2",controller));

            //-----------------------------------------------------------------------------------

            //private const int ankit = 559;


            // thread1.Start();
            //thread2.Start();

            // Console.WriteLine(mainThread.Name + " is complete!");


            (controller, controllerY, controllerZ) = initializeXYZ();

            // decimal  x = controller.Position;

            /*      Console.WriteLine(controller.Position);
                  Console.WriteLine(controllerY.Position);
                  Console.WriteLine(controllerZ.Position);*/


            InitializeAllDeviceStatement();

            InitializeComboBox();
        }


        public void Move_Down(String name, IGenericAdvancedMotor controller)
        {

            VelocityParameters velPars = controller.GetVelocityParams();

            velPars.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velPars.Acceleration = 120m;
            // controllerY.

            controller.SetVelocityParams(velPars);

            controller.MoveRelative(0, Convert.ToDecimal(textBox3.Text), 0);
            Console.WriteLine("Timer #1 is complete!");
        }
        public void Move_Up(String name, IGenericAdvancedMotor controller)
        {
            VelocityParameters velPars = controller.GetVelocityParams();

            velPars.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velPars.Acceleration = 120m;
            // controllerY.

            controller.SetVelocityParams(velPars);

            controller.MoveRelative(0, -1 * Convert.ToDecimal(textBox3.Text), 0);
            Console.WriteLine("Timer #2 is complete!");
        }

        public void Move_Clock(String name, IGenericAdvancedMotor controllerY)
        {
            VelocityParameters velParsY = controllerY.GetVelocityParams();

            velParsY.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velParsY.Acceleration = 120m;

            controllerY.SetVelocityParams(velParsY);
            controllerY.MoveRelative(0, Convert.ToDecimal(textBox3.Text), 0);

            Console.WriteLine(controllerY.DevicePosition);
        }

        public void Move_AntiClock(String name, IGenericAdvancedMotor controllerY)
        {
            // controllerY.MoveTo(-5m, 0);
            VelocityParameters velParsY = controllerY.GetVelocityParams();

            velParsY.MaxVelocity = Convert.ToDecimal(textBox4.Text);

            velParsY.Acceleration = 120m;
            // controllerY.

            controllerY.SetVelocityParams(velParsY);
            controllerY.MoveRelative(0, -1 * Convert.ToDecimal(textBox3.Text), 0);
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

                return (controller, controllerY, controllerZ);
            }

        }

        #endregion

        #region Communication control

        private void _buttonOpenUsbCommunication_Click(object sender, EventArgs e)
        {
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
                             *//*
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
                             *//*
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
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Move_downthread();

        }

        private void button5_Click(object sender, EventArgs e, Thread thread1)
        {
            //thread1.Start();
        }

        public static void testt(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controller, Thorlabs.MotionControl.KCube.DCServoCLI.KCubeDCServo controllerY, Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ)
        {
            /* try
             {
                 DeviceManagerCLI.BuildDeviceList();
             }

             catch (Exception)
             {
                 Console.WriteLine("Faillll");
                 return;

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
                 return;

             }
             String serialNoX = "45252174";
             String serialNoY = "27260648";
             String serialNoZ = "45252164";

             LongTravelStage controller = LongTravelStage.CreateLongTravelStage(serialNoX);
             //LongTravelStage controllerZ = LongTravelStage.CreateLongTravelStage(serialNoZ);
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

 */
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
            Console.WriteLine("Device {0} = {1}", deviceInfoY.SerialNumber, deviceInfoY.Name);


            //--------------------------------------------------


            bool homed = controller.Status.IsHomed;
            Console.WriteLine(homed);

            decimal position = 0.00m;
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

            controller.StopPolling();
            controller.Disconnect(true);  //.DisableDevice()
            controllerY.StopPolling();
            controllerY.Disconnect(true);


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
            Thread thread5 = new Thread(() => KeyenceData());
            thread5.Start();
        }

        public void KeyenceData()
        {
           
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
                    this.Invoke((MethodInvoker)(() => OutputLogMessage(measurementData.outMeasurementData[0].measurementValue.ToString())));

                    // OutputLogMessage("GetMeasurementData", returnCode, loggingProperties);
                    //Console.WriteLine(measurementData.outMeasurementData[0].measurementValue.ToString());
                    heightData = Convert.ToDecimal( measurementData.outMeasurementData[0].measurementValue.ToString())/10000;
                    Console.WriteLine(heightData);
                    this.Invoke((MethodInvoker)(() => OutputLogMessage(heightData.ToString())));


                    this.Invoke((MethodInvoker)(() => textBox6.Text = controller.Position.ToString()));
                    this.Invoke((MethodInvoker)(() => textBox7.Text = controllerY.Position.ToString()));
                    this.Invoke((MethodInvoker)(() => textBox8.Text = controllerZ.Position.ToString()));

                    if (!controllerZ.IsDeviceBusy)
                    {
                        Thread threadZ = new Thread(() => MoveZ(controllerZ, heightData));
                        threadZ.Start();
                    }

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

        private void MoveZ(Thorlabs.MotionControl.IntegratedStepperMotorsCLI.LongTravelStage controllerZ, decimal heightData)
        {
            VelocityParameters velParsZ = controllerZ.GetVelocityParams();
            velParsZ.MaxVelocity = 60m;
            velParsZ.Acceleration = 120m;
            controllerZ.SetVelocityParams(velParsZ);

            if ((heightData <= -1m) && (heightData != -99.9999m))
            {
                try
                {
                    controllerZ.MoveTo(11m + heightData,0);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to move to position");
                    // Console.ReadKey();
                    return;
                }
            }

            else if((heightData >= 1m) && (heightData != -99.9999m))
                {
                    try
                    {
                        controllerZ.MoveTo(11 - heightData, 0);
                    }
                catch (Exception)
                {
                    Console.WriteLine("Failed to move to position");
                    // Console.ReadKey();
                    return;
                }
            }

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

        private void button8_Click(object sender, EventArgs e)
        {
            testt(controller, controllerY, controllerZ);

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
            decimal position = 0.001m;
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

            Thread thread5 = new Thread(() => Move_HomeX(controller,position));
            thread5.Start();

            //while (newPosY != 0 && newPosX != 0)
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //decimal position = 0.001m;
            decimal positionY = 0.001m;
           /* if (Convert.ToDecimal(textBox4.Text) != 0)
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
            }*/

            Move_Method_Home(controllerY, positionY);
            Console.WriteLine("Home Method Called");

            Thread thread5 = new Thread(() => Move_HomeY(controllerY, positionY));
            thread5.Start();

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
    }
}

