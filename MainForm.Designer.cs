namespace Binomics_Labs_Software_Suite
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnIDM = new System.Windows.Forms.Button();
            this.btnCodonBiasAnalyzer = new System.Windows.Forms.Button();
            this.btnGenbankParser = new System.Windows.Forms.Button();
            this.btnBacterialMiniSpecController = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGeneticVisualAbstractor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIDM
            // 
            this.btnIDM.Location = new System.Drawing.Point(12, 12);
            this.btnIDM.Name = "btnIDM";
            this.btnIDM.Size = new System.Drawing.Size(552, 88);
            this.btnIDM.TabIndex = 0;
            this.btnIDM.Text = "Infinite Discovery Machine Simulator";
            this.btnIDM.UseVisualStyleBackColor = true;
            this.btnIDM.Click += new System.EventHandler(this.BtnIDM_Click);
            // 
            // btnCodonBiasAnalyzer
            // 
            this.btnCodonBiasAnalyzer.Location = new System.Drawing.Point(12, 200);
            this.btnCodonBiasAnalyzer.Name = "btnCodonBiasAnalyzer";
            this.btnCodonBiasAnalyzer.Size = new System.Drawing.Size(552, 88);
            this.btnCodonBiasAnalyzer.TabIndex = 2;
            this.btnCodonBiasAnalyzer.Text = "Codon Bias Analyzer";
            this.btnCodonBiasAnalyzer.UseVisualStyleBackColor = true;
            this.btnCodonBiasAnalyzer.Click += new System.EventHandler(this.BtnCodonBiasAnalyzer_Click);
            // 
            // btnGenbankParser
            // 
            this.btnGenbankParser.Location = new System.Drawing.Point(12, 388);
            this.btnGenbankParser.Name = "btnGenbankParser";
            this.btnGenbankParser.Size = new System.Drawing.Size(552, 88);
            this.btnGenbankParser.TabIndex = 4;
            this.btnGenbankParser.Text = "Genbank Parser";
            this.btnGenbankParser.UseVisualStyleBackColor = true;
            this.btnGenbankParser.Click += new System.EventHandler(this.BtnGenbankParser_Click);
            // 
            // btnBacterialMiniSpecController
            // 
            this.btnBacterialMiniSpecController.Location = new System.Drawing.Point(12, 294);
            this.btnBacterialMiniSpecController.Name = "btnBacterialMiniSpecController";
            this.btnBacterialMiniSpecController.Size = new System.Drawing.Size(552, 88);
            this.btnBacterialMiniSpecController.TabIndex = 3;
            this.btnBacterialMiniSpecController.Text = "Bacterial MiniSpec Controller";
            this.btnBacterialMiniSpecController.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(809, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 25);
            this.label3.TabIndex = 7;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 482);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(552, 88);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnGeneticVisualAbstractor
            // 
            this.btnGeneticVisualAbstractor.Location = new System.Drawing.Point(12, 106);
            this.btnGeneticVisualAbstractor.Name = "btnGeneticVisualAbstractor";
            this.btnGeneticVisualAbstractor.Size = new System.Drawing.Size(552, 88);
            this.btnGeneticVisualAbstractor.TabIndex = 9;
            this.btnGeneticVisualAbstractor.Text = "Genetic Visual Abstractor";
            this.btnGeneticVisualAbstractor.UseVisualStyleBackColor = true;
            this.btnGeneticVisualAbstractor.Click += new System.EventHandler(this.BtnGeneticVisualAbstractor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(576, 579);
            this.Controls.Add(this.btnGeneticVisualAbstractor);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenbankParser);
            this.Controls.Add(this.btnBacterialMiniSpecController);
            this.Controls.Add(this.btnCodonBiasAnalyzer);
            this.Controls.Add(this.btnIDM);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binomica Labs Software Suite";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIDM;
        private System.Windows.Forms.Button btnCodonBiasAnalyzer;
        private System.Windows.Forms.Button btnGenbankParser;
        private System.Windows.Forms.Button btnBacterialMiniSpecController;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGeneticVisualAbstractor;
    }
}