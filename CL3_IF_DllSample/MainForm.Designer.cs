namespace CL3_IF_DllSample
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._buttonCloseCommunication = new System.Windows.Forms.Button();
            this._buttonOpenEthernetCommunication = new System.Windows.Forms.Button();
            this._buttonOpenUsbCommunication = new System.Windows.Forms.Button();
            this._buttonAutoZeroGroup = new System.Windows.Forms.Button();
            this._buttonAutoZeroSingle = new System.Windows.Forms.Button();
            this._buttonAutoZeroMulti = new System.Windows.Forms.Button();
            this._buttonTimingSingle = new System.Windows.Forms.Button();
            this._buttonTimingMulti = new System.Windows.Forms.Button();
            this._buttonTimingGroup = new System.Windows.Forms.Button();
            this._buttonResetSingle = new System.Windows.Forms.Button();
            this._buttonResetMulti = new System.Windows.Forms.Button();
            this._buttonResetGroup = new System.Windows.Forms.Button();
            this._buttonSwitchProgram = new System.Windows.Forms.Button();
            this._buttonGetProgramNo = new System.Windows.Forms.Button();
            this._pnlDeviceId = new System.Windows.Forms.Panel();
            this._labelStatementTitle = new System.Windows.Forms.Label();
            this._labelDeviceStatus2 = new System.Windows.Forms.Label();
            this._labelDeviceStatus1 = new System.Windows.Forms.Label();
            this._labelDeviceStatus0 = new System.Windows.Forms.Label();
            this._labelIdTitle = new System.Windows.Forms.Label();
            this._radioButtonDevice0 = new System.Windows.Forms.RadioButton();
            this._radioButtonDevice1 = new System.Windows.Forms.RadioButton();
            this._radioButtonDevice2 = new System.Windows.Forms.RadioButton();
            this._tabControlCommand = new System.Windows.Forms.TabControl();
            this._tabPageMeasurementControl = new System.Windows.Forms.TabPage();
            this._groupBoxGetData = new System.Windows.Forms.GroupBox();
            this._groupBoxPeakNo = new System.Windows.Forms.GroupBox();
            this._checkBoxGetLightWaveformPeakNo4 = new System.Windows.Forms.CheckBox();
            this._checkBoxGetLightWaveformPeakNo3 = new System.Windows.Forms.CheckBox();
            this._checkBoxGetLightWaveformPeakNo2 = new System.Windows.Forms.CheckBox();
            this._checkBoxGetLightWaveformPeakNo1 = new System.Windows.Forms.CheckBox();
            this._buttonSaveLightWave = new System.Windows.Forms.Button();
            this._labelTrendDataContinuously = new System.Windows.Forms.Label();
            this._buttonSaveAsTrendData = new System.Windows.Forms.Button();
            this._textBoxGettingDataCount = new System.Windows.Forms.TextBox();
            this._labelTrendDataCount = new System.Windows.Forms.Label();
            this._buttonStartGettingData = new System.Windows.Forms.Button();
            this._buttonTrendContinuouslySave = new System.Windows.Forms.Button();
            this._buttonGetLightWaveform = new System.Windows.Forms.Button();
            this._comboBoxGetLightWaveformHead = new System.Windows.Forms.ComboBox();
            this._labelGetLightWaveformHead = new System.Windows.Forms.Label();
            this._textBoxTrendRequestDataCount = new System.Windows.Forms.TextBox();
            this._labelGetTrendDataReadCount = new System.Windows.Forms.Label();
            this._textBoxTrendIndex = new System.Windows.Forms.TextBox();
            this._labelGetTrendDataIndex = new System.Windows.Forms.Label();
            this._buttonGetTrendData = new System.Windows.Forms.Button();
            this._buttonGetTrendIndex = new System.Windows.Forms.Button();
            this._buttonGetTerminalStatus = new System.Windows.Forms.Button();
            this._buttonGetMeasurementData = new System.Windows.Forms.Button();
            this._groupBoxLockPanel = new System.Windows.Forms.GroupBox();
            this._comboBoxLockPanelOnOff = new System.Windows.Forms.ComboBox();
            this._labelLockPanelOnOff = new System.Windows.Forms.Label();
            this._buttonLockPanel = new System.Windows.Forms.Button();
            this._groupBoxProgramNo = new System.Windows.Forms.GroupBox();
            this._comboBoxSwitchProgramProgramNo = new System.Windows.Forms.ComboBox();
            this._labelSwitchProgramProgramNo = new System.Windows.Forms.Label();
            this._groupBoxTiming = new System.Windows.Forms.GroupBox();
            this._checkBoxTimingOutGroup2 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOutGroup1 = new System.Windows.Forms.CheckBox();
            this._comboBoxTimingOutGroupValue = new System.Windows.Forms.ComboBox();
            this._labelTimingGroupOutValue = new System.Windows.Forms.Label();
            this._comboBoxTimingMultiOutValue = new System.Windows.Forms.ComboBox();
            this._comboBoxTimingSingleOnOff = new System.Windows.Forms.ComboBox();
            this._labelTimingMultiOutValue = new System.Windows.Forms.Label();
            this._labelTimingSingleOnOff = new System.Windows.Forms.Label();
            this._groupBoxTimingOutMulti = new System.Windows.Forms.GroupBox();
            this._checkBoxTimingOut8 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut7 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut6 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut5 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut4 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut3 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut2 = new System.Windows.Forms.CheckBox();
            this._checkBoxTimingOut1 = new System.Windows.Forms.CheckBox();
            this._comboBoxTimingSingleOutNo = new System.Windows.Forms.ComboBox();
            this._groupBoxReset = new System.Windows.Forms.GroupBox();
            this._checkBoxResetOutGroup2 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOutGroup1 = new System.Windows.Forms.CheckBox();
            this._groupBoxResetOutMulti = new System.Windows.Forms.GroupBox();
            this._checkBoxResetOut8 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut7 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut6 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut5 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut4 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut3 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut2 = new System.Windows.Forms.CheckBox();
            this._checkBoxResetOut1 = new System.Windows.Forms.CheckBox();
            this._comboBoxResetOutNo = new System.Windows.Forms.ComboBox();
            this._groupBoxZero = new System.Windows.Forms.GroupBox();
            this._checkBoxAutoZeroGroup2 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroGroup1 = new System.Windows.Forms.CheckBox();
            this._comboBoxAutoZeroGroupOnOff = new System.Windows.Forms.ComboBox();
            this._labelAutoZeroGroupOnOff = new System.Windows.Forms.Label();
            this._comboBoxAutoZeroMultiOnOff = new System.Windows.Forms.ComboBox();
            this._comboBoxAutoZeroSingleOnOff = new System.Windows.Forms.ComboBox();
            this._labelAutoZeroMultiOnOff = new System.Windows.Forms.Label();
            this._labelAutoZeroSingleOnOff = new System.Windows.Forms.Label();
            this._groupBoxAutoZeroMultiOut = new System.Windows.Forms.GroupBox();
            this._checkBoxAutoZeroMultiOutNo8 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo7 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo6 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo5 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo4 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo3 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo2 = new System.Windows.Forms.CheckBox();
            this._checkBoxAutoZeroMultiOutNo1 = new System.Windows.Forms.CheckBox();
            this._comboBoxAutoZeroSingleOutNo = new System.Windows.Forms.ComboBox();
            this._tabPageSettingRW = new System.Windows.Forms.TabPage();
            this._groupBoxProgramSettings = new System.Windows.Forms.GroupBox();
            this._labelAnalogOutAllocation = new System.Windows.Forms.Label();
            this._buttonGetAnalogOutAllocation = new System.Windows.Forms.Button();
            this._buttonSetAnalogOutAllocation = new System.Windows.Forms.Button();
            this._buttonGetStorageTiming = new System.Windows.Forms.Button();
            this._buttonSetStorageTiming = new System.Windows.Forms.Button();
            this._labelStorageTiming = new System.Windows.Forms.Label();
            this._groupBoxHeadSettings = new System.Windows.Forms.GroupBox();
            this._labelHighSensitivity = new System.Windows.Forms.Label();
            this._buttonSetHighSensitivity = new System.Windows.Forms.Button();
            this._buttonGetHighSensitivity = new System.Windows.Forms.Button();
            this._comboBoxSettingHead = new System.Windows.Forms.ComboBox();
            this._labelLightIntensityIntegration = new System.Windows.Forms.Label();
            this._buttonSetLightIntensityIntegration = new System.Windows.Forms.Button();
            this._buttonGetLightIntensityIntegration = new System.Windows.Forms.Button();
            this._labelMeasurementPeaks = new System.Windows.Forms.Label();
            this._buttonSetMeasurementPeaks = new System.Windows.Forms.Button();
            this._buttonGetMeasurementPeaks = new System.Windows.Forms.Button();
            this._labelCheckingNumberOfPeaks = new System.Windows.Forms.Label();
            this._buttonSetCheckingNumberOfPeaks = new System.Windows.Forms.Button();
            this._buttonGetCheckingNumberOfPeaks = new System.Windows.Forms.Button();
            this._labelMultiLightIntensityControl = new System.Windows.Forms.Label();
            this._labelSettingHead = new System.Windows.Forms.Label();
            this._buttonSetMultiLightIntensityControl = new System.Windows.Forms.Button();
            this._buttonGetMultiLightIntensityControl = new System.Windows.Forms.Button();
            this._buttonGetPeakShapeFilter = new System.Windows.Forms.Button();
            this._buttonSetPeakShapeFilter = new System.Windows.Forms.Button();
            this._labelPeakShapeFilter = new System.Windows.Forms.Label();
            this._labelQuadProcessing = new System.Windows.Forms.Label();
            this._buttonGetLightIntensityControl = new System.Windows.Forms.Button();
            this._labelMedianFilter = new System.Windows.Forms.Label();
            this._buttonSetQuadProcessing = new System.Windows.Forms.Button();
            this._buttonSetLightIntensityControl = new System.Windows.Forms.Button();
            this._buttonSetMedianFilter = new System.Windows.Forms.Button();
            this._buttonGetQuadProcessing = new System.Windows.Forms.Button();
            this._labelLightIntensityControl = new System.Windows.Forms.Label();
            this._buttonGetMedianFilter = new System.Windows.Forms.Button();
            this._labelRefractiveIndexCorrection = new System.Windows.Forms.Label();
            this._buttonGetMask = new System.Windows.Forms.Button();
            this._labelThreshold = new System.Windows.Forms.Label();
            this._buttonSetRefractiveIndexCorrection = new System.Windows.Forms.Button();
            this._buttonSetMask = new System.Windows.Forms.Button();
            this._buttonSetThreshold = new System.Windows.Forms.Button();
            this._buttonGetRefractiveIndexCorrection = new System.Windows.Forms.Button();
            this._labelMask = new System.Windows.Forms.Label();
            this._buttonGetThreshold = new System.Windows.Forms.Button();
            this._groupBoxOutSettings = new System.Windows.Forms.GroupBox();
            this._labelAnalogOutputScaling = new System.Windows.Forms.Label();
            this._buttonGetAnalogOutputScaling = new System.Windows.Forms.Button();
            this._buttonSetAnalogOutputScaling = new System.Windows.Forms.Button();
            this._labelMeasurementMethod = new System.Windows.Forms.Label();
            this._buttonSetMeasurementMethod = new System.Windows.Forms.Button();
            this._buttonGetMeasurementMethod = new System.Windows.Forms.Button();
            this._buttonSetInvalidDataProcessing = new System.Windows.Forms.Button();
            this._comboBoxSettingOut = new System.Windows.Forms.ComboBox();
            this._buttonGetInvalidDataProcessing = new System.Windows.Forms.Button();
            this._labelInvalidDataProcessing = new System.Windows.Forms.Label();
            this._labelOut = new System.Windows.Forms.Label();
            this._labelDisplayUnit = new System.Windows.Forms.Label();
            this._buttonSetDisplayUnit = new System.Windows.Forms.Button();
            this._buttonGetDisplayUnit = new System.Windows.Forms.Button();
            this._buttonGetHold = new System.Windows.Forms.Button();
            this._buttonSetHold = new System.Windows.Forms.Button();
            this._labelHold = new System.Windows.Forms.Label();
            this._buttonGetFilter = new System.Windows.Forms.Button();
            this._buttonSetFilter = new System.Windows.Forms.Button();
            this._labelFilter = new System.Windows.Forms.Label();
            this._buttonGetTolerance = new System.Windows.Forms.Button();
            this._buttonSetTolerance = new System.Windows.Forms.Button();
            this._labelTolerance = new System.Windows.Forms.Label();
            this._buttonGetOffset = new System.Windows.Forms.Button();
            this._buttonGetTerminalAllocation = new System.Windows.Forms.Button();
            this._buttonSetOffset = new System.Windows.Forms.Button();
            this._buttonSetTerminalAllocation = new System.Windows.Forms.Button();
            this._labelTerminalAllocation = new System.Windows.Forms.Label();
            this._labelOffset = new System.Windows.Forms.Label();
            this._buttonGetScaling = new System.Windows.Forms.Button();
            this._buttonSetScaling = new System.Windows.Forms.Button();
            this._labelScaling = new System.Windows.Forms.Label();
            this._buttonGetSamplingCycle = new System.Windows.Forms.Button();
            this._labelSamplingCycle = new System.Windows.Forms.Label();
            this._buttonSetSamplingCycle = new System.Windows.Forms.Button();
            this._labelAmbientLightFilter = new System.Windows.Forms.Label();
            this._buttonSetAmbientLightFilter = new System.Windows.Forms.Button();
            this._buttonGetAmbientLightFilter = new System.Windows.Forms.Button();
            this._labelMutualInterferencePrevention = new System.Windows.Forms.Label();
            this._buttonSetMutualInterferencePrevention = new System.Windows.Forms.Button();
            this._buttonGetMutualInterferencePrevention = new System.Windows.Forms.Button();
            this._comboBoxSettingProgramNo = new System.Windows.Forms.ComboBox();
            this._labelSettingProgramNo = new System.Windows.Forms.Label();
            this._buttonGetStorageNumber = new System.Windows.Forms.Button();
            this._buttonSetStorageNumber = new System.Windows.Forms.Button();
            this._labelStorageNumber = new System.Windows.Forms.Label();
            this._buttonGetStorageTarget = new System.Windows.Forms.Button();
            this._buttonSetStorageTarget = new System.Windows.Forms.Button();
            this._labelStorageTarget = new System.Windows.Forms.Label();
            this._buttonGetJudgmentOutput = new System.Windows.Forms.Button();
            this._buttonSetJudgmentOutput = new System.Windows.Forms.Button();
            this._labelJudgmentOutput = new System.Windows.Forms.Label();
            this._tabPageSettingBatch = new System.Windows.Forms.TabPage();
            this._groupBoxEncoderSetting = new System.Windows.Forms.GroupBox();
            this._buttonSetEncoderSetting = new System.Windows.Forms.Button();
            this._buttonGetEncoderSetting = new System.Windows.Forms.Button();
            this._groupBoxByProgram = new System.Windows.Forms.GroupBox();
            this._comboBoxTargetProgramNo = new System.Windows.Forms.ComboBox();
            this._labelTargetProgramNo = new System.Windows.Forms.Label();
            this._buttonGetProgram = new System.Windows.Forms.Button();
            this._buttonSetProgram = new System.Windows.Forms.Button();
            this._groupBoxAllSettings = new System.Windows.Forms.GroupBox();
            this._buttonSetSettings = new System.Windows.Forms.Button();
            this._buttonGetSettings = new System.Windows.Forms.Button();
            this._labelSetNote = new System.Windows.Forms.Label();
            this._labelGetNote = new System.Windows.Forms.Label();
            this._tabPageDataStorage = new System.Windows.Forms.TabPage();
            this._groupBoxDll = new System.Windows.Forms.GroupBox();
            this._buttonGetVersion = new System.Windows.Forms.Button();
            this._groupBoxOther = new System.Windows.Forms.GroupBox();
            this._comboBoxSetMeasureEnableValue = new System.Windows.Forms.ComboBox();
            this._buttonLightControl = new System.Windows.Forms.Button();
            this._buttonMeasurementControl = new System.Windows.Forms.Button();
            this._labelSetMeasureEnableValue = new System.Windows.Forms.Label();
            this._labelSetLaserControlValue = new System.Windows.Forms.Label();
            this._comboBoxSetLaserControlValue = new System.Windows.Forms.ComboBox();
            this._groupBoxSystem = new System.Windows.Forms.GroupBox();
            this._buttonGetSystemConfiguration = new System.Windows.Forms.Button();
            this._buttonReturnToFactoryDefaultSetting = new System.Windows.Forms.Button();
            this._groupBoxEncoder = new System.Windows.Forms.GroupBox();
            this._buttonGetPulseCount = new System.Windows.Forms.Button();
            this._buttonResetPulseCount = new System.Windows.Forms.Button();
            this._groupBoxModeChange = new System.Windows.Forms.GroupBox();
            this._buttonTransitToMeasurementMode = new System.Windows.Forms.Button();
            this._buttonTransitToSettingMode = new System.Windows.Forms.Button();
            this._groupBoxCalibration = new System.Windows.Forms.GroupBox();
            this._comboBoxCalibration = new System.Windows.Forms.ComboBox();
            this._labelCalibrationAmpNo = new System.Windows.Forms.Label();
            this._buttonCancelCalibration = new System.Windows.Forms.Button();
            this._buttonStopCalibration = new System.Windows.Forms.Button();
            this._buttonStartCalibration = new System.Windows.Forms.Button();
            this._groupBoxDataStorage = new System.Windows.Forms.GroupBox();
            this._comboBoxSelectedIndex = new System.Windows.Forms.ComboBox();
            this._labelSelectedIndex = new System.Windows.Forms.Label();
            this._textBoxStorageDataCount = new System.Windows.Forms.TextBox();
            this._labelStorageDataCount = new System.Windows.Forms.Label();
            this._buttonStorageContinuouslySave = new System.Windows.Forms.Button();
            this._buttonStorageContinuously = new System.Windows.Forms.Button();
            this._labelStorageContinuously = new System.Windows.Forms.Label();
            this._buttonStorageSave = new System.Windows.Forms.Button();
            this._buttonStartStorage = new System.Windows.Forms.Button();
            this._buttonClearStorageData = new System.Windows.Forms.Button();
            this._textBoxGetStorageDataReadCount = new System.Windows.Forms.TextBox();
            this._buttonStopStorage = new System.Windows.Forms.Button();
            this._labelGetStorageDataReadCount = new System.Windows.Forms.Label();
            this._buttonGetStorageData = new System.Windows.Forms.Button();
            this._textBoxGetStorageDataIndex = new System.Windows.Forms.TextBox();
            this._buttonGetStorageIndex = new System.Windows.Forms.Button();
            this._labelGetStorageDataIndex = new System.Windows.Forms.Label();
            this._groupBoxAbleTuning = new System.Windows.Forms.GroupBox();
            this._comboBoxLightIntensityTuning = new System.Windows.Forms.ComboBox();
            this._labelABLETuningAmpNo = new System.Windows.Forms.Label();
            this._buttonStartLightIntensityTuning = new System.Windows.Forms.Button();
            this._buttonCancelLightIntensityTuning = new System.Windows.Forms.Button();
            this._buttonStopLightIntensityTuning = new System.Windows.Forms.Button();
            this._textBoxFourthSegment = new System.Windows.Forms.TextBox();
            this._textBoxThirdSegment = new System.Windows.Forms.TextBox();
            this._textBoxSecondSegment = new System.Windows.Forms.TextBox();
            this._textBoxFirstSegment = new System.Windows.Forms.TextBox();
            this._groupBoxCommunicationControl = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this._textBoxPortNo = new System.Windows.Forms.TextBox();
            this._labelPortNoBranch = new System.Windows.Forms.Label();
            this._buttonClearLog = new System.Windows.Forms.Button();
            this._listboxLog = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this._pnlDeviceId.SuspendLayout();
            this._tabControlCommand.SuspendLayout();
            this._tabPageMeasurementControl.SuspendLayout();
            this._groupBoxGetData.SuspendLayout();
            this._groupBoxPeakNo.SuspendLayout();
            this._groupBoxLockPanel.SuspendLayout();
            this._groupBoxProgramNo.SuspendLayout();
            this._groupBoxTiming.SuspendLayout();
            this._groupBoxTimingOutMulti.SuspendLayout();
            this._groupBoxReset.SuspendLayout();
            this._groupBoxResetOutMulti.SuspendLayout();
            this._groupBoxZero.SuspendLayout();
            this._groupBoxAutoZeroMultiOut.SuspendLayout();
            this._tabPageSettingRW.SuspendLayout();
            this._groupBoxProgramSettings.SuspendLayout();
            this._groupBoxHeadSettings.SuspendLayout();
            this._groupBoxOutSettings.SuspendLayout();
            this._tabPageSettingBatch.SuspendLayout();
            this._groupBoxEncoderSetting.SuspendLayout();
            this._groupBoxByProgram.SuspendLayout();
            this._groupBoxAllSettings.SuspendLayout();
            this._tabPageDataStorage.SuspendLayout();
            this._groupBoxDll.SuspendLayout();
            this._groupBoxOther.SuspendLayout();
            this._groupBoxSystem.SuspendLayout();
            this._groupBoxEncoder.SuspendLayout();
            this._groupBoxModeChange.SuspendLayout();
            this._groupBoxCalibration.SuspendLayout();
            this._groupBoxDataStorage.SuspendLayout();
            this._groupBoxAbleTuning.SuspendLayout();
            this._groupBoxCommunicationControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonCloseCommunication
            // 
            this._buttonCloseCommunication.Location = new System.Drawing.Point(12, 162);
            this._buttonCloseCommunication.Margin = new System.Windows.Forms.Padding(6);
            this._buttonCloseCommunication.Name = "_buttonCloseCommunication";
            this._buttonCloseCommunication.Size = new System.Drawing.Size(320, 48);
            this._buttonCloseCommunication.TabIndex = 8;
            this._buttonCloseCommunication.Text = "CloseCommunication";
            this._buttonCloseCommunication.UseVisualStyleBackColor = true;
            this._buttonCloseCommunication.Click += new System.EventHandler(this._buttonCloseCommunication_Click);
            // 
            // _buttonOpenEthernetCommunication
            // 
            this._buttonOpenEthernetCommunication.Location = new System.Drawing.Point(12, 102);
            this._buttonOpenEthernetCommunication.Margin = new System.Windows.Forms.Padding(6);
            this._buttonOpenEthernetCommunication.Name = "_buttonOpenEthernetCommunication";
            this._buttonOpenEthernetCommunication.Size = new System.Drawing.Size(320, 48);
            this._buttonOpenEthernetCommunication.TabIndex = 1;
            this._buttonOpenEthernetCommunication.Text = "OpenEthernetCommunication";
            this._buttonOpenEthernetCommunication.UseVisualStyleBackColor = true;
            this._buttonOpenEthernetCommunication.Click += new System.EventHandler(this._buttonOpenEthernetCommunication_Click);
            // 
            // _buttonOpenUsbCommunication
            // 
            this._buttonOpenUsbCommunication.Location = new System.Drawing.Point(12, 42);
            this._buttonOpenUsbCommunication.Margin = new System.Windows.Forms.Padding(6);
            this._buttonOpenUsbCommunication.Name = "_buttonOpenUsbCommunication";
            this._buttonOpenUsbCommunication.Size = new System.Drawing.Size(320, 48);
            this._buttonOpenUsbCommunication.TabIndex = 0;
            this._buttonOpenUsbCommunication.Text = "OpenUsbCommunication";
            this._buttonOpenUsbCommunication.UseVisualStyleBackColor = true;
            this._buttonOpenUsbCommunication.Click += new System.EventHandler(this._buttonOpenUsbCommunication_Click);
            // 
            // _buttonAutoZeroGroup
            // 
            this._buttonAutoZeroGroup.Location = new System.Drawing.Point(24, 246);
            this._buttonAutoZeroGroup.Margin = new System.Windows.Forms.Padding(6);
            this._buttonAutoZeroGroup.Name = "_buttonAutoZeroGroup";
            this._buttonAutoZeroGroup.Size = new System.Drawing.Size(216, 48);
            this._buttonAutoZeroGroup.TabIndex = 8;
            this._buttonAutoZeroGroup.Text = "AutoZeroGroup";
            this._buttonAutoZeroGroup.UseVisualStyleBackColor = true;
            this._buttonAutoZeroGroup.Click += new System.EventHandler(this._buttonAutoZeroGroup_Click);
            // 
            // _buttonAutoZeroSingle
            // 
            this._buttonAutoZeroSingle.Location = new System.Drawing.Point(24, 38);
            this._buttonAutoZeroSingle.Margin = new System.Windows.Forms.Padding(6);
            this._buttonAutoZeroSingle.Name = "_buttonAutoZeroSingle";
            this._buttonAutoZeroSingle.Size = new System.Drawing.Size(216, 48);
            this._buttonAutoZeroSingle.TabIndex = 0;
            this._buttonAutoZeroSingle.Text = "AutoZeroSingle";
            this._buttonAutoZeroSingle.UseVisualStyleBackColor = true;
            this._buttonAutoZeroSingle.Click += new System.EventHandler(this._buttonAutoZeroSingle_Click);
            // 
            // _buttonAutoZeroMulti
            // 
            this._buttonAutoZeroMulti.Location = new System.Drawing.Point(24, 98);
            this._buttonAutoZeroMulti.Margin = new System.Windows.Forms.Padding(6);
            this._buttonAutoZeroMulti.Name = "_buttonAutoZeroMulti";
            this._buttonAutoZeroMulti.Size = new System.Drawing.Size(216, 48);
            this._buttonAutoZeroMulti.TabIndex = 4;
            this._buttonAutoZeroMulti.Text = "AutoZeroMulti";
            this._buttonAutoZeroMulti.UseVisualStyleBackColor = true;
            this._buttonAutoZeroMulti.Click += new System.EventHandler(this._buttonAutoZeroMulti_Click);
            // 
            // _buttonTimingSingle
            // 
            this._buttonTimingSingle.Location = new System.Drawing.Point(24, 35);
            this._buttonTimingSingle.Margin = new System.Windows.Forms.Padding(6);
            this._buttonTimingSingle.Name = "_buttonTimingSingle";
            this._buttonTimingSingle.Size = new System.Drawing.Size(216, 48);
            this._buttonTimingSingle.TabIndex = 0;
            this._buttonTimingSingle.Text = "TimingSingle";
            this._buttonTimingSingle.UseVisualStyleBackColor = true;
            this._buttonTimingSingle.Click += new System.EventHandler(this._buttonTimingSingle_Click);
            // 
            // _buttonTimingMulti
            // 
            this._buttonTimingMulti.Location = new System.Drawing.Point(24, 110);
            this._buttonTimingMulti.Margin = new System.Windows.Forms.Padding(6);
            this._buttonTimingMulti.Name = "_buttonTimingMulti";
            this._buttonTimingMulti.Size = new System.Drawing.Size(216, 48);
            this._buttonTimingMulti.TabIndex = 4;
            this._buttonTimingMulti.Text = "TimingMulti";
            this._buttonTimingMulti.UseVisualStyleBackColor = true;
            this._buttonTimingMulti.Click += new System.EventHandler(this._buttonTimingMulti_Click);
            // 
            // _buttonTimingGroup
            // 
            this._buttonTimingGroup.Location = new System.Drawing.Point(28, 244);
            this._buttonTimingGroup.Margin = new System.Windows.Forms.Padding(6);
            this._buttonTimingGroup.Name = "_buttonTimingGroup";
            this._buttonTimingGroup.Size = new System.Drawing.Size(216, 48);
            this._buttonTimingGroup.TabIndex = 8;
            this._buttonTimingGroup.Text = "TimingGroup";
            this._buttonTimingGroup.UseVisualStyleBackColor = true;
            this._buttonTimingGroup.Click += new System.EventHandler(this._buttonTimingGroup_Click);
            // 
            // _buttonResetSingle
            // 
            this._buttonResetSingle.Location = new System.Drawing.Point(24, 38);
            this._buttonResetSingle.Margin = new System.Windows.Forms.Padding(6);
            this._buttonResetSingle.Name = "_buttonResetSingle";
            this._buttonResetSingle.Size = new System.Drawing.Size(216, 48);
            this._buttonResetSingle.TabIndex = 0;
            this._buttonResetSingle.Text = "ResetSingle";
            this._buttonResetSingle.UseVisualStyleBackColor = true;
            this._buttonResetSingle.Click += new System.EventHandler(this._buttonResetSingle_Click);
            // 
            // _buttonResetMulti
            // 
            this._buttonResetMulti.Location = new System.Drawing.Point(24, 98);
            this._buttonResetMulti.Margin = new System.Windows.Forms.Padding(6);
            this._buttonResetMulti.Name = "_buttonResetMulti";
            this._buttonResetMulti.Size = new System.Drawing.Size(216, 48);
            this._buttonResetMulti.TabIndex = 2;
            this._buttonResetMulti.Text = "ResetMulti";
            this._buttonResetMulti.UseVisualStyleBackColor = true;
            this._buttonResetMulti.Click += new System.EventHandler(this._buttonResetMulti_Click);
            // 
            // _buttonResetGroup
            // 
            this._buttonResetGroup.Location = new System.Drawing.Point(24, 246);
            this._buttonResetGroup.Margin = new System.Windows.Forms.Padding(6);
            this._buttonResetGroup.Name = "_buttonResetGroup";
            this._buttonResetGroup.Size = new System.Drawing.Size(216, 48);
            this._buttonResetGroup.TabIndex = 4;
            this._buttonResetGroup.Text = "ResetGroup";
            this._buttonResetGroup.UseVisualStyleBackColor = true;
            this._buttonResetGroup.Click += new System.EventHandler(this._buttonResetGroup_Click);
            // 
            // _buttonSwitchProgram
            // 
            this._buttonSwitchProgram.Location = new System.Drawing.Point(24, 33);
            this._buttonSwitchProgram.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSwitchProgram.Name = "_buttonSwitchProgram";
            this._buttonSwitchProgram.Size = new System.Drawing.Size(228, 48);
            this._buttonSwitchProgram.TabIndex = 0;
            this._buttonSwitchProgram.Text = "SwitchProgram";
            this._buttonSwitchProgram.UseVisualStyleBackColor = true;
            this._buttonSwitchProgram.Click += new System.EventHandler(this._buttonSwitchProgram_Click);
            // 
            // _buttonGetProgramNo
            // 
            this._buttonGetProgramNo.Location = new System.Drawing.Point(24, 94);
            this._buttonGetProgramNo.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetProgramNo.Name = "_buttonGetProgramNo";
            this._buttonGetProgramNo.Size = new System.Drawing.Size(228, 48);
            this._buttonGetProgramNo.TabIndex = 3;
            this._buttonGetProgramNo.Text = "GetProgramNo";
            this._buttonGetProgramNo.UseVisualStyleBackColor = true;
            this._buttonGetProgramNo.Click += new System.EventHandler(this._buttonGetProgramNo_Click);
            // 
            // _pnlDeviceId
            // 
            this._pnlDeviceId.BackColor = System.Drawing.Color.DarkGray;
            this._pnlDeviceId.Controls.Add(this._labelStatementTitle);
            this._pnlDeviceId.Controls.Add(this._labelDeviceStatus2);
            this._pnlDeviceId.Controls.Add(this._labelDeviceStatus1);
            this._pnlDeviceId.Controls.Add(this._labelDeviceStatus0);
            this._pnlDeviceId.Controls.Add(this._labelIdTitle);
            this._pnlDeviceId.Controls.Add(this._radioButtonDevice0);
            this._pnlDeviceId.Controls.Add(this._radioButtonDevice1);
            this._pnlDeviceId.Controls.Add(this._radioButtonDevice2);
            this._pnlDeviceId.Location = new System.Drawing.Point(730, 50);
            this._pnlDeviceId.Margin = new System.Windows.Forms.Padding(6);
            this._pnlDeviceId.Name = "_pnlDeviceId";
            this._pnlDeviceId.Size = new System.Drawing.Size(410, 107);
            this._pnlDeviceId.TabIndex = 9;
            // 
            // _labelStatementTitle
            // 
            this._labelStatementTitle.AutoSize = true;
            this._labelStatementTitle.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._labelStatementTitle.Location = new System.Drawing.Point(118, 10);
            this._labelStatementTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelStatementTitle.Name = "_labelStatementTitle";
            this._labelStatementTitle.Size = new System.Drawing.Size(122, 24);
            this._labelStatementTitle.TabIndex = 4;
            this._labelStatementTitle.Text = "Statement";
            // 
            // _labelDeviceStatus2
            // 
            this._labelDeviceStatus2.AutoSize = true;
            this._labelDeviceStatus2.Location = new System.Drawing.Point(118, 110);
            this._labelDeviceStatus2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelDeviceStatus2.Name = "_labelDeviceStatus2";
            this._labelDeviceStatus2.Size = new System.Drawing.Size(140, 25);
            this._labelDeviceStatus2.TabIndex = 7;
            this._labelDeviceStatus2.Text = "disconnected";
            // 
            // _labelDeviceStatus1
            // 
            this._labelDeviceStatus1.AutoSize = true;
            this._labelDeviceStatus1.Location = new System.Drawing.Point(118, 77);
            this._labelDeviceStatus1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelDeviceStatus1.Name = "_labelDeviceStatus1";
            this._labelDeviceStatus1.Size = new System.Drawing.Size(140, 25);
            this._labelDeviceStatus1.TabIndex = 6;
            this._labelDeviceStatus1.Text = "disconnected";
            // 
            // _labelDeviceStatus0
            // 
            this._labelDeviceStatus0.AutoSize = true;
            this._labelDeviceStatus0.Location = new System.Drawing.Point(118, 44);
            this._labelDeviceStatus0.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelDeviceStatus0.Name = "_labelDeviceStatus0";
            this._labelDeviceStatus0.Size = new System.Drawing.Size(140, 25);
            this._labelDeviceStatus0.TabIndex = 5;
            this._labelDeviceStatus0.Text = "disconnected";
            // 
            // _labelIdTitle
            // 
            this._labelIdTitle.AutoSize = true;
            this._labelIdTitle.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._labelIdTitle.Location = new System.Drawing.Point(46, 10);
            this._labelIdTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelIdTitle.Name = "_labelIdTitle";
            this._labelIdTitle.Size = new System.Drawing.Size(34, 24);
            this._labelIdTitle.TabIndex = 3;
            this._labelIdTitle.Text = "ID";
            // 
            // _radioButtonDevice0
            // 
            this._radioButtonDevice0.AutoSize = true;
            this._radioButtonDevice0.Checked = true;
            this._radioButtonDevice0.Location = new System.Drawing.Point(16, 40);
            this._radioButtonDevice0.Margin = new System.Windows.Forms.Padding(6);
            this._radioButtonDevice0.Name = "_radioButtonDevice0";
            this._radioButtonDevice0.Size = new System.Drawing.Size(55, 29);
            this._radioButtonDevice0.TabIndex = 0;
            this._radioButtonDevice0.TabStop = true;
            this._radioButtonDevice0.Tag = "0";
            this._radioButtonDevice0.Text = "&0";
            this._radioButtonDevice0.UseVisualStyleBackColor = true;
            // 
            // _radioButtonDevice1
            // 
            this._radioButtonDevice1.AutoSize = true;
            this._radioButtonDevice1.Location = new System.Drawing.Point(16, 73);
            this._radioButtonDevice1.Margin = new System.Windows.Forms.Padding(6);
            this._radioButtonDevice1.Name = "_radioButtonDevice1";
            this._radioButtonDevice1.Size = new System.Drawing.Size(55, 29);
            this._radioButtonDevice1.TabIndex = 1;
            this._radioButtonDevice1.Tag = "1";
            this._radioButtonDevice1.Text = "&1";
            this._radioButtonDevice1.UseVisualStyleBackColor = true;
            // 
            // _radioButtonDevice2
            // 
            this._radioButtonDevice2.AutoSize = true;
            this._radioButtonDevice2.Location = new System.Drawing.Point(16, 106);
            this._radioButtonDevice2.Margin = new System.Windows.Forms.Padding(6);
            this._radioButtonDevice2.Name = "_radioButtonDevice2";
            this._radioButtonDevice2.Size = new System.Drawing.Size(55, 29);
            this._radioButtonDevice2.TabIndex = 2;
            this._radioButtonDevice2.Tag = "2";
            this._radioButtonDevice2.Text = "&2";
            this._radioButtonDevice2.UseVisualStyleBackColor = true;
            // 
            // _tabControlCommand
            // 
            this._tabControlCommand.Controls.Add(this._tabPageMeasurementControl);
            this._tabControlCommand.Controls.Add(this._tabPageSettingRW);
            this._tabControlCommand.Controls.Add(this._tabPageSettingBatch);
            this._tabControlCommand.Controls.Add(this._tabPageDataStorage);
            this._tabControlCommand.Location = new System.Drawing.Point(24, 323);
            this._tabControlCommand.Margin = new System.Windows.Forms.Padding(6);
            this._tabControlCommand.Name = "_tabControlCommand";
            this._tabControlCommand.SelectedIndex = 0;
            this._tabControlCommand.Size = new System.Drawing.Size(1968, 1171);
            this._tabControlCommand.TabIndex = 2;
            // 
            // _tabPageMeasurementControl
            // 
            this._tabPageMeasurementControl.Controls.Add(this._groupBoxGetData);
            this._tabPageMeasurementControl.Controls.Add(this._groupBoxLockPanel);
            this._tabPageMeasurementControl.Controls.Add(this._groupBoxProgramNo);
            this._tabPageMeasurementControl.Controls.Add(this._groupBoxTiming);
            this._tabPageMeasurementControl.Controls.Add(this._groupBoxReset);
            this._tabPageMeasurementControl.Controls.Add(this._groupBoxZero);
            this._tabPageMeasurementControl.Location = new System.Drawing.Point(8, 39);
            this._tabPageMeasurementControl.Margin = new System.Windows.Forms.Padding(6);
            this._tabPageMeasurementControl.Name = "_tabPageMeasurementControl";
            this._tabPageMeasurementControl.Padding = new System.Windows.Forms.Padding(6);
            this._tabPageMeasurementControl.Size = new System.Drawing.Size(1952, 1124);
            this._tabPageMeasurementControl.TabIndex = 0;
            this._tabPageMeasurementControl.Text = "Measurement";
            this._tabPageMeasurementControl.UseVisualStyleBackColor = true;
            // 
            // _groupBoxGetData
            // 
            this._groupBoxGetData.Controls.Add(this._groupBoxPeakNo);
            this._groupBoxGetData.Controls.Add(this._buttonSaveLightWave);
            this._groupBoxGetData.Controls.Add(this._labelTrendDataContinuously);
            this._groupBoxGetData.Controls.Add(this._buttonSaveAsTrendData);
            this._groupBoxGetData.Controls.Add(this._textBoxGettingDataCount);
            this._groupBoxGetData.Controls.Add(this._labelTrendDataCount);
            this._groupBoxGetData.Controls.Add(this._buttonStartGettingData);
            this._groupBoxGetData.Controls.Add(this._buttonTrendContinuouslySave);
            this._groupBoxGetData.Controls.Add(this._buttonGetLightWaveform);
            this._groupBoxGetData.Controls.Add(this._comboBoxGetLightWaveformHead);
            this._groupBoxGetData.Controls.Add(this._labelGetLightWaveformHead);
            this._groupBoxGetData.Controls.Add(this._textBoxTrendRequestDataCount);
            this._groupBoxGetData.Controls.Add(this._labelGetTrendDataReadCount);
            this._groupBoxGetData.Controls.Add(this._textBoxTrendIndex);
            this._groupBoxGetData.Controls.Add(this._labelGetTrendDataIndex);
            this._groupBoxGetData.Controls.Add(this._buttonGetTrendData);
            this._groupBoxGetData.Controls.Add(this._buttonGetTrendIndex);
            this._groupBoxGetData.Controls.Add(this._buttonGetTerminalStatus);
            this._groupBoxGetData.Controls.Add(this._buttonGetMeasurementData);
            this._groupBoxGetData.Location = new System.Drawing.Point(12, 12);
            this._groupBoxGetData.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxGetData.Name = "_groupBoxGetData";
            this._groupBoxGetData.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxGetData.Size = new System.Drawing.Size(820, 698);
            this._groupBoxGetData.TabIndex = 0;
            this._groupBoxGetData.TabStop = false;
            this._groupBoxGetData.Text = "Get data";
            // 
            // _groupBoxPeakNo
            // 
            this._groupBoxPeakNo.Controls.Add(this._checkBoxGetLightWaveformPeakNo4);
            this._groupBoxPeakNo.Controls.Add(this._checkBoxGetLightWaveformPeakNo3);
            this._groupBoxPeakNo.Controls.Add(this._checkBoxGetLightWaveformPeakNo2);
            this._groupBoxPeakNo.Controls.Add(this._checkBoxGetLightWaveformPeakNo1);
            this._groupBoxPeakNo.Location = new System.Drawing.Point(536, 560);
            this._groupBoxPeakNo.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxPeakNo.Name = "_groupBoxPeakNo";
            this._groupBoxPeakNo.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxPeakNo.Size = new System.Drawing.Size(272, 94);
            this._groupBoxPeakNo.TabIndex = 17;
            this._groupBoxPeakNo.TabStop = false;
            this._groupBoxPeakNo.Text = "Peak No";
            // 
            // _checkBoxGetLightWaveformPeakNo4
            // 
            this._checkBoxGetLightWaveformPeakNo4.AutoSize = true;
            this._checkBoxGetLightWaveformPeakNo4.Location = new System.Drawing.Point(210, 38);
            this._checkBoxGetLightWaveformPeakNo4.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxGetLightWaveformPeakNo4.Name = "_checkBoxGetLightWaveformPeakNo4";
            this._checkBoxGetLightWaveformPeakNo4.Size = new System.Drawing.Size(56, 29);
            this._checkBoxGetLightWaveformPeakNo4.TabIndex = 3;
            this._checkBoxGetLightWaveformPeakNo4.Text = "4";
            this._checkBoxGetLightWaveformPeakNo4.UseVisualStyleBackColor = true;
            // 
            // _checkBoxGetLightWaveformPeakNo3
            // 
            this._checkBoxGetLightWaveformPeakNo3.AutoSize = true;
            this._checkBoxGetLightWaveformPeakNo3.Location = new System.Drawing.Point(144, 38);
            this._checkBoxGetLightWaveformPeakNo3.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxGetLightWaveformPeakNo3.Name = "_checkBoxGetLightWaveformPeakNo3";
            this._checkBoxGetLightWaveformPeakNo3.Size = new System.Drawing.Size(56, 29);
            this._checkBoxGetLightWaveformPeakNo3.TabIndex = 2;
            this._checkBoxGetLightWaveformPeakNo3.Text = "3";
            this._checkBoxGetLightWaveformPeakNo3.UseVisualStyleBackColor = true;
            // 
            // _checkBoxGetLightWaveformPeakNo2
            // 
            this._checkBoxGetLightWaveformPeakNo2.AutoSize = true;
            this._checkBoxGetLightWaveformPeakNo2.Location = new System.Drawing.Point(78, 38);
            this._checkBoxGetLightWaveformPeakNo2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxGetLightWaveformPeakNo2.Name = "_checkBoxGetLightWaveformPeakNo2";
            this._checkBoxGetLightWaveformPeakNo2.Size = new System.Drawing.Size(56, 29);
            this._checkBoxGetLightWaveformPeakNo2.TabIndex = 1;
            this._checkBoxGetLightWaveformPeakNo2.Text = "2";
            this._checkBoxGetLightWaveformPeakNo2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxGetLightWaveformPeakNo1
            // 
            this._checkBoxGetLightWaveformPeakNo1.AutoSize = true;
            this._checkBoxGetLightWaveformPeakNo1.Checked = true;
            this._checkBoxGetLightWaveformPeakNo1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxGetLightWaveformPeakNo1.Location = new System.Drawing.Point(12, 38);
            this._checkBoxGetLightWaveformPeakNo1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxGetLightWaveformPeakNo1.Name = "_checkBoxGetLightWaveformPeakNo1";
            this._checkBoxGetLightWaveformPeakNo1.Size = new System.Drawing.Size(56, 29);
            this._checkBoxGetLightWaveformPeakNo1.TabIndex = 0;
            this._checkBoxGetLightWaveformPeakNo1.Text = "1";
            this._checkBoxGetLightWaveformPeakNo1.UseVisualStyleBackColor = true;
            // 
            // _buttonSaveLightWave
            // 
            this._buttonSaveLightWave.Image = ((System.Drawing.Image)(resources.GetObject("_buttonSaveLightWave.Image")));
            this._buttonSaveLightWave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonSaveLightWave.Location = new System.Drawing.Point(26, 623);
            this._buttonSaveLightWave.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSaveLightWave.Name = "_buttonSaveLightWave";
            this._buttonSaveLightWave.Size = new System.Drawing.Size(232, 48);
            this._buttonSaveLightWave.TabIndex = 18;
            this._buttonSaveLightWave.Text = "Save as csv...";
            this._buttonSaveLightWave.UseVisualStyleBackColor = true;
            this._buttonSaveLightWave.Click += new System.EventHandler(this._buttonSaveLightWave_Click);
            // 
            // _labelTrendDataContinuously
            // 
            this._labelTrendDataContinuously.AutoSize = true;
            this._labelTrendDataContinuously.Location = new System.Drawing.Point(20, 300);
            this._labelTrendDataContinuously.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTrendDataContinuously.Name = "_labelTrendDataContinuously";
            this._labelTrendDataContinuously.Size = new System.Drawing.Size(276, 25);
            this._labelTrendDataContinuously.TabIndex = 8;
            this._labelTrendDataContinuously.Text = "Get trend data continuously";
            // 
            // _buttonSaveAsTrendData
            // 
            this._buttonSaveAsTrendData.Image = ((System.Drawing.Image)(resources.GetObject("_buttonSaveAsTrendData.Image")));
            this._buttonSaveAsTrendData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonSaveAsTrendData.Location = new System.Drawing.Point(24, 229);
            this._buttonSaveAsTrendData.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSaveAsTrendData.Name = "_buttonSaveAsTrendData";
            this._buttonSaveAsTrendData.Size = new System.Drawing.Size(232, 48);
            this._buttonSaveAsTrendData.TabIndex = 7;
            this._buttonSaveAsTrendData.Text = "Save as csv...";
            this._buttonSaveAsTrendData.UseVisualStyleBackColor = true;
            this._buttonSaveAsTrendData.Click += new System.EventHandler(this._buttonSaveAsTrendData_Click);
            // 
            // _textBoxGettingDataCount
            // 
            this._textBoxGettingDataCount.Location = new System.Drawing.Point(396, 335);
            this._textBoxGettingDataCount.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxGettingDataCount.Name = "_textBoxGettingDataCount";
            this._textBoxGettingDataCount.ReadOnly = true;
            this._textBoxGettingDataCount.Size = new System.Drawing.Size(156, 31);
            this._textBoxGettingDataCount.TabIndex = 11;
            // 
            // _labelTrendDataCount
            // 
            this._labelTrendDataCount.AutoSize = true;
            this._labelTrendDataCount.Location = new System.Drawing.Point(270, 342);
            this._labelTrendDataCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTrendDataCount.Name = "_labelTrendDataCount";
            this._labelTrendDataCount.Size = new System.Drawing.Size(111, 25);
            this._labelTrendDataCount.TabIndex = 10;
            this._labelTrendDataCount.Text = "dataCount";
            // 
            // _buttonStartGettingData
            // 
            this._buttonStartGettingData.BackColor = System.Drawing.Color.LightBlue;
            this._buttonStartGettingData.Location = new System.Drawing.Point(20, 331);
            this._buttonStartGettingData.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStartGettingData.Name = "_buttonStartGettingData";
            this._buttonStartGettingData.Size = new System.Drawing.Size(232, 48);
            this._buttonStartGettingData.TabIndex = 9;
            this._buttonStartGettingData.Text = "Start getting data";
            this._buttonStartGettingData.UseVisualStyleBackColor = false;
            this._buttonStartGettingData.Click += new System.EventHandler(this._buttonStartGettingData_Click);
            // 
            // _buttonTrendContinuouslySave
            // 
            this._buttonTrendContinuouslySave.Image = ((System.Drawing.Image)(resources.GetObject("_buttonTrendContinuouslySave.Image")));
            this._buttonTrendContinuouslySave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonTrendContinuouslySave.Location = new System.Drawing.Point(20, 392);
            this._buttonTrendContinuouslySave.Margin = new System.Windows.Forms.Padding(6);
            this._buttonTrendContinuouslySave.Name = "_buttonTrendContinuouslySave";
            this._buttonTrendContinuouslySave.Size = new System.Drawing.Size(232, 48);
            this._buttonTrendContinuouslySave.TabIndex = 12;
            this._buttonTrendContinuouslySave.Text = "Save as csv...";
            this._buttonTrendContinuouslySave.UseVisualStyleBackColor = true;
            this._buttonTrendContinuouslySave.Click += new System.EventHandler(this._buttonTrendContinuouslySave_Click);
            // 
            // _buttonGetLightWaveform
            // 
            this._buttonGetLightWaveform.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetLightWaveform.Location = new System.Drawing.Point(24, 560);
            this._buttonGetLightWaveform.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetLightWaveform.Name = "_buttonGetLightWaveform";
            this._buttonGetLightWaveform.Size = new System.Drawing.Size(232, 48);
            this._buttonGetLightWaveform.TabIndex = 14;
            this._buttonGetLightWaveform.Text = "GetLightWaveform";
            this._buttonGetLightWaveform.UseVisualStyleBackColor = false;
            this._buttonGetLightWaveform.Click += new System.EventHandler(this._buttonGetLightWaveform_Click);
            // 
            // _comboBoxGetLightWaveformHead
            // 
            this._comboBoxGetLightWaveformHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxGetLightWaveformHead.FormattingEnabled = true;
            this._comboBoxGetLightWaveformHead.Items.AddRange(new object[] {
            "HEAD 01",
            "HEAD 02",
            "HEAD 03",
            "HEAD 04",
            "HEAD 05",
            "HEAD 06"});
            this._comboBoxGetLightWaveformHead.Location = new System.Drawing.Point(356, 567);
            this._comboBoxGetLightWaveformHead.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxGetLightWaveformHead.Name = "_comboBoxGetLightWaveformHead";
            this._comboBoxGetLightWaveformHead.Size = new System.Drawing.Size(152, 33);
            this._comboBoxGetLightWaveformHead.TabIndex = 16;
            // 
            // _labelGetLightWaveformHead
            // 
            this._labelGetLightWaveformHead.AutoSize = true;
            this._labelGetLightWaveformHead.Location = new System.Drawing.Point(278, 573);
            this._labelGetLightWaveformHead.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelGetLightWaveformHead.Name = "_labelGetLightWaveformHead";
            this._labelGetLightWaveformHead.Size = new System.Drawing.Size(70, 25);
            this._labelGetLightWaveformHead.TabIndex = 15;
            this._labelGetLightWaveformHead.Text = "HEAD";
            // 
            // _textBoxTrendRequestDataCount
            // 
            this._textBoxTrendRequestDataCount.Location = new System.Drawing.Point(710, 173);
            this._textBoxTrendRequestDataCount.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxTrendRequestDataCount.MaxLength = 5;
            this._textBoxTrendRequestDataCount.Name = "_textBoxTrendRequestDataCount";
            this._textBoxTrendRequestDataCount.Size = new System.Drawing.Size(70, 31);
            this._textBoxTrendRequestDataCount.TabIndex = 6;
            this._textBoxTrendRequestDataCount.Text = "1";
            // 
            // _labelGetTrendDataReadCount
            // 
            this._labelGetTrendDataReadCount.AutoSize = true;
            this._labelGetTrendDataReadCount.Location = new System.Drawing.Point(506, 179);
            this._labelGetTrendDataReadCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelGetTrendDataReadCount.Name = "_labelGetTrendDataReadCount";
            this._labelGetTrendDataReadCount.Size = new System.Drawing.Size(186, 25);
            this._labelGetTrendDataReadCount.TabIndex = 5;
            this._labelGetTrendDataReadCount.Text = "requestDataCount";
            // 
            // _textBoxTrendIndex
            // 
            this._textBoxTrendIndex.Location = new System.Drawing.Point(338, 173);
            this._textBoxTrendIndex.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxTrendIndex.MaxLength = 11;
            this._textBoxTrendIndex.Name = "_textBoxTrendIndex";
            this._textBoxTrendIndex.Size = new System.Drawing.Size(152, 31);
            this._textBoxTrendIndex.TabIndex = 4;
            this._textBoxTrendIndex.Text = "0";
            // 
            // _labelGetTrendDataIndex
            // 
            this._labelGetTrendDataIndex.AutoSize = true;
            this._labelGetTrendDataIndex.Location = new System.Drawing.Point(268, 179);
            this._labelGetTrendDataIndex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelGetTrendDataIndex.Name = "_labelGetTrendDataIndex";
            this._labelGetTrendDataIndex.Size = new System.Drawing.Size(64, 25);
            this._labelGetTrendDataIndex.TabIndex = 3;
            this._labelGetTrendDataIndex.Text = "index";
            // 
            // _buttonGetTrendData
            // 
            this._buttonGetTrendData.Location = new System.Drawing.Point(24, 169);
            this._buttonGetTrendData.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetTrendData.Name = "_buttonGetTrendData";
            this._buttonGetTrendData.Size = new System.Drawing.Size(232, 48);
            this._buttonGetTrendData.TabIndex = 2;
            this._buttonGetTrendData.Text = "GetTrendData";
            this._buttonGetTrendData.UseVisualStyleBackColor = true;
            this._buttonGetTrendData.Click += new System.EventHandler(this._buttonGetTrendData_Click);
            // 
            // _buttonGetTrendIndex
            // 
            this._buttonGetTrendIndex.Location = new System.Drawing.Point(24, 108);
            this._buttonGetTrendIndex.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetTrendIndex.Name = "_buttonGetTrendIndex";
            this._buttonGetTrendIndex.Size = new System.Drawing.Size(232, 48);
            this._buttonGetTrendIndex.TabIndex = 1;
            this._buttonGetTrendIndex.Text = "GetTrendIndex";
            this._buttonGetTrendIndex.UseVisualStyleBackColor = true;
            this._buttonGetTrendIndex.Click += new System.EventHandler(this._buttonGetTrendIndex_Click);
            // 
            // _buttonGetTerminalStatus
            // 
            this._buttonGetTerminalStatus.Location = new System.Drawing.Point(22, 475);
            this._buttonGetTerminalStatus.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetTerminalStatus.Name = "_buttonGetTerminalStatus";
            this._buttonGetTerminalStatus.Size = new System.Drawing.Size(232, 48);
            this._buttonGetTerminalStatus.TabIndex = 13;
            this._buttonGetTerminalStatus.Text = "GetTerminalStatus";
            this._buttonGetTerminalStatus.UseVisualStyleBackColor = true;
            this._buttonGetTerminalStatus.Click += new System.EventHandler(this._buttonGetTerminalStatus_Click);
            // 
            // _buttonGetMeasurementData
            // 
            this._buttonGetMeasurementData.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetMeasurementData.Location = new System.Drawing.Point(24, 38);
            this._buttonGetMeasurementData.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMeasurementData.Name = "_buttonGetMeasurementData";
            this._buttonGetMeasurementData.Size = new System.Drawing.Size(270, 48);
            this._buttonGetMeasurementData.TabIndex = 0;
            this._buttonGetMeasurementData.Text = "GetMeasurementData";
            this._buttonGetMeasurementData.UseVisualStyleBackColor = false;
            this._buttonGetMeasurementData.Click += new System.EventHandler(this._buttonGetMeasurementData_Click);
            // 
            // _groupBoxLockPanel
            // 
            this._groupBoxLockPanel.Controls.Add(this._comboBoxLockPanelOnOff);
            this._groupBoxLockPanel.Controls.Add(this._labelLockPanelOnOff);
            this._groupBoxLockPanel.Controls.Add(this._buttonLockPanel);
            this._groupBoxLockPanel.Location = new System.Drawing.Point(14, 896);
            this._groupBoxLockPanel.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxLockPanel.Name = "_groupBoxLockPanel";
            this._groupBoxLockPanel.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxLockPanel.Size = new System.Drawing.Size(818, 112);
            this._groupBoxLockPanel.TabIndex = 2;
            this._groupBoxLockPanel.TabStop = false;
            this._groupBoxLockPanel.Text = "Lock panel";
            // 
            // _comboBoxLockPanelOnOff
            // 
            this._comboBoxLockPanelOnOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxLockPanelOnOff.FormattingEnabled = true;
            this._comboBoxLockPanelOnOff.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxLockPanelOnOff.Location = new System.Drawing.Point(340, 42);
            this._comboBoxLockPanelOnOff.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxLockPanelOnOff.Name = "_comboBoxLockPanelOnOff";
            this._comboBoxLockPanelOnOff.Size = new System.Drawing.Size(116, 33);
            this._comboBoxLockPanelOnOff.TabIndex = 2;
            // 
            // _labelLockPanelOnOff
            // 
            this._labelLockPanelOnOff.AutoSize = true;
            this._labelLockPanelOnOff.Location = new System.Drawing.Point(264, 48);
            this._labelLockPanelOnOff.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelLockPanelOnOff.Name = "_labelLockPanelOnOff";
            this._labelLockPanelOnOff.Size = new System.Drawing.Size(64, 25);
            this._labelLockPanelOnOff.TabIndex = 1;
            this._labelLockPanelOnOff.Text = "onOff";
            // 
            // _buttonLockPanel
            // 
            this._buttonLockPanel.BackColor = System.Drawing.Color.Transparent;
            this._buttonLockPanel.Location = new System.Drawing.Point(24, 38);
            this._buttonLockPanel.Margin = new System.Windows.Forms.Padding(6);
            this._buttonLockPanel.Name = "_buttonLockPanel";
            this._buttonLockPanel.Size = new System.Drawing.Size(228, 48);
            this._buttonLockPanel.TabIndex = 0;
            this._buttonLockPanel.Text = "LockPanel";
            this._buttonLockPanel.UseVisualStyleBackColor = false;
            this._buttonLockPanel.Click += new System.EventHandler(this._buttonLockPanel_Click);
            // 
            // _groupBoxProgramNo
            // 
            this._groupBoxProgramNo.Controls.Add(this._comboBoxSwitchProgramProgramNo);
            this._groupBoxProgramNo.Controls.Add(this._labelSwitchProgramProgramNo);
            this._groupBoxProgramNo.Controls.Add(this._buttonSwitchProgram);
            this._groupBoxProgramNo.Controls.Add(this._buttonGetProgramNo);
            this._groupBoxProgramNo.Location = new System.Drawing.Point(14, 723);
            this._groupBoxProgramNo.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxProgramNo.Name = "_groupBoxProgramNo";
            this._groupBoxProgramNo.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxProgramNo.Size = new System.Drawing.Size(818, 160);
            this._groupBoxProgramNo.TabIndex = 1;
            this._groupBoxProgramNo.TabStop = false;
            this._groupBoxProgramNo.Text = "Program";
            // 
            // _comboBoxSwitchProgramProgramNo
            // 
            this._comboBoxSwitchProgramProgramNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSwitchProgramProgramNo.FormattingEnabled = true;
            this._comboBoxSwitchProgramProgramNo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this._comboBoxSwitchProgramProgramNo.Location = new System.Drawing.Point(394, 33);
            this._comboBoxSwitchProgramProgramNo.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSwitchProgramProgramNo.Name = "_comboBoxSwitchProgramProgramNo";
            this._comboBoxSwitchProgramProgramNo.Size = new System.Drawing.Size(94, 33);
            this._comboBoxSwitchProgramProgramNo.TabIndex = 2;
            // 
            // _labelSwitchProgramProgramNo
            // 
            this._labelSwitchProgramProgramNo.AutoSize = true;
            this._labelSwitchProgramProgramNo.Location = new System.Drawing.Point(268, 44);
            this._labelSwitchProgramProgramNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSwitchProgramProgramNo.Name = "_labelSwitchProgramProgramNo";
            this._labelSwitchProgramProgramNo.Size = new System.Drawing.Size(118, 25);
            this._labelSwitchProgramProgramNo.TabIndex = 1;
            this._labelSwitchProgramProgramNo.Text = "programNo";
            // 
            // _groupBoxTiming
            // 
            this._groupBoxTiming.Controls.Add(this._checkBoxTimingOutGroup2);
            this._groupBoxTiming.Controls.Add(this._checkBoxTimingOutGroup1);
            this._groupBoxTiming.Controls.Add(this._comboBoxTimingOutGroupValue);
            this._groupBoxTiming.Controls.Add(this._labelTimingGroupOutValue);
            this._groupBoxTiming.Controls.Add(this._comboBoxTimingMultiOutValue);
            this._groupBoxTiming.Controls.Add(this._comboBoxTimingSingleOnOff);
            this._groupBoxTiming.Controls.Add(this._labelTimingMultiOutValue);
            this._groupBoxTiming.Controls.Add(this._labelTimingSingleOnOff);
            this._groupBoxTiming.Controls.Add(this._groupBoxTimingOutMulti);
            this._groupBoxTiming.Controls.Add(this._comboBoxTimingSingleOutNo);
            this._groupBoxTiming.Controls.Add(this._buttonTimingSingle);
            this._groupBoxTiming.Controls.Add(this._buttonTimingGroup);
            this._groupBoxTiming.Controls.Add(this._buttonTimingMulti);
            this._groupBoxTiming.Location = new System.Drawing.Point(844, 354);
            this._groupBoxTiming.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxTiming.Name = "_groupBoxTiming";
            this._groupBoxTiming.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxTiming.Size = new System.Drawing.Size(800, 327);
            this._groupBoxTiming.TabIndex = 4;
            this._groupBoxTiming.TabStop = false;
            this._groupBoxTiming.Text = "Timing";
            // 
            // _checkBoxTimingOutGroup2
            // 
            this._checkBoxTimingOutGroup2.AutoSize = true;
            this._checkBoxTimingOutGroup2.Location = new System.Drawing.Point(384, 250);
            this._checkBoxTimingOutGroup2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOutGroup2.Name = "_checkBoxTimingOutGroup2";
            this._checkBoxTimingOutGroup2.Size = new System.Drawing.Size(111, 29);
            this._checkBoxTimingOutGroup2.TabIndex = 10;
            this._checkBoxTimingOutGroup2.Text = "group2";
            this._checkBoxTimingOutGroup2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOutGroup1
            // 
            this._checkBoxTimingOutGroup1.AutoSize = true;
            this._checkBoxTimingOutGroup1.Checked = true;
            this._checkBoxTimingOutGroup1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxTimingOutGroup1.Location = new System.Drawing.Point(256, 252);
            this._checkBoxTimingOutGroup1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOutGroup1.Name = "_checkBoxTimingOutGroup1";
            this._checkBoxTimingOutGroup1.Size = new System.Drawing.Size(111, 29);
            this._checkBoxTimingOutGroup1.TabIndex = 9;
            this._checkBoxTimingOutGroup1.Text = "group1";
            this._checkBoxTimingOutGroup1.UseVisualStyleBackColor = true;
            // 
            // _comboBoxTimingOutGroupValue
            // 
            this._comboBoxTimingOutGroupValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTimingOutGroupValue.FormattingEnabled = true;
            this._comboBoxTimingOutGroupValue.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxTimingOutGroupValue.Location = new System.Drawing.Point(664, 248);
            this._comboBoxTimingOutGroupValue.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxTimingOutGroupValue.Name = "_comboBoxTimingOutGroupValue";
            this._comboBoxTimingOutGroupValue.Size = new System.Drawing.Size(116, 33);
            this._comboBoxTimingOutGroupValue.TabIndex = 12;
            // 
            // _labelTimingGroupOutValue
            // 
            this._labelTimingGroupOutValue.AutoSize = true;
            this._labelTimingGroupOutValue.Location = new System.Drawing.Point(588, 254);
            this._labelTimingGroupOutValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTimingGroupOutValue.Name = "_labelTimingGroupOutValue";
            this._labelTimingGroupOutValue.Size = new System.Drawing.Size(64, 25);
            this._labelTimingGroupOutValue.TabIndex = 11;
            this._labelTimingGroupOutValue.Text = "onOff";
            // 
            // _comboBoxTimingMultiOutValue
            // 
            this._comboBoxTimingMultiOutValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTimingMultiOutValue.FormattingEnabled = true;
            this._comboBoxTimingMultiOutValue.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxTimingMultiOutValue.Location = new System.Drawing.Point(664, 100);
            this._comboBoxTimingMultiOutValue.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxTimingMultiOutValue.Name = "_comboBoxTimingMultiOutValue";
            this._comboBoxTimingMultiOutValue.Size = new System.Drawing.Size(116, 33);
            this._comboBoxTimingMultiOutValue.TabIndex = 7;
            // 
            // _comboBoxTimingSingleOnOff
            // 
            this._comboBoxTimingSingleOnOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTimingSingleOnOff.FormattingEnabled = true;
            this._comboBoxTimingSingleOnOff.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxTimingSingleOnOff.Location = new System.Drawing.Point(664, 40);
            this._comboBoxTimingSingleOnOff.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxTimingSingleOnOff.Name = "_comboBoxTimingSingleOnOff";
            this._comboBoxTimingSingleOnOff.Size = new System.Drawing.Size(116, 33);
            this._comboBoxTimingSingleOnOff.TabIndex = 3;
            // 
            // _labelTimingMultiOutValue
            // 
            this._labelTimingMultiOutValue.AutoSize = true;
            this._labelTimingMultiOutValue.Location = new System.Drawing.Point(588, 106);
            this._labelTimingMultiOutValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTimingMultiOutValue.Name = "_labelTimingMultiOutValue";
            this._labelTimingMultiOutValue.Size = new System.Drawing.Size(64, 25);
            this._labelTimingMultiOutValue.TabIndex = 6;
            this._labelTimingMultiOutValue.Text = "onOff";
            // 
            // _labelTimingSingleOnOff
            // 
            this._labelTimingSingleOnOff.AutoSize = true;
            this._labelTimingSingleOnOff.Location = new System.Drawing.Point(588, 46);
            this._labelTimingSingleOnOff.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTimingSingleOnOff.Name = "_labelTimingSingleOnOff";
            this._labelTimingSingleOnOff.Size = new System.Drawing.Size(64, 25);
            this._labelTimingSingleOnOff.TabIndex = 2;
            this._labelTimingSingleOnOff.Text = "onOff";
            // 
            // _groupBoxTimingOutMulti
            // 
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut8);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut7);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut6);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut5);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut4);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut3);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut2);
            this._groupBoxTimingOutMulti.Controls.Add(this._checkBoxTimingOut1);
            this._groupBoxTimingOutMulti.Location = new System.Drawing.Point(256, 96);
            this._groupBoxTimingOutMulti.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxTimingOutMulti.Name = "_groupBoxTimingOutMulti";
            this._groupBoxTimingOutMulti.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxTimingOutMulti.Size = new System.Drawing.Size(298, 142);
            this._groupBoxTimingOutMulti.TabIndex = 5;
            this._groupBoxTimingOutMulti.TabStop = false;
            this._groupBoxTimingOutMulti.Text = "OUT";
            // 
            // _checkBoxTimingOut8
            // 
            this._checkBoxTimingOut8.AutoSize = true;
            this._checkBoxTimingOut8.Location = new System.Drawing.Point(222, 85);
            this._checkBoxTimingOut8.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut8.Name = "_checkBoxTimingOut8";
            this._checkBoxTimingOut8.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut8.TabIndex = 7;
            this._checkBoxTimingOut8.Text = "8";
            this._checkBoxTimingOut8.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut7
            // 
            this._checkBoxTimingOut7.AutoSize = true;
            this._checkBoxTimingOut7.Location = new System.Drawing.Point(150, 85);
            this._checkBoxTimingOut7.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut7.Name = "_checkBoxTimingOut7";
            this._checkBoxTimingOut7.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut7.TabIndex = 6;
            this._checkBoxTimingOut7.Text = "7";
            this._checkBoxTimingOut7.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut6
            // 
            this._checkBoxTimingOut6.AutoSize = true;
            this._checkBoxTimingOut6.Location = new System.Drawing.Point(78, 85);
            this._checkBoxTimingOut6.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut6.Name = "_checkBoxTimingOut6";
            this._checkBoxTimingOut6.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut6.TabIndex = 5;
            this._checkBoxTimingOut6.Text = "6";
            this._checkBoxTimingOut6.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut5
            // 
            this._checkBoxTimingOut5.AutoSize = true;
            this._checkBoxTimingOut5.Location = new System.Drawing.Point(12, 85);
            this._checkBoxTimingOut5.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut5.Name = "_checkBoxTimingOut5";
            this._checkBoxTimingOut5.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut5.TabIndex = 4;
            this._checkBoxTimingOut5.Text = "5";
            this._checkBoxTimingOut5.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut4
            // 
            this._checkBoxTimingOut4.AutoSize = true;
            this._checkBoxTimingOut4.Location = new System.Drawing.Point(222, 38);
            this._checkBoxTimingOut4.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut4.Name = "_checkBoxTimingOut4";
            this._checkBoxTimingOut4.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut4.TabIndex = 3;
            this._checkBoxTimingOut4.Text = "4";
            this._checkBoxTimingOut4.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut3
            // 
            this._checkBoxTimingOut3.AutoSize = true;
            this._checkBoxTimingOut3.Location = new System.Drawing.Point(150, 38);
            this._checkBoxTimingOut3.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut3.Name = "_checkBoxTimingOut3";
            this._checkBoxTimingOut3.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut3.TabIndex = 2;
            this._checkBoxTimingOut3.Text = "3";
            this._checkBoxTimingOut3.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut2
            // 
            this._checkBoxTimingOut2.AutoSize = true;
            this._checkBoxTimingOut2.Location = new System.Drawing.Point(78, 38);
            this._checkBoxTimingOut2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut2.Name = "_checkBoxTimingOut2";
            this._checkBoxTimingOut2.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut2.TabIndex = 1;
            this._checkBoxTimingOut2.Text = "2";
            this._checkBoxTimingOut2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxTimingOut1
            // 
            this._checkBoxTimingOut1.AutoSize = true;
            this._checkBoxTimingOut1.Checked = true;
            this._checkBoxTimingOut1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxTimingOut1.Location = new System.Drawing.Point(12, 38);
            this._checkBoxTimingOut1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxTimingOut1.Name = "_checkBoxTimingOut1";
            this._checkBoxTimingOut1.Size = new System.Drawing.Size(56, 29);
            this._checkBoxTimingOut1.TabIndex = 0;
            this._checkBoxTimingOut1.Text = "1";
            this._checkBoxTimingOut1.UseVisualStyleBackColor = true;
            // 
            // _comboBoxTimingSingleOutNo
            // 
            this._comboBoxTimingSingleOutNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTimingSingleOutNo.FormattingEnabled = true;
            this._comboBoxTimingSingleOutNo.Items.AddRange(new object[] {
            "OUT 1",
            "OUT 2",
            "OUT 3",
            "OUT 4",
            "OUT 5",
            "OUT 6",
            "OUT 7",
            "OUT 8"});
            this._comboBoxTimingSingleOutNo.Location = new System.Drawing.Point(258, 40);
            this._comboBoxTimingSingleOutNo.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxTimingSingleOutNo.Name = "_comboBoxTimingSingleOutNo";
            this._comboBoxTimingSingleOutNo.Size = new System.Drawing.Size(144, 33);
            this._comboBoxTimingSingleOutNo.TabIndex = 1;
            // 
            // _groupBoxReset
            // 
            this._groupBoxReset.Controls.Add(this._checkBoxResetOutGroup2);
            this._groupBoxReset.Controls.Add(this._checkBoxResetOutGroup1);
            this._groupBoxReset.Controls.Add(this._groupBoxResetOutMulti);
            this._groupBoxReset.Controls.Add(this._comboBoxResetOutNo);
            this._groupBoxReset.Controls.Add(this._buttonResetSingle);
            this._groupBoxReset.Controls.Add(this._buttonResetGroup);
            this._groupBoxReset.Controls.Add(this._buttonResetMulti);
            this._groupBoxReset.Location = new System.Drawing.Point(844, 696);
            this._groupBoxReset.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxReset.Name = "_groupBoxReset";
            this._groupBoxReset.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxReset.Size = new System.Drawing.Size(800, 312);
            this._groupBoxReset.TabIndex = 5;
            this._groupBoxReset.TabStop = false;
            this._groupBoxReset.Text = "Reset";
            // 
            // _checkBoxResetOutGroup2
            // 
            this._checkBoxResetOutGroup2.AutoSize = true;
            this._checkBoxResetOutGroup2.Location = new System.Drawing.Point(384, 254);
            this._checkBoxResetOutGroup2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOutGroup2.Name = "_checkBoxResetOutGroup2";
            this._checkBoxResetOutGroup2.Size = new System.Drawing.Size(111, 29);
            this._checkBoxResetOutGroup2.TabIndex = 6;
            this._checkBoxResetOutGroup2.Text = "group2";
            this._checkBoxResetOutGroup2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOutGroup1
            // 
            this._checkBoxResetOutGroup1.AutoSize = true;
            this._checkBoxResetOutGroup1.Checked = true;
            this._checkBoxResetOutGroup1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxResetOutGroup1.Location = new System.Drawing.Point(256, 254);
            this._checkBoxResetOutGroup1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOutGroup1.Name = "_checkBoxResetOutGroup1";
            this._checkBoxResetOutGroup1.Size = new System.Drawing.Size(111, 29);
            this._checkBoxResetOutGroup1.TabIndex = 5;
            this._checkBoxResetOutGroup1.Text = "group1";
            this._checkBoxResetOutGroup1.UseVisualStyleBackColor = true;
            // 
            // _groupBoxResetOutMulti
            // 
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut8);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut7);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut6);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut5);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut4);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut3);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut2);
            this._groupBoxResetOutMulti.Controls.Add(this._checkBoxResetOut1);
            this._groupBoxResetOutMulti.Location = new System.Drawing.Point(256, 98);
            this._groupBoxResetOutMulti.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxResetOutMulti.Name = "_groupBoxResetOutMulti";
            this._groupBoxResetOutMulti.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxResetOutMulti.Size = new System.Drawing.Size(298, 142);
            this._groupBoxResetOutMulti.TabIndex = 3;
            this._groupBoxResetOutMulti.TabStop = false;
            this._groupBoxResetOutMulti.Text = "OUT";
            // 
            // _checkBoxResetOut8
            // 
            this._checkBoxResetOut8.AutoSize = true;
            this._checkBoxResetOut8.Location = new System.Drawing.Point(222, 85);
            this._checkBoxResetOut8.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut8.Name = "_checkBoxResetOut8";
            this._checkBoxResetOut8.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut8.TabIndex = 7;
            this._checkBoxResetOut8.Text = "8";
            this._checkBoxResetOut8.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut7
            // 
            this._checkBoxResetOut7.AutoSize = true;
            this._checkBoxResetOut7.Location = new System.Drawing.Point(150, 85);
            this._checkBoxResetOut7.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut7.Name = "_checkBoxResetOut7";
            this._checkBoxResetOut7.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut7.TabIndex = 6;
            this._checkBoxResetOut7.Text = "7";
            this._checkBoxResetOut7.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut6
            // 
            this._checkBoxResetOut6.AutoSize = true;
            this._checkBoxResetOut6.Location = new System.Drawing.Point(78, 85);
            this._checkBoxResetOut6.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut6.Name = "_checkBoxResetOut6";
            this._checkBoxResetOut6.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut6.TabIndex = 5;
            this._checkBoxResetOut6.Text = "6";
            this._checkBoxResetOut6.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut5
            // 
            this._checkBoxResetOut5.AutoSize = true;
            this._checkBoxResetOut5.Location = new System.Drawing.Point(12, 85);
            this._checkBoxResetOut5.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut5.Name = "_checkBoxResetOut5";
            this._checkBoxResetOut5.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut5.TabIndex = 4;
            this._checkBoxResetOut5.Text = "5";
            this._checkBoxResetOut5.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut4
            // 
            this._checkBoxResetOut4.AutoSize = true;
            this._checkBoxResetOut4.Location = new System.Drawing.Point(222, 38);
            this._checkBoxResetOut4.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut4.Name = "_checkBoxResetOut4";
            this._checkBoxResetOut4.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut4.TabIndex = 3;
            this._checkBoxResetOut4.Text = "4";
            this._checkBoxResetOut4.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut3
            // 
            this._checkBoxResetOut3.AutoSize = true;
            this._checkBoxResetOut3.Location = new System.Drawing.Point(150, 38);
            this._checkBoxResetOut3.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut3.Name = "_checkBoxResetOut3";
            this._checkBoxResetOut3.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut3.TabIndex = 2;
            this._checkBoxResetOut3.Text = "3";
            this._checkBoxResetOut3.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut2
            // 
            this._checkBoxResetOut2.AutoSize = true;
            this._checkBoxResetOut2.Location = new System.Drawing.Point(78, 38);
            this._checkBoxResetOut2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut2.Name = "_checkBoxResetOut2";
            this._checkBoxResetOut2.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut2.TabIndex = 1;
            this._checkBoxResetOut2.Text = "2";
            this._checkBoxResetOut2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxResetOut1
            // 
            this._checkBoxResetOut1.AutoSize = true;
            this._checkBoxResetOut1.Checked = true;
            this._checkBoxResetOut1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxResetOut1.Location = new System.Drawing.Point(12, 38);
            this._checkBoxResetOut1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxResetOut1.Name = "_checkBoxResetOut1";
            this._checkBoxResetOut1.Size = new System.Drawing.Size(56, 29);
            this._checkBoxResetOut1.TabIndex = 0;
            this._checkBoxResetOut1.Text = "1";
            this._checkBoxResetOut1.UseVisualStyleBackColor = true;
            // 
            // _comboBoxResetOutNo
            // 
            this._comboBoxResetOutNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxResetOutNo.FormattingEnabled = true;
            this._comboBoxResetOutNo.Items.AddRange(new object[] {
            "OUT 1",
            "OUT 2",
            "OUT 3",
            "OUT 4",
            "OUT 5",
            "OUT 6",
            "OUT 7",
            "OUT 8"});
            this._comboBoxResetOutNo.Location = new System.Drawing.Point(258, 40);
            this._comboBoxResetOutNo.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxResetOutNo.Name = "_comboBoxResetOutNo";
            this._comboBoxResetOutNo.Size = new System.Drawing.Size(144, 33);
            this._comboBoxResetOutNo.TabIndex = 1;
            // 
            // _groupBoxZero
            // 
            this._groupBoxZero.Controls.Add(this._checkBoxAutoZeroGroup2);
            this._groupBoxZero.Controls.Add(this._checkBoxAutoZeroGroup1);
            this._groupBoxZero.Controls.Add(this._comboBoxAutoZeroGroupOnOff);
            this._groupBoxZero.Controls.Add(this._labelAutoZeroGroupOnOff);
            this._groupBoxZero.Controls.Add(this._comboBoxAutoZeroMultiOnOff);
            this._groupBoxZero.Controls.Add(this._comboBoxAutoZeroSingleOnOff);
            this._groupBoxZero.Controls.Add(this._labelAutoZeroMultiOnOff);
            this._groupBoxZero.Controls.Add(this._labelAutoZeroSingleOnOff);
            this._groupBoxZero.Controls.Add(this._groupBoxAutoZeroMultiOut);
            this._groupBoxZero.Controls.Add(this._comboBoxAutoZeroSingleOutNo);
            this._groupBoxZero.Controls.Add(this._buttonAutoZeroSingle);
            this._groupBoxZero.Controls.Add(this._buttonAutoZeroGroup);
            this._groupBoxZero.Controls.Add(this._buttonAutoZeroMulti);
            this._groupBoxZero.Location = new System.Drawing.Point(844, 12);
            this._groupBoxZero.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxZero.Name = "_groupBoxZero";
            this._groupBoxZero.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxZero.Size = new System.Drawing.Size(800, 325);
            this._groupBoxZero.TabIndex = 3;
            this._groupBoxZero.TabStop = false;
            this._groupBoxZero.Text = "Zero";
            this._groupBoxZero.Enter += new System.EventHandler(this._groupBoxZero_Enter);
            // 
            // _checkBoxAutoZeroGroup2
            // 
            this._checkBoxAutoZeroGroup2.AutoSize = true;
            this._checkBoxAutoZeroGroup2.Location = new System.Drawing.Point(384, 254);
            this._checkBoxAutoZeroGroup2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroGroup2.Name = "_checkBoxAutoZeroGroup2";
            this._checkBoxAutoZeroGroup2.Size = new System.Drawing.Size(111, 29);
            this._checkBoxAutoZeroGroup2.TabIndex = 10;
            this._checkBoxAutoZeroGroup2.Text = "group2";
            this._checkBoxAutoZeroGroup2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroGroup1
            // 
            this._checkBoxAutoZeroGroup1.AutoSize = true;
            this._checkBoxAutoZeroGroup1.Checked = true;
            this._checkBoxAutoZeroGroup1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxAutoZeroGroup1.Location = new System.Drawing.Point(256, 254);
            this._checkBoxAutoZeroGroup1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroGroup1.Name = "_checkBoxAutoZeroGroup1";
            this._checkBoxAutoZeroGroup1.Size = new System.Drawing.Size(111, 29);
            this._checkBoxAutoZeroGroup1.TabIndex = 9;
            this._checkBoxAutoZeroGroup1.Text = "group1";
            this._checkBoxAutoZeroGroup1.UseVisualStyleBackColor = true;
            // 
            // _comboBoxAutoZeroGroupOnOff
            // 
            this._comboBoxAutoZeroGroupOnOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxAutoZeroGroupOnOff.FormattingEnabled = true;
            this._comboBoxAutoZeroGroupOnOff.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxAutoZeroGroupOnOff.Location = new System.Drawing.Point(664, 250);
            this._comboBoxAutoZeroGroupOnOff.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxAutoZeroGroupOnOff.Name = "_comboBoxAutoZeroGroupOnOff";
            this._comboBoxAutoZeroGroupOnOff.Size = new System.Drawing.Size(116, 33);
            this._comboBoxAutoZeroGroupOnOff.TabIndex = 12;
            // 
            // _labelAutoZeroGroupOnOff
            // 
            this._labelAutoZeroGroupOnOff.AutoSize = true;
            this._labelAutoZeroGroupOnOff.Location = new System.Drawing.Point(588, 256);
            this._labelAutoZeroGroupOnOff.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelAutoZeroGroupOnOff.Name = "_labelAutoZeroGroupOnOff";
            this._labelAutoZeroGroupOnOff.Size = new System.Drawing.Size(64, 25);
            this._labelAutoZeroGroupOnOff.TabIndex = 11;
            this._labelAutoZeroGroupOnOff.Text = "onOff";
            // 
            // _comboBoxAutoZeroMultiOnOff
            // 
            this._comboBoxAutoZeroMultiOnOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxAutoZeroMultiOnOff.FormattingEnabled = true;
            this._comboBoxAutoZeroMultiOnOff.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxAutoZeroMultiOnOff.Location = new System.Drawing.Point(664, 102);
            this._comboBoxAutoZeroMultiOnOff.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxAutoZeroMultiOnOff.Name = "_comboBoxAutoZeroMultiOnOff";
            this._comboBoxAutoZeroMultiOnOff.Size = new System.Drawing.Size(116, 33);
            this._comboBoxAutoZeroMultiOnOff.TabIndex = 7;
            // 
            // _comboBoxAutoZeroSingleOnOff
            // 
            this._comboBoxAutoZeroSingleOnOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxAutoZeroSingleOnOff.FormattingEnabled = true;
            this._comboBoxAutoZeroSingleOnOff.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxAutoZeroSingleOnOff.Location = new System.Drawing.Point(664, 42);
            this._comboBoxAutoZeroSingleOnOff.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxAutoZeroSingleOnOff.Name = "_comboBoxAutoZeroSingleOnOff";
            this._comboBoxAutoZeroSingleOnOff.Size = new System.Drawing.Size(116, 33);
            this._comboBoxAutoZeroSingleOnOff.TabIndex = 3;
            // 
            // _labelAutoZeroMultiOnOff
            // 
            this._labelAutoZeroMultiOnOff.AutoSize = true;
            this._labelAutoZeroMultiOnOff.Location = new System.Drawing.Point(588, 108);
            this._labelAutoZeroMultiOnOff.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelAutoZeroMultiOnOff.Name = "_labelAutoZeroMultiOnOff";
            this._labelAutoZeroMultiOnOff.Size = new System.Drawing.Size(64, 25);
            this._labelAutoZeroMultiOnOff.TabIndex = 6;
            this._labelAutoZeroMultiOnOff.Text = "onOff";
            // 
            // _labelAutoZeroSingleOnOff
            // 
            this._labelAutoZeroSingleOnOff.AutoSize = true;
            this._labelAutoZeroSingleOnOff.Location = new System.Drawing.Point(588, 48);
            this._labelAutoZeroSingleOnOff.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelAutoZeroSingleOnOff.Name = "_labelAutoZeroSingleOnOff";
            this._labelAutoZeroSingleOnOff.Size = new System.Drawing.Size(64, 25);
            this._labelAutoZeroSingleOnOff.TabIndex = 2;
            this._labelAutoZeroSingleOnOff.Text = "onOff";
            // 
            // _groupBoxAutoZeroMultiOut
            // 
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo8);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo7);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo6);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo5);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo4);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo3);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo2);
            this._groupBoxAutoZeroMultiOut.Controls.Add(this._checkBoxAutoZeroMultiOutNo1);
            this._groupBoxAutoZeroMultiOut.Location = new System.Drawing.Point(256, 98);
            this._groupBoxAutoZeroMultiOut.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxAutoZeroMultiOut.Name = "_groupBoxAutoZeroMultiOut";
            this._groupBoxAutoZeroMultiOut.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxAutoZeroMultiOut.Size = new System.Drawing.Size(298, 142);
            this._groupBoxAutoZeroMultiOut.TabIndex = 5;
            this._groupBoxAutoZeroMultiOut.TabStop = false;
            this._groupBoxAutoZeroMultiOut.Text = "OUT";
            // 
            // _checkBoxAutoZeroMultiOutNo8
            // 
            this._checkBoxAutoZeroMultiOutNo8.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo8.Location = new System.Drawing.Point(222, 85);
            this._checkBoxAutoZeroMultiOutNo8.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo8.Name = "_checkBoxAutoZeroMultiOutNo8";
            this._checkBoxAutoZeroMultiOutNo8.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo8.TabIndex = 7;
            this._checkBoxAutoZeroMultiOutNo8.Text = "8";
            this._checkBoxAutoZeroMultiOutNo8.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo7
            // 
            this._checkBoxAutoZeroMultiOutNo7.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo7.Location = new System.Drawing.Point(150, 85);
            this._checkBoxAutoZeroMultiOutNo7.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo7.Name = "_checkBoxAutoZeroMultiOutNo7";
            this._checkBoxAutoZeroMultiOutNo7.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo7.TabIndex = 6;
            this._checkBoxAutoZeroMultiOutNo7.Text = "7";
            this._checkBoxAutoZeroMultiOutNo7.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo6
            // 
            this._checkBoxAutoZeroMultiOutNo6.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo6.Location = new System.Drawing.Point(78, 85);
            this._checkBoxAutoZeroMultiOutNo6.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo6.Name = "_checkBoxAutoZeroMultiOutNo6";
            this._checkBoxAutoZeroMultiOutNo6.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo6.TabIndex = 5;
            this._checkBoxAutoZeroMultiOutNo6.Text = "6";
            this._checkBoxAutoZeroMultiOutNo6.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo5
            // 
            this._checkBoxAutoZeroMultiOutNo5.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo5.Location = new System.Drawing.Point(12, 85);
            this._checkBoxAutoZeroMultiOutNo5.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo5.Name = "_checkBoxAutoZeroMultiOutNo5";
            this._checkBoxAutoZeroMultiOutNo5.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo5.TabIndex = 4;
            this._checkBoxAutoZeroMultiOutNo5.Text = "5";
            this._checkBoxAutoZeroMultiOutNo5.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo4
            // 
            this._checkBoxAutoZeroMultiOutNo4.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo4.Location = new System.Drawing.Point(222, 38);
            this._checkBoxAutoZeroMultiOutNo4.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo4.Name = "_checkBoxAutoZeroMultiOutNo4";
            this._checkBoxAutoZeroMultiOutNo4.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo4.TabIndex = 3;
            this._checkBoxAutoZeroMultiOutNo4.Text = "4";
            this._checkBoxAutoZeroMultiOutNo4.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo3
            // 
            this._checkBoxAutoZeroMultiOutNo3.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo3.Location = new System.Drawing.Point(150, 38);
            this._checkBoxAutoZeroMultiOutNo3.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo3.Name = "_checkBoxAutoZeroMultiOutNo3";
            this._checkBoxAutoZeroMultiOutNo3.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo3.TabIndex = 2;
            this._checkBoxAutoZeroMultiOutNo3.Text = "3";
            this._checkBoxAutoZeroMultiOutNo3.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo2
            // 
            this._checkBoxAutoZeroMultiOutNo2.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo2.Location = new System.Drawing.Point(78, 38);
            this._checkBoxAutoZeroMultiOutNo2.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo2.Name = "_checkBoxAutoZeroMultiOutNo2";
            this._checkBoxAutoZeroMultiOutNo2.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo2.TabIndex = 1;
            this._checkBoxAutoZeroMultiOutNo2.Text = "2";
            this._checkBoxAutoZeroMultiOutNo2.UseVisualStyleBackColor = true;
            // 
            // _checkBoxAutoZeroMultiOutNo1
            // 
            this._checkBoxAutoZeroMultiOutNo1.AutoSize = true;
            this._checkBoxAutoZeroMultiOutNo1.Checked = true;
            this._checkBoxAutoZeroMultiOutNo1.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkBoxAutoZeroMultiOutNo1.Location = new System.Drawing.Point(12, 38);
            this._checkBoxAutoZeroMultiOutNo1.Margin = new System.Windows.Forms.Padding(6);
            this._checkBoxAutoZeroMultiOutNo1.Name = "_checkBoxAutoZeroMultiOutNo1";
            this._checkBoxAutoZeroMultiOutNo1.Size = new System.Drawing.Size(56, 29);
            this._checkBoxAutoZeroMultiOutNo1.TabIndex = 0;
            this._checkBoxAutoZeroMultiOutNo1.Text = "1";
            this._checkBoxAutoZeroMultiOutNo1.UseVisualStyleBackColor = true;
            // 
            // _comboBoxAutoZeroSingleOutNo
            // 
            this._comboBoxAutoZeroSingleOutNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxAutoZeroSingleOutNo.FormattingEnabled = true;
            this._comboBoxAutoZeroSingleOutNo.Items.AddRange(new object[] {
            "OUT 1",
            "OUT 2",
            "OUT 3",
            "OUT 4",
            "OUT 5",
            "OUT 6",
            "OUT 7",
            "OUT 8"});
            this._comboBoxAutoZeroSingleOutNo.Location = new System.Drawing.Point(258, 42);
            this._comboBoxAutoZeroSingleOutNo.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxAutoZeroSingleOutNo.Name = "_comboBoxAutoZeroSingleOutNo";
            this._comboBoxAutoZeroSingleOutNo.Size = new System.Drawing.Size(144, 33);
            this._comboBoxAutoZeroSingleOutNo.TabIndex = 1;
            // 
            // _tabPageSettingRW
            // 
            this._tabPageSettingRW.Controls.Add(this._groupBoxProgramSettings);
            this._tabPageSettingRW.Location = new System.Drawing.Point(8, 39);
            this._tabPageSettingRW.Margin = new System.Windows.Forms.Padding(6);
            this._tabPageSettingRW.Name = "_tabPageSettingRW";
            this._tabPageSettingRW.Padding = new System.Windows.Forms.Padding(6);
            this._tabPageSettingRW.Size = new System.Drawing.Size(1952, 1124);
            this._tabPageSettingRW.TabIndex = 1;
            this._tabPageSettingRW.Text = "Set/Get settings";
            this._tabPageSettingRW.UseVisualStyleBackColor = true;
            // 
            // _groupBoxProgramSettings
            // 
            this._groupBoxProgramSettings.Controls.Add(this._labelAnalogOutAllocation);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetAnalogOutAllocation);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetAnalogOutAllocation);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetStorageTiming);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetStorageTiming);
            this._groupBoxProgramSettings.Controls.Add(this._labelStorageTiming);
            this._groupBoxProgramSettings.Controls.Add(this._groupBoxHeadSettings);
            this._groupBoxProgramSettings.Controls.Add(this._groupBoxOutSettings);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetSamplingCycle);
            this._groupBoxProgramSettings.Controls.Add(this._labelSamplingCycle);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetSamplingCycle);
            this._groupBoxProgramSettings.Controls.Add(this._labelAmbientLightFilter);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetAmbientLightFilter);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetAmbientLightFilter);
            this._groupBoxProgramSettings.Controls.Add(this._labelMutualInterferencePrevention);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetMutualInterferencePrevention);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetMutualInterferencePrevention);
            this._groupBoxProgramSettings.Controls.Add(this._comboBoxSettingProgramNo);
            this._groupBoxProgramSettings.Controls.Add(this._labelSettingProgramNo);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetStorageNumber);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetStorageNumber);
            this._groupBoxProgramSettings.Controls.Add(this._labelStorageNumber);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetStorageTarget);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetStorageTarget);
            this._groupBoxProgramSettings.Controls.Add(this._labelStorageTarget);
            this._groupBoxProgramSettings.Controls.Add(this._buttonGetJudgmentOutput);
            this._groupBoxProgramSettings.Controls.Add(this._buttonSetJudgmentOutput);
            this._groupBoxProgramSettings.Controls.Add(this._labelJudgmentOutput);
            this._groupBoxProgramSettings.Location = new System.Drawing.Point(12, 12);
            this._groupBoxProgramSettings.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxProgramSettings.Name = "_groupBoxProgramSettings";
            this._groupBoxProgramSettings.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxProgramSettings.Size = new System.Drawing.Size(1928, 1092);
            this._groupBoxProgramSettings.TabIndex = 0;
            this._groupBoxProgramSettings.TabStop = false;
            this._groupBoxProgramSettings.Text = "Program settings";
            // 
            // _labelAnalogOutAllocation
            // 
            this._labelAnalogOutAllocation.AutoSize = true;
            this._labelAnalogOutAllocation.Location = new System.Drawing.Point(1304, 173);
            this._labelAnalogOutAllocation.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelAnalogOutAllocation.Name = "_labelAnalogOutAllocation";
            this._labelAnalogOutAllocation.Size = new System.Drawing.Size(207, 25);
            this._labelAnalogOutAllocation.TabIndex = 23;
            this._labelAnalogOutAllocation.Text = "AnalogOutAllocation";
            // 
            // _buttonGetAnalogOutAllocation
            // 
            this._buttonGetAnalogOutAllocation.Location = new System.Drawing.Point(1726, 162);
            this._buttonGetAnalogOutAllocation.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetAnalogOutAllocation.Name = "_buttonGetAnalogOutAllocation";
            this._buttonGetAnalogOutAllocation.Size = new System.Drawing.Size(100, 48);
            this._buttonGetAnalogOutAllocation.TabIndex = 25;
            this._buttonGetAnalogOutAllocation.Text = "Get";
            this._buttonGetAnalogOutAllocation.UseVisualStyleBackColor = true;
            this._buttonGetAnalogOutAllocation.Click += new System.EventHandler(this._buttonGetAnalogOutAllocation_Click);
            // 
            // _buttonSetAnalogOutAllocation
            // 
            this._buttonSetAnalogOutAllocation.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetAnalogOutAllocation.Location = new System.Drawing.Point(1614, 162);
            this._buttonSetAnalogOutAllocation.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetAnalogOutAllocation.Name = "_buttonSetAnalogOutAllocation";
            this._buttonSetAnalogOutAllocation.Size = new System.Drawing.Size(100, 48);
            this._buttonSetAnalogOutAllocation.TabIndex = 24;
            this._buttonSetAnalogOutAllocation.Text = "Set";
            this._buttonSetAnalogOutAllocation.UseVisualStyleBackColor = false;
            this._buttonSetAnalogOutAllocation.Click += new System.EventHandler(this._buttonSetAnalogOutAllocation_Click);
            // 
            // _buttonGetStorageTiming
            // 
            this._buttonGetStorageTiming.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetStorageTiming.Location = new System.Drawing.Point(1170, 223);
            this._buttonGetStorageTiming.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetStorageTiming.Name = "_buttonGetStorageTiming";
            this._buttonGetStorageTiming.Size = new System.Drawing.Size(100, 48);
            this._buttonGetStorageTiming.TabIndex = 19;
            this._buttonGetStorageTiming.Text = "Get";
            this._buttonGetStorageTiming.UseVisualStyleBackColor = false;
            this._buttonGetStorageTiming.Click += new System.EventHandler(this._buttonGetStorageTiming_Click);
            // 
            // _buttonSetStorageTiming
            // 
            this._buttonSetStorageTiming.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetStorageTiming.Location = new System.Drawing.Point(1058, 223);
            this._buttonSetStorageTiming.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetStorageTiming.Name = "_buttonSetStorageTiming";
            this._buttonSetStorageTiming.Size = new System.Drawing.Size(100, 48);
            this._buttonSetStorageTiming.TabIndex = 18;
            this._buttonSetStorageTiming.Text = "Set";
            this._buttonSetStorageTiming.UseVisualStyleBackColor = false;
            this._buttonSetStorageTiming.Click += new System.EventHandler(this._buttonSetStorageTiming_Click);
            // 
            // _labelStorageTiming
            // 
            this._labelStorageTiming.AutoSize = true;
            this._labelStorageTiming.Location = new System.Drawing.Point(716, 233);
            this._labelStorageTiming.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelStorageTiming.Name = "_labelStorageTiming";
            this._labelStorageTiming.Size = new System.Drawing.Size(151, 25);
            this._labelStorageTiming.TabIndex = 17;
            this._labelStorageTiming.Text = "StorageTiming";
            // 
            // _groupBoxHeadSettings
            // 
            this._groupBoxHeadSettings.Controls.Add(this._labelHighSensitivity);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetHighSensitivity);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetHighSensitivity);
            this._groupBoxHeadSettings.Controls.Add(this._comboBoxSettingHead);
            this._groupBoxHeadSettings.Controls.Add(this._labelLightIntensityIntegration);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetLightIntensityIntegration);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetLightIntensityIntegration);
            this._groupBoxHeadSettings.Controls.Add(this._labelMeasurementPeaks);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetMeasurementPeaks);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetMeasurementPeaks);
            this._groupBoxHeadSettings.Controls.Add(this._labelCheckingNumberOfPeaks);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetCheckingNumberOfPeaks);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetCheckingNumberOfPeaks);
            this._groupBoxHeadSettings.Controls.Add(this._labelMultiLightIntensityControl);
            this._groupBoxHeadSettings.Controls.Add(this._labelSettingHead);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetMultiLightIntensityControl);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetMultiLightIntensityControl);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetPeakShapeFilter);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetPeakShapeFilter);
            this._groupBoxHeadSettings.Controls.Add(this._labelPeakShapeFilter);
            this._groupBoxHeadSettings.Controls.Add(this._labelQuadProcessing);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetLightIntensityControl);
            this._groupBoxHeadSettings.Controls.Add(this._labelMedianFilter);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetQuadProcessing);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetLightIntensityControl);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetMedianFilter);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetQuadProcessing);
            this._groupBoxHeadSettings.Controls.Add(this._labelLightIntensityControl);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetMedianFilter);
            this._groupBoxHeadSettings.Controls.Add(this._labelRefractiveIndexCorrection);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetMask);
            this._groupBoxHeadSettings.Controls.Add(this._labelThreshold);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetRefractiveIndexCorrection);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetMask);
            this._groupBoxHeadSettings.Controls.Add(this._buttonSetThreshold);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetRefractiveIndexCorrection);
            this._groupBoxHeadSettings.Controls.Add(this._labelMask);
            this._groupBoxHeadSettings.Controls.Add(this._buttonGetThreshold);
            this._groupBoxHeadSettings.Location = new System.Drawing.Point(8, 275);
            this._groupBoxHeadSettings.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxHeadSettings.Name = "_groupBoxHeadSettings";
            this._groupBoxHeadSettings.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxHeadSettings.Size = new System.Drawing.Size(662, 804);
            this._groupBoxHeadSettings.TabIndex = 26;
            this._groupBoxHeadSettings.TabStop = false;
            this._groupBoxHeadSettings.Text = "HEAD settings";
            // 
            // _labelHighSensitivity
            // 
            this._labelHighSensitivity.AutoSize = true;
            this._labelHighSensitivity.Location = new System.Drawing.Point(42, 750);
            this._labelHighSensitivity.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelHighSensitivity.Name = "_labelHighSensitivity";
            this._labelHighSensitivity.Size = new System.Drawing.Size(154, 25);
            this._labelHighSensitivity.TabIndex = 35;
            this._labelHighSensitivity.Text = "HighSensitivity";
            // 
            // _buttonSetHighSensitivity
            // 
            this._buttonSetHighSensitivity.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetHighSensitivity.Location = new System.Drawing.Point(384, 740);
            this._buttonSetHighSensitivity.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetHighSensitivity.Name = "_buttonSetHighSensitivity";
            this._buttonSetHighSensitivity.Size = new System.Drawing.Size(100, 48);
            this._buttonSetHighSensitivity.TabIndex = 36;
            this._buttonSetHighSensitivity.Text = "Set";
            this._buttonSetHighSensitivity.UseVisualStyleBackColor = false;
            this._buttonSetHighSensitivity.Click += new System.EventHandler(this._buttonSetHighSensitivity_Click);
            // 
            // _buttonGetHighSensitivity
            // 
            this._buttonGetHighSensitivity.Location = new System.Drawing.Point(496, 740);
            this._buttonGetHighSensitivity.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetHighSensitivity.Name = "_buttonGetHighSensitivity";
            this._buttonGetHighSensitivity.Size = new System.Drawing.Size(100, 48);
            this._buttonGetHighSensitivity.TabIndex = 37;
            this._buttonGetHighSensitivity.Text = "Get";
            this._buttonGetHighSensitivity.UseVisualStyleBackColor = true;
            this._buttonGetHighSensitivity.Click += new System.EventHandler(this._buttonGetHighSensitivity_Click);
            // 
            // _comboBoxSettingHead
            // 
            this._comboBoxSettingHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSettingHead.FormattingEnabled = true;
            this._comboBoxSettingHead.Items.AddRange(new object[] {
            "HEAD 01",
            "HEAD 02",
            "HEAD 03",
            "HEAD 04",
            "HEAD 05",
            "HEAD 06"});
            this._comboBoxSettingHead.Location = new System.Drawing.Point(188, 35);
            this._comboBoxSettingHead.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSettingHead.Name = "_comboBoxSettingHead";
            this._comboBoxSettingHead.Size = new System.Drawing.Size(196, 33);
            this._comboBoxSettingHead.TabIndex = 1;
            // 
            // _labelLightIntensityIntegration
            // 
            this._labelLightIntensityIntegration.AutoSize = true;
            this._labelLightIntensityIntegration.Location = new System.Drawing.Point(42, 396);
            this._labelLightIntensityIntegration.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelLightIntensityIntegration.Name = "_labelLightIntensityIntegration";
            this._labelLightIntensityIntegration.Size = new System.Drawing.Size(240, 25);
            this._labelLightIntensityIntegration.TabIndex = 17;
            this._labelLightIntensityIntegration.Text = "LightIntensityIntegration";
            // 
            // _buttonSetLightIntensityIntegration
            // 
            this._buttonSetLightIntensityIntegration.Location = new System.Drawing.Point(384, 385);
            this._buttonSetLightIntensityIntegration.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetLightIntensityIntegration.Name = "_buttonSetLightIntensityIntegration";
            this._buttonSetLightIntensityIntegration.Size = new System.Drawing.Size(100, 48);
            this._buttonSetLightIntensityIntegration.TabIndex = 18;
            this._buttonSetLightIntensityIntegration.Text = "Set";
            this._buttonSetLightIntensityIntegration.UseVisualStyleBackColor = true;
            this._buttonSetLightIntensityIntegration.Click += new System.EventHandler(this._buttonSetLightIntensityIntegration_Click);
            // 
            // _buttonGetLightIntensityIntegration
            // 
            this._buttonGetLightIntensityIntegration.Location = new System.Drawing.Point(496, 385);
            this._buttonGetLightIntensityIntegration.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetLightIntensityIntegration.Name = "_buttonGetLightIntensityIntegration";
            this._buttonGetLightIntensityIntegration.Size = new System.Drawing.Size(100, 48);
            this._buttonGetLightIntensityIntegration.TabIndex = 19;
            this._buttonGetLightIntensityIntegration.Text = "Get";
            this._buttonGetLightIntensityIntegration.UseVisualStyleBackColor = true;
            this._buttonGetLightIntensityIntegration.Click += new System.EventHandler(this._buttonGetLightIntensityIntegration_Click);
            // 
            // _labelMeasurementPeaks
            // 
            this._labelMeasurementPeaks.AutoSize = true;
            this._labelMeasurementPeaks.Location = new System.Drawing.Point(42, 454);
            this._labelMeasurementPeaks.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelMeasurementPeaks.Name = "_labelMeasurementPeaks";
            this._labelMeasurementPeaks.Size = new System.Drawing.Size(203, 25);
            this._labelMeasurementPeaks.TabIndex = 20;
            this._labelMeasurementPeaks.Text = "MeasurementPeaks";
            // 
            // _buttonSetMeasurementPeaks
            // 
            this._buttonSetMeasurementPeaks.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetMeasurementPeaks.Location = new System.Drawing.Point(384, 444);
            this._buttonSetMeasurementPeaks.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetMeasurementPeaks.Name = "_buttonSetMeasurementPeaks";
            this._buttonSetMeasurementPeaks.Size = new System.Drawing.Size(100, 48);
            this._buttonSetMeasurementPeaks.TabIndex = 21;
            this._buttonSetMeasurementPeaks.Text = "Set";
            this._buttonSetMeasurementPeaks.UseVisualStyleBackColor = false;
            this._buttonSetMeasurementPeaks.Click += new System.EventHandler(this._buttonSetMeasurementPeaks_Click);
            // 
            // _buttonGetMeasurementPeaks
            // 
            this._buttonGetMeasurementPeaks.Location = new System.Drawing.Point(496, 444);
            this._buttonGetMeasurementPeaks.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMeasurementPeaks.Name = "_buttonGetMeasurementPeaks";
            this._buttonGetMeasurementPeaks.Size = new System.Drawing.Size(100, 48);
            this._buttonGetMeasurementPeaks.TabIndex = 22;
            this._buttonGetMeasurementPeaks.Text = "Get";
            this._buttonGetMeasurementPeaks.UseVisualStyleBackColor = true;
            this._buttonGetMeasurementPeaks.Click += new System.EventHandler(this._buttonGetMeasurementPeaks_Click);
            // 
            // _labelCheckingNumberOfPeaks
            // 
            this._labelCheckingNumberOfPeaks.AutoSize = true;
            this._labelCheckingNumberOfPeaks.Location = new System.Drawing.Point(42, 515);
            this._labelCheckingNumberOfPeaks.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelCheckingNumberOfPeaks.Name = "_labelCheckingNumberOfPeaks";
            this._labelCheckingNumberOfPeaks.Size = new System.Drawing.Size(259, 25);
            this._labelCheckingNumberOfPeaks.TabIndex = 23;
            this._labelCheckingNumberOfPeaks.Text = "CheckingNumberOfPeaks";
            // 
            // _buttonSetCheckingNumberOfPeaks
            // 
            this._buttonSetCheckingNumberOfPeaks.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetCheckingNumberOfPeaks.Location = new System.Drawing.Point(384, 504);
            this._buttonSetCheckingNumberOfPeaks.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetCheckingNumberOfPeaks.Name = "_buttonSetCheckingNumberOfPeaks";
            this._buttonSetCheckingNumberOfPeaks.Size = new System.Drawing.Size(100, 48);
            this._buttonSetCheckingNumberOfPeaks.TabIndex = 24;
            this._buttonSetCheckingNumberOfPeaks.Text = "Set";
            this._buttonSetCheckingNumberOfPeaks.UseVisualStyleBackColor = false;
            this._buttonSetCheckingNumberOfPeaks.Click += new System.EventHandler(this._buttonSetCheckingNumberOfPeaks_Click);
            // 
            // _buttonGetCheckingNumberOfPeaks
            // 
            this._buttonGetCheckingNumberOfPeaks.Location = new System.Drawing.Point(496, 504);
            this._buttonGetCheckingNumberOfPeaks.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetCheckingNumberOfPeaks.Name = "_buttonGetCheckingNumberOfPeaks";
            this._buttonGetCheckingNumberOfPeaks.Size = new System.Drawing.Size(100, 48);
            this._buttonGetCheckingNumberOfPeaks.TabIndex = 25;
            this._buttonGetCheckingNumberOfPeaks.Text = "Get";
            this._buttonGetCheckingNumberOfPeaks.UseVisualStyleBackColor = true;
            this._buttonGetCheckingNumberOfPeaks.Click += new System.EventHandler(this._buttonGetCheckingNumberOfPeaks_Click);
            // 
            // _labelMultiLightIntensityControl
            // 
            this._labelMultiLightIntensityControl.AutoSize = true;
            this._labelMultiLightIntensityControl.Location = new System.Drawing.Point(42, 575);
            this._labelMultiLightIntensityControl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelMultiLightIntensityControl.Name = "_labelMultiLightIntensityControl";
            this._labelMultiLightIntensityControl.Size = new System.Drawing.Size(254, 25);
            this._labelMultiLightIntensityControl.TabIndex = 26;
            this._labelMultiLightIntensityControl.Text = "MultiLightIntensityControl";
            // 
            // _labelSettingHead
            // 
            this._labelSettingHead.AutoSize = true;
            this._labelSettingHead.Location = new System.Drawing.Point(10, 42);
            this._labelSettingHead.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSettingHead.Name = "_labelSettingHead";
            this._labelSettingHead.Size = new System.Drawing.Size(138, 25);
            this._labelSettingHead.TabIndex = 0;
            this._labelSettingHead.Text = "Target HEAD";
            // 
            // _buttonSetMultiLightIntensityControl
            // 
            this._buttonSetMultiLightIntensityControl.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetMultiLightIntensityControl.Location = new System.Drawing.Point(384, 565);
            this._buttonSetMultiLightIntensityControl.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetMultiLightIntensityControl.Name = "_buttonSetMultiLightIntensityControl";
            this._buttonSetMultiLightIntensityControl.Size = new System.Drawing.Size(100, 48);
            this._buttonSetMultiLightIntensityControl.TabIndex = 27;
            this._buttonSetMultiLightIntensityControl.Text = "Set";
            this._buttonSetMultiLightIntensityControl.UseVisualStyleBackColor = false;
            this._buttonSetMultiLightIntensityControl.Click += new System.EventHandler(this._buttonSetMultiLightIntensityControl_Click);
            // 
            // _buttonGetMultiLightIntensityControl
            // 
            this._buttonGetMultiLightIntensityControl.Location = new System.Drawing.Point(496, 565);
            this._buttonGetMultiLightIntensityControl.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMultiLightIntensityControl.Name = "_buttonGetMultiLightIntensityControl";
            this._buttonGetMultiLightIntensityControl.Size = new System.Drawing.Size(100, 48);
            this._buttonGetMultiLightIntensityControl.TabIndex = 28;
            this._buttonGetMultiLightIntensityControl.Text = "Get";
            this._buttonGetMultiLightIntensityControl.UseVisualStyleBackColor = true;
            this._buttonGetMultiLightIntensityControl.Click += new System.EventHandler(this._buttonGetMultiLightIntensityControl_Click);
            // 
            // _buttonGetPeakShapeFilter
            // 
            this._buttonGetPeakShapeFilter.Location = new System.Drawing.Point(496, 325);
            this._buttonGetPeakShapeFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetPeakShapeFilter.Name = "_buttonGetPeakShapeFilter";
            this._buttonGetPeakShapeFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonGetPeakShapeFilter.TabIndex = 16;
            this._buttonGetPeakShapeFilter.Text = "Get";
            this._buttonGetPeakShapeFilter.UseVisualStyleBackColor = true;
            this._buttonGetPeakShapeFilter.Click += new System.EventHandler(this._buttonGetPeakShapeFilter_Click);
            // 
            // _buttonSetPeakShapeFilter
            // 
            this._buttonSetPeakShapeFilter.Location = new System.Drawing.Point(384, 325);
            this._buttonSetPeakShapeFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetPeakShapeFilter.Name = "_buttonSetPeakShapeFilter";
            this._buttonSetPeakShapeFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonSetPeakShapeFilter.TabIndex = 15;
            this._buttonSetPeakShapeFilter.Text = "Set";
            this._buttonSetPeakShapeFilter.UseVisualStyleBackColor = true;
            this._buttonSetPeakShapeFilter.Click += new System.EventHandler(this._buttonSetPeakShapeFilter_Click);
            // 
            // _labelPeakShapeFilter
            // 
            this._labelPeakShapeFilter.AutoSize = true;
            this._labelPeakShapeFilter.Location = new System.Drawing.Point(40, 335);
            this._labelPeakShapeFilter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelPeakShapeFilter.Name = "_labelPeakShapeFilter";
            this._labelPeakShapeFilter.Size = new System.Drawing.Size(171, 25);
            this._labelPeakShapeFilter.TabIndex = 14;
            this._labelPeakShapeFilter.Text = "PeakShapeFilter";
            // 
            // _labelQuadProcessing
            // 
            this._labelQuadProcessing.AutoSize = true;
            this._labelQuadProcessing.Location = new System.Drawing.Point(42, 694);
            this._labelQuadProcessing.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelQuadProcessing.Name = "_labelQuadProcessing";
            this._labelQuadProcessing.Size = new System.Drawing.Size(171, 25);
            this._labelQuadProcessing.TabIndex = 32;
            this._labelQuadProcessing.Text = "QuadProcessing";
            // 
            // _buttonGetLightIntensityControl
            // 
            this._buttonGetLightIntensityControl.Location = new System.Drawing.Point(496, 265);
            this._buttonGetLightIntensityControl.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetLightIntensityControl.Name = "_buttonGetLightIntensityControl";
            this._buttonGetLightIntensityControl.Size = new System.Drawing.Size(100, 48);
            this._buttonGetLightIntensityControl.TabIndex = 13;
            this._buttonGetLightIntensityControl.Text = "Get";
            this._buttonGetLightIntensityControl.UseVisualStyleBackColor = true;
            this._buttonGetLightIntensityControl.Click += new System.EventHandler(this._buttonGetLightIntensityControl_Click);
            // 
            // _labelMedianFilter
            // 
            this._labelMedianFilter.AutoSize = true;
            this._labelMedianFilter.Location = new System.Drawing.Point(40, 98);
            this._labelMedianFilter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelMedianFilter.Name = "_labelMedianFilter";
            this._labelMedianFilter.Size = new System.Drawing.Size(131, 25);
            this._labelMedianFilter.TabIndex = 2;
            this._labelMedianFilter.Text = "MedianFilter";
            // 
            // _buttonSetQuadProcessing
            // 
            this._buttonSetQuadProcessing.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetQuadProcessing.Location = new System.Drawing.Point(384, 683);
            this._buttonSetQuadProcessing.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetQuadProcessing.Name = "_buttonSetQuadProcessing";
            this._buttonSetQuadProcessing.Size = new System.Drawing.Size(100, 48);
            this._buttonSetQuadProcessing.TabIndex = 33;
            this._buttonSetQuadProcessing.Text = "Set";
            this._buttonSetQuadProcessing.UseVisualStyleBackColor = false;
            this._buttonSetQuadProcessing.Click += new System.EventHandler(this._buttonSetQuadProcessing_Click);
            // 
            // _buttonSetLightIntensityControl
            // 
            this._buttonSetLightIntensityControl.Location = new System.Drawing.Point(384, 265);
            this._buttonSetLightIntensityControl.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetLightIntensityControl.Name = "_buttonSetLightIntensityControl";
            this._buttonSetLightIntensityControl.Size = new System.Drawing.Size(100, 48);
            this._buttonSetLightIntensityControl.TabIndex = 12;
            this._buttonSetLightIntensityControl.Text = "Set";
            this._buttonSetLightIntensityControl.UseVisualStyleBackColor = true;
            this._buttonSetLightIntensityControl.Click += new System.EventHandler(this._buttonSetLightIntensityControl_Click);
            // 
            // _buttonSetMedianFilter
            // 
            this._buttonSetMedianFilter.Location = new System.Drawing.Point(384, 88);
            this._buttonSetMedianFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetMedianFilter.Name = "_buttonSetMedianFilter";
            this._buttonSetMedianFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonSetMedianFilter.TabIndex = 3;
            this._buttonSetMedianFilter.Text = "Set";
            this._buttonSetMedianFilter.UseVisualStyleBackColor = true;
            this._buttonSetMedianFilter.Click += new System.EventHandler(this._buttonSetMedianFilter_Click);
            // 
            // _buttonGetQuadProcessing
            // 
            this._buttonGetQuadProcessing.Location = new System.Drawing.Point(496, 683);
            this._buttonGetQuadProcessing.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetQuadProcessing.Name = "_buttonGetQuadProcessing";
            this._buttonGetQuadProcessing.Size = new System.Drawing.Size(100, 48);
            this._buttonGetQuadProcessing.TabIndex = 34;
            this._buttonGetQuadProcessing.Text = "Get";
            this._buttonGetQuadProcessing.UseVisualStyleBackColor = true;
            this._buttonGetQuadProcessing.Click += new System.EventHandler(this._buttonGetQuadProcessing_Click);
            // 
            // _labelLightIntensityControl
            // 
            this._labelLightIntensityControl.AutoSize = true;
            this._labelLightIntensityControl.Location = new System.Drawing.Point(40, 275);
            this._labelLightIntensityControl.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelLightIntensityControl.Name = "_labelLightIntensityControl";
            this._labelLightIntensityControl.Size = new System.Drawing.Size(208, 25);
            this._labelLightIntensityControl.TabIndex = 11;
            this._labelLightIntensityControl.Text = "LightIntensityControl";
            // 
            // _buttonGetMedianFilter
            // 
            this._buttonGetMedianFilter.Location = new System.Drawing.Point(496, 88);
            this._buttonGetMedianFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMedianFilter.Name = "_buttonGetMedianFilter";
            this._buttonGetMedianFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonGetMedianFilter.TabIndex = 4;
            this._buttonGetMedianFilter.Text = "Get";
            this._buttonGetMedianFilter.UseVisualStyleBackColor = true;
            this._buttonGetMedianFilter.Click += new System.EventHandler(this._buttonGetMedianFilter_Click);
            // 
            // _labelRefractiveIndexCorrection
            // 
            this._labelRefractiveIndexCorrection.AutoSize = true;
            this._labelRefractiveIndexCorrection.Location = new System.Drawing.Point(42, 635);
            this._labelRefractiveIndexCorrection.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelRefractiveIndexCorrection.Name = "_labelRefractiveIndexCorrection";
            this._labelRefractiveIndexCorrection.Size = new System.Drawing.Size(260, 25);
            this._labelRefractiveIndexCorrection.TabIndex = 29;
            this._labelRefractiveIndexCorrection.Text = "RefractiveIndexCorrection";
            // 
            // _buttonGetMask
            // 
            this._buttonGetMask.Location = new System.Drawing.Point(496, 208);
            this._buttonGetMask.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMask.Name = "_buttonGetMask";
            this._buttonGetMask.Size = new System.Drawing.Size(100, 48);
            this._buttonGetMask.TabIndex = 10;
            this._buttonGetMask.Text = "Get";
            this._buttonGetMask.UseVisualStyleBackColor = true;
            this._buttonGetMask.Click += new System.EventHandler(this._buttonGetMask_Click);
            // 
            // _labelThreshold
            // 
            this._labelThreshold.AutoSize = true;
            this._labelThreshold.Location = new System.Drawing.Point(40, 158);
            this._labelThreshold.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelThreshold.Name = "_labelThreshold";
            this._labelThreshold.Size = new System.Drawing.Size(108, 25);
            this._labelThreshold.TabIndex = 5;
            this._labelThreshold.Text = "Threshold";
            // 
            // _buttonSetRefractiveIndexCorrection
            // 
            this._buttonSetRefractiveIndexCorrection.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetRefractiveIndexCorrection.Location = new System.Drawing.Point(384, 625);
            this._buttonSetRefractiveIndexCorrection.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetRefractiveIndexCorrection.Name = "_buttonSetRefractiveIndexCorrection";
            this._buttonSetRefractiveIndexCorrection.Size = new System.Drawing.Size(100, 48);
            this._buttonSetRefractiveIndexCorrection.TabIndex = 30;
            this._buttonSetRefractiveIndexCorrection.Text = "Set";
            this._buttonSetRefractiveIndexCorrection.UseVisualStyleBackColor = false;
            this._buttonSetRefractiveIndexCorrection.Click += new System.EventHandler(this._buttonSetRefractiveIndexCorrection_Click);
            // 
            // _buttonSetMask
            // 
            this._buttonSetMask.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetMask.Location = new System.Drawing.Point(384, 208);
            this._buttonSetMask.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetMask.Name = "_buttonSetMask";
            this._buttonSetMask.Size = new System.Drawing.Size(100, 48);
            this._buttonSetMask.TabIndex = 9;
            this._buttonSetMask.Text = "Set";
            this._buttonSetMask.UseVisualStyleBackColor = false;
            this._buttonSetMask.Click += new System.EventHandler(this._buttonSetMask_Click);
            // 
            // _buttonSetThreshold
            // 
            this._buttonSetThreshold.Location = new System.Drawing.Point(384, 148);
            this._buttonSetThreshold.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetThreshold.Name = "_buttonSetThreshold";
            this._buttonSetThreshold.Size = new System.Drawing.Size(100, 48);
            this._buttonSetThreshold.TabIndex = 6;
            this._buttonSetThreshold.Text = "Set";
            this._buttonSetThreshold.UseVisualStyleBackColor = true;
            this._buttonSetThreshold.Click += new System.EventHandler(this._buttonSetThreshold_Click);
            // 
            // _buttonGetRefractiveIndexCorrection
            // 
            this._buttonGetRefractiveIndexCorrection.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetRefractiveIndexCorrection.Location = new System.Drawing.Point(496, 625);
            this._buttonGetRefractiveIndexCorrection.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetRefractiveIndexCorrection.Name = "_buttonGetRefractiveIndexCorrection";
            this._buttonGetRefractiveIndexCorrection.Size = new System.Drawing.Size(100, 48);
            this._buttonGetRefractiveIndexCorrection.TabIndex = 31;
            this._buttonGetRefractiveIndexCorrection.Text = "Get";
            this._buttonGetRefractiveIndexCorrection.UseVisualStyleBackColor = false;
            this._buttonGetRefractiveIndexCorrection.Click += new System.EventHandler(this._buttonGetRefractiveIndexCorrection_Click);
            // 
            // _labelMask
            // 
            this._labelMask.AutoSize = true;
            this._labelMask.Location = new System.Drawing.Point(40, 219);
            this._labelMask.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelMask.Name = "_labelMask";
            this._labelMask.Size = new System.Drawing.Size(64, 25);
            this._labelMask.TabIndex = 8;
            this._labelMask.Text = "Mask";
            // 
            // _buttonGetThreshold
            // 
            this._buttonGetThreshold.Location = new System.Drawing.Point(496, 148);
            this._buttonGetThreshold.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetThreshold.Name = "_buttonGetThreshold";
            this._buttonGetThreshold.Size = new System.Drawing.Size(100, 48);
            this._buttonGetThreshold.TabIndex = 7;
            this._buttonGetThreshold.Text = "Get";
            this._buttonGetThreshold.UseVisualStyleBackColor = true;
            this._buttonGetThreshold.Click += new System.EventHandler(this._buttonGetThreshold_Click);
            // 
            // _groupBoxOutSettings
            // 
            this._groupBoxOutSettings.Controls.Add(this._labelAnalogOutputScaling);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetAnalogOutputScaling);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetAnalogOutputScaling);
            this._groupBoxOutSettings.Controls.Add(this._labelMeasurementMethod);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetMeasurementMethod);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetMeasurementMethod);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetInvalidDataProcessing);
            this._groupBoxOutSettings.Controls.Add(this._comboBoxSettingOut);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetInvalidDataProcessing);
            this._groupBoxOutSettings.Controls.Add(this._labelInvalidDataProcessing);
            this._groupBoxOutSettings.Controls.Add(this._labelOut);
            this._groupBoxOutSettings.Controls.Add(this._labelDisplayUnit);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetDisplayUnit);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetDisplayUnit);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetHold);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetHold);
            this._groupBoxOutSettings.Controls.Add(this._labelHold);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetFilter);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetFilter);
            this._groupBoxOutSettings.Controls.Add(this._labelFilter);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetTolerance);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetTolerance);
            this._groupBoxOutSettings.Controls.Add(this._labelTolerance);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetOffset);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetTerminalAllocation);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetOffset);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetTerminalAllocation);
            this._groupBoxOutSettings.Controls.Add(this._labelTerminalAllocation);
            this._groupBoxOutSettings.Controls.Add(this._labelOffset);
            this._groupBoxOutSettings.Controls.Add(this._buttonGetScaling);
            this._groupBoxOutSettings.Controls.Add(this._buttonSetScaling);
            this._groupBoxOutSettings.Controls.Add(this._labelScaling);
            this._groupBoxOutSettings.Location = new System.Drawing.Point(682, 275);
            this._groupBoxOutSettings.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxOutSettings.Name = "_groupBoxOutSettings";
            this._groupBoxOutSettings.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxOutSettings.Size = new System.Drawing.Size(1232, 804);
            this._groupBoxOutSettings.TabIndex = 27;
            this._groupBoxOutSettings.TabStop = false;
            this._groupBoxOutSettings.Text = "OUT settings";
            // 
            // _labelAnalogOutputScaling
            // 
            this._labelAnalogOutputScaling.AutoSize = true;
            this._labelAnalogOutputScaling.Location = new System.Drawing.Point(36, 635);
            this._labelAnalogOutputScaling.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelAnalogOutputScaling.Name = "_labelAnalogOutputScaling";
            this._labelAnalogOutputScaling.Size = new System.Drawing.Size(214, 25);
            this._labelAnalogOutputScaling.TabIndex = 29;
            this._labelAnalogOutputScaling.Text = "AnalogOutputScaling";
            // 
            // _buttonGetAnalogOutputScaling
            // 
            this._buttonGetAnalogOutputScaling.Location = new System.Drawing.Point(488, 625);
            this._buttonGetAnalogOutputScaling.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetAnalogOutputScaling.Name = "_buttonGetAnalogOutputScaling";
            this._buttonGetAnalogOutputScaling.Size = new System.Drawing.Size(100, 48);
            this._buttonGetAnalogOutputScaling.TabIndex = 31;
            this._buttonGetAnalogOutputScaling.Text = "Get";
            this._buttonGetAnalogOutputScaling.UseVisualStyleBackColor = true;
            this._buttonGetAnalogOutputScaling.Click += new System.EventHandler(this._buttonGetAnalogOutputScaling_Click);
            // 
            // _buttonSetAnalogOutputScaling
            // 
            this._buttonSetAnalogOutputScaling.Location = new System.Drawing.Point(376, 625);
            this._buttonSetAnalogOutputScaling.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetAnalogOutputScaling.Name = "_buttonSetAnalogOutputScaling";
            this._buttonSetAnalogOutputScaling.Size = new System.Drawing.Size(100, 48);
            this._buttonSetAnalogOutputScaling.TabIndex = 30;
            this._buttonSetAnalogOutputScaling.Text = "Set";
            this._buttonSetAnalogOutputScaling.UseVisualStyleBackColor = true;
            this._buttonSetAnalogOutputScaling.Click += new System.EventHandler(this._buttonSetAnalogOutputScaling_Click);
            // 
            // _labelMeasurementMethod
            // 
            this._labelMeasurementMethod.AutoSize = true;
            this._labelMeasurementMethod.Location = new System.Drawing.Point(36, 98);
            this._labelMeasurementMethod.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelMeasurementMethod.Name = "_labelMeasurementMethod";
            this._labelMeasurementMethod.Size = new System.Drawing.Size(215, 25);
            this._labelMeasurementMethod.TabIndex = 2;
            this._labelMeasurementMethod.Text = "MeasurementMethod";
            // 
            // _buttonSetMeasurementMethod
            // 
            this._buttonSetMeasurementMethod.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetMeasurementMethod.Location = new System.Drawing.Point(376, 88);
            this._buttonSetMeasurementMethod.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetMeasurementMethod.Name = "_buttonSetMeasurementMethod";
            this._buttonSetMeasurementMethod.Size = new System.Drawing.Size(100, 48);
            this._buttonSetMeasurementMethod.TabIndex = 3;
            this._buttonSetMeasurementMethod.Text = "Set";
            this._buttonSetMeasurementMethod.UseVisualStyleBackColor = false;
            this._buttonSetMeasurementMethod.Click += new System.EventHandler(this._buttonSetMeasurementMethod_Click);
            // 
            // _buttonGetMeasurementMethod
            // 
            this._buttonGetMeasurementMethod.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetMeasurementMethod.Location = new System.Drawing.Point(488, 88);
            this._buttonGetMeasurementMethod.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMeasurementMethod.Name = "_buttonGetMeasurementMethod";
            this._buttonGetMeasurementMethod.Size = new System.Drawing.Size(100, 48);
            this._buttonGetMeasurementMethod.TabIndex = 4;
            this._buttonGetMeasurementMethod.Text = "Get";
            this._buttonGetMeasurementMethod.UseVisualStyleBackColor = false;
            this._buttonGetMeasurementMethod.Click += new System.EventHandler(this._buttonGetMeasurementMethod_Click);
            // 
            // _buttonSetInvalidDataProcessing
            // 
            this._buttonSetInvalidDataProcessing.Location = new System.Drawing.Point(376, 444);
            this._buttonSetInvalidDataProcessing.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetInvalidDataProcessing.Name = "_buttonSetInvalidDataProcessing";
            this._buttonSetInvalidDataProcessing.Size = new System.Drawing.Size(100, 48);
            this._buttonSetInvalidDataProcessing.TabIndex = 21;
            this._buttonSetInvalidDataProcessing.Text = "Set";
            this._buttonSetInvalidDataProcessing.UseVisualStyleBackColor = true;
            this._buttonSetInvalidDataProcessing.Click += new System.EventHandler(this._buttonSetInvalidDataProcessing_Click);
            // 
            // _comboBoxSettingOut
            // 
            this._comboBoxSettingOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSettingOut.FormattingEnabled = true;
            this._comboBoxSettingOut.Items.AddRange(new object[] {
            "OUT 01",
            "OUT 02",
            "OUT 03",
            "OUT 04",
            "OUT 05",
            "OUT 06",
            "OUT 07",
            "OUT 08"});
            this._comboBoxSettingOut.Location = new System.Drawing.Point(176, 35);
            this._comboBoxSettingOut.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSettingOut.Name = "_comboBoxSettingOut";
            this._comboBoxSettingOut.Size = new System.Drawing.Size(196, 33);
            this._comboBoxSettingOut.TabIndex = 1;
            // 
            // _buttonGetInvalidDataProcessing
            // 
            this._buttonGetInvalidDataProcessing.Location = new System.Drawing.Point(488, 444);
            this._buttonGetInvalidDataProcessing.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetInvalidDataProcessing.Name = "_buttonGetInvalidDataProcessing";
            this._buttonGetInvalidDataProcessing.Size = new System.Drawing.Size(100, 48);
            this._buttonGetInvalidDataProcessing.TabIndex = 22;
            this._buttonGetInvalidDataProcessing.Text = "Get";
            this._buttonGetInvalidDataProcessing.UseVisualStyleBackColor = true;
            this._buttonGetInvalidDataProcessing.Click += new System.EventHandler(this._buttonGetInvalidDataProcessing_Click);
            // 
            // _labelInvalidDataProcessing
            // 
            this._labelInvalidDataProcessing.AutoSize = true;
            this._labelInvalidDataProcessing.Location = new System.Drawing.Point(36, 454);
            this._labelInvalidDataProcessing.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelInvalidDataProcessing.Name = "_labelInvalidDataProcessing";
            this._labelInvalidDataProcessing.Size = new System.Drawing.Size(226, 25);
            this._labelInvalidDataProcessing.TabIndex = 20;
            this._labelInvalidDataProcessing.Text = "InvalidDataProcessing";
            // 
            // _labelOut
            // 
            this._labelOut.AutoSize = true;
            this._labelOut.Location = new System.Drawing.Point(10, 42);
            this._labelOut.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelOut.Name = "_labelOut";
            this._labelOut.Size = new System.Drawing.Size(124, 25);
            this._labelOut.TabIndex = 0;
            this._labelOut.Text = "Target OUT";
            // 
            // _labelDisplayUnit
            // 
            this._labelDisplayUnit.AutoSize = true;
            this._labelDisplayUnit.Location = new System.Drawing.Point(36, 515);
            this._labelDisplayUnit.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelDisplayUnit.Name = "_labelDisplayUnit";
            this._labelDisplayUnit.Size = new System.Drawing.Size(121, 25);
            this._labelDisplayUnit.TabIndex = 23;
            this._labelDisplayUnit.Text = "DisplayUnit";
            // 
            // _buttonSetDisplayUnit
            // 
            this._buttonSetDisplayUnit.Location = new System.Drawing.Point(376, 504);
            this._buttonSetDisplayUnit.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetDisplayUnit.Name = "_buttonSetDisplayUnit";
            this._buttonSetDisplayUnit.Size = new System.Drawing.Size(100, 48);
            this._buttonSetDisplayUnit.TabIndex = 24;
            this._buttonSetDisplayUnit.Text = "Set";
            this._buttonSetDisplayUnit.UseVisualStyleBackColor = true;
            this._buttonSetDisplayUnit.Click += new System.EventHandler(this._buttonSetDisplayUnit_Click);
            // 
            // _buttonGetDisplayUnit
            // 
            this._buttonGetDisplayUnit.Location = new System.Drawing.Point(488, 504);
            this._buttonGetDisplayUnit.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetDisplayUnit.Name = "_buttonGetDisplayUnit";
            this._buttonGetDisplayUnit.Size = new System.Drawing.Size(100, 48);
            this._buttonGetDisplayUnit.TabIndex = 25;
            this._buttonGetDisplayUnit.Text = "Get";
            this._buttonGetDisplayUnit.UseVisualStyleBackColor = true;
            this._buttonGetDisplayUnit.Click += new System.EventHandler(this._buttonGetDisplayUnit_Click);
            // 
            // _buttonGetHold
            // 
            this._buttonGetHold.Location = new System.Drawing.Point(488, 385);
            this._buttonGetHold.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetHold.Name = "_buttonGetHold";
            this._buttonGetHold.Size = new System.Drawing.Size(100, 48);
            this._buttonGetHold.TabIndex = 19;
            this._buttonGetHold.Text = "Get";
            this._buttonGetHold.UseVisualStyleBackColor = true;
            this._buttonGetHold.Click += new System.EventHandler(this._buttonGetHold_Click);
            // 
            // _buttonSetHold
            // 
            this._buttonSetHold.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetHold.Location = new System.Drawing.Point(376, 385);
            this._buttonSetHold.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetHold.Name = "_buttonSetHold";
            this._buttonSetHold.Size = new System.Drawing.Size(100, 48);
            this._buttonSetHold.TabIndex = 18;
            this._buttonSetHold.Text = "Set";
            this._buttonSetHold.UseVisualStyleBackColor = false;
            this._buttonSetHold.Click += new System.EventHandler(this._buttonSetHold_Click);
            // 
            // _labelHold
            // 
            this._labelHold.AutoSize = true;
            this._labelHold.Location = new System.Drawing.Point(36, 396);
            this._labelHold.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelHold.Name = "_labelHold";
            this._labelHold.Size = new System.Drawing.Size(56, 25);
            this._labelHold.TabIndex = 17;
            this._labelHold.Text = "Hold";
            // 
            // _buttonGetFilter
            // 
            this._buttonGetFilter.Location = new System.Drawing.Point(488, 325);
            this._buttonGetFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetFilter.Name = "_buttonGetFilter";
            this._buttonGetFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonGetFilter.TabIndex = 16;
            this._buttonGetFilter.Text = "Get";
            this._buttonGetFilter.UseVisualStyleBackColor = true;
            this._buttonGetFilter.Click += new System.EventHandler(this._buttonGetFilter_Click);
            // 
            // _buttonSetFilter
            // 
            this._buttonSetFilter.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetFilter.Location = new System.Drawing.Point(376, 325);
            this._buttonSetFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetFilter.Name = "_buttonSetFilter";
            this._buttonSetFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonSetFilter.TabIndex = 15;
            this._buttonSetFilter.Text = "Set";
            this._buttonSetFilter.UseVisualStyleBackColor = false;
            this._buttonSetFilter.Click += new System.EventHandler(this._buttonSetFilter_Click);
            // 
            // _labelFilter
            // 
            this._labelFilter.AutoSize = true;
            this._labelFilter.Location = new System.Drawing.Point(36, 335);
            this._labelFilter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelFilter.Name = "_labelFilter";
            this._labelFilter.Size = new System.Drawing.Size(60, 25);
            this._labelFilter.TabIndex = 14;
            this._labelFilter.Text = "Filter";
            // 
            // _buttonGetTolerance
            // 
            this._buttonGetTolerance.Location = new System.Drawing.Point(488, 265);
            this._buttonGetTolerance.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetTolerance.Name = "_buttonGetTolerance";
            this._buttonGetTolerance.Size = new System.Drawing.Size(100, 48);
            this._buttonGetTolerance.TabIndex = 13;
            this._buttonGetTolerance.Text = "Get";
            this._buttonGetTolerance.UseVisualStyleBackColor = true;
            this._buttonGetTolerance.Click += new System.EventHandler(this._buttonGetTolerance_Click);
            // 
            // _buttonSetTolerance
            // 
            this._buttonSetTolerance.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetTolerance.Location = new System.Drawing.Point(376, 265);
            this._buttonSetTolerance.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetTolerance.Name = "_buttonSetTolerance";
            this._buttonSetTolerance.Size = new System.Drawing.Size(100, 48);
            this._buttonSetTolerance.TabIndex = 12;
            this._buttonSetTolerance.Text = "Set";
            this._buttonSetTolerance.UseVisualStyleBackColor = false;
            this._buttonSetTolerance.Click += new System.EventHandler(this._buttonSetTolerance_Click);
            // 
            // _labelTolerance
            // 
            this._labelTolerance.AutoSize = true;
            this._labelTolerance.Location = new System.Drawing.Point(36, 275);
            this._labelTolerance.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTolerance.Name = "_labelTolerance";
            this._labelTolerance.Size = new System.Drawing.Size(108, 25);
            this._labelTolerance.TabIndex = 11;
            this._labelTolerance.Text = "Tolerance";
            // 
            // _buttonGetOffset
            // 
            this._buttonGetOffset.Location = new System.Drawing.Point(488, 208);
            this._buttonGetOffset.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetOffset.Name = "_buttonGetOffset";
            this._buttonGetOffset.Size = new System.Drawing.Size(100, 48);
            this._buttonGetOffset.TabIndex = 10;
            this._buttonGetOffset.Text = "Get";
            this._buttonGetOffset.UseVisualStyleBackColor = true;
            this._buttonGetOffset.Click += new System.EventHandler(this._buttonGetOffset_Click);
            // 
            // _buttonGetTerminalAllocation
            // 
            this._buttonGetTerminalAllocation.Location = new System.Drawing.Point(488, 565);
            this._buttonGetTerminalAllocation.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetTerminalAllocation.Name = "_buttonGetTerminalAllocation";
            this._buttonGetTerminalAllocation.Size = new System.Drawing.Size(100, 48);
            this._buttonGetTerminalAllocation.TabIndex = 28;
            this._buttonGetTerminalAllocation.Text = "Get";
            this._buttonGetTerminalAllocation.UseVisualStyleBackColor = true;
            this._buttonGetTerminalAllocation.Click += new System.EventHandler(this._buttonGetTerminalAllocation_Click);
            // 
            // _buttonSetOffset
            // 
            this._buttonSetOffset.Location = new System.Drawing.Point(376, 208);
            this._buttonSetOffset.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetOffset.Name = "_buttonSetOffset";
            this._buttonSetOffset.Size = new System.Drawing.Size(100, 48);
            this._buttonSetOffset.TabIndex = 9;
            this._buttonSetOffset.Text = "Set";
            this._buttonSetOffset.UseVisualStyleBackColor = true;
            this._buttonSetOffset.Click += new System.EventHandler(this._buttonSetOffset_Click);
            // 
            // _buttonSetTerminalAllocation
            // 
            this._buttonSetTerminalAllocation.Location = new System.Drawing.Point(376, 565);
            this._buttonSetTerminalAllocation.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetTerminalAllocation.Name = "_buttonSetTerminalAllocation";
            this._buttonSetTerminalAllocation.Size = new System.Drawing.Size(100, 48);
            this._buttonSetTerminalAllocation.TabIndex = 27;
            this._buttonSetTerminalAllocation.Text = "Set";
            this._buttonSetTerminalAllocation.UseVisualStyleBackColor = true;
            this._buttonSetTerminalAllocation.Click += new System.EventHandler(this._buttonSetTerminalAllocation_Click);
            // 
            // _labelTerminalAllocation
            // 
            this._labelTerminalAllocation.AutoSize = true;
            this._labelTerminalAllocation.Location = new System.Drawing.Point(36, 575);
            this._labelTerminalAllocation.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTerminalAllocation.Name = "_labelTerminalAllocation";
            this._labelTerminalAllocation.Size = new System.Drawing.Size(189, 25);
            this._labelTerminalAllocation.TabIndex = 26;
            this._labelTerminalAllocation.Text = "TerminalAllocation";
            // 
            // _labelOffset
            // 
            this._labelOffset.AutoSize = true;
            this._labelOffset.Location = new System.Drawing.Point(36, 219);
            this._labelOffset.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelOffset.Name = "_labelOffset";
            this._labelOffset.Size = new System.Drawing.Size(69, 25);
            this._labelOffset.TabIndex = 8;
            this._labelOffset.Text = "Offset";
            // 
            // _buttonGetScaling
            // 
            this._buttonGetScaling.Location = new System.Drawing.Point(488, 148);
            this._buttonGetScaling.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetScaling.Name = "_buttonGetScaling";
            this._buttonGetScaling.Size = new System.Drawing.Size(100, 48);
            this._buttonGetScaling.TabIndex = 7;
            this._buttonGetScaling.Text = "Get";
            this._buttonGetScaling.UseVisualStyleBackColor = true;
            this._buttonGetScaling.Click += new System.EventHandler(this._buttonGetScaling_Click);
            // 
            // _buttonSetScaling
            // 
            this._buttonSetScaling.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetScaling.Location = new System.Drawing.Point(376, 148);
            this._buttonSetScaling.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetScaling.Name = "_buttonSetScaling";
            this._buttonSetScaling.Size = new System.Drawing.Size(100, 48);
            this._buttonSetScaling.TabIndex = 6;
            this._buttonSetScaling.Text = "Set";
            this._buttonSetScaling.UseVisualStyleBackColor = false;
            this._buttonSetScaling.Click += new System.EventHandler(this._buttonSetScaling_Click);
            // 
            // _labelScaling
            // 
            this._labelScaling.AutoSize = true;
            this._labelScaling.Location = new System.Drawing.Point(36, 158);
            this._labelScaling.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelScaling.Name = "_labelScaling";
            this._labelScaling.Size = new System.Drawing.Size(83, 25);
            this._labelScaling.TabIndex = 5;
            this._labelScaling.Text = "Scaling";
            // 
            // _buttonGetSamplingCycle
            // 
            this._buttonGetSamplingCycle.Location = new System.Drawing.Point(504, 102);
            this._buttonGetSamplingCycle.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetSamplingCycle.Name = "_buttonGetSamplingCycle";
            this._buttonGetSamplingCycle.Size = new System.Drawing.Size(100, 48);
            this._buttonGetSamplingCycle.TabIndex = 4;
            this._buttonGetSamplingCycle.Text = "Get";
            this._buttonGetSamplingCycle.UseVisualStyleBackColor = true;
            this._buttonGetSamplingCycle.Click += new System.EventHandler(this._buttonGetSamplingCycle_Click);
            // 
            // _labelSamplingCycle
            // 
            this._labelSamplingCycle.AutoSize = true;
            this._labelSamplingCycle.Location = new System.Drawing.Point(48, 112);
            this._labelSamplingCycle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSamplingCycle.Name = "_labelSamplingCycle";
            this._labelSamplingCycle.Size = new System.Drawing.Size(155, 25);
            this._labelSamplingCycle.TabIndex = 2;
            this._labelSamplingCycle.Text = "SamplingCycle";
            // 
            // _buttonSetSamplingCycle
            // 
            this._buttonSetSamplingCycle.Location = new System.Drawing.Point(392, 102);
            this._buttonSetSamplingCycle.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetSamplingCycle.Name = "_buttonSetSamplingCycle";
            this._buttonSetSamplingCycle.Size = new System.Drawing.Size(100, 48);
            this._buttonSetSamplingCycle.TabIndex = 3;
            this._buttonSetSamplingCycle.Text = "Set";
            this._buttonSetSamplingCycle.UseVisualStyleBackColor = true;
            this._buttonSetSamplingCycle.Click += new System.EventHandler(this._buttonSetSamplingCycle_Click);
            // 
            // _labelAmbientLightFilter
            // 
            this._labelAmbientLightFilter.AutoSize = true;
            this._labelAmbientLightFilter.Location = new System.Drawing.Point(48, 233);
            this._labelAmbientLightFilter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelAmbientLightFilter.Name = "_labelAmbientLightFilter";
            this._labelAmbientLightFilter.Size = new System.Drawing.Size(185, 25);
            this._labelAmbientLightFilter.TabIndex = 8;
            this._labelAmbientLightFilter.Text = "AmbientLightFilter";
            // 
            // _buttonSetAmbientLightFilter
            // 
            this._buttonSetAmbientLightFilter.Location = new System.Drawing.Point(392, 223);
            this._buttonSetAmbientLightFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetAmbientLightFilter.Name = "_buttonSetAmbientLightFilter";
            this._buttonSetAmbientLightFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonSetAmbientLightFilter.TabIndex = 9;
            this._buttonSetAmbientLightFilter.Text = "Set";
            this._buttonSetAmbientLightFilter.UseVisualStyleBackColor = true;
            this._buttonSetAmbientLightFilter.Click += new System.EventHandler(this._buttonSetAmbientLightFilter_Click);
            // 
            // _buttonGetAmbientLightFilter
            // 
            this._buttonGetAmbientLightFilter.Location = new System.Drawing.Point(504, 223);
            this._buttonGetAmbientLightFilter.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetAmbientLightFilter.Name = "_buttonGetAmbientLightFilter";
            this._buttonGetAmbientLightFilter.Size = new System.Drawing.Size(100, 48);
            this._buttonGetAmbientLightFilter.TabIndex = 10;
            this._buttonGetAmbientLightFilter.Text = "Get";
            this._buttonGetAmbientLightFilter.UseVisualStyleBackColor = true;
            this._buttonGetAmbientLightFilter.Click += new System.EventHandler(this._buttonGetAmbientLightFilter_Click);
            // 
            // _labelMutualInterferencePrevention
            // 
            this._labelMutualInterferencePrevention.AutoSize = true;
            this._labelMutualInterferencePrevention.Location = new System.Drawing.Point(48, 173);
            this._labelMutualInterferencePrevention.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelMutualInterferencePrevention.Name = "_labelMutualInterferencePrevention";
            this._labelMutualInterferencePrevention.Size = new System.Drawing.Size(294, 25);
            this._labelMutualInterferencePrevention.TabIndex = 5;
            this._labelMutualInterferencePrevention.Text = "MutualInterferencePrevention";
            // 
            // _buttonSetMutualInterferencePrevention
            // 
            this._buttonSetMutualInterferencePrevention.Location = new System.Drawing.Point(392, 162);
            this._buttonSetMutualInterferencePrevention.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetMutualInterferencePrevention.Name = "_buttonSetMutualInterferencePrevention";
            this._buttonSetMutualInterferencePrevention.Size = new System.Drawing.Size(100, 48);
            this._buttonSetMutualInterferencePrevention.TabIndex = 6;
            this._buttonSetMutualInterferencePrevention.Text = "Set";
            this._buttonSetMutualInterferencePrevention.UseVisualStyleBackColor = true;
            this._buttonSetMutualInterferencePrevention.Click += new System.EventHandler(this._buttonSetMutualInterferencePrevention_Click);
            // 
            // _buttonGetMutualInterferencePrevention
            // 
            this._buttonGetMutualInterferencePrevention.Location = new System.Drawing.Point(504, 162);
            this._buttonGetMutualInterferencePrevention.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetMutualInterferencePrevention.Name = "_buttonGetMutualInterferencePrevention";
            this._buttonGetMutualInterferencePrevention.Size = new System.Drawing.Size(100, 48);
            this._buttonGetMutualInterferencePrevention.TabIndex = 7;
            this._buttonGetMutualInterferencePrevention.Text = "Get";
            this._buttonGetMutualInterferencePrevention.UseVisualStyleBackColor = true;
            this._buttonGetMutualInterferencePrevention.Click += new System.EventHandler(this._buttonGetMutualInterferencePrevention_Click);
            // 
            // _comboBoxSettingProgramNo
            // 
            this._comboBoxSettingProgramNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSettingProgramNo.FormattingEnabled = true;
            this._comboBoxSettingProgramNo.Items.AddRange(new object[] {
            "Program 00",
            "Program 01",
            "Program 02",
            "Program 03",
            "Program 04",
            "Program 05",
            "Program 06",
            "Program 07"});
            this._comboBoxSettingProgramNo.Location = new System.Drawing.Point(192, 38);
            this._comboBoxSettingProgramNo.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSettingProgramNo.Name = "_comboBoxSettingProgramNo";
            this._comboBoxSettingProgramNo.Size = new System.Drawing.Size(196, 33);
            this._comboBoxSettingProgramNo.TabIndex = 1;
            // 
            // _labelSettingProgramNo
            // 
            this._labelSettingProgramNo.AutoSize = true;
            this._labelSettingProgramNo.Location = new System.Drawing.Point(14, 44);
            this._labelSettingProgramNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSettingProgramNo.Name = "_labelSettingProgramNo";
            this._labelSettingProgramNo.Size = new System.Drawing.Size(159, 25);
            this._labelSettingProgramNo.TabIndex = 0;
            this._labelSettingProgramNo.Text = "Target program";
            // 
            // _buttonGetStorageNumber
            // 
            this._buttonGetStorageNumber.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetStorageNumber.Location = new System.Drawing.Point(1170, 162);
            this._buttonGetStorageNumber.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetStorageNumber.Name = "_buttonGetStorageNumber";
            this._buttonGetStorageNumber.Size = new System.Drawing.Size(100, 48);
            this._buttonGetStorageNumber.TabIndex = 16;
            this._buttonGetStorageNumber.Text = "Get";
            this._buttonGetStorageNumber.UseVisualStyleBackColor = false;
            this._buttonGetStorageNumber.Click += new System.EventHandler(this._buttonGetStorageNumber_Click);
            // 
            // _buttonSetStorageNumber
            // 
            this._buttonSetStorageNumber.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetStorageNumber.Location = new System.Drawing.Point(1058, 162);
            this._buttonSetStorageNumber.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetStorageNumber.Name = "_buttonSetStorageNumber";
            this._buttonSetStorageNumber.Size = new System.Drawing.Size(100, 48);
            this._buttonSetStorageNumber.TabIndex = 15;
            this._buttonSetStorageNumber.Text = "Set";
            this._buttonSetStorageNumber.UseVisualStyleBackColor = false;
            this._buttonSetStorageNumber.Click += new System.EventHandler(this._buttonSetStorageNumber_Click);
            // 
            // _labelStorageNumber
            // 
            this._labelStorageNumber.AutoSize = true;
            this._labelStorageNumber.Location = new System.Drawing.Point(716, 173);
            this._labelStorageNumber.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelStorageNumber.Name = "_labelStorageNumber";
            this._labelStorageNumber.Size = new System.Drawing.Size(162, 25);
            this._labelStorageNumber.TabIndex = 14;
            this._labelStorageNumber.Text = "StorageNumber";
            // 
            // _buttonGetStorageTarget
            // 
            this._buttonGetStorageTarget.Location = new System.Drawing.Point(1726, 102);
            this._buttonGetStorageTarget.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetStorageTarget.Name = "_buttonGetStorageTarget";
            this._buttonGetStorageTarget.Size = new System.Drawing.Size(100, 48);
            this._buttonGetStorageTarget.TabIndex = 22;
            this._buttonGetStorageTarget.Text = "Get";
            this._buttonGetStorageTarget.UseVisualStyleBackColor = true;
            this._buttonGetStorageTarget.Click += new System.EventHandler(this._buttonGetStorageTarget_Click);
            // 
            // _buttonSetStorageTarget
            // 
            this._buttonSetStorageTarget.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetStorageTarget.Location = new System.Drawing.Point(1614, 102);
            this._buttonSetStorageTarget.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetStorageTarget.Name = "_buttonSetStorageTarget";
            this._buttonSetStorageTarget.Size = new System.Drawing.Size(100, 48);
            this._buttonSetStorageTarget.TabIndex = 21;
            this._buttonSetStorageTarget.Text = "Set";
            this._buttonSetStorageTarget.UseVisualStyleBackColor = false;
            this._buttonSetStorageTarget.Click += new System.EventHandler(this._buttonSetStorageTarget_Click);
            // 
            // _labelStorageTarget
            // 
            this._labelStorageTarget.AutoSize = true;
            this._labelStorageTarget.Location = new System.Drawing.Point(1304, 112);
            this._labelStorageTarget.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelStorageTarget.Name = "_labelStorageTarget";
            this._labelStorageTarget.Size = new System.Drawing.Size(149, 25);
            this._labelStorageTarget.TabIndex = 20;
            this._labelStorageTarget.Text = "StorageTarget";
            // 
            // _buttonGetJudgmentOutput
            // 
            this._buttonGetJudgmentOutput.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetJudgmentOutput.Location = new System.Drawing.Point(1170, 102);
            this._buttonGetJudgmentOutput.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetJudgmentOutput.Name = "_buttonGetJudgmentOutput";
            this._buttonGetJudgmentOutput.Size = new System.Drawing.Size(100, 48);
            this._buttonGetJudgmentOutput.TabIndex = 13;
            this._buttonGetJudgmentOutput.Text = "Get";
            this._buttonGetJudgmentOutput.UseVisualStyleBackColor = false;
            this._buttonGetJudgmentOutput.Click += new System.EventHandler(this._buttonGetJudgmentOutput_Click);
            // 
            // _buttonSetJudgmentOutput
            // 
            this._buttonSetJudgmentOutput.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetJudgmentOutput.Location = new System.Drawing.Point(1058, 102);
            this._buttonSetJudgmentOutput.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetJudgmentOutput.Name = "_buttonSetJudgmentOutput";
            this._buttonSetJudgmentOutput.Size = new System.Drawing.Size(100, 48);
            this._buttonSetJudgmentOutput.TabIndex = 12;
            this._buttonSetJudgmentOutput.Text = "Set";
            this._buttonSetJudgmentOutput.UseVisualStyleBackColor = false;
            this._buttonSetJudgmentOutput.Click += new System.EventHandler(this._buttonSetJudgmentOutput_Click);
            // 
            // _labelJudgmentOutput
            // 
            this._labelJudgmentOutput.AutoSize = true;
            this._labelJudgmentOutput.Location = new System.Drawing.Point(716, 112);
            this._labelJudgmentOutput.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelJudgmentOutput.Name = "_labelJudgmentOutput";
            this._labelJudgmentOutput.Size = new System.Drawing.Size(170, 25);
            this._labelJudgmentOutput.TabIndex = 11;
            this._labelJudgmentOutput.Text = "JudgmentOutput";
            // 
            // _tabPageSettingBatch
            // 
            this._tabPageSettingBatch.Controls.Add(this._groupBoxEncoderSetting);
            this._tabPageSettingBatch.Controls.Add(this._groupBoxByProgram);
            this._tabPageSettingBatch.Controls.Add(this._groupBoxAllSettings);
            this._tabPageSettingBatch.Controls.Add(this._labelSetNote);
            this._tabPageSettingBatch.Controls.Add(this._labelGetNote);
            this._tabPageSettingBatch.Location = new System.Drawing.Point(8, 39);
            this._tabPageSettingBatch.Margin = new System.Windows.Forms.Padding(6);
            this._tabPageSettingBatch.Name = "_tabPageSettingBatch";
            this._tabPageSettingBatch.Size = new System.Drawing.Size(1952, 1124);
            this._tabPageSettingBatch.TabIndex = 7;
            this._tabPageSettingBatch.Text = "Set/Get settings(batch)";
            this._tabPageSettingBatch.UseVisualStyleBackColor = true;
            // 
            // _groupBoxEncoderSetting
            // 
            this._groupBoxEncoderSetting.Controls.Add(this._buttonSetEncoderSetting);
            this._groupBoxEncoderSetting.Controls.Add(this._buttonGetEncoderSetting);
            this._groupBoxEncoderSetting.Location = new System.Drawing.Point(30, 471);
            this._groupBoxEncoderSetting.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxEncoderSetting.Name = "_groupBoxEncoderSetting";
            this._groupBoxEncoderSetting.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxEncoderSetting.Size = new System.Drawing.Size(626, 117);
            this._groupBoxEncoderSetting.TabIndex = 4;
            this._groupBoxEncoderSetting.TabStop = false;
            this._groupBoxEncoderSetting.Text = "Encoder";
            // 
            // _buttonSetEncoderSetting
            // 
            this._buttonSetEncoderSetting.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetEncoderSetting.Location = new System.Drawing.Point(302, 42);
            this._buttonSetEncoderSetting.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetEncoderSetting.Name = "_buttonSetEncoderSetting";
            this._buttonSetEncoderSetting.Size = new System.Drawing.Size(218, 48);
            this._buttonSetEncoderSetting.TabIndex = 1;
            this._buttonSetEncoderSetting.Text = "SetEncoder";
            this._buttonSetEncoderSetting.UseVisualStyleBackColor = false;
            this._buttonSetEncoderSetting.Click += new System.EventHandler(this._buttonSetEncoderSetting_Click);
            // 
            // _buttonGetEncoderSetting
            // 
            this._buttonGetEncoderSetting.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetEncoderSetting.Location = new System.Drawing.Point(48, 42);
            this._buttonGetEncoderSetting.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetEncoderSetting.Name = "_buttonGetEncoderSetting";
            this._buttonGetEncoderSetting.Size = new System.Drawing.Size(218, 48);
            this._buttonGetEncoderSetting.TabIndex = 0;
            this._buttonGetEncoderSetting.Text = "GetEncoder";
            this._buttonGetEncoderSetting.UseVisualStyleBackColor = false;
            this._buttonGetEncoderSetting.Click += new System.EventHandler(this._buttonGetEncoderSetting_Click);
            // 
            // _groupBoxByProgram
            // 
            this._groupBoxByProgram.Controls.Add(this._comboBoxTargetProgramNo);
            this._groupBoxByProgram.Controls.Add(this._labelTargetProgramNo);
            this._groupBoxByProgram.Controls.Add(this._buttonGetProgram);
            this._groupBoxByProgram.Controls.Add(this._buttonSetProgram);
            this._groupBoxByProgram.Location = new System.Drawing.Point(30, 265);
            this._groupBoxByProgram.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxByProgram.Name = "_groupBoxByProgram";
            this._groupBoxByProgram.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxByProgram.Size = new System.Drawing.Size(626, 192);
            this._groupBoxByProgram.TabIndex = 3;
            this._groupBoxByProgram.TabStop = false;
            this._groupBoxByProgram.Text = "By program";
            // 
            // _comboBoxTargetProgramNo
            // 
            this._comboBoxTargetProgramNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxTargetProgramNo.FormattingEnabled = true;
            this._comboBoxTargetProgramNo.Items.AddRange(new object[] {
            "Program 00",
            "Program 01",
            "Program 02",
            "Program 03",
            "Program 04",
            "Program 05",
            "Program 06",
            "Program 07"});
            this._comboBoxTargetProgramNo.Location = new System.Drawing.Point(226, 38);
            this._comboBoxTargetProgramNo.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxTargetProgramNo.Name = "_comboBoxTargetProgramNo";
            this._comboBoxTargetProgramNo.Size = new System.Drawing.Size(196, 33);
            this._comboBoxTargetProgramNo.TabIndex = 1;
            // 
            // _labelTargetProgramNo
            // 
            this._labelTargetProgramNo.AutoSize = true;
            this._labelTargetProgramNo.Location = new System.Drawing.Point(48, 44);
            this._labelTargetProgramNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelTargetProgramNo.Name = "_labelTargetProgramNo";
            this._labelTargetProgramNo.Size = new System.Drawing.Size(159, 25);
            this._labelTargetProgramNo.TabIndex = 0;
            this._labelTargetProgramNo.Text = "Target program";
            // 
            // _buttonGetProgram
            // 
            this._buttonGetProgram.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetProgram.Location = new System.Drawing.Point(48, 108);
            this._buttonGetProgram.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetProgram.Name = "_buttonGetProgram";
            this._buttonGetProgram.Size = new System.Drawing.Size(218, 48);
            this._buttonGetProgram.TabIndex = 2;
            this._buttonGetProgram.Text = "GetProgram";
            this._buttonGetProgram.UseVisualStyleBackColor = false;
            this._buttonGetProgram.Click += new System.EventHandler(this._buttonGetProgram_Click);
            // 
            // _buttonSetProgram
            // 
            this._buttonSetProgram.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetProgram.Location = new System.Drawing.Point(302, 108);
            this._buttonSetProgram.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetProgram.Name = "_buttonSetProgram";
            this._buttonSetProgram.Size = new System.Drawing.Size(218, 48);
            this._buttonSetProgram.TabIndex = 3;
            this._buttonSetProgram.Text = "SetProgram";
            this._buttonSetProgram.UseVisualStyleBackColor = false;
            this._buttonSetProgram.Click += new System.EventHandler(this._buttonSetProgram_Click);
            // 
            // _groupBoxAllSettings
            // 
            this._groupBoxAllSettings.Controls.Add(this._buttonSetSettings);
            this._groupBoxAllSettings.Controls.Add(this._buttonGetSettings);
            this._groupBoxAllSettings.Location = new System.Drawing.Point(30, 133);
            this._groupBoxAllSettings.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxAllSettings.Name = "_groupBoxAllSettings";
            this._groupBoxAllSettings.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxAllSettings.Size = new System.Drawing.Size(626, 117);
            this._groupBoxAllSettings.TabIndex = 2;
            this._groupBoxAllSettings.TabStop = false;
            this._groupBoxAllSettings.Text = "All settings";
            // 
            // _buttonSetSettings
            // 
            this._buttonSetSettings.BackColor = System.Drawing.Color.Transparent;
            this._buttonSetSettings.Location = new System.Drawing.Point(302, 38);
            this._buttonSetSettings.Margin = new System.Windows.Forms.Padding(6);
            this._buttonSetSettings.Name = "_buttonSetSettings";
            this._buttonSetSettings.Size = new System.Drawing.Size(218, 48);
            this._buttonSetSettings.TabIndex = 1;
            this._buttonSetSettings.Text = "SetSettings";
            this._buttonSetSettings.UseVisualStyleBackColor = false;
            this._buttonSetSettings.Click += new System.EventHandler(this._buttonSetSettings_Click);
            // 
            // _buttonGetSettings
            // 
            this._buttonGetSettings.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetSettings.Location = new System.Drawing.Point(48, 38);
            this._buttonGetSettings.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetSettings.Name = "_buttonGetSettings";
            this._buttonGetSettings.Size = new System.Drawing.Size(218, 48);
            this._buttonGetSettings.TabIndex = 0;
            this._buttonGetSettings.Text = "GetSettings";
            this._buttonGetSettings.UseVisualStyleBackColor = false;
            this._buttonGetSettings.Click += new System.EventHandler(this._buttonGetSettings_Click);
            // 
            // _labelSetNote
            // 
            this._labelSetNote.AutoSize = true;
            this._labelSetNote.Location = new System.Drawing.Point(26, 77);
            this._labelSetNote.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSetNote.Name = "_labelSetNote";
            this._labelSetNote.Size = new System.Drawing.Size(306, 25);
            this._labelSetNote.TabIndex = 1;
            this._labelSetNote.Text = "Set : Restore settings from file.";
            // 
            // _labelGetNote
            // 
            this._labelGetNote.AutoSize = true;
            this._labelGetNote.Location = new System.Drawing.Point(26, 42);
            this._labelGetNote.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelGetNote.Name = "_labelGetNote";
            this._labelGetNote.Size = new System.Drawing.Size(323, 25);
            this._labelGetNote.TabIndex = 0;
            this._labelGetNote.Text = "Get : Back up the settings to file.";
            // 
            // _tabPageDataStorage
            // 
            this._tabPageDataStorage.Controls.Add(this._groupBoxDll);
            this._tabPageDataStorage.Controls.Add(this._groupBoxOther);
            this._tabPageDataStorage.Controls.Add(this._groupBoxSystem);
            this._tabPageDataStorage.Controls.Add(this._groupBoxEncoder);
            this._tabPageDataStorage.Controls.Add(this._groupBoxModeChange);
            this._tabPageDataStorage.Controls.Add(this._groupBoxCalibration);
            this._tabPageDataStorage.Controls.Add(this._groupBoxDataStorage);
            this._tabPageDataStorage.Controls.Add(this._groupBoxAbleTuning);
            this._tabPageDataStorage.Location = new System.Drawing.Point(8, 39);
            this._tabPageDataStorage.Margin = new System.Windows.Forms.Padding(6);
            this._tabPageDataStorage.Name = "_tabPageDataStorage";
            this._tabPageDataStorage.Size = new System.Drawing.Size(1952, 1124);
            this._tabPageDataStorage.TabIndex = 6;
            this._tabPageDataStorage.Text = "Data Storage/Other";
            this._tabPageDataStorage.UseVisualStyleBackColor = true;
            // 
            // _groupBoxDll
            // 
            this._groupBoxDll.Controls.Add(this._buttonGetVersion);
            this._groupBoxDll.Location = new System.Drawing.Point(1452, 377);
            this._groupBoxDll.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxDll.Name = "_groupBoxDll";
            this._groupBoxDll.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxDll.Size = new System.Drawing.Size(494, 119);
            this._groupBoxDll.TabIndex = 7;
            this._groupBoxDll.TabStop = false;
            this._groupBoxDll.Text = "DLL";
            // 
            // _buttonGetVersion
            // 
            this._buttonGetVersion.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetVersion.Location = new System.Drawing.Point(24, 38);
            this._buttonGetVersion.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetVersion.Name = "_buttonGetVersion";
            this._buttonGetVersion.Size = new System.Drawing.Size(288, 48);
            this._buttonGetVersion.TabIndex = 0;
            this._buttonGetVersion.Text = "GetVersion";
            this._buttonGetVersion.UseVisualStyleBackColor = false;
            this._buttonGetVersion.Click += new System.EventHandler(this._buttonGetVersion_Click);
            // 
            // _groupBoxOther
            // 
            this._groupBoxOther.Controls.Add(this._comboBoxSetMeasureEnableValue);
            this._groupBoxOther.Controls.Add(this._buttonLightControl);
            this._groupBoxOther.Controls.Add(this._buttonMeasurementControl);
            this._groupBoxOther.Controls.Add(this._labelSetMeasureEnableValue);
            this._groupBoxOther.Controls.Add(this._labelSetLaserControlValue);
            this._groupBoxOther.Controls.Add(this._comboBoxSetLaserControlValue);
            this._groupBoxOther.Location = new System.Drawing.Point(24, 685);
            this._groupBoxOther.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxOther.Name = "_groupBoxOther";
            this._groupBoxOther.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxOther.Size = new System.Drawing.Size(726, 177);
            this._groupBoxOther.TabIndex = 2;
            this._groupBoxOther.TabStop = false;
            this._groupBoxOther.Text = "Other control";
            // 
            // _comboBoxSetMeasureEnableValue
            // 
            this._comboBoxSetMeasureEnableValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSetMeasureEnableValue.FormattingEnabled = true;
            this._comboBoxSetMeasureEnableValue.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxSetMeasureEnableValue.Location = new System.Drawing.Point(356, 102);
            this._comboBoxSetMeasureEnableValue.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSetMeasureEnableValue.Name = "_comboBoxSetMeasureEnableValue";
            this._comboBoxSetMeasureEnableValue.Size = new System.Drawing.Size(116, 33);
            this._comboBoxSetMeasureEnableValue.TabIndex = 5;
            // 
            // _buttonLightControl
            // 
            this._buttonLightControl.Location = new System.Drawing.Point(24, 38);
            this._buttonLightControl.Margin = new System.Windows.Forms.Padding(6);
            this._buttonLightControl.Name = "_buttonLightControl";
            this._buttonLightControl.Size = new System.Drawing.Size(242, 48);
            this._buttonLightControl.TabIndex = 0;
            this._buttonLightControl.Text = "LightControl";
            this._buttonLightControl.UseVisualStyleBackColor = true;
            this._buttonLightControl.Click += new System.EventHandler(this._buttonLightControl_Click);
            // 
            // _buttonMeasurementControl
            // 
            this._buttonMeasurementControl.Location = new System.Drawing.Point(24, 98);
            this._buttonMeasurementControl.Margin = new System.Windows.Forms.Padding(6);
            this._buttonMeasurementControl.Name = "_buttonMeasurementControl";
            this._buttonMeasurementControl.Size = new System.Drawing.Size(242, 48);
            this._buttonMeasurementControl.TabIndex = 3;
            this._buttonMeasurementControl.Text = "MeasurementControl";
            this._buttonMeasurementControl.UseVisualStyleBackColor = true;
            this._buttonMeasurementControl.Click += new System.EventHandler(this._buttonMeasurementControl_Click);
            // 
            // _labelSetMeasureEnableValue
            // 
            this._labelSetMeasureEnableValue.AutoSize = true;
            this._labelSetMeasureEnableValue.Location = new System.Drawing.Point(278, 108);
            this._labelSetMeasureEnableValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSetMeasureEnableValue.Name = "_labelSetMeasureEnableValue";
            this._labelSetMeasureEnableValue.Size = new System.Drawing.Size(64, 25);
            this._labelSetMeasureEnableValue.TabIndex = 4;
            this._labelSetMeasureEnableValue.Text = "onOff";
            // 
            // _labelSetLaserControlValue
            // 
            this._labelSetLaserControlValue.AutoSize = true;
            this._labelSetLaserControlValue.Location = new System.Drawing.Point(280, 48);
            this._labelSetLaserControlValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSetLaserControlValue.Name = "_labelSetLaserControlValue";
            this._labelSetLaserControlValue.Size = new System.Drawing.Size(64, 25);
            this._labelSetLaserControlValue.TabIndex = 1;
            this._labelSetLaserControlValue.Text = "onOff";
            // 
            // _comboBoxSetLaserControlValue
            // 
            this._comboBoxSetLaserControlValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSetLaserControlValue.FormattingEnabled = true;
            this._comboBoxSetLaserControlValue.Items.AddRange(new object[] {
            "ON",
            "OFF"});
            this._comboBoxSetLaserControlValue.Location = new System.Drawing.Point(356, 42);
            this._comboBoxSetLaserControlValue.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSetLaserControlValue.Name = "_comboBoxSetLaserControlValue";
            this._comboBoxSetLaserControlValue.Size = new System.Drawing.Size(116, 33);
            this._comboBoxSetLaserControlValue.TabIndex = 2;
            // 
            // _groupBoxSystem
            // 
            this._groupBoxSystem.Controls.Add(this._buttonGetSystemConfiguration);
            this._groupBoxSystem.Controls.Add(this._buttonReturnToFactoryDefaultSetting);
            this._groupBoxSystem.Location = new System.Drawing.Point(1452, 192);
            this._groupBoxSystem.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxSystem.Name = "_groupBoxSystem";
            this._groupBoxSystem.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxSystem.Size = new System.Drawing.Size(494, 173);
            this._groupBoxSystem.TabIndex = 6;
            this._groupBoxSystem.TabStop = false;
            this._groupBoxSystem.Text = "System";
            // 
            // _buttonGetSystemConfiguration
            // 
            this._buttonGetSystemConfiguration.Location = new System.Drawing.Point(24, 38);
            this._buttonGetSystemConfiguration.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetSystemConfiguration.Name = "_buttonGetSystemConfiguration";
            this._buttonGetSystemConfiguration.Size = new System.Drawing.Size(356, 48);
            this._buttonGetSystemConfiguration.TabIndex = 0;
            this._buttonGetSystemConfiguration.Text = "GetSystemConfiguration";
            this._buttonGetSystemConfiguration.UseVisualStyleBackColor = true;
            this._buttonGetSystemConfiguration.Click += new System.EventHandler(this._buttonGetSystemConfiguration_Click);
            // 
            // _buttonReturnToFactoryDefaultSetting
            // 
            this._buttonReturnToFactoryDefaultSetting.Location = new System.Drawing.Point(24, 98);
            this._buttonReturnToFactoryDefaultSetting.Margin = new System.Windows.Forms.Padding(6);
            this._buttonReturnToFactoryDefaultSetting.Name = "_buttonReturnToFactoryDefaultSetting";
            this._buttonReturnToFactoryDefaultSetting.Size = new System.Drawing.Size(356, 48);
            this._buttonReturnToFactoryDefaultSetting.TabIndex = 1;
            this._buttonReturnToFactoryDefaultSetting.Text = "ReturnToFactoryDefaultSetting";
            this._buttonReturnToFactoryDefaultSetting.UseVisualStyleBackColor = true;
            this._buttonReturnToFactoryDefaultSetting.Click += new System.EventHandler(this._buttonReturnToFactoryDefaultSetting_Click);
            // 
            // _groupBoxEncoder
            // 
            this._groupBoxEncoder.Controls.Add(this._buttonGetPulseCount);
            this._groupBoxEncoder.Controls.Add(this._buttonResetPulseCount);
            this._groupBoxEncoder.Location = new System.Drawing.Point(24, 508);
            this._groupBoxEncoder.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxEncoder.Name = "_groupBoxEncoder";
            this._groupBoxEncoder.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxEncoder.Size = new System.Drawing.Size(726, 165);
            this._groupBoxEncoder.TabIndex = 1;
            this._groupBoxEncoder.TabStop = false;
            this._groupBoxEncoder.Text = "Encoder";
            // 
            // _buttonGetPulseCount
            // 
            this._buttonGetPulseCount.BackColor = System.Drawing.Color.Transparent;
            this._buttonGetPulseCount.Location = new System.Drawing.Point(24, 38);
            this._buttonGetPulseCount.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetPulseCount.Name = "_buttonGetPulseCount";
            this._buttonGetPulseCount.Size = new System.Drawing.Size(232, 48);
            this._buttonGetPulseCount.TabIndex = 0;
            this._buttonGetPulseCount.Text = "GetPulseCount";
            this._buttonGetPulseCount.UseVisualStyleBackColor = false;
            this._buttonGetPulseCount.Click += new System.EventHandler(this._buttonGetPulseCount_Click);
            // 
            // _buttonResetPulseCount
            // 
            this._buttonResetPulseCount.BackColor = System.Drawing.Color.Transparent;
            this._buttonResetPulseCount.Location = new System.Drawing.Point(24, 98);
            this._buttonResetPulseCount.Margin = new System.Windows.Forms.Padding(6);
            this._buttonResetPulseCount.Name = "_buttonResetPulseCount";
            this._buttonResetPulseCount.Size = new System.Drawing.Size(228, 48);
            this._buttonResetPulseCount.TabIndex = 1;
            this._buttonResetPulseCount.Text = "ResetPulseCount";
            this._buttonResetPulseCount.UseVisualStyleBackColor = false;
            this._buttonResetPulseCount.Click += new System.EventHandler(this._buttonResetPulseCount_Click);
            // 
            // _groupBoxModeChange
            // 
            this._groupBoxModeChange.Controls.Add(this._buttonTransitToMeasurementMode);
            this._groupBoxModeChange.Controls.Add(this._buttonTransitToSettingMode);
            this._groupBoxModeChange.Location = new System.Drawing.Point(1452, 6);
            this._groupBoxModeChange.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxModeChange.Name = "_groupBoxModeChange";
            this._groupBoxModeChange.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxModeChange.Size = new System.Drawing.Size(494, 173);
            this._groupBoxModeChange.TabIndex = 5;
            this._groupBoxModeChange.TabStop = false;
            this._groupBoxModeChange.Text = "Change mode";
            // 
            // _buttonTransitToMeasurementMode
            // 
            this._buttonTransitToMeasurementMode.Location = new System.Drawing.Point(24, 38);
            this._buttonTransitToMeasurementMode.Margin = new System.Windows.Forms.Padding(6);
            this._buttonTransitToMeasurementMode.Name = "_buttonTransitToMeasurementMode";
            this._buttonTransitToMeasurementMode.Size = new System.Drawing.Size(330, 48);
            this._buttonTransitToMeasurementMode.TabIndex = 0;
            this._buttonTransitToMeasurementMode.Text = "TransitToMeasurementMode";
            this._buttonTransitToMeasurementMode.UseVisualStyleBackColor = true;
            this._buttonTransitToMeasurementMode.Click += new System.EventHandler(this._buttonTransitToMeasurementMode_Click);
            // 
            // _buttonTransitToSettingMode
            // 
            this._buttonTransitToSettingMode.Location = new System.Drawing.Point(24, 98);
            this._buttonTransitToSettingMode.Margin = new System.Windows.Forms.Padding(6);
            this._buttonTransitToSettingMode.Name = "_buttonTransitToSettingMode";
            this._buttonTransitToSettingMode.Size = new System.Drawing.Size(330, 48);
            this._buttonTransitToSettingMode.TabIndex = 1;
            this._buttonTransitToSettingMode.Text = "TransitToSettingMode";
            this._buttonTransitToSettingMode.UseVisualStyleBackColor = true;
            this._buttonTransitToSettingMode.Click += new System.EventHandler(this._buttonTransitToSettingMode_Click);
            // 
            // _groupBoxCalibration
            // 
            this._groupBoxCalibration.Controls.Add(this._comboBoxCalibration);
            this._groupBoxCalibration.Controls.Add(this._labelCalibrationAmpNo);
            this._groupBoxCalibration.Controls.Add(this._buttonCancelCalibration);
            this._groupBoxCalibration.Controls.Add(this._buttonStopCalibration);
            this._groupBoxCalibration.Controls.Add(this._buttonStartCalibration);
            this._groupBoxCalibration.Location = new System.Drawing.Point(844, 258);
            this._groupBoxCalibration.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxCalibration.Name = "_groupBoxCalibration";
            this._groupBoxCalibration.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxCalibration.Size = new System.Drawing.Size(598, 237);
            this._groupBoxCalibration.TabIndex = 4;
            this._groupBoxCalibration.TabStop = false;
            this._groupBoxCalibration.Text = "Calibration of light intensity";
            // 
            // _comboBoxCalibration
            // 
            this._comboBoxCalibration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxCalibration.FormattingEnabled = true;
            this._comboBoxCalibration.Items.AddRange(new object[] {
            "HEAD 01",
            "HEAD 02",
            "HEAD 03",
            "HEAD 04",
            "HEAD 05",
            "HEAD 06"});
            this._comboBoxCalibration.Location = new System.Drawing.Point(28, 69);
            this._comboBoxCalibration.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxCalibration.Name = "_comboBoxCalibration";
            this._comboBoxCalibration.Size = new System.Drawing.Size(178, 33);
            this._comboBoxCalibration.TabIndex = 1;
            // 
            // _labelCalibrationAmpNo
            // 
            this._labelCalibrationAmpNo.AutoSize = true;
            this._labelCalibrationAmpNo.Location = new System.Drawing.Point(24, 38);
            this._labelCalibrationAmpNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelCalibrationAmpNo.Name = "_labelCalibrationAmpNo";
            this._labelCalibrationAmpNo.Size = new System.Drawing.Size(138, 25);
            this._labelCalibrationAmpNo.TabIndex = 0;
            this._labelCalibrationAmpNo.Text = "Target HEAD";
            // 
            // _buttonCancelCalibration
            // 
            this._buttonCancelCalibration.Location = new System.Drawing.Point(268, 158);
            this._buttonCancelCalibration.Margin = new System.Windows.Forms.Padding(6);
            this._buttonCancelCalibration.Name = "_buttonCancelCalibration";
            this._buttonCancelCalibration.Size = new System.Drawing.Size(226, 48);
            this._buttonCancelCalibration.TabIndex = 4;
            this._buttonCancelCalibration.Text = "CancelCalibration";
            this._buttonCancelCalibration.UseVisualStyleBackColor = true;
            this._buttonCancelCalibration.Click += new System.EventHandler(this._buttonCancelCalibration_Click);
            // 
            // _buttonStopCalibration
            // 
            this._buttonStopCalibration.Location = new System.Drawing.Point(268, 98);
            this._buttonStopCalibration.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStopCalibration.Name = "_buttonStopCalibration";
            this._buttonStopCalibration.Size = new System.Drawing.Size(226, 48);
            this._buttonStopCalibration.TabIndex = 3;
            this._buttonStopCalibration.Text = "StopCalibration";
            this._buttonStopCalibration.UseVisualStyleBackColor = true;
            this._buttonStopCalibration.Click += new System.EventHandler(this._buttonStopCalibration_Click);
            // 
            // _buttonStartCalibration
            // 
            this._buttonStartCalibration.Location = new System.Drawing.Point(268, 38);
            this._buttonStartCalibration.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStartCalibration.Name = "_buttonStartCalibration";
            this._buttonStartCalibration.Size = new System.Drawing.Size(226, 48);
            this._buttonStartCalibration.TabIndex = 2;
            this._buttonStartCalibration.Text = "StartCalibration";
            this._buttonStartCalibration.UseVisualStyleBackColor = true;
            this._buttonStartCalibration.Click += new System.EventHandler(this._buttonStartCalibration_Click);
            // 
            // _groupBoxDataStorage
            // 
            this._groupBoxDataStorage.Controls.Add(this._comboBoxSelectedIndex);
            this._groupBoxDataStorage.Controls.Add(this._labelSelectedIndex);
            this._groupBoxDataStorage.Controls.Add(this._textBoxStorageDataCount);
            this._groupBoxDataStorage.Controls.Add(this._labelStorageDataCount);
            this._groupBoxDataStorage.Controls.Add(this._buttonStorageContinuouslySave);
            this._groupBoxDataStorage.Controls.Add(this._buttonStorageContinuously);
            this._groupBoxDataStorage.Controls.Add(this._labelStorageContinuously);
            this._groupBoxDataStorage.Controls.Add(this._buttonStorageSave);
            this._groupBoxDataStorage.Controls.Add(this._buttonStartStorage);
            this._groupBoxDataStorage.Controls.Add(this._buttonClearStorageData);
            this._groupBoxDataStorage.Controls.Add(this._textBoxGetStorageDataReadCount);
            this._groupBoxDataStorage.Controls.Add(this._buttonStopStorage);
            this._groupBoxDataStorage.Controls.Add(this._labelGetStorageDataReadCount);
            this._groupBoxDataStorage.Controls.Add(this._buttonGetStorageData);
            this._groupBoxDataStorage.Controls.Add(this._textBoxGetStorageDataIndex);
            this._groupBoxDataStorage.Controls.Add(this._buttonGetStorageIndex);
            this._groupBoxDataStorage.Controls.Add(this._labelGetStorageDataIndex);
            this._groupBoxDataStorage.Location = new System.Drawing.Point(24, 6);
            this._groupBoxDataStorage.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxDataStorage.Name = "_groupBoxDataStorage";
            this._groupBoxDataStorage.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxDataStorage.Size = new System.Drawing.Size(808, 490);
            this._groupBoxDataStorage.TabIndex = 0;
            this._groupBoxDataStorage.TabStop = false;
            this._groupBoxDataStorage.Text = "Data storage";
            // 
            // _comboBoxSelectedIndex
            // 
            this._comboBoxSelectedIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxSelectedIndex.FormattingEnabled = true;
            this._comboBoxSelectedIndex.Items.AddRange(new object[] {
            "OLDEST",
            "NEWEST"});
            this._comboBoxSelectedIndex.Location = new System.Drawing.Point(422, 125);
            this._comboBoxSelectedIndex.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxSelectedIndex.Name = "_comboBoxSelectedIndex";
            this._comboBoxSelectedIndex.Size = new System.Drawing.Size(164, 33);
            this._comboBoxSelectedIndex.TabIndex = 5;
            // 
            // _labelSelectedIndex
            // 
            this._labelSelectedIndex.AutoSize = true;
            this._labelSelectedIndex.Location = new System.Drawing.Point(260, 131);
            this._labelSelectedIndex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelSelectedIndex.Name = "_labelSelectedIndex";
            this._labelSelectedIndex.Size = new System.Drawing.Size(145, 25);
            this._labelSelectedIndex.TabIndex = 4;
            this._labelSelectedIndex.Text = "selectedIndex";
            // 
            // _textBoxStorageDataCount
            // 
            this._textBoxStorageDataCount.Location = new System.Drawing.Point(386, 350);
            this._textBoxStorageDataCount.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxStorageDataCount.MaxLength = 7;
            this._textBoxStorageDataCount.Name = "_textBoxStorageDataCount";
            this._textBoxStorageDataCount.ReadOnly = true;
            this._textBoxStorageDataCount.Size = new System.Drawing.Size(156, 31);
            this._textBoxStorageDataCount.TabIndex = 15;
            this._textBoxStorageDataCount.Text = "0";
            // 
            // _labelStorageDataCount
            // 
            this._labelStorageDataCount.AutoSize = true;
            this._labelStorageDataCount.Location = new System.Drawing.Point(260, 356);
            this._labelStorageDataCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelStorageDataCount.Name = "_labelStorageDataCount";
            this._labelStorageDataCount.Size = new System.Drawing.Size(111, 25);
            this._labelStorageDataCount.TabIndex = 14;
            this._labelStorageDataCount.Text = "dataCount";
            // 
            // _buttonStorageContinuouslySave
            // 
            this._buttonStorageContinuouslySave.Image = ((System.Drawing.Image)(resources.GetObject("_buttonStorageContinuouslySave.Image")));
            this._buttonStorageContinuouslySave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonStorageContinuouslySave.Location = new System.Drawing.Point(24, 406);
            this._buttonStorageContinuouslySave.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStorageContinuouslySave.Name = "_buttonStorageContinuouslySave";
            this._buttonStorageContinuouslySave.Size = new System.Drawing.Size(224, 48);
            this._buttonStorageContinuouslySave.TabIndex = 16;
            this._buttonStorageContinuouslySave.Text = "Save as csv...";
            this._buttonStorageContinuouslySave.UseVisualStyleBackColor = true;
            this._buttonStorageContinuouslySave.Click += new System.EventHandler(this._buttonStorageContinuouslySave_Click);
            // 
            // _buttonStorageContinuously
            // 
            this._buttonStorageContinuously.BackColor = System.Drawing.Color.LightBlue;
            this._buttonStorageContinuously.Location = new System.Drawing.Point(24, 346);
            this._buttonStorageContinuously.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStorageContinuously.Name = "_buttonStorageContinuously";
            this._buttonStorageContinuously.Size = new System.Drawing.Size(224, 48);
            this._buttonStorageContinuously.TabIndex = 13;
            this._buttonStorageContinuously.Text = "Start getting data";
            this._buttonStorageContinuously.UseVisualStyleBackColor = false;
            this._buttonStorageContinuously.Click += new System.EventHandler(this._buttonStorageContinuously_Click);
            // 
            // _labelStorageContinuously
            // 
            this._labelStorageContinuously.AutoSize = true;
            this._labelStorageContinuously.Location = new System.Drawing.Point(20, 315);
            this._labelStorageContinuously.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelStorageContinuously.Name = "_labelStorageContinuously";
            this._labelStorageContinuously.Size = new System.Drawing.Size(299, 25);
            this._labelStorageContinuously.TabIndex = 12;
            this._labelStorageContinuously.Text = "Get storage data continuously";
            // 
            // _buttonStorageSave
            // 
            this._buttonStorageSave.Image = ((System.Drawing.Image)(resources.GetObject("_buttonStorageSave.Image")));
            this._buttonStorageSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._buttonStorageSave.Location = new System.Drawing.Point(24, 242);
            this._buttonStorageSave.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStorageSave.Name = "_buttonStorageSave";
            this._buttonStorageSave.Size = new System.Drawing.Size(224, 48);
            this._buttonStorageSave.TabIndex = 11;
            this._buttonStorageSave.Text = "Save as csv...";
            this._buttonStorageSave.UseVisualStyleBackColor = true;
            this._buttonStorageSave.Click += new System.EventHandler(this._buttonStorageSave_Click);
            // 
            // _buttonStartStorage
            // 
            this._buttonStartStorage.Location = new System.Drawing.Point(24, 38);
            this._buttonStartStorage.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStartStorage.Name = "_buttonStartStorage";
            this._buttonStartStorage.Size = new System.Drawing.Size(218, 48);
            this._buttonStartStorage.TabIndex = 0;
            this._buttonStartStorage.Text = "StartStorage";
            this._buttonStartStorage.UseVisualStyleBackColor = true;
            this._buttonStartStorage.Click += new System.EventHandler(this._buttonStartStorage_Click);
            // 
            // _buttonClearStorageData
            // 
            this._buttonClearStorageData.Location = new System.Drawing.Point(484, 38);
            this._buttonClearStorageData.Margin = new System.Windows.Forms.Padding(6);
            this._buttonClearStorageData.Name = "_buttonClearStorageData";
            this._buttonClearStorageData.Size = new System.Drawing.Size(218, 48);
            this._buttonClearStorageData.TabIndex = 2;
            this._buttonClearStorageData.Text = "ClearStorageData";
            this._buttonClearStorageData.UseVisualStyleBackColor = true;
            this._buttonClearStorageData.Click += new System.EventHandler(this._buttonClearStorageData_Click);
            // 
            // _textBoxGetStorageDataReadCount
            // 
            this._textBoxGetStorageDataReadCount.Location = new System.Drawing.Point(704, 185);
            this._textBoxGetStorageDataReadCount.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxGetStorageDataReadCount.MaxLength = 5;
            this._textBoxGetStorageDataReadCount.Name = "_textBoxGetStorageDataReadCount";
            this._textBoxGetStorageDataReadCount.Size = new System.Drawing.Size(70, 31);
            this._textBoxGetStorageDataReadCount.TabIndex = 10;
            this._textBoxGetStorageDataReadCount.Text = "1";
            // 
            // _buttonStopStorage
            // 
            this._buttonStopStorage.Location = new System.Drawing.Point(254, 38);
            this._buttonStopStorage.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStopStorage.Name = "_buttonStopStorage";
            this._buttonStopStorage.Size = new System.Drawing.Size(218, 48);
            this._buttonStopStorage.TabIndex = 1;
            this._buttonStopStorage.Text = "StopStorage";
            this._buttonStopStorage.UseVisualStyleBackColor = true;
            this._buttonStopStorage.Click += new System.EventHandler(this._buttonStopStorage_Click);
            // 
            // _labelGetStorageDataReadCount
            // 
            this._labelGetStorageDataReadCount.AutoSize = true;
            this._labelGetStorageDataReadCount.Location = new System.Drawing.Point(498, 192);
            this._labelGetStorageDataReadCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelGetStorageDataReadCount.Name = "_labelGetStorageDataReadCount";
            this._labelGetStorageDataReadCount.Size = new System.Drawing.Size(186, 25);
            this._labelGetStorageDataReadCount.TabIndex = 9;
            this._labelGetStorageDataReadCount.Text = "requestDataCount";
            // 
            // _buttonGetStorageData
            // 
            this._buttonGetStorageData.Location = new System.Drawing.Point(24, 181);
            this._buttonGetStorageData.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetStorageData.Name = "_buttonGetStorageData";
            this._buttonGetStorageData.Size = new System.Drawing.Size(224, 48);
            this._buttonGetStorageData.TabIndex = 6;
            this._buttonGetStorageData.Text = "GetStorageData";
            this._buttonGetStorageData.UseVisualStyleBackColor = true;
            this._buttonGetStorageData.Click += new System.EventHandler(this._buttonGetStorageData_Click);
            // 
            // _textBoxGetStorageDataIndex
            // 
            this._textBoxGetStorageDataIndex.Location = new System.Drawing.Point(330, 185);
            this._textBoxGetStorageDataIndex.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxGetStorageDataIndex.MaxLength = 11;
            this._textBoxGetStorageDataIndex.Name = "_textBoxGetStorageDataIndex";
            this._textBoxGetStorageDataIndex.Size = new System.Drawing.Size(152, 31);
            this._textBoxGetStorageDataIndex.TabIndex = 8;
            this._textBoxGetStorageDataIndex.Text = "0";
            // 
            // _buttonGetStorageIndex
            // 
            this._buttonGetStorageIndex.Location = new System.Drawing.Point(24, 121);
            this._buttonGetStorageIndex.Margin = new System.Windows.Forms.Padding(6);
            this._buttonGetStorageIndex.Name = "_buttonGetStorageIndex";
            this._buttonGetStorageIndex.Size = new System.Drawing.Size(224, 48);
            this._buttonGetStorageIndex.TabIndex = 3;
            this._buttonGetStorageIndex.Text = "GetStorageIndex";
            this._buttonGetStorageIndex.UseVisualStyleBackColor = true;
            this._buttonGetStorageIndex.Click += new System.EventHandler(this._buttonGetStorageIndex_Click);
            // 
            // _labelGetStorageDataIndex
            // 
            this._labelGetStorageDataIndex.AutoSize = true;
            this._labelGetStorageDataIndex.Location = new System.Drawing.Point(260, 192);
            this._labelGetStorageDataIndex.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelGetStorageDataIndex.Name = "_labelGetStorageDataIndex";
            this._labelGetStorageDataIndex.Size = new System.Drawing.Size(64, 25);
            this._labelGetStorageDataIndex.TabIndex = 7;
            this._labelGetStorageDataIndex.Text = "index";
            // 
            // _groupBoxAbleTuning
            // 
            this._groupBoxAbleTuning.Controls.Add(this._comboBoxLightIntensityTuning);
            this._groupBoxAbleTuning.Controls.Add(this._labelABLETuningAmpNo);
            this._groupBoxAbleTuning.Controls.Add(this._buttonStartLightIntensityTuning);
            this._groupBoxAbleTuning.Controls.Add(this._buttonCancelLightIntensityTuning);
            this._groupBoxAbleTuning.Controls.Add(this._buttonStopLightIntensityTuning);
            this._groupBoxAbleTuning.Location = new System.Drawing.Point(844, 6);
            this._groupBoxAbleTuning.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxAbleTuning.Name = "_groupBoxAbleTuning";
            this._groupBoxAbleTuning.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxAbleTuning.Size = new System.Drawing.Size(598, 240);
            this._groupBoxAbleTuning.TabIndex = 3;
            this._groupBoxAbleTuning.TabStop = false;
            this._groupBoxAbleTuning.Text = "Light intensity tuning";
            // 
            // _comboBoxLightIntensityTuning
            // 
            this._comboBoxLightIntensityTuning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBoxLightIntensityTuning.FormattingEnabled = true;
            this._comboBoxLightIntensityTuning.Items.AddRange(new object[] {
            "HEAD 01",
            "HEAD 02",
            "HEAD 03",
            "HEAD 04",
            "HEAD 05",
            "HEAD 06"});
            this._comboBoxLightIntensityTuning.Location = new System.Drawing.Point(28, 69);
            this._comboBoxLightIntensityTuning.Margin = new System.Windows.Forms.Padding(6);
            this._comboBoxLightIntensityTuning.Name = "_comboBoxLightIntensityTuning";
            this._comboBoxLightIntensityTuning.Size = new System.Drawing.Size(178, 33);
            this._comboBoxLightIntensityTuning.TabIndex = 1;
            // 
            // _labelABLETuningAmpNo
            // 
            this._labelABLETuningAmpNo.AutoSize = true;
            this._labelABLETuningAmpNo.Location = new System.Drawing.Point(24, 38);
            this._labelABLETuningAmpNo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelABLETuningAmpNo.Name = "_labelABLETuningAmpNo";
            this._labelABLETuningAmpNo.Size = new System.Drawing.Size(138, 25);
            this._labelABLETuningAmpNo.TabIndex = 0;
            this._labelABLETuningAmpNo.Text = "Target HEAD";
            // 
            // _buttonStartLightIntensityTuning
            // 
            this._buttonStartLightIntensityTuning.Location = new System.Drawing.Point(268, 35);
            this._buttonStartLightIntensityTuning.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStartLightIntensityTuning.Name = "_buttonStartLightIntensityTuning";
            this._buttonStartLightIntensityTuning.Size = new System.Drawing.Size(304, 48);
            this._buttonStartLightIntensityTuning.TabIndex = 2;
            this._buttonStartLightIntensityTuning.Text = "StartLightIntensityTuning";
            this._buttonStartLightIntensityTuning.UseVisualStyleBackColor = true;
            this._buttonStartLightIntensityTuning.Click += new System.EventHandler(this._buttonStartLightIntensityTuning_Click);
            // 
            // _buttonCancelLightIntensityTuning
            // 
            this._buttonCancelLightIntensityTuning.Location = new System.Drawing.Point(268, 156);
            this._buttonCancelLightIntensityTuning.Margin = new System.Windows.Forms.Padding(6);
            this._buttonCancelLightIntensityTuning.Name = "_buttonCancelLightIntensityTuning";
            this._buttonCancelLightIntensityTuning.Size = new System.Drawing.Size(304, 48);
            this._buttonCancelLightIntensityTuning.TabIndex = 4;
            this._buttonCancelLightIntensityTuning.Text = "CancelLightIntensityTuning";
            this._buttonCancelLightIntensityTuning.UseVisualStyleBackColor = true;
            this._buttonCancelLightIntensityTuning.Click += new System.EventHandler(this._buttonCancelLightIntensityTuning_Click);
            // 
            // _buttonStopLightIntensityTuning
            // 
            this._buttonStopLightIntensityTuning.Location = new System.Drawing.Point(268, 96);
            this._buttonStopLightIntensityTuning.Margin = new System.Windows.Forms.Padding(6);
            this._buttonStopLightIntensityTuning.Name = "_buttonStopLightIntensityTuning";
            this._buttonStopLightIntensityTuning.Size = new System.Drawing.Size(304, 48);
            this._buttonStopLightIntensityTuning.TabIndex = 3;
            this._buttonStopLightIntensityTuning.Text = "StopLightIntensityTuning";
            this._buttonStopLightIntensityTuning.UseVisualStyleBackColor = true;
            this._buttonStopLightIntensityTuning.Click += new System.EventHandler(this._buttonStopLightIntensityTuning_Click);
            // 
            // _textBoxFourthSegment
            // 
            this._textBoxFourthSegment.Location = new System.Drawing.Point(544, 106);
            this._textBoxFourthSegment.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxFourthSegment.MaxLength = 3;
            this._textBoxFourthSegment.Name = "_textBoxFourthSegment";
            this._textBoxFourthSegment.Size = new System.Drawing.Size(52, 31);
            this._textBoxFourthSegment.TabIndex = 5;
            this._textBoxFourthSegment.Text = "1";
            this._textBoxFourthSegment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _textBoxThirdSegment
            // 
            this._textBoxThirdSegment.Location = new System.Drawing.Point(476, 106);
            this._textBoxThirdSegment.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxThirdSegment.Name = "_textBoxThirdSegment";
            this._textBoxThirdSegment.Size = new System.Drawing.Size(52, 31);
            this._textBoxThirdSegment.TabIndex = 4;
            this._textBoxThirdSegment.Text = "0";
            this._textBoxThirdSegment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _textBoxSecondSegment
            // 
            this._textBoxSecondSegment.Location = new System.Drawing.Point(408, 106);
            this._textBoxSecondSegment.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxSecondSegment.MaxLength = 3;
            this._textBoxSecondSegment.Name = "_textBoxSecondSegment";
            this._textBoxSecondSegment.Size = new System.Drawing.Size(52, 31);
            this._textBoxSecondSegment.TabIndex = 3;
            this._textBoxSecondSegment.Text = "168";
            this._textBoxSecondSegment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _textBoxFirstSegment
            // 
            this._textBoxFirstSegment.Location = new System.Drawing.Point(340, 106);
            this._textBoxFirstSegment.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxFirstSegment.MaxLength = 3;
            this._textBoxFirstSegment.Name = "_textBoxFirstSegment";
            this._textBoxFirstSegment.Size = new System.Drawing.Size(52, 31);
            this._textBoxFirstSegment.TabIndex = 2;
            this._textBoxFirstSegment.Text = "192";
            this._textBoxFirstSegment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _groupBoxCommunicationControl
            // 
            this._groupBoxCommunicationControl.Controls.Add(this.button9);
            this._groupBoxCommunicationControl.Controls.Add(this.button1);
            this._groupBoxCommunicationControl.Controls.Add(this._textBoxPortNo);
            this._groupBoxCommunicationControl.Controls.Add(this._textBoxFourthSegment);
            this._groupBoxCommunicationControl.Controls.Add(this._textBoxThirdSegment);
            this._groupBoxCommunicationControl.Controls.Add(this._textBoxSecondSegment);
            this._groupBoxCommunicationControl.Controls.Add(this._textBoxFirstSegment);
            this._groupBoxCommunicationControl.Controls.Add(this._labelPortNoBranch);
            this._groupBoxCommunicationControl.Controls.Add(this._buttonOpenUsbCommunication);
            this._groupBoxCommunicationControl.Controls.Add(this._buttonCloseCommunication);
            this._groupBoxCommunicationControl.Controls.Add(this._buttonOpenEthernetCommunication);
            this._groupBoxCommunicationControl.Controls.Add(this._pnlDeviceId);
            this._groupBoxCommunicationControl.Location = new System.Drawing.Point(24, 25);
            this._groupBoxCommunicationControl.Margin = new System.Windows.Forms.Padding(6);
            this._groupBoxCommunicationControl.Name = "_groupBoxCommunicationControl";
            this._groupBoxCommunicationControl.Padding = new System.Windows.Forms.Padding(6);
            this._groupBoxCommunicationControl.Size = new System.Drawing.Size(1152, 229);
            this._groupBoxCommunicationControl.TabIndex = 0;
            this._groupBoxCommunicationControl.TabStop = false;
            this._groupBoxCommunicationControl.Text = "Communication control";
            this._groupBoxCommunicationControl.Enter += new System.EventHandler(this._groupBoxCommunicationControl_Enter);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(358, 162);
            this.button9.Margin = new System.Windows.Forms.Padding(6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(320, 48);
            this.button9.TabIndex = 10;
            this.button9.Text = "Info";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(320, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "Height Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _textBoxPortNo
            // 
            this._textBoxPortNo.Location = new System.Drawing.Point(630, 104);
            this._textBoxPortNo.Margin = new System.Windows.Forms.Padding(6);
            this._textBoxPortNo.MaxLength = 6;
            this._textBoxPortNo.Name = "_textBoxPortNo";
            this._textBoxPortNo.Size = new System.Drawing.Size(84, 31);
            this._textBoxPortNo.TabIndex = 7;
            this._textBoxPortNo.Text = "24685";
            this._textBoxPortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _labelPortNoBranch
            // 
            this._labelPortNoBranch.AutoSize = true;
            this._labelPortNoBranch.Location = new System.Drawing.Point(608, 112);
            this._labelPortNoBranch.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labelPortNoBranch.Name = "_labelPortNoBranch";
            this._labelPortNoBranch.Size = new System.Drawing.Size(18, 25);
            this._labelPortNoBranch.TabIndex = 6;
            this._labelPortNoBranch.Text = ":";
            // 
            // _buttonClearLog
            // 
            this._buttonClearLog.Location = new System.Drawing.Point(1906, 38);
            this._buttonClearLog.Margin = new System.Windows.Forms.Padding(6);
            this._buttonClearLog.Name = "_buttonClearLog";
            this._buttonClearLog.Size = new System.Drawing.Size(94, 48);
            this._buttonClearLog.TabIndex = 1;
            this._buttonClearLog.Text = "Clear";
            this._buttonClearLog.UseVisualStyleBackColor = true;
            this._buttonClearLog.Click += new System.EventHandler(this._buttonClearLog_Click);
            // 
            // _listboxLog
            // 
            this._listboxLog.FormattingEnabled = true;
            this._listboxLog.HorizontalScrollbar = true;
            this._listboxLog.ItemHeight = 25;
            this._listboxLog.Location = new System.Drawing.Point(1188, 38);
            this._listboxLog.Margin = new System.Windows.Forms.Padding(6);
            this._listboxLog.Name = "_listboxLog";
            this._listboxLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._listboxLog.Size = new System.Drawing.Size(702, 254);
            this._listboxLog.TabIndex = 3;
            this._listboxLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this._listboxLog_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.listBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(0, 323);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2004, 1197);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(281, 683);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(237, 31);
            this.textBox4.TabIndex = 21;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(282, 627);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(238, 31);
            this.textBox3.TabIndex = 20;
            this.textBox3.Text = "0.00m";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(1724, 215);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(216, 67);
            this.button11.TabIndex = 19;
            this.button11.Text = "HOME Z";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1173, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(154, 31);
            this.textBox2.TabIndex = 18;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(833, 712);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(333, 37);
            this.label6.TabIndex = 16;
            this.label6.Text = "Extrusion Parameters:";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 25;
            this.listBox2.Location = new System.Drawing.Point(1172, 683);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(293, 129);
            this.listBox2.TabIndex = 14;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(61, 677);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(222, 37);
            this.label5.TabIndex = 13;
            this.label5.Text = "Velocity (mm):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 622);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 37);
            this.label4.TabIndex = 12;
            this.label4.Text = "Move By (mm):";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(193, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 37);
            this.label3.TabIndex = 10;
            this.label3.Text = "Manual Controls";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1430, 215);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(223, 67);
            this.button7.TabIndex = 9;
            this.button7.Text = "HOME Y";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1173, 216);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(198, 66);
            this.button6.TabIndex = 8;
            this.button6.Text = "HOME X";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(841, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 37);
            this.label2.TabIndex = 7;
            this.label2.Text = "CURRENT HEIGHT";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(841, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 37);
            this.label1.TabIndex = 6;
            this.label1.Text = "X Y Z Position";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(1172, 386);
            this.listBox1.Margin = new System.Windows.Forms.Padding(6);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(702, 254);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1172, 314);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(433, 31);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "0.00 mm";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(386, 337);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(166, 81);
            this.button5.TabIndex = 3;
            this.button5.Text = "RIGHT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(200, 450);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(192, 83);
            this.button4.TabIndex = 2;
            this.button4.Text = "ANTICLCOKWISE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(70, 337);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 81);
            this.button3.TabIndex = 1;
            this.button3.Text = "LEFT";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(200, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 83);
            this.button2.TabIndex = 0;
            this.button2.Text = "CLOCKWISE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(34, 245);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(229, 48);
            this.button10.TabIndex = 17;
            this.button10.Text = "Open File";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button8.Location = new System.Drawing.Point(401, 257);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(313, 52);
            this.button8.TabIndex = 18;
            this.button8.Text = "Start Print";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(936, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 37);
            this.label7.TabIndex = 22;
            this.label7.Text = "DIAMETER";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(905, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 37);
            this.label8.TabIndex = 23;
            this.label8.Text = "Y-DISTANCE";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(1173, 106);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(154, 31);
            this.textBox5.TabIndex = 24;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2016, 1519);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._listboxLog);
            this.Controls.Add(this._buttonClearLog);
            this.Controls.Add(this._tabControlCommand);
            this.Controls.Add(this._groupBoxCommunicationControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "3D BIOPRINTING - LIMR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this._pnlDeviceId.ResumeLayout(false);
            this._pnlDeviceId.PerformLayout();
            this._tabControlCommand.ResumeLayout(false);
            this._tabPageMeasurementControl.ResumeLayout(false);
            this._groupBoxGetData.ResumeLayout(false);
            this._groupBoxGetData.PerformLayout();
            this._groupBoxPeakNo.ResumeLayout(false);
            this._groupBoxPeakNo.PerformLayout();
            this._groupBoxLockPanel.ResumeLayout(false);
            this._groupBoxLockPanel.PerformLayout();
            this._groupBoxProgramNo.ResumeLayout(false);
            this._groupBoxProgramNo.PerformLayout();
            this._groupBoxTiming.ResumeLayout(false);
            this._groupBoxTiming.PerformLayout();
            this._groupBoxTimingOutMulti.ResumeLayout(false);
            this._groupBoxTimingOutMulti.PerformLayout();
            this._groupBoxReset.ResumeLayout(false);
            this._groupBoxReset.PerformLayout();
            this._groupBoxResetOutMulti.ResumeLayout(false);
            this._groupBoxResetOutMulti.PerformLayout();
            this._groupBoxZero.ResumeLayout(false);
            this._groupBoxZero.PerformLayout();
            this._groupBoxAutoZeroMultiOut.ResumeLayout(false);
            this._groupBoxAutoZeroMultiOut.PerformLayout();
            this._tabPageSettingRW.ResumeLayout(false);
            this._groupBoxProgramSettings.ResumeLayout(false);
            this._groupBoxProgramSettings.PerformLayout();
            this._groupBoxHeadSettings.ResumeLayout(false);
            this._groupBoxHeadSettings.PerformLayout();
            this._groupBoxOutSettings.ResumeLayout(false);
            this._groupBoxOutSettings.PerformLayout();
            this._tabPageSettingBatch.ResumeLayout(false);
            this._tabPageSettingBatch.PerformLayout();
            this._groupBoxEncoderSetting.ResumeLayout(false);
            this._groupBoxByProgram.ResumeLayout(false);
            this._groupBoxByProgram.PerformLayout();
            this._groupBoxAllSettings.ResumeLayout(false);
            this._tabPageDataStorage.ResumeLayout(false);
            this._groupBoxDll.ResumeLayout(false);
            this._groupBoxOther.ResumeLayout(false);
            this._groupBoxOther.PerformLayout();
            this._groupBoxSystem.ResumeLayout(false);
            this._groupBoxEncoder.ResumeLayout(false);
            this._groupBoxModeChange.ResumeLayout(false);
            this._groupBoxCalibration.ResumeLayout(false);
            this._groupBoxCalibration.PerformLayout();
            this._groupBoxDataStorage.ResumeLayout(false);
            this._groupBoxDataStorage.PerformLayout();
            this._groupBoxAbleTuning.ResumeLayout(false);
            this._groupBoxAbleTuning.PerformLayout();
            this._groupBoxCommunicationControl.ResumeLayout(false);
            this._groupBoxCommunicationControl.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.Button _buttonCloseCommunication;
        private System.Windows.Forms.Button _buttonOpenEthernetCommunication;
        private System.Windows.Forms.Button _buttonOpenUsbCommunication;
        private System.Windows.Forms.Button _buttonAutoZeroGroup;
        private System.Windows.Forms.Button _buttonAutoZeroSingle;
        private System.Windows.Forms.Button _buttonAutoZeroMulti;
        private System.Windows.Forms.Button _buttonTimingSingle;
        private System.Windows.Forms.Button _buttonTimingMulti;
        private System.Windows.Forms.Button _buttonTimingGroup;
        private System.Windows.Forms.Button _buttonResetSingle;
        private System.Windows.Forms.Button _buttonResetMulti;
        private System.Windows.Forms.Button _buttonResetGroup;
        private System.Windows.Forms.Button _buttonSwitchProgram;
        private System.Windows.Forms.Button _buttonGetProgramNo;
        private System.Windows.Forms.Panel _pnlDeviceId;
        private System.Windows.Forms.Label _labelStatementTitle;
        private System.Windows.Forms.Label _labelDeviceStatus2;
        private System.Windows.Forms.Label _labelDeviceStatus1;
        private System.Windows.Forms.Label _labelDeviceStatus0;
        private System.Windows.Forms.Label _labelIdTitle;
        private System.Windows.Forms.RadioButton _radioButtonDevice0;
        private System.Windows.Forms.RadioButton _radioButtonDevice1;
        private System.Windows.Forms.RadioButton _radioButtonDevice2;
        private System.Windows.Forms.TabControl _tabControlCommand;
        private System.Windows.Forms.TabPage _tabPageMeasurementControl;
        private System.Windows.Forms.TabPage _tabPageSettingRW;
        private System.Windows.Forms.TextBox _textBoxFourthSegment;
        private System.Windows.Forms.TextBox _textBoxThirdSegment;
        private System.Windows.Forms.TextBox _textBoxSecondSegment;
        private System.Windows.Forms.TextBox _textBoxFirstSegment;
        private System.Windows.Forms.GroupBox _groupBoxCommunicationControl;
        private System.Windows.Forms.TextBox _textBoxPortNo;
        private System.Windows.Forms.Label _labelPortNoBranch;
        private System.Windows.Forms.Button _buttonGetSystemConfiguration;
        private System.Windows.Forms.Button _buttonReturnToFactoryDefaultSetting;
        private System.Windows.Forms.Button _buttonTransitToSettingMode;
        private System.Windows.Forms.Button _buttonTransitToMeasurementMode;
        private System.Windows.Forms.GroupBox _groupBoxZero;
        private System.Windows.Forms.ComboBox _comboBoxAutoZeroGroupOnOff;
        private System.Windows.Forms.Label _labelAutoZeroGroupOnOff;
        private System.Windows.Forms.ComboBox _comboBoxAutoZeroMultiOnOff;
        private System.Windows.Forms.ComboBox _comboBoxAutoZeroSingleOnOff;
        private System.Windows.Forms.Label _labelAutoZeroMultiOnOff;
        private System.Windows.Forms.Label _labelAutoZeroSingleOnOff;
        private System.Windows.Forms.GroupBox _groupBoxAutoZeroMultiOut;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo8;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo7;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo6;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo5;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo4;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo3;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo2;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroMultiOutNo1;
        private System.Windows.Forms.ComboBox _comboBoxAutoZeroSingleOutNo;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroGroup2;
        private System.Windows.Forms.CheckBox _checkBoxAutoZeroGroup1;
        private System.Windows.Forms.GroupBox _groupBoxReset;
        private System.Windows.Forms.CheckBox _checkBoxResetOutGroup2;
        private System.Windows.Forms.CheckBox _checkBoxResetOutGroup1;
        private System.Windows.Forms.GroupBox _groupBoxResetOutMulti;
        private System.Windows.Forms.CheckBox _checkBoxResetOut8;
        private System.Windows.Forms.CheckBox _checkBoxResetOut7;
        private System.Windows.Forms.CheckBox _checkBoxResetOut6;
        private System.Windows.Forms.CheckBox _checkBoxResetOut5;
        private System.Windows.Forms.CheckBox _checkBoxResetOut4;
        private System.Windows.Forms.CheckBox _checkBoxResetOut3;
        private System.Windows.Forms.CheckBox _checkBoxResetOut2;
        private System.Windows.Forms.CheckBox _checkBoxResetOut1;
        private System.Windows.Forms.ComboBox _comboBoxResetOutNo;
        private System.Windows.Forms.GroupBox _groupBoxTiming;
        private System.Windows.Forms.CheckBox _checkBoxTimingOutGroup2;
        private System.Windows.Forms.CheckBox _checkBoxTimingOutGroup1;
        private System.Windows.Forms.ComboBox _comboBoxTimingOutGroupValue;
        private System.Windows.Forms.Label _labelTimingGroupOutValue;
        private System.Windows.Forms.ComboBox _comboBoxTimingMultiOutValue;
        private System.Windows.Forms.ComboBox _comboBoxTimingSingleOnOff;
        private System.Windows.Forms.Label _labelTimingMultiOutValue;
        private System.Windows.Forms.Label _labelTimingSingleOnOff;
        private System.Windows.Forms.GroupBox _groupBoxTimingOutMulti;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut8;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut7;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut6;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut5;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut4;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut3;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut2;
        private System.Windows.Forms.CheckBox _checkBoxTimingOut1;
        private System.Windows.Forms.ComboBox _comboBoxTimingSingleOutNo;
        private System.Windows.Forms.GroupBox _groupBoxProgramNo;
        private System.Windows.Forms.ComboBox _comboBoxSwitchProgramProgramNo;
        private System.Windows.Forms.Label _labelSwitchProgramProgramNo;
        private System.Windows.Forms.Button _buttonMeasurementControl;
        private System.Windows.Forms.Button _buttonLightControl;
        private System.Windows.Forms.GroupBox _groupBoxAbleTuning;
        private System.Windows.Forms.Label _labelABLETuningAmpNo;
        private System.Windows.Forms.Button _buttonStartLightIntensityTuning;
        private System.Windows.Forms.Button _buttonCancelLightIntensityTuning;
        private System.Windows.Forms.Button _buttonStopLightIntensityTuning;
        private System.Windows.Forms.GroupBox _groupBoxCalibration;
        private System.Windows.Forms.Label _labelCalibrationAmpNo;
        private System.Windows.Forms.Button _buttonCancelCalibration;
        private System.Windows.Forms.Button _buttonStopCalibration;
        private System.Windows.Forms.Button _buttonStartCalibration;
        private System.Windows.Forms.Button _buttonGetMeasurementData;
        private System.Windows.Forms.Button _buttonGetTerminalStatus;
        private System.Windows.Forms.Button _buttonGetTrendIndex;
        private System.Windows.Forms.GroupBox _groupBoxGetData;
        private System.Windows.Forms.TextBox _textBoxTrendRequestDataCount;
        private System.Windows.Forms.Label _labelGetTrendDataReadCount;
        private System.Windows.Forms.TextBox _textBoxTrendIndex;
        private System.Windows.Forms.Label _labelGetTrendDataIndex;
        private System.Windows.Forms.Button _buttonGetTrendData;
        private System.Windows.Forms.ComboBox _comboBoxSetLaserControlValue;
        private System.Windows.Forms.Label _labelSetLaserControlValue;
        private System.Windows.Forms.ComboBox _comboBoxSetMeasureEnableValue;
        private System.Windows.Forms.Label _labelSetMeasureEnableValue;
        private System.Windows.Forms.GroupBox _groupBoxSystem;
        private System.Windows.Forms.GroupBox _groupBoxModeChange;
        private System.Windows.Forms.Label _labelSettingProgramNo;
        private System.Windows.Forms.Label _labelSettingHead;
        private System.Windows.Forms.ComboBox _comboBoxSettingProgramNo;
        private System.Windows.Forms.ComboBox _comboBoxSettingOut;
        private System.Windows.Forms.Label _labelOut;
        private System.Windows.Forms.ComboBox _comboBoxSettingHead;
        private System.Windows.Forms.Button _buttonGetSamplingCycle;
        private System.Windows.Forms.Button _buttonSetSamplingCycle;
        private System.Windows.Forms.Label _labelSamplingCycle;
        private System.Windows.Forms.Button _buttonGetAmbientLightFilter;
        private System.Windows.Forms.Button _buttonSetAmbientLightFilter;
        private System.Windows.Forms.Label _labelAmbientLightFilter;
        private System.Windows.Forms.Button _buttonGetMutualInterferencePrevention;
        private System.Windows.Forms.Button _buttonSetMutualInterferencePrevention;
        private System.Windows.Forms.Label _labelMutualInterferencePrevention;
        private System.Windows.Forms.Button _buttonGetMedianFilter;
        private System.Windows.Forms.Button _buttonSetMedianFilter;
        private System.Windows.Forms.Label _labelMedianFilter;
        private System.Windows.Forms.Label _labelThreshold;
        private System.Windows.Forms.Button _buttonGetThreshold;
        private System.Windows.Forms.Button _buttonSetThreshold;
        private System.Windows.Forms.Button _buttonGetMask;
        private System.Windows.Forms.Button _buttonSetMask;
        private System.Windows.Forms.Label _labelMask;
        private System.Windows.Forms.Button _buttonGetLightIntensityControl;
        private System.Windows.Forms.Button _buttonSetLightIntensityControl;
        private System.Windows.Forms.Label _labelLightIntensityControl;
        private System.Windows.Forms.Button _buttonGetPeakShapeFilter;
        private System.Windows.Forms.Button _buttonSetPeakShapeFilter;
        private System.Windows.Forms.Label _labelPeakShapeFilter;
        private System.Windows.Forms.Button _buttonGetScaling;
        private System.Windows.Forms.Button _buttonSetScaling;
        private System.Windows.Forms.Label _labelScaling;
        private System.Windows.Forms.Button _buttonGetOffset;
        private System.Windows.Forms.Button _buttonSetOffset;
        private System.Windows.Forms.Label _labelOffset;
        private System.Windows.Forms.Button _buttonGetTolerance;
        private System.Windows.Forms.Button _buttonSetTolerance;
        private System.Windows.Forms.Label _labelTolerance;
        private System.Windows.Forms.Button _buttonGetFilter;
        private System.Windows.Forms.Button _buttonSetFilter;
        private System.Windows.Forms.Label _labelFilter;
        private System.Windows.Forms.Button _buttonGetHold;
        private System.Windows.Forms.Button _buttonSetHold;
        private System.Windows.Forms.Label _labelHold;
        private System.Windows.Forms.Button _buttonGetInvalidDataProcessing;
        private System.Windows.Forms.Button _buttonSetInvalidDataProcessing;
        private System.Windows.Forms.Label _labelInvalidDataProcessing;
        private System.Windows.Forms.Button _buttonGetDisplayUnit;
        private System.Windows.Forms.Button _buttonSetDisplayUnit;
        private System.Windows.Forms.Label _labelDisplayUnit;
        private System.Windows.Forms.Button _buttonGetJudgmentOutput;
        private System.Windows.Forms.Button _buttonSetJudgmentOutput;
        private System.Windows.Forms.Label _labelJudgmentOutput;
        private System.Windows.Forms.Button _buttonGetTerminalAllocation;
        private System.Windows.Forms.Button _buttonSetTerminalAllocation;
        private System.Windows.Forms.Label _labelTerminalAllocation;
        private System.Windows.Forms.Button _buttonGetStorageTarget;
        private System.Windows.Forms.Button _buttonSetStorageTarget;
        private System.Windows.Forms.Label _labelStorageTarget;
        private System.Windows.Forms.Button _buttonGetPulseCount;
        private System.Windows.Forms.Button _buttonGetLightIntensityIntegration;
        private System.Windows.Forms.Button _buttonSetLightIntensityIntegration;
        private System.Windows.Forms.Label _labelLightIntensityIntegration;
        private System.Windows.Forms.Button _buttonGetMeasurementPeaks;
        private System.Windows.Forms.Button _buttonSetMeasurementPeaks;
        private System.Windows.Forms.Label _labelMeasurementPeaks;
        private System.Windows.Forms.Button _buttonGetCheckingNumberOfPeaks;
        private System.Windows.Forms.Button _buttonSetCheckingNumberOfPeaks;
        private System.Windows.Forms.Label _labelCheckingNumberOfPeaks;
        private System.Windows.Forms.Button _buttonGetMultiLightIntensityControl;
        private System.Windows.Forms.Button _buttonSetMultiLightIntensityControl;
        private System.Windows.Forms.Label _labelMultiLightIntensityControl;
        private System.Windows.Forms.Button _buttonGetQuadProcessing;
        private System.Windows.Forms.Button _buttonSetQuadProcessing;
        private System.Windows.Forms.Label _labelQuadProcessing;
        private System.Windows.Forms.Button _buttonGetRefractiveIndexCorrection;
        private System.Windows.Forms.Button _buttonSetRefractiveIndexCorrection;
        private System.Windows.Forms.Label _labelRefractiveIndexCorrection;
        private System.Windows.Forms.Button _buttonGetStorageNumber;
        private System.Windows.Forms.Button _buttonSetStorageNumber;
        private System.Windows.Forms.Label _labelStorageNumber;
        private System.Windows.Forms.Button _buttonGetLightWaveform;
        private System.Windows.Forms.GroupBox _groupBoxLockPanel;
        private System.Windows.Forms.Button _buttonLockPanel;
        private System.Windows.Forms.Button _buttonResetPulseCount;
        private System.Windows.Forms.Button _buttonGetProgram;
        private System.Windows.Forms.Button _buttonSetProgram;
        private System.Windows.Forms.Button _buttonGetSettings;
        private System.Windows.Forms.Button _buttonSetSettings;
        private System.Windows.Forms.GroupBox _groupBoxDll;
        private System.Windows.Forms.Button _buttonGetVersion;
        private System.Windows.Forms.ComboBox _comboBoxLockPanelOnOff;
        private System.Windows.Forms.Label _labelLockPanelOnOff;
        private System.Windows.Forms.ComboBox _comboBoxGetLightWaveformHead;
        private System.Windows.Forms.Label _labelGetLightWaveformHead;
        private System.Windows.Forms.Button _buttonTrendContinuouslySave;
        private System.Windows.Forms.Button _buttonStartGettingData;
        private System.Windows.Forms.Label _labelTrendDataCount;
        private System.Windows.Forms.TextBox _textBoxGettingDataCount;
        private System.Windows.Forms.Button _buttonSaveAsTrendData;
        private System.Windows.Forms.TabPage _tabPageDataStorage;
        private System.Windows.Forms.GroupBox _groupBoxDataStorage;
        private System.Windows.Forms.Button _buttonStartStorage;
        private System.Windows.Forms.Button _buttonClearStorageData;
        private System.Windows.Forms.TextBox _textBoxGetStorageDataReadCount;
        private System.Windows.Forms.Button _buttonStopStorage;
        private System.Windows.Forms.Label _labelGetStorageDataReadCount;
        private System.Windows.Forms.Button _buttonGetStorageData;
        private System.Windows.Forms.TextBox _textBoxGetStorageDataIndex;
        private System.Windows.Forms.Button _buttonGetStorageIndex;
        private System.Windows.Forms.Label _labelGetStorageDataIndex;
        private System.Windows.Forms.TextBox _textBoxStorageDataCount;
        private System.Windows.Forms.Label _labelStorageDataCount;
        private System.Windows.Forms.Button _buttonStorageContinuouslySave;
        private System.Windows.Forms.Button _buttonStorageContinuously;
        private System.Windows.Forms.Label _labelStorageContinuously;
        private System.Windows.Forms.Button _buttonStorageSave;
        private System.Windows.Forms.GroupBox _groupBoxEncoder;
        private System.Windows.Forms.GroupBox _groupBoxOther;
        private System.Windows.Forms.GroupBox _groupBoxProgramSettings;
        private System.Windows.Forms.GroupBox _groupBoxHeadSettings;
        private System.Windows.Forms.GroupBox _groupBoxOutSettings;
        private System.Windows.Forms.TabPage _tabPageSettingBatch;
        private System.Windows.Forms.Button _buttonSaveLightWave;
        private System.Windows.Forms.Label _labelTrendDataContinuously;
        private System.Windows.Forms.Label _labelSetNote;
        private System.Windows.Forms.Label _labelGetNote;
        private System.Windows.Forms.GroupBox _groupBoxAllSettings;
        private System.Windows.Forms.GroupBox _groupBoxByProgram;
        private System.Windows.Forms.ComboBox _comboBoxTargetProgramNo;
        private System.Windows.Forms.Label _labelTargetProgramNo;
        private System.Windows.Forms.Button _buttonClearLog;
        private System.Windows.Forms.ComboBox _comboBoxCalibration;
        private System.Windows.Forms.ComboBox _comboBoxLightIntensityTuning;
        private System.Windows.Forms.Label _labelSelectedIndex;
        private System.Windows.Forms.ComboBox _comboBoxSelectedIndex;
        private System.Windows.Forms.GroupBox _groupBoxPeakNo;
        private System.Windows.Forms.CheckBox _checkBoxGetLightWaveformPeakNo4;
        private System.Windows.Forms.CheckBox _checkBoxGetLightWaveformPeakNo3;
        private System.Windows.Forms.CheckBox _checkBoxGetLightWaveformPeakNo2;
        private System.Windows.Forms.CheckBox _checkBoxGetLightWaveformPeakNo1;
        private System.Windows.Forms.Label _labelMeasurementMethod;
        private System.Windows.Forms.Button _buttonSetMeasurementMethod;
        private System.Windows.Forms.Button _buttonGetMeasurementMethod;
        private System.Windows.Forms.Button _buttonGetStorageTiming;
        private System.Windows.Forms.Button _buttonSetStorageTiming;
        private System.Windows.Forms.Label _labelStorageTiming;
        private System.Windows.Forms.ListBox _listboxLog;
        private System.Windows.Forms.Button _buttonSetHighSensitivity;
        private System.Windows.Forms.Button _buttonGetHighSensitivity;
        private System.Windows.Forms.Label _labelHighSensitivity;
        private System.Windows.Forms.Label _labelAnalogOutputScaling;
        private System.Windows.Forms.Button _buttonGetAnalogOutputScaling;
        private System.Windows.Forms.Button _buttonSetAnalogOutputScaling;
        private System.Windows.Forms.Label _labelAnalogOutAllocation;
        private System.Windows.Forms.Button _buttonGetAnalogOutAllocation;
        private System.Windows.Forms.Button _buttonSetAnalogOutAllocation;
        private System.Windows.Forms.GroupBox _groupBoxEncoderSetting;
        private System.Windows.Forms.Button _buttonSetEncoderSetting;
        private System.Windows.Forms.Button _buttonGetEncoderSetting;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label8;
    }
}

