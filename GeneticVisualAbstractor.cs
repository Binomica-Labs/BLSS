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
    public partial class GeneticVisualAbstractor : Form
    {
        public GeneticVisualAbstractor()
        {
            InitializeComponent();
        }

        //Variables
        public string loadedDNA;
        public int loadedDNALength;
        public char[] loadedNucleotides;
        public Bitmap visualizedDNA;
        public Color colorA;
        public Color colorT;
        public Color colorC;
        public Color colorG;
        public int entropyImageHeight;
        public int colorPosition = 0;
        public string[] fileLines;
        public string visualizationFileName;
        public string visualizationFolderPath;
        public Thread abstractorVisualizer;
        public Thread batchVisualizer;


        //abstraction stuff
        public string[] abstractionData;
        public int abstractCountA;
        public int abstractCountT;
        public int abstractCountC;
        public int abstractCountG;
        public string abstractedDNA;
        public char[] abstractedNucleotides;

        //DNA Ladder Stuff
        public int ladderLength;
        public string ladderFile;
        public string[] rungArray;

        //Batch Abstractor Stuff
        public Image visualAbstraction1;
        public Image visualAbstraction20;
        public Image visualAbstraction40;
        public Image visualAbstraction80;
        public Image visualAbstraction100;
        public Image visualAbstraction200;
        public Image visualAbstraction400;
        public string batchFolderPath;
        public List<String> batchAbstractorFiles = new List<string>();
        public Bitmap completedVisualization;

        public static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
            {
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
            }
        }



        public Image visualizeDNA(char[] input)
        {
            pickNucleotideColor();

            if (loadedDNALength / 200 < 1)
            {
                entropyImageHeight = 1;
            }

            else
            {
                entropyImageHeight = loadedDNALength / 200;
            }

            visualizedDNA = new Bitmap(200, entropyImageHeight);

            if (loadedDNALength > 200)
            {
                for (int y = 0; y < entropyImageHeight; y++)
                {
                    for (int x = 0; x < 200; x++)
                    {

                        if (input[colorPosition] == 'A')
                        {
                            visualizedDNA.SetPixel(x, y, colorA);
                            colorPosition++;
                        }

                        else if (input[colorPosition] == 'T')
                        {
                            visualizedDNA.SetPixel(x, y, colorT);
                            colorPosition++;
                        }

                        else if (input[colorPosition] == 'G')
                        {
                            visualizedDNA.SetPixel(x, y, colorG);
                            colorPosition++;
                        }

                        else if (input[colorPosition] == 'C')
                        {
                            visualizedDNA.SetPixel(x, y, colorC);
                            colorPosition++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < loadedDNALength; i++)
                {
                    if (input[colorPosition] == 'A')
                    {
                        visualizedDNA.SetPixel(i, 0, colorA);
                        colorPosition++;
                    }

                    else if (input[colorPosition] == 'T')
                    {
                        visualizedDNA.SetPixel(i, 0, colorT);
                        colorPosition++;
                    }

                    else if (input[colorPosition] == 'G')
                    {
                        visualizedDNA.SetPixel(i, 0, colorG);
                        colorPosition++;
                    }

                    else if (input[colorPosition] == 'C')
                    {
                        visualizedDNA.SetPixel(i, 0, colorC);
                        colorPosition++;
                    }
                }
            }

            colorPosition = 0;
            return visualizedDNA;
        }



        public void pickNucleotideColor()
        {
            try
            {
                colorA = Color.FromArgb(0, 230, 255); //cyan
                colorT = Color.FromArgb(255, 255, 0); //yellow
                colorC = Color.FromArgb(220, 0, 0); //red
                colorG = Color.FromArgb(0, 0, 0); //black

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



        private void abstractDNA(int filterSize)
        {
            abstractedDNA = "";
            abstractCountA = 0;
            abstractCountT = 0;
            abstractCountC = 0;
            abstractCountG = 0;

            abstractionData = ChunksUpto(new string(loadedNucleotides), filterSize).ToArray<string>();
            foreach (string segment in abstractionData)
            {
                abstractCountA = segment.Count(x => x == 'A');
                abstractCountT = segment.Count(x => x == 'T');
                abstractCountC = segment.Count(x => x == 'C');
                abstractCountG = segment.Count(x => x == 'G');

                if (abstractCountA > abstractCountC &&
                    abstractCountA > abstractCountT &&
                    abstractCountA > abstractCountG)
                {
                    abstractedDNA = abstractedDNA + new string('A', segment.Length);
                }

                else if (abstractCountC > abstractCountA &&
                         abstractCountC > abstractCountT &&
                         abstractCountC > abstractCountG)
                {
                    abstractedDNA = abstractedDNA + new string('C', segment.Length);
                }

                else if (abstractCountT > abstractCountC &&
                         abstractCountT > abstractCountA &&
                         abstractCountT > abstractCountG)
                {
                    abstractedDNA = abstractedDNA + new string('T', segment.Length);
                }

                else
                {
                    abstractedDNA = abstractedDNA + new string('G', segment.Length);
                }
            }

            abstractedNucleotides = abstractedDNA.ToCharArray();
        }



        public string generateLadderDNA()
        {
            ladderLength = 1000000 / 200; //desiredRandomDNALength.Round(1000)/400;
            int ladderRung = ladderLength / 10;
            rungArray = new string[ladderLength];

            rungArray[0] = String.Join("", new string('T', 100), new string('G', 300));
            rungArray[1] = String.Join("", new string('T', 100), new string('G', 300));
            rungArray[2] = String.Join("", new string('T', 100), new string('G', 300));

            for (int y = 3; y < ladderLength; y++)
            {
                rungArray[y] = String.Join("", new string('T', 5), new string('G', 395));
            }

            for (int y = (10000 / 400); y <= ladderLength - 1; y += (10000 / 400))
            {
                rungArray[y - 1] = String.Join("", new string('T', 100), new string('G', 300));
                rungArray[y] = String.Join("", new string('T', 100), new string('G', 300));
                rungArray[y + 1] = String.Join("", new string('T', 100), new string('G', 300));
            }

            for (int y = (ladderRung); y <= ladderLength - 1; y += ladderRung)
            {
                rungArray[y - 1] = String.Join("", new string('T', 400));
                rungArray[y] = String.Join("", new string('T', 400));
                rungArray[y + 1] = String.Join("", new string('T', 400));
            }

            rungArray[ladderLength - 3] = String.Join("", new string('T', 400));
            rungArray[ladderLength - 2] = String.Join("", new string('T', 400));
            rungArray[ladderLength - 1] = String.Join("", new string('T', 400));
            return String.Join("", rungArray);
        }



        public Bitmap MultiVisualize()
        {
            UpdateStatusBar("Abstracting pixel perfect column...");
            visualAbstraction1 = visualizeDNA(loadedNucleotides);
            UpdateStatusBar("Abstracting 20bp column...");
            abstractDNA(20);
            visualAbstraction20 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 40bp column...");
            abstractDNA(40);
            visualAbstraction40 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 80bp column...");
            abstractDNA(80);
            visualAbstraction80 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 100bp column...");
            abstractDNA(100);
            visualAbstraction100 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 200bp column...");
            abstractDNA(200);
            visualAbstraction200 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 400bp column...");
            abstractDNA(400);
            visualAbstraction400 = visualizeDNA(abstractedNucleotides);

            Bitmap combinedVisualAbstraction = new Bitmap((visualAbstraction1.Width +
                visualAbstraction20.Width +
                visualAbstraction40.Width +
                visualAbstraction80.Width +
                visualAbstraction100.Width +
                visualAbstraction200.Width +
                visualAbstraction400.Width),
                visualAbstraction1.Height);

            using (Graphics g = Graphics.FromImage(combinedVisualAbstraction))
            {

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                {
                    g.FillRectangle(brush, 0, 0, combinedVisualAbstraction.Width, combinedVisualAbstraction.Height);
                }

                g.DrawImage(visualAbstraction1, 0, 0);
                g.DrawImage(visualAbstraction20, visualAbstraction1.Width + 5, 0);
                g.DrawImage(visualAbstraction40, visualAbstraction1.Width * 2 + 10, 0);
                g.DrawImage(visualAbstraction80, visualAbstraction1.Width * 3 + 15, 0);
                g.DrawImage(visualAbstraction100, visualAbstraction1.Width * 4 + 20, 0);
                g.DrawImage(visualAbstraction200, visualAbstraction1.Width * 5 + 25, 0);
                g.DrawImage(visualAbstraction400, visualAbstraction1.Width * 6 + 30, 0);
            }

            
            visualAbstraction1.Dispose();
            visualAbstraction20.Dispose();
            visualAbstraction40.Dispose();
            visualAbstraction80.Dispose();
            visualAbstraction100.Dispose();
            visualAbstraction200.Dispose();
            visualAbstraction400.Dispose();
            return combinedVisualAbstraction;
        }



        public void UpdateStatusBar(string status)
        {
            txtConsoleOutput.Invoke((MethodInvoker)delegate
            {
                txtConsoleOutput.Text = status;
            });
        }



        public void UpdateVisualizationWindow(Image currentImage)
        {
            picAbstractor.Invoke((MethodInvoker)delegate
            {
                picAbstractor.Image = currentImage;
            });
        }



        public void ButtonStates(string state)
        {
            if (state == "off")
            {
                btnAbstractorVisualize.Invoke((MethodInvoker)delegate
                {
                    btnAbstractorVisualize.Enabled = false;
                });

                btnBatchVisualize.Invoke((MethodInvoker)delegate
                {
                    btnBatchVisualize.Enabled = false;
                });

                btnAbstractorSave.Invoke((MethodInvoker)delegate
                {
                    btnAbstractorSave.Enabled = false;
                });
            }
            else if (state == "on")
            {
                btnAbstractorVisualize.Invoke((MethodInvoker)delegate
                {
                    btnAbstractorVisualize.Enabled = true;
                });

                btnBatchVisualize.Invoke((MethodInvoker)delegate
                {
                    btnBatchVisualize.Enabled = true;
                });

                btnAbstractorSave.Invoke((MethodInvoker)delegate
                {
                    btnAbstractorSave.Enabled = true;
                });
            }
        }



        private void BtnAbstractorVisualize_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
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
                            fileLines = File.ReadAllLines(openFileDialog1.FileName);
                            visualizationFileName = openFileDialog1.FileName;
                            ButtonStates("off");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                
            }
            else
            {
                ButtonStates("on");
            }

            Thread abstractorVisualizer = new Thread(absractorVisualize);
            abstractorVisualizer.SetApartmentState(ApartmentState.STA);
            abstractorVisualizer.IsBackground = true;
            abstractorVisualizer.Start();
           
        }



        public void absractorVisualize()
        {

            UpdateStatusBar("Loading Genomic Data...");


            loadedDNA = "";
            string inputData = "";



            UpdateStatusBar("Removing FASTA comments...");
            if (fileLines != null)
            {
                for (int i = 0; i < fileLines.Length; i++)
                {
                    if (fileLines[i].Contains('>'))
                    {
                        fileLines[i] = "@"; //replace entire FASTA comment line with a single @ for character delimited CDS's, not elegant but works
                    }
                }
                //var newLines = fileLines.Where(line => !line.Contains('>'));
                inputData = string.Join("", fileLines);
                UpdateStatusBar("Removing line breaks and whitespace...");
                inputData = inputData.Trim(new char[] { '\r', '\n', ' ' }); //REMOVE ALL NONSPECIFIC NOTATIONS
                inputData = inputData.Replace("@", "");
                inputData = inputData.Replace("N", "");
                inputData = inputData.Replace("U", "");
                inputData = inputData.Replace("W", "");
                inputData = inputData.Replace("S", "");
                inputData = inputData.Replace("M", "");
                inputData = inputData.Replace("K", "");
                inputData = inputData.Replace("R", "");
                inputData = inputData.Replace("Y", "");
                inputData = inputData.Replace("B", "");
                inputData = inputData.Replace("D", "");
                inputData = inputData.Replace("H", "");
                inputData = inputData.Replace("V", "");
                inputData = inputData.Replace("Z", "");


                if (inputData.Length > 1000000)

                {
                    loadedDNA = inputData.Substring(0, 1000000); //take only the first million basepairs for time's sake. Will move to full size once process is faster
                }

                else

                {
                    loadedDNA = inputData;
                }

                UpdateStatusBar("File formatting complete!");
                loadedDNALength = loadedDNA.Length;
                UpdateStatusBar("Chopping DNA...");
                loadedNucleotides = new char[loadedDNALength];
                loadedNucleotides = loadedDNA.ToCharArray();
                UpdateStatusBar("Rendering combined visualization...");
                completedVisualization = MultiVisualize();
                UpdateVisualizationWindow(completedVisualization);
                UpdateStatusBar("DNA visualization complete!!!");
                ButtonStates("on");
            }
        }



        public void batchVisualize()
        {
            UpdateStatusBar("Starting batch visualization thread...");
            foreach (string file in batchAbstractorFiles)
            {
                string inputData = "";
                loadedDNA = "";
                UpdateStatusBar("Removing FASTA comments...");
                var fileLines = File.ReadAllLines(file);  //FIX THIS
                for (int i = 0; i < fileLines.Length; i++)
                {
                    if (fileLines[i].Contains('>'))
                    {
                        fileLines[i] = "@"; //replace entire FASTA comment line with a single @ for character delimited CDS's, not elegant but works
                    }
                }
                //var newLines = fileLines.Where(line => !line.Contains('>'));
                inputData = string.Join("", fileLines);
                UpdateStatusBar("Removing line breaks and whitespace...");
                inputData = inputData.Trim(new char[] { '\r', '\n', ' ' }); //REMOVE ALL NONSPECIFIC NOTATIONS
                inputData = inputData.Replace("@", "");
                inputData = inputData.Replace("N", "");
                inputData = inputData.Replace("U", "");
                inputData = inputData.Replace("W", "");
                inputData = inputData.Replace("S", "");
                inputData = inputData.Replace("M", "");
                inputData = inputData.Replace("K", "");
                inputData = inputData.Replace("R", "");
                inputData = inputData.Replace("Y", "");
                inputData = inputData.Replace("B", "");
                inputData = inputData.Replace("D", "");
                inputData = inputData.Replace("H", "");
                inputData = inputData.Replace("V", "");
                inputData = inputData.Replace("Z", "");

                
            if (inputData.Length > 1000000)

            {
                loadedDNA = inputData.Substring(0, 1000000); //take only the first million basepairs for time's sake. Will move to full size once process is faster
            }

            else

            {
                loadedDNA = inputData;
            }

                UpdateStatusBar("Chopping up DNA...");
                loadedDNALength = loadedDNA.Length;
                loadedNucleotides = new char[loadedDNALength];
                loadedNucleotides = loadedDNA.ToCharArray();
                UpdateStatusBar("Rendering combined image...");
                completedVisualization = MultiVisualize();
                UpdateStatusBar("Saving Abstraction Map to image file...");
                completedVisualization.Save(batchFolderPath + "\\" + Path.GetFileNameWithoutExtension(file) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                UpdateStatusBar("Abstraction Map saved!");
                UpdateVisualizationWindow(completedVisualization);
            }
            UpdateStatusBar("Batch visualization complete!!!");
            ButtonStates("off");
        }
        


        private void BtnAbstractorSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialogOutputDir = new FolderBrowserDialog();


            if (folderBrowserDialogOutputDir.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    visualizationFolderPath = folderBrowserDialogOutputDir.SelectedPath;
                }
                catch
                {

                }
            }
            UpdateStatusBar("Saving Abstraction Map to image file...");
            completedVisualization.Save(visualizationFolderPath + "\\" + Path.GetFileNameWithoutExtension(visualizationFileName) + ".png", System.Drawing.Imaging.ImageFormat.Png);
            UpdateStatusBar("Abstraction Map saved!");
            completedVisualization.Dispose();
        }



        private void BtnBatchVisualize_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Loading Genomic Data...");


            FolderBrowserDialog folderBrowserDialogInputDir = new FolderBrowserDialog();
            FolderBrowserDialog folderBrowserDialogOutputDir = new FolderBrowserDialog();
          

            if (folderBrowserDialogInputDir.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    batchFolderPath = folderBrowserDialogInputDir.SelectedPath;
                    DirectoryInfo subdirectoryEntries = new DirectoryInfo(batchFolderPath);
                    foreach (DirectoryInfo subdirectory in subdirectoryEntries.GetDirectories())
                    {
                        foreach (FileInfo file in subdirectory.GetFiles("*.fna"))  //change depending on target file
                        {
                            batchAbstractorFiles.Add(file.FullName);
                        }
                    }

                    if (folderBrowserDialogOutputDir.ShowDialog() == DialogResult.OK)
                    {
                        batchFolderPath = folderBrowserDialogOutputDir.SelectedPath;
                        Thread batchVisualizer = new Thread(batchVisualize);
                        UpdateStatusBar("Initiating batch visualization thread...");
                        batchVisualizer.SetApartmentState(ApartmentState.STA);
                        batchVisualizer.IsBackground = true;
                        batchVisualizer.Start();
                        ButtonStates("off");
                    }
                    else
                    {
                        ButtonStates("on");
                    }
                }

                catch
                {

                }
            }
            
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (abstractorVisualizer != null)
            abstractorVisualizer.Abort();
            if (batchVisualizer != null)
            batchVisualizer.Abort();
            this.Close();
        }

        private void GeneticVisualAbstractor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (abstractorVisualizer != null)
                abstractorVisualizer.Abort();
            if (batchVisualizer != null)
                batchVisualizer.Abort();
        }
    }
}
