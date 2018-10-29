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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picEntropy = new System.Windows.Forms.PictureBox();
            this.btnGlue = new System.Windows.Forms.Button();
            this.btnShred = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnGenerateRandomDNA = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRawData = new System.Windows.Forms.RichTextBox();
            this.txtDNALength = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblStatusUpdate = new System.Windows.Forms.Label();
            this.lblShredderTotalBases = new System.Windows.Forms.Label();
            this.lblShredderFragmentCount = new System.Windows.Forms.Label();
            this.lblFragmentLengthAverage = new System.Windows.Forms.Label();
            this.lblShredderCutPosition = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEntropy)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPortsList
            // 
            this.cmbPortsList.FormattingEnabled = true;
            this.cmbPortsList.Location = new System.Drawing.Point(245, 73);
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
            this.txtDNA.Text = "\n\n\n\n             Add your DNA sequence here...";
            this.txtDNA.Enter += new System.EventHandler(this.txtDNA_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(891, 708);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.btnGlue);
            this.tabPage1.Controls.Add(this.btnShred);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnLoad);
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
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(6, 234);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(411, 100);
            this.btnLoad.TabIndex = 11;
            this.btnLoad.Text = "L O A D";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(80, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "RNG Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "DNA Length:";
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
            this.txtDNALength.Location = new System.Drawing.Point(243, 12);
            this.txtDNALength.Name = "txtDNALength";
            this.txtDNALength.Size = new System.Drawing.Size(120, 38);
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
            // IDMSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 768);
            this.Controls.Add(this.lblStatusUpdate);
            this.Controls.Add(this.tabControl1);
            this.Name = "IDMSimulator";
            this.Text = "The Infinite Discovery Machine Simulator v0.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEntropy)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblStatusUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblShredderTotalBases;
        private System.Windows.Forms.Label lblShredderFragmentCount;
        private System.Windows.Forms.Label lblFragmentLengthAverage;
        private System.Windows.Forms.Label lblShredderCutPosition;
    }
}

