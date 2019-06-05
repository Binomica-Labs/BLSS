using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Binomics_Labs_Software_Suite
{
    public partial class GenbankParser : Form
    {
        public GenbankParser()
        {
            InitializeComponent();
        }

        //variables

        public string wholeFile;
        public string[] entries;
        public string statusUpdateString;

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            wholeFile = "";
            UpdateStatusBar("Loading Genbank File...");
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();


            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "FASTA files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            wholeFile = File.ReadAllText(openFileDialog1.FileName);
                        }
                    }
                }
                catch
                {

                }
            }
        }



        public void UpdateStatusBar(string status)
        {
            statusUpdateString = status;
            txtConsoleOutput.Invoke((MethodInvoker)delegate
            {
                txtConsoleOutput.Text = statusUpdateString;
            });
        }



        private void BtnParse_Click(object sender, EventArgs e)
        {
            //entries = wholeFile.
            /*
             * remove header from file
             * 
             * 
             */
        }
    }
}

