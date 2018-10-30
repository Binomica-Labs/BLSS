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
    public partial class IDMSimulator : Form
    {
        public IDMSimulator()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            foreach (string comport in ports)
            {
                cmbPortsList.Items.Add(comport);
            }
            if (cmbPortsList.Items.Count > 0)
            {
                cmbPortsList.SelectedIndex = 0;
            }
        }

        //stats stuff
        public int desiredRandomDNALength = 0;
        public int countA = 0;
        public int countT = 0;
        public int countC = 0;
        public int countG = 0;
        public int avgRNG = 0;
        public int sumRNG = 0;

        public double percentA = 0.0;
        public double percentT = 0.0;
        public double percentC = 0.0;
        public double percentG = 0.0;
        public double percentRNG = 0.0;

        //random generator stuff
        //public RandomNumberGenerator semiBullshitRNG;  //internal cryptographic software RNG, still kinda shitty
        public Random bullshitRNG;  //internal software RNG of low quality bullshit, for demo purposes only until TrueRNG v3 class for handling 64bit numbers is made
        public int[] rngRawNums;
        public char[] rngNucleotides;
        public string[] codons;
        public string rngDNA;
        public string rngRaw;
        public bool generatorThreadComplete;
        public bool hasHardwareRNG = false;

        //shredder stuff
        public int shredderCutPosition;
        string choppedFragment;
        string remainingFragment;
        public List<string> fragments = new List<string>();
        public List<int> fragLengths = new List<int>();
        public int fragmentChoice;
        public BackgroundWorker backgroundWorker = new BackgroundWorker();
        public int shredderFragmentCount;
        public int shredderFragmentLengthAverage;
        public long shredderTotalBases;

        //glue stuff
        public List<string> ligatedFragments = new List<string>();
        public List<int> ligatedFragLengths = new List<int>();

        //filter stuff
        public List<string> openReadingFramesPassList = new List<string>();
        public List<string> openReadingFramesFailList = new List<string>();
        public List<string> stopCodonPassList = new List<string>();
        public List<string> stopCodonFailList = new List<string>();
        public List<string> startCodonPassList = new List<string>();
        public List<string> startCodonFailList = new List<string>();
        public List<string> finalDNAList = new List<string>();
        public List<string> finalProteinList = new List<string>();

        //bitmap data vis stuff
        public Bitmap bmpDNA;
        public int entropyImageHeight;
        public int colorPosition = 0;
        public Color colorA;
        public Color colorT;
        public Color colorC;
        public Color colorG;
        public Color mouseColor;

        //misc stuff
        delegate void SetTextCallback(string text);
        string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        public ToolTip mapPositionToolTip = new ToolTip();
        private SerialPort rngPort = new SerialPort();
        private int sensorBaud = 115200;
        public string statusUpdateString = "Hi, please generate or load DNA sequence.";









        private void IDMsimulator_Load(object sender, EventArgs e)
        {
            pickNucleotideColor();
            panel1.VerticalScroll.Visible = true;

        }

        private void IDMsimulator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rngPort.IsOpen == true)
            {
                rngPort.DiscardInBuffer();
                rngPort.DiscardOutBuffer();
                rngPort.Dispose();
                rngPort.Close();
            }
        }

        public static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
            {
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (checkHardwareRNG.Checked)
            {
                try
                {
                    statusUpdateString = "Generating Truly Random DNA...";
                    desiredRandomDNALength = Convert.ToInt32(txtDNALength.Text);
                    rngPort = new SerialPort(cmbPortsList.Text, sensorBaud);
                    rngPort.DtrEnable = true;
                    rngPort.ReadBufferSize = desiredRandomDNALength;
                    rngPort.ReceivedBytesThreshold = desiredRandomDNALength;

                    if (rngPort.IsOpen == false)
                    {
                        rngPort.Open();
                    }

                    generateHardwareRandomDNA();

                    txtDNA.Text = rngDNA;
                    txtDNALength.Text = rngDNA.Length.ToString();
                    visualizeDNA();
                    computeStats();
                }
                catch
                {
                    statusUpdateString = "Can't seem to talk to the RNG. Check ports and restart app!";
                    lblStatusUpdate.Text = statusUpdateString;
                }
            }
            else
            {
                statusUpdateString = "Generating Pseudo-Random DNA...";
                desiredRandomDNALength = Convert.ToInt32(txtDNALength.Text);
                generateSoftwareRandomDNA();

                txtDNA.Text = rngDNA;
                txtDNALength.Text = rngDNA.Length.ToString();
                visualizeDNA();
                computeStats();
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            rngDNA = "";
            

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
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
                            rngDNA = File.ReadAllText(openFileDialog1.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            if (rngDNA.StartsWith("A") || rngDNA.StartsWith("T") || rngDNA.StartsWith("C") || rngDNA.StartsWith("G"))  //check if input is DNA in all caps
            {
                txtDNA.Text = rngDNA;
                txtDNALength.Text = rngDNA.Length.ToString();
                desiredRandomDNALength = rngDNA.Length;
                rngNucleotides = new char[desiredRandomDNALength];
                rngNucleotides = rngDNA.ToCharArray();
                visualizeDNA();
                computeStats();
                statusUpdateString = "DNA Loaded!";
                lblStatusUpdate.Text = statusUpdateString;
                btnShred.Enabled = true;
                btnSave.Enabled = true;
                btnGlue.Enabled = true;
            }
            else
            {
                statusUpdateString = "Check DNA input format. All caps, no craps!";
                lblStatusUpdate.Text = statusUpdateString;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            statusUpdateString = "Saving Entropy Map to image file...";
            lblStatusUpdate.Text = statusUpdateString;
            bmpDNA.Save(desktopPath + "\\EntropyGraph " + DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss") + ".png", System.Drawing.Imaging.ImageFormat.Png);
            statusUpdateString = "Entropy Map saved!";
            lblStatusUpdate.Text = statusUpdateString;
        }

        private void btnShred_Click(object sender, EventArgs e)
        {
            shredderFragmentCount = 0;
            shredderCutPosition = 0;
            shredderFragmentLengthAverage = 0;
            shredderTotalBases = 0;
            fragments = new List<string>();
            fragLengths = new List<int>();
            fragments.Add(txtDNA.Text);                     //loads dna sequence into first slot of fragments list
            fragLengths.Add(txtDNA.Text.Length);
            Thread shredThread = new Thread(shredDNA);
            shredThread.Start();
        }

        private void btnGlue_Click(object sender, EventArgs e)
        {
            ligatedFragments = new List<string>();
            ligatedFragLengths = new List<int>();
            openReadingFramesPassList = new List<string>();
            openReadingFramesFailList = new List<string>();
            stopCodonPassList = new List<string>();
            stopCodonFailList = new List<string>();
            startCodonPassList = new List<string>();
            startCodonFailList = new List<string>();
            finalDNAList = new List<string>();
            finalProteinList = new List<string>();

            Thread shredThread = new Thread(glueDNA);
            shredThread.Start();
        }

        public void generateHardwareRandomDNA()
        {
            try
            {
                statusUpdateString = "Generating Random DNA...";
                lblStatusUpdate.Invoke((MethodInvoker)delegate
                {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                rngRawNums = new int[desiredRandomDNALength];
                rngNucleotides = new char[desiredRandomDNALength];



                for (int i = 0; i < desiredRandomDNALength; i++)
                {
                    statusUpdateString = "Generating Random DNA..." + ((i / desiredRandomDNALength) * 100).ToString();
                    lblStatusUpdate.Invoke((MethodInvoker)delegate
                    {

                        lblStatusUpdate.Text = statusUpdateString;
                    });
                    rngRawNums[i] = rngPort.ReadByte();

                    if (rngRawNums[i] >= 0 && rngRawNums[i] <= 63)
                    {
                        rngNucleotides[i] = 'A';
                    }
                    else if (rngRawNums[i] >= 64 && rngRawNums[i] <= 127)
                    {
                        rngNucleotides[i] = 'T';
                    }
                    else if (rngRawNums[i] >= 128 && rngRawNums[i] <= 191)
                    {
                        rngNucleotides[i] = 'G';
                    }
                    else if (rngRawNums[i] >= 192 && rngRawNums[i] <= 255)
                    {
                        rngNucleotides[i] = 'C';
                    }
                }

                rngDNA = string.Join("", rngNucleotides);
                rngRaw = string.Join(" ", rngRawNums);
                rngPort.Close();
                statusUpdateString = "Generation Complete!";
                lblStatusUpdate.Invoke((MethodInvoker)delegate
                {
                    lblStatusUpdate.Text = statusUpdateString;
                });

                txtDNA.Invoke((MethodInvoker)delegate
                {
                    txtDNA.Text = rngDNA;
                });

                txtRawData.Invoke((MethodInvoker)delegate
                {
                    txtRawData.Text = rngRaw;
                });

                btnShred.Invoke((MethodInvoker)delegate
                {
                    btnShred.Enabled = true;
                });

                btnSave.Invoke((MethodInvoker)delegate
                {
                    btnSave.Enabled = true;
                });

                btnGlue.Invoke((MethodInvoker)delegate
                {
                    btnGlue.Enabled = true;
                });

                txtDNA.Invoke((MethodInvoker)delegate
                {
                    txtDNA.Text = rngDNA;
                });

                txtDNALength.Invoke((MethodInvoker)delegate
                {
                    txtDNALength.Text = rngDNA;
                });
                generatorThreadComplete = true;
            }

            catch (Exception e)
            {
                statusUpdateString = "Generation Error. Check RNG connection...";
                lblStatusUpdate.Invoke((MethodInvoker)delegate {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                rngPort.Close();
                MessageBox.Show(e.Message);
            }

        }

        public void generateSoftwareRandomDNA()
        {
            try
            {
                statusUpdateString = "Generating Pseudo-Random DNA...";
                lblStatusUpdate.Invoke((MethodInvoker)delegate
                {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                rngRawNums = new int[desiredRandomDNALength];
                rngNucleotides = new char[desiredRandomDNALength];
                bullshitRNG = new Random();


                for (int i = 0; i < desiredRandomDNALength; i++)
                {
                    statusUpdateString = "Generating Pseudo-Random DNA..." + ((i / desiredRandomDNALength) * 100).ToString();
                    lblStatusUpdate.Invoke((MethodInvoker)delegate
                    {

                        lblStatusUpdate.Text = statusUpdateString;
                    });
                    rngRawNums[i] = bullshitRNG.Next(0, 255);  //use internal software RNG

                    if (rngRawNums[i] >= 0 && rngRawNums[i] <= 63)
                    {
                        rngNucleotides[i] = 'A';
                    }
                    else if (rngRawNums[i] >= 64 && rngRawNums[i] <= 127)
                    {
                        rngNucleotides[i] = 'T';
                    }
                    else if (rngRawNums[i] >= 128 && rngRawNums[i] <= 191)
                    {
                        rngNucleotides[i] = 'G';
                    }
                    else if (rngRawNums[i] >= 192 && rngRawNums[i] <= 255)
                    {
                        rngNucleotides[i] = 'C';
                    }
                }

                rngDNA = string.Join("", rngNucleotides);
                rngRaw = string.Join(" ", rngRawNums);
                statusUpdateString = "Generation Complete!";
                lblStatusUpdate.Invoke((MethodInvoker)delegate
                {
                    lblStatusUpdate.Text = statusUpdateString;
                });

                txtDNA.Invoke((MethodInvoker)delegate
                {
                    txtDNA.Text = rngDNA;
                });

                txtRawData.Invoke((MethodInvoker)delegate
                {
                    txtRawData.Text = rngRaw;
                });

                btnShred.Invoke((MethodInvoker)delegate
                {
                    btnShred.Enabled = true;
                });

                btnSave.Invoke((MethodInvoker)delegate
                {
                    btnSave.Enabled = true;
                });

                btnGlue.Invoke((MethodInvoker)delegate
                {
                    btnGlue.Enabled = true;
                });

                txtDNA.Invoke((MethodInvoker)delegate
                {
                    txtDNA.Text = rngDNA;
                });

                txtDNALength.Invoke((MethodInvoker)delegate
                {
                    txtDNALength.Text = rngDNA;
                });
                generatorThreadComplete = true;
            }

            catch (Exception e)
            {
                statusUpdateString = "Internal Software RNG error...";
                lblStatusUpdate.Invoke((MethodInvoker)delegate {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                MessageBox.Show(e.Message);
            }

        }

        public void computeStats()
        {
            countA = 0;
            countT = 0;
            countC = 0;
            countG = 0;
            avgRNG = 0;
            sumRNG = 0;

            foreach (char nucleotide in rngNucleotides)
            {
                if (nucleotide == 'A')
                {
                    countA++;
                }
                else if (nucleotide == 'T')
                {
                    countT++;

                }
                else if (nucleotide == 'C')
                {
                    countC++;

                }
                else if (nucleotide == 'G')
                {
                    countG++;

                }
            }
            try
            {

            }
            catch
            {
                foreach (int num in rngRawNums)
                {
                    sumRNG = sumRNG + num;
                }

                avgRNG = sumRNG / rngRawNums.Length;
            }

            percentA = Math.Truncate((countA / Convert.ToDouble(rngNucleotides.Length)) * 100);
            percentT = Math.Truncate((countT / Convert.ToDouble(rngNucleotides.Length)) * 100);
            percentC = Math.Truncate((countC / Convert.ToDouble(rngNucleotides.Length)) * 100);
            percentG = Math.Truncate((countG / Convert.ToDouble(rngNucleotides.Length)) * 100);

            /*
            lblPercentA.Text = percentA.ToString();
            lblPercentT.Text = percentT.ToString();
            lblPercentC.Text = percentC.ToString();
            lblPercentG.Text = percentG.ToString();
            lblAverageRNGValue.Text = avgRNG.ToString();
            */

        }

        public void visualizeDNA()
        {
            pickNucleotideColor();

            if (desiredRandomDNALength / picEntropy.Width < 1)
            {
                entropyImageHeight = 1;
            }

            else
            {
                entropyImageHeight = desiredRandomDNALength / picEntropy.Width;
            }

            bmpDNA = new Bitmap(200, entropyImageHeight);

            if (desiredRandomDNALength > 200)
            {
                for (int y = 0; y < entropyImageHeight; y++)
                {
                    for (int x = 0; x < 200; x++)
                    {

                        if (rngNucleotides[colorPosition] == 'A')
                        {
                            bmpDNA.SetPixel(x, y, colorA);
                            colorPosition++;
                        }

                        else if (rngNucleotides[colorPosition] == 'T')
                        {
                            bmpDNA.SetPixel(x, y, colorT);
                            colorPosition++;
                        }

                        else if (rngNucleotides[colorPosition] == 'G')
                        {
                            bmpDNA.SetPixel(x, y, colorC);
                            colorPosition++;
                        }

                        else if (rngNucleotides[colorPosition] == 'C')
                        {
                            bmpDNA.SetPixel(x, y, colorG);
                            colorPosition++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < desiredRandomDNALength; i++)
                {
                    if (rngNucleotides[colorPosition] == 'A')
                    {
                        bmpDNA.SetPixel(i, 0, colorA);
                        colorPosition++;
                    }

                    else if (rngNucleotides[colorPosition] == 'T')
                    {
                        bmpDNA.SetPixel(i, 0, colorT);
                        colorPosition++;
                    }

                    else if (rngNucleotides[colorPosition] == 'G')
                    {
                        bmpDNA.SetPixel(i, 0, colorG);
                        colorPosition++;
                    }

                    else if (rngNucleotides[colorPosition] == 'C')
                    {
                        bmpDNA.SetPixel(i, 0, colorC);
                        colorPosition++;
                    }
                }
            }

            picEntropy.SizeMode = PictureBoxSizeMode.AutoSize;
            picEntropy.Image = bmpDNA;
            colorPosition = 0;
        }

        public void pickNucleotideColor()
        {
            try
            {
                colorA = Color.FromArgb(0, 230, 255);
                colorT = Color.FromArgb(255, 255, 0);
                colorC = Color.FromArgb(220, 0, 0);
                colorG = Color.FromArgb(0, 0, 0);

                /*
                colorA = Color.FromArgb(Convert.ToInt16(txtColorAred.Text),
                                                    Convert.ToInt16(txtColorAgreen.Text),
                                                    Convert.ToInt16(txtColorAblue.Text));

                colorT = Color.FromArgb(Convert.ToInt16(txtColorTred.Text),
                                        Convert.ToInt16(txtColorTgreen.Text),
                                        Convert.ToInt16(txtColorTblue.Text));

                colorC = Color.FromArgb(Convert.ToInt16(txtColorCred.Text),
                                        Convert.ToInt16(txtColorCgreen.Text),
                                        Convert.ToInt16(txtColorCblue.Text));

                colorG = Color.FromArgb(Convert.ToInt16(txtColorGred.Text),
                                        Convert.ToInt16(txtColorGgreen.Text),
                                        Convert.ToInt16(txtColorGblue.Text));

                panColorPickerA.BackColor = colorA;
                panColorPickerT.BackColor = colorT;
                panColorPickerG.BackColor = colorG;
                panColorPickerC.BackColor = colorC;
                */

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        public void shredDNA()
        {
            statusUpdateString = "Shredding DNA...";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
            string currentFragment;
            bullshitRNG = new Random();

            while (fragLengths.Average() > 4)
            {
                statusUpdateString = "Shredding DNA..." + Math.Ceiling(((4.00/(fragLengths.Average()))*100)).ToString()+"%";
                lblStatusUpdate.Invoke((MethodInvoker)delegate {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                fragmentChoice = bullshitRNG.Next(0, fragments.Count);

                if (fragments[fragmentChoice].Length > 4)
                {
                    shredderCutPosition = bullshitRNG.Next(1, fragments[fragmentChoice].Length - 1);
                    currentFragment = fragments[fragmentChoice];
                    choppedFragment = currentFragment.Substring(0, shredderCutPosition);
                    remainingFragment = currentFragment.Substring(shredderCutPosition);


                    if (choppedFragment.Length > 0 && remainingFragment.Length > 0)
                    {
                        fragments.Add(choppedFragment);
                        fragLengths.Add(choppedFragment.Length);

                        fragments.Add(remainingFragment);
                        fragLengths.Add(remainingFragment.Length);

                        fragments.Remove(currentFragment);
                        fragLengths.Remove(currentFragment.Length);

                        shredderFragmentCount = fragLengths.Count();
                        shredderFragmentLengthAverage = Convert.ToInt32(Math.Ceiling(fragLengths.Average()));
                        shredderTotalBases = fragLengths.Sum();

                    }
                }
            }

            statusUpdateString = "Writing Shreds to file...";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
            using (var countFile = File.CreateText(desktopPath + "\\Fragmentation Length Data" + DateTime.Now.ToBinary().ToString() + ".csv"))
            {
                foreach (int fragLength in fragLengths)
                {
                    countFile.WriteLine(string.Join(",", fragLength.ToString()));
                }
            }

            using (var fragFile = File.CreateText(desktopPath + "\\Fragmentation Pool" + DateTime.Now.ToBinary().ToString() + ".csv"))
            {
                foreach (string fragment in fragments)
                {
                    fragFile.WriteLine(string.Join(",", fragment));
                }
            }
            statusUpdateString = "Shredding Complete!";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
        }

        public void glueDNA()
        {
            statusUpdateString = "Ligating DNA..." + Math.Ceiling((((fragLengths.Average())/500) * 100)).ToString() + "%";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
            ligatedFragments = fragments;
            ligatedFragLengths = fragLengths;

            while (shredderFragmentLengthAverage < 2000)
            {
                statusUpdateString = "Ligating DNA..." + Math.Ceiling((((fragLengths.Average()) / 500) * 100)).ToString() + "%";
                lblStatusUpdate.Invoke((MethodInvoker)delegate {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                int firstFragmentChoice = bullshitRNG.Next(0, fragments.Count);
                string firstFragment = fragments[firstFragmentChoice];

                int secondFragmentChoice = bullshitRNG.Next(0, fragments.Count);
                string secondFragment = fragments[secondFragmentChoice];

                if (firstFragmentChoice != secondFragmentChoice)
                {
                    string gluedFragment = string.Concat(ligatedFragments[firstFragmentChoice], ligatedFragments[secondFragmentChoice]);
                    ligatedFragments.Add(gluedFragment);
                    ligatedFragLengths.Add(gluedFragment.Length);

                    ligatedFragments.Remove(firstFragment);
                    ligatedFragments.Remove(secondFragment);
                    ligatedFragLengths.Remove(firstFragment.Length);
                    ligatedFragLengths.Remove(secondFragment.Length);
                    
                    shredderFragmentCount = ligatedFragLengths.Count();
                    shredderFragmentLengthAverage = Convert.ToInt32((Math.Ceiling(ligatedFragLengths.Average())));
                    shredderTotalBases = (long)ligatedFragLengths.Sum();
                }
            }
            statusUpdateString = "Writing ligations to file...";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
            using (var ligLengthFile = File.CreateText(desktopPath + "\\Ligation Length Data" + DateTime.Now.ToBinary().ToString() + ".csv"))
            {
                foreach (int fragLength in fragLengths)
                {
                    ligLengthFile.WriteLine(string.Join(",", fragLength.ToString()));
                }
            }

            using (var ligPoolFile = File.CreateText(desktopPath + "\\Ligated DNA Pool" + DateTime.Now.ToBinary().ToString() + ".csv"))
            {
                foreach (string fragment in fragments)
                {
                    ligPoolFile.WriteLine(string.Join(",", fragment));
                }
            }
            statusUpdateString = "Ligations Complete!";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
            filterDNA();

        }

        public void filterDNA()
        {
            statusUpdateString = "Filtering DNA...";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
            foreach (string fragment in fragments)
            {
                if (fragment.Length % 3 == 0)
                {
                    openReadingFramesPassList.Add(fragment);
                }
                else
                {
                    openReadingFramesFailList.Add(fragment);
                }
            }

            if (openReadingFramesPassList.Count > 0)        //check for stop codons inside ORF
            {
                foreach (string ORF in openReadingFramesPassList)
                {
                    codons = ChunksUpto(ORF, 3).ToArray<string>();
                    bool stopDetected = codons.Contains("TAA") || codons.Contains("TAG") || codons.Contains("TGA");

                    if (stopDetected)
                    {
                        stopCodonFailList.Add(ORF);
                    }
                    else
                    {
                        stopCodonPassList.Add(ORF);
                    }
                }
            }

           /* if (stopCodonPassList.Count > 0)        //check for start codons inside ORF
            {
                foreach (string ORF in stopCodonPassList)
                {
                    codons = ChunksUpto(ORF, 3).ToArray<string>();

                    bool startDetected = codons.Contains("ATG");

                    if (startDetected)
                    {
                        startCodonFailList.Add(ORF);
                    }
                    else
                    {
                        startCodonPassList.Add(ORF);
                    }
                }
            } */

            if (stopCodonPassList.Count > 0) //change to startCodonPassList if you want to screen for METs as well
            {
                statusUpdateString = "Adding IDM motiffs...";
                lblStatusUpdate.Invoke((MethodInvoker)delegate {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                foreach (string ORF in stopCodonPassList)
                {
                    string polishedFragment = "ATG" + ORF + "CATCACCATCACCATCACTTATAATAA";
                    finalDNAList.Add(polishedFragment);
                }

                using (var finalDNAFile = File.CreateText(desktopPath + "\\IDM Final DNA Data" + DateTime.Now.ToBinary().ToString() + ".csv"))
                {
                    foreach (string cds in finalDNAList)
                    {
                        finalDNAFile.WriteLine(string.Join(",", cds));
                    }
                }

            }

            if (finalDNAList.Count > 0)
            {
                statusUpdateString = "Writing proteins to file...";
                lblStatusUpdate.Invoke((MethodInvoker)delegate {

                    lblStatusUpdate.Text = statusUpdateString;
                });
                foreach (string CDS in finalDNAList)
                {
                    finalProteinList.Add(ConvertToProtein(CDS));
                }

                using (var finalProteinFile = File.CreateText(desktopPath + "\\IDM Final Protein Data" + DateTime.Now.ToBinary().ToString() + ".csv"))
                {
                    foreach (string cds in finalProteinList)
                    {
                        finalProteinFile.WriteLine(string.Join(",", cds));
                    }
                }
            }
            statusUpdateString = "Filtration Complete!";
            lblStatusUpdate.Invoke((MethodInvoker)delegate {

                lblStatusUpdate.Text = statusUpdateString;
            });
        }

        public string ConvertToProtein(string inputDNA)
        {

            string cds = "";
            codons = ChunksUpto(inputDNA, 3).ToArray<string>();

            for (int i = 0; i < codons.Length; i++)
            {
                switch (codons[i])
                {

                    case "TGG":
                        cds += "W";
                        break;

                    case "TAC":
                        cds += "Y";
                        break;

                    case "TAT":
                        cds += "Y";
                        break;

                    case "TGC":
                        cds += "C";
                        break;

                    case "TGT":
                        cds += "C";
                        break;

                    case "GAA":
                        cds += "E";
                        break;

                    case "GAG":
                        cds += "E";
                        break;

                    case "AAA":
                        cds += "K";
                        break;

                    case "AAG":
                        cds += "K";
                        break;

                    case "CAA":
                        cds += "Q";
                        break;

                    case "CAG":
                        cds += "Q";
                        break;

                    case "AGC":
                        cds += "S";
                        break;

                    case "AGT":
                        cds += "S";
                        break;

                    case "TCA":
                        cds += "S";
                        break;

                    case "TCC":
                        cds += "S";
                        break;

                    case "TCG":
                        cds += "S";
                        break;

                    case "TCT":
                        cds += "S";
                        break;

                    case "TTA":
                        cds += "L";
                        break;

                    case "TTG":
                        cds += "L";
                        break;

                    case "CTA":
                        cds += "L";
                        break;

                    case "CTC":
                        cds += "L";
                        break;

                    case "CTG":
                        cds += "L";
                        break;

                    case "CTT":
                        cds += "L";
                        break;

                    case "AGA":
                        cds += "R";
                        break;

                    case "AGG":
                        cds += "R";
                        break;

                    case "CGA":
                        cds += "R";
                        break;

                    case "CGC":
                        cds += "R";
                        break;

                    case "CGG":
                        cds += "R";
                        break;

                    case "CGT":
                        cds += "R";
                        break;

                    case "GGA":
                        cds += "G";
                        break;

                    case "GGC":
                        cds += "G";
                        break;

                    case "GGG":
                        cds += "G";
                        break;

                    case "GGT":
                        cds += "G";
                        break;

                    case "TTC":
                        cds += "F";
                        break;

                    case "TTT":
                        cds += "F";
                        break;

                    case "GAC":
                        cds += "D";
                        break;

                    case "GAT":
                        cds += "D";
                        break;

                    case "CAC":
                        cds += "H";
                        break;

                    case "CAT":
                        cds += "H";
                        break;

                    case "AAC":
                        cds += "N";
                        break;

                    case "AAT":
                        cds += "N";
                        break;

                    case "ATG":
                        cds += "M";
                        break;

                    case "GCA":
                        cds += "A";
                        break;

                    case "GCC":
                        cds += "A";
                        break;

                    case "GCG":
                        cds += "A";
                        break;

                    case "GCT":
                        cds += "A";
                        break;

                    case "CCA":
                        cds += "P";
                        break;

                    case "CCC":
                        cds += "P";
                        break;

                    case "CCG":
                        cds += "P";
                        break;

                    case "CCT":
                        cds += "P";
                        break;

                    case "ACA":
                        cds += "T";
                        break;

                    case "ACC":
                        cds += "T";
                        break;

                    case "ACG":
                        cds += "T";
                        break;

                    case "ACT":
                        cds += "T";
                        break;

                    case "GTA":
                        cds += "V";
                        break;

                    case "GTC":
                        cds += "V";
                        break;

                    case "GTG":
                        cds += "V";
                        break;

                    case "GTT":
                        cds += "V";
                        break;

                    case "ATA":
                        cds += "I";
                        break;

                    case "ATC":
                        cds += "I";
                        break;

                    case "ATT":
                        cds += "I";
                        break;

                    case "TAA":
                        cds += "X";
                        break;

                    case "TAG":
                        cds += "X";
                        break;

                    case "TGA":
                        cds += "X";
                        break;

                    default:
                        break;
                }


            }
            return cds;
        }

        private void txtDNALength_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbPortsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPortsList_DropDown(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cmbPortsList.Items.Clear();
            foreach (string comport in ports)
            {
                cmbPortsList.Items.Add(comport);
            }
        }

        private void txtDNA_Enter(object sender, EventArgs e)
        {
            txtDNA.Text = "";
        }

        private void txtRawData_Enter(object sender, EventArgs e)
        {
            txtRawData.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkSoftwareRNG_CheckedChanged(object sender, EventArgs e)
        {
            checkHardwareRNG.Checked = !checkSoftwareRNG.Checked;
            if (checkSoftwareRNG.Checked)
            {
                hasHardwareRNG = false;
            }
        }

        private void checkHardwareRNG_CheckedChanged(object sender, EventArgs e)
        {
            checkSoftwareRNG.Checked = !checkHardwareRNG.Checked;
            if (checkHardwareRNG.Checked)
            {
                hasHardwareRNG = true;
                cmbPortsList.Enabled = true;
            }
            else
            {
                hasHardwareRNG = false;
                cmbPortsList.Enabled = false;
            }
        }

        private void btnLoadTextbox_Click(object sender, EventArgs e)
        {
            statusUpdateString = "Loading DNA from Textbox...";
            lblStatusUpdate.Text = statusUpdateString;
            rngDNA = txtDNA.Text;

            if (rngDNA.StartsWith("A") || rngDNA.StartsWith("T") || rngDNA.StartsWith("C") || rngDNA.StartsWith("G"))  //check if input is DNA in all caps
            {
                txtDNA.Text = rngDNA;
                txtDNALength.Text = rngDNA.Length.ToString();
                desiredRandomDNALength = rngDNA.Length;
                rngNucleotides = new char[desiredRandomDNALength];
                rngNucleotides = rngDNA.ToCharArray();
                visualizeDNA();
                computeStats();
                statusUpdateString = "DNA Loaded!";
                lblStatusUpdate.Text = statusUpdateString;
                btnShred.Enabled = true;
                btnSave.Enabled = true;
                btnGlue.Enabled = true;
            }
            else
            {
                statusUpdateString = "Check DNA input format. All caps, no craps!";
                lblStatusUpdate.Text = statusUpdateString;
            }

        }
    }
}
