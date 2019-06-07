using System;
using System.Windows.Forms;

namespace Binomics_Labs_Software_Suite
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void BtnIDM_Click(object sender, EventArgs e)
        {
            InfiniteDiscoveryMachineSimulator IDM = new InfiniteDiscoveryMachineSimulator();
            this.WindowState = FormWindowState.Minimized;
            IDM.Show();
            IDM.Activate();
            IDM.FormClosed += new FormClosedEventHandler(IDM_FormClosed);
        }



        void IDM_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }



        private void BtnCodonBiasAnalyzer_Click(object sender, EventArgs e)
        {
            CodonBiasAnalyzer CBA = new CodonBiasAnalyzer();
            this.WindowState = FormWindowState.Minimized;
            CBA.Show();
            CBA.Activate();
            CBA.FormClosed += new FormClosedEventHandler(CBA_FormClosed);
        }



        void CBA_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }



        private void BtnGenbankParser_Click(object sender, EventArgs e)
        {
            GenbankParser GBP = new GenbankParser();
            this.WindowState = FormWindowState.Minimized;
            GBP.Show();
            GBP.Activate();
            GBP.FormClosed += new FormClosedEventHandler(GBP_FormClosed);
        }



        void GBP_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }



        private void BtnGeneticVisualAbstractor_Click(object sender, EventArgs e)
        {
            GeneticVisualAbstractor GVA = new GeneticVisualAbstractor();
            this.WindowState = FormWindowState.Minimized;
            GVA.Show();
            GVA.Activate();
            GVA.FormClosed += new FormClosedEventHandler(GBP_FormClosed);
        }



        void GVA_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
