namespace Binomics_Labs_Software_Suite
{
    partial class IDMSimulator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbPortsList = new System.Windows.Forms.ComboBox();
            this.txtDNA = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnLoadTextbox = new System.Windows.Forms.Button();
            this.checkHardwareRNG = new System.Windows.Forms.CheckBox();
            this.checkSoftwareRNG = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picEntropy = new System.Windows.Forms.PictureBox();
            this.btnGlue = new System.Windows.Forms.Button();
            this.btnShred = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnGenerateRandomDNA = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRawData = new System.Windows.Forms.RichTextBox();
            this.txtDNALength = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblShredderTotalBases = new System.Windows.Forms.Label();
            this.lblShredderFragmentCount = new System.Windows.Forms.Label();
            this.lblFragmentLengthAverage = new System.Windows.Forms.Label();
            this.lblShredderCutPosition = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblStatusUpdate = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picAbstractor = new System.Windows.Forms.PictureBox();
            this.trackBarFilterSize = new System.Windows.Forms.TrackBar();
            this.btnAbstractorVisualize = new System.Windows.Forms.Button();
            this.btnAbstractorSave = new System.Windows.Forms.Button();
            this.btnAbstractorLoad = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAbstractorWindowSize = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEntropy)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAbstractor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFilterSize)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPortsList
            // 
            this.cmbPortsList.Enabled = false;
            this.cmbPortsList.FormattingEnabled = true;
            this.cmbPortsList.Location = new System.Drawing.Point(279, 2);
            this.cmbPortsList.Name = "cmbPortsList";
            this.cmbPortsList.Size = new System.Drawing.Size(120, 33);
            this.cmbPortsList.TabIndex = 0;
            this.cmbPortsList.DropDown += new System.EventHandler(this.cmbPortsList_DropDown);
            this.cmbPortsList.SelectedIndexChanged += new System.EventHandler(this.cmbPortsList_SelectedIndexChanged);
            // 
            // txtDNA
            // 
            this.txtDNA.Location = new System.Drawing.Point(423, 234);
            this.txtDNA.Name = "txtDNA";
            this.txtDNA.Size = new System.Drawing.Size(444, 206);
            this.txtDNA.TabIndex = 1;
            this.txtDNA.Text = "\n\n\n\n             add your DNA sequence here...";
            this.txtDNA.Enter += new System.EventHandler(this.txtDNA_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(891, 708);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnLoadTextbox);
            this.tabPage1.Controls.Add(this.checkHardwareRNG);
            this.tabPage1.Controls.Add(this.checkSoftwareRNG);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.btnGlue);
            this.tabPage1.Controls.Add(this.btnShred);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnLoadFile);
            this.tabPage1.Controls.Add(this.btnGenerateRandomDNA);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtRawData);
            this.tabPage1.Controls.Add(this.txtDNALength);
            this.tabPage1.Controls.Add(this.cmbPortsList);
            this.tabPage1.Controls.Add(this.txtDNA);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(875, 661);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Simulation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnLoadTextbox
            // 
            this.btnLoadTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadTextbox.Location = new System.Drawing.Point(217, 234);
            this.btnLoadTextbox.Name = "btnLoadTextbox";
            this.btnLoadTextbox.Size = new System.Drawing.Size(200, 100);
            this.btnLoadTextbox.TabIndex = 18;
            this.btnLoadTextbox.Text = "L O A D\r\nT E X T";
            this.btnLoadTextbox.UseVisualStyleBackColor = true;
            this.btnLoadTextbox.Click += new System.EventHandler(this.btnLoadTextbox_Click);
            // 
            // checkHardwareRNG
            // 
            this.checkHardwareRNG.AutoSize = true;
            this.checkHardwareRNG.Location = new System.Drawing.Point(6, 6);
            this.checkHardwareRNG.Name = "checkHardwareRNG";
            this.checkHardwareRNG.Size = new System.Drawing.Size(188, 29);
            this.checkHardwareRNG.TabIndex = 17;
            this.checkHardwareRNG.Text = "Hardware RNG";
            this.checkHardwareRNG.UseVisualStyleBackColor = true;
            this.checkHardwareRNG.CheckedChanged += new System.EventHandler(this.checkHardwareRNG_CheckedChanged);
            // 
            // checkSoftwareRNG
            // 
            this.checkSoftwareRNG.AutoSize = true;
            this.checkSoftwareRNG.Checked = true;
            this.checkSoftwareRNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSoftwareRNG.Location = new System.Drawing.Point(6, 41);
            this.checkSoftwareRNG.Name = "checkSoftwareRNG";
            this.checkSoftwareRNG.Size = new System.Drawing.Size(180, 29);
            this.checkSoftwareRNG.TabIndex = 16;
            this.checkSoftwareRNG.Text = "Software RNG";
            this.checkSoftwareRNG.UseVisualStyleBackColor = true;
            this.checkSoftwareRNG.CheckedChanged += new System.EventHandler(this.checkSoftwareRNG_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.picEntropy);
            this.panel1.Location = new System.Drawing.Point(423, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 210);
            this.panel1.TabIndex = 15;
            // 
            // picEntropy
            // 
            this.picEntropy.BackColor = System.Drawing.Color.Black;
            this.picEntropy.Location = new System.Drawing.Point(3, 3);
            this.picEntropy.Name = "picEntropy";
            this.picEntropy.Size = new System.Drawing.Size(400, 210);
            this.picEntropy.TabIndex = 4;
            this.picEntropy.TabStop = false;
            // 
            // btnGlue
            // 
            this.btnGlue.Enabled = false;
            this.btnGlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGlue.Location = new System.Drawing.Point(6, 552);
            this.btnGlue.Name = "btnGlue";
            this.btnGlue.Size = new System.Drawing.Size(411, 100);
            this.btnGlue.TabIndex = 14;
            this.btnGlue.Text = "G L U E";
            this.btnGlue.UseVisualStyleBackColor = true;
            this.btnGlue.Click += new System.EventHandler(this.btnGlue_Click);
            // 
            // btnShred
            // 
            this.btnShred.Enabled = false;
            this.btnShred.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShred.Location = new System.Drawing.Point(6, 446);
            this.btnShred.Name = "btnShred";
            this.btnShred.Size = new System.Drawing.Size(411, 100);
            this.btnShred.TabIndex = 13;
            this.btnShred.Text = "S H R E D";
            this.btnShred.UseVisualStyleBackColor = true;
            this.btnShred.Click += new System.EventHandler(this.btnShred_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(6, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(411, 100);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "S A V E";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadFile.Location = new System.Drawing.Point(6, 234);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(200, 100);
            this.btnLoadFile.TabIndex = 11;
            this.btnLoadFile.Text = "L O A D\r\n F I L E\r\n";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnGenerateRandomDNA
            // 
            this.btnGenerateRandomDNA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateRandomDNA.Location = new System.Drawing.Point(6, 128);
            this.btnGenerateRandomDNA.Name = "btnGenerateRandomDNA";
            this.btnGenerateRandomDNA.Size = new System.Drawing.Size(411, 100);
            this.btnGenerateRandomDNA.TabIndex = 10;
            this.btnGenerateRandomDNA.Text = "G E N E R A T E";
            this.btnGenerateRandomDNA.UseVisualStyleBackColor = true;
            this.btnGenerateRandomDNA.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Desired DNA Length:";
            // 
            // txtRawData
            // 
            this.txtRawData.Location = new System.Drawing.Point(423, 446);
            this.txtRawData.Name = "txtRawData";
            this.txtRawData.Size = new System.Drawing.Size(444, 206);
            this.txtRawData.TabIndex = 3;
            this.txtRawData.Text = "\n\n\n\n            Random Raw Data appears here...";
            this.txtRawData.Enter += new System.EventHandler(this.txtRawData_Enter);
            // 
            // txtDNALength
            // 
            this.txtDNALength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDNALength.Location = new System.Drawing.Point(223, 79);
            this.txtDNALength.Name = "txtDNALength";
            this.txtDNALength.Size = new System.Drawing.Size(162, 38);
            this.txtDNALength.TabIndex = 2;
            this.txtDNALength.Text = "10000";
            this.txtDNALength.TextChanged += new System.EventHandler(this.txtDNALength_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblShredderTotalBases);
            this.tabPage2.Controls.Add(this.lblShredderFragmentCount);
            this.tabPage2.Controls.Add(this.lblFragmentLengthAverage);
            this.tabPage2.Controls.Add(this.lblShredderCutPosition);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(875, 661);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Statistics";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblShredderTotalBases
            // 
            this.lblShredderTotalBases.AutoSize = true;
            this.lblShredderTotalBases.Location = new System.Drawing.Point(139, 139);
            this.lblShredderTotalBases.Name = "lblShredderTotalBases";
            this.lblShredderTotalBases.Size = new System.Drawing.Size(113, 25);
            this.lblShredderTotalBases.TabIndex = 14;
            this.lblShredderTotalBases.Text = "STB#####";
            // 
            // lblShredderFragmentCount
            // 
            this.lblShredderFragmentCount.AutoSize = true;
            this.lblShredderFragmentCount.Location = new System.Drawing.Point(274, 99);
            this.lblShredderFragmentCount.Name = "lblShredderFragmentCount";
            this.lblShredderFragmentCount.Size = new System.Drawing.Size(114, 25);
            this.lblShredderFragmentCount.TabIndex = 13;
            this.lblShredderFragmentCount.Text = "SFC#####";
            // 
            // lblFragmentLengthAverage
            // 
            this.lblFragmentLengthAverage.AutoSize = true;
            this.lblFragmentLengthAverage.Location = new System.Drawing.Point(508, 99);
            this.lblFragmentLengthAverage.Name = "lblFragmentLengthAverage";
            this.lblFragmentLengthAverage.Size = new System.Drawing.Size(98, 25);
            this.lblFragmentLengthAverage.TabIndex = 12;
            this.lblFragmentLengthAverage.Text = "LA#####";
            // 
            // lblShredderCutPosition
            // 
            this.lblShredderCutPosition.AutoSize = true;
            this.lblShredderCutPosition.Location = new System.Drawing.Point(137, 99);
            this.lblShredderCutPosition.Name = "lblShredderCutPosition";
            this.lblShredderCutPosition.Size = new System.Drawing.Size(115, 25);
            this.lblShredderCutPosition.TabIndex = 11;
            this.lblShredderCutPosition.Text = "SCP#####";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.richTextBox2);
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Controls.Add(this.pictureBox1);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(875, 661);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Alignment";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(238, 569);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(400, 89);
            this.button3.TabIndex = 10;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(643, 569);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(229, 89);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 89);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(644, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(228, 560);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "\n\n\n\n             add your DNA sequence here...";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(229, 560);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "\n\n\n\n             add your DNA sequence here...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(238, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 560);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblStatusUpdate
            // 
            this.lblStatusUpdate.AutoSize = true;
            this.lblStatusUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusUpdate.Location = new System.Drawing.Point(5, 730);
            this.lblStatusUpdate.Name = "lblStatusUpdate";
            this.lblStatusUpdate.Size = new System.Drawing.Size(577, 31);
            this.lblStatusUpdate.TabIndex = 11;
            this.lblStatusUpdate.Text = "Hi, please generate or load DNA sequence.";
            this.lblStatusUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.txtAbstractorWindowSize);
            this.tabPage4.Controls.Add(this.btnAbstractorLoad);
            this.tabPage4.Controls.Add(this.btnAbstractorSave);
            this.tabPage4.Controls.Add(this.btnAbstractorVisualize);
            this.tabPage4.Controls.Add(this.trackBarFilterSize);
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(875, 661);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Visual Abstractor";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.picAbstractor);
            this.panel2.Location = new System.Drawing.Point(99, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 666);
            this.panel2.TabIndex = 16;
            // 
            // picAbstractor
            // 
            this.picAbstractor.BackColor = System.Drawing.Color.Black;
            this.picAbstractor.Location = new System.Drawing.Point(3, 3);
            this.picAbstractor.Name = "picAbstractor";
            this.picAbstractor.Size = new System.Drawing.Size(400, 649);
            this.picAbstractor.TabIndex = 4;
            this.picAbstractor.TabStop = false;
            // 
            // trackBarFilterSize
            // 
            this.trackBarFilterSize.LargeChange = 1000;
            this.trackBarFilterSize.Location = new System.Drawing.Point(3, 3);
            this.trackBarFilterSize.Maximum = 100000;
            this.trackBarFilterSize.Minimum = 20;
            this.trackBarFilterSize.Name = "trackBarFilterSize";
            this.trackBarFilterSize.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarFilterSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarFilterSize.Size = new System.Drawing.Size(90, 655);
            this.trackBarFilterSize.SmallChange = 200;
            this.trackBarFilterSize.TabIndex = 17;
            this.trackBarFilterSize.TickFrequency = 1000;
            this.trackBarFilterSize.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarFilterSize.Value = 200;
            this.trackBarFilterSize.Scroll += new System.EventHandler(this.TrackBarFilterSize_Scroll);
            // 
            // btnAbstractorVisualize
            // 
            this.btnAbstractorVisualize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbstractorVisualize.Location = new System.Drawing.Point(549, 249);
            this.btnAbstractorVisualize.Name = "btnAbstractorVisualize";
            this.btnAbstractorVisualize.Size = new System.Drawing.Size(323, 100);
            this.btnAbstractorVisualize.TabIndex = 18;
            this.btnAbstractorVisualize.Text = "V I S U A L I Z E";
            this.btnAbstractorVisualize.UseVisualStyleBackColor = true;
            this.btnAbstractorVisualize.Click += new System.EventHandler(this.BtnAbstractorVisualize_Click);
            // 
            // btnAbstractorSave
            // 
            this.btnAbstractorSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbstractorSave.Location = new System.Drawing.Point(549, 355);
            this.btnAbstractorSave.Name = "btnAbstractorSave";
            this.btnAbstractorSave.Size = new System.Drawing.Size(323, 100);
            this.btnAbstractorSave.TabIndex = 19;
            this.btnAbstractorSave.Text = "S A V E";
            this.btnAbstractorSave.UseVisualStyleBackColor = true;
            this.btnAbstractorSave.Click += new System.EventHandler(this.BtnAbstractorSave_Click);
            // 
            // btnAbstractorLoad
            // 
            this.btnAbstractorLoad.Enabled = false;
            this.btnAbstractorLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbstractorLoad.Location = new System.Drawing.Point(549, 143);
            this.btnAbstractorLoad.Name = "btnAbstractorLoad";
            this.btnAbstractorLoad.Size = new System.Drawing.Size(323, 100);
            this.btnAbstractorLoad.TabIndex = 20;
            this.btnAbstractorLoad.Text = "L O A D";
            this.btnAbstractorLoad.UseVisualStyleBackColor = true;
            this.btnAbstractorLoad.Click += new System.EventHandler(this.BtnAbstractorLoad_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(632, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "Window Size";
            // 
            // txtAbstractorWindowSize
            // 
            this.txtAbstractorWindowSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbstractorWindowSize.Location = new System.Drawing.Point(623, 65);
            this.txtAbstractorWindowSize.Name = "txtAbstractorWindowSize";
            this.txtAbstractorWindowSize.Size = new System.Drawing.Size(162, 38);
            this.txtAbstractorWindowSize.TabIndex = 21;
            this.txtAbstractorWindowSize.Text = "200";
            this.txtAbstractorWindowSize.TextChanged += new System.EventHandler(this.TxtAbstractorWindowSize_TextChanged);
            // 
            // IDMSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 768);
            this.Controls.Add(this.lblStatusUpdate);
            this.Controls.Add(this.tabControl1);
            this.Name = "IDMSimulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Infinite Discovery Machine Simulator v0.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEntropy)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAbstractor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFilterSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPortsList;
        private System.Windows.Forms.RichTextBox txtDNA;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtDNALength;
        private System.Windows.Forms.RichTextBox txtRawData;
        private System.Windows.Forms.PictureBox picEntropy;
        private System.Windows.Forms.Button btnGenerateRandomDNA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGlue;
        private System.Windows.Forms.Button btnShred;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Label lblStatusUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblShredderTotalBases;
        private System.Windows.Forms.Label lblShredderFragmentCount;
        private System.Windows.Forms.Label lblFragmentLengthAverage;
        private System.Windows.Forms.Label lblShredderCutPosition;
        private System.Windows.Forms.CheckBox checkSoftwareRNG;
        private System.Windows.Forms.CheckBox checkHardwareRNG;
        private System.Windows.Forms.Button btnLoadTextbox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAbstractorWindowSize;
        private System.Windows.Forms.Button btnAbstractorLoad;
        private System.Windows.Forms.Button btnAbstractorSave;
        private System.Windows.Forms.Button btnAbstractorVisualize;
        private System.Windows.Forms.TrackBar trackBarFilterSize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picAbstractor;
    }
}

