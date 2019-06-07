namespace Binomics_Labs_Software_Suite
{
    partial class GeneticVisualAbstractor
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
            this.btnBatchVisualize = new System.Windows.Forms.Button();
            this.btnAbstractorSave = new System.Windows.Forms.Button();
            this.btnAbstractorVisualize = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picAbstractor = new System.Windows.Forms.PictureBox();
            this.txtConsoleOutput = new System.Windows.Forms.RichTextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAbstractor)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBatchVisualize
            // 
            this.btnBatchVisualize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatchVisualize.Location = new System.Drawing.Point(670, 681);
            this.btnBatchVisualize.Name = "btnBatchVisualize";
            this.btnBatchVisualize.Size = new System.Drawing.Size(323, 100);
            this.btnBatchVisualize.TabIndex = 33;
            this.btnBatchVisualize.Text = "B A T C H   V I S";
            this.btnBatchVisualize.UseVisualStyleBackColor = true;
            this.btnBatchVisualize.Click += new System.EventHandler(this.BtnBatchVisualize_Click);
            // 
            // btnAbstractorSave
            // 
            this.btnAbstractorSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbstractorSave.Location = new System.Drawing.Point(341, 681);
            this.btnAbstractorSave.Name = "btnAbstractorSave";
            this.btnAbstractorSave.Size = new System.Drawing.Size(323, 100);
            this.btnAbstractorSave.TabIndex = 28;
            this.btnAbstractorSave.Text = "S A V E";
            this.btnAbstractorSave.UseVisualStyleBackColor = true;
            this.btnAbstractorSave.Click += new System.EventHandler(this.BtnAbstractorSave_Click);
            // 
            // btnAbstractorVisualize
            // 
            this.btnAbstractorVisualize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbstractorVisualize.Location = new System.Drawing.Point(12, 681);
            this.btnAbstractorVisualize.Name = "btnAbstractorVisualize";
            this.btnAbstractorVisualize.Size = new System.Drawing.Size(323, 100);
            this.btnAbstractorVisualize.TabIndex = 27;
            this.btnAbstractorVisualize.Text = "V I S U A L I Z E";
            this.btnAbstractorVisualize.UseVisualStyleBackColor = true;
            this.btnAbstractorVisualize.Click += new System.EventHandler(this.BtnAbstractorVisualize_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.picAbstractor);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1317, 666);
            this.panel2.TabIndex = 25;
            // 
            // picAbstractor
            // 
            this.picAbstractor.BackColor = System.Drawing.Color.Black;
            this.picAbstractor.Location = new System.Drawing.Point(3, 3);
            this.picAbstractor.Name = "picAbstractor";
            this.picAbstractor.Size = new System.Drawing.Size(1307, 660);
            this.picAbstractor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAbstractor.TabIndex = 4;
            this.picAbstractor.TabStop = false;
            // 
            // txtConsoleOutput
            // 
            this.txtConsoleOutput.BackColor = System.Drawing.Color.Black;
            this.txtConsoleOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsoleOutput.ForeColor = System.Drawing.Color.White;
            this.txtConsoleOutput.Location = new System.Drawing.Point(12, 790);
            this.txtConsoleOutput.Name = "txtConsoleOutput";
            this.txtConsoleOutput.Size = new System.Drawing.Size(1317, 48);
            this.txtConsoleOutput.TabIndex = 205;
            this.txtConsoleOutput.Text = "";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(999, 681);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(330, 100);
            this.btnExit.TabIndex = 32;
            this.btnExit.Text = "E X I T";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // GeneticVisualAbstractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 848);
            this.Controls.Add(this.txtConsoleOutput);
            this.Controls.Add(this.btnBatchVisualize);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAbstractorSave);
            this.Controls.Add(this.btnAbstractorVisualize);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.Name = "GeneticVisualAbstractor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Genetic Visual Abstractor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneticVisualAbstractor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeneticVisualAbstractor_FormClosed);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAbstractor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBatchVisualize;
        private System.Windows.Forms.Button btnAbstractorSave;
        private System.Windows.Forms.Button btnAbstractorVisualize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picAbstractor;
        private System.Windows.Forms.RichTextBox txtConsoleOutput;
        private System.Windows.Forms.Button btnExit;
    }
}