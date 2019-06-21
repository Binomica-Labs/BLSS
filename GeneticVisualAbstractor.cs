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
        public Image ladderImage;

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

        //Ratio Image Stuff
        public int nucleotideCountA;
        public int nucleotideCountT;
        public int nucleotideCountC;
        public int nucleotideCountG;
        public double percentA;
        public double percentT;
        public double percentC;
        public double percentG;
        public int nucleotideTotal;
        public int dnaImageSize;
        public Image ratioImage;


        public static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
            {                                                                                       //string extension to chop string into chunks of a user defined size, super useful!
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
            }
        }



        public Image visualizeDNA(char[] input)
        {
            PickNucleotideColor();

            if (loadedDNALength / 200 < 1)
            {                                           //logic to handle when input DNA length is less than a full row across the data image column
                entropyImageHeight = 1;
            }

            else
            {
                entropyImageHeight = (loadedDNALength + (200 - (loadedDNALength % 200)))/ 200;
                int nucleotideRemainder = 200 - (loadedDNALength % 200);
                List<char> tempNucleotides = new List<char>(input);
                for (int i = 0; i <= nucleotideRemainder; i++)
                {
                    tempNucleotides.Add('X');
                }
                input = tempNucleotides.ToArray<char>();
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
                            colorPosition++;                                //nested for loop going through each pixel in the column and assigning color based on corresponding DNA letter
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
                        else if (input[colorPosition] == 'X')
                        {
                            visualizedDNA.SetPixel(x, y, Color.Magenta);        //X denotes padding on last row of image, to make divisible cleanly by width (200bp)
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

                    else if (input[colorPosition] == 'X')
                    {
                        visualizedDNA.SetPixel(i, 0, Color.Magenta);
                        colorPosition++;
                    }
                }
            }

            colorPosition = 0;
            return visualizedDNA;
        }



        public void PickNucleotideColor()
        {
            try
            {
                colorA = Color.FromArgb(0, 230, 255); //cyan
                colorT = Color.FromArgb(255, 255, 0); //yellow
                colorC = Color.FromArgb(220, 0, 0); //red               //these colors work well and their optical illusion of blending makes for very distinct pattern recognition
                colorG = Color.FromArgb(0, 0, 0); //black
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }



        private void AbstractDNA(int filterSize)
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
                abstractCountT = segment.Count(x => x == 'T');      //count how many of each letter there are in the segment of dna
                abstractCountC = segment.Count(x => x == 'C');
                abstractCountG = segment.Count(x => x == 'G');

                if (abstractCountA > abstractCountC &&
                    abstractCountA > abstractCountT &&
                    abstractCountA > abstractCountG)
                {
                    abstractedDNA = abstractedDNA + new string('A', segment.Length);
                }

                else if (abstractCountC > abstractCountA &&
                         abstractCountC > abstractCountT &&                                 //check which letter occurs most frequently in said segment of DNA
                         abstractCountC > abstractCountG)                                   //replace the entire segment of DNA with an eqivalent segment full of just the dominant letter
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

            abstractedNucleotides = abstractedDNA.ToCharArray();                            //take this new data and apply it to a new char[] array to be visualized later
        }



        public Image GenerateLadderDNA()       //function for adding a scale bar or ladder to the visualizations so one can pinpoint the region of interest visually and genomically
        {
            
            ladderLength = entropyImageHeight;
            int ladderRung = ladderLength / 10;     //scale the ladder to the input DNA length
            rungArray = new string[ladderLength];


            rungArray[0] = String.Join("", new string('G', 50), new string('T', 150));      //add a long rung at the very first row

            for (int y = 1; y < ladderLength; y++)
            {
                rungArray[y] = String.Join("", new string('G', 199), new string('T', 1));
            }



            for (int y = ladderRung / 10; y < ladderLength; y += ladderRung / 10)
            {
                rungArray[y] = String.Join("", new string('G', 150), new string('T', 50));
            }



            for (int y = ladderRung; y < ladderLength; y += ladderRung)
            {
                rungArray[y] = String.Join("", new string('G', 100), new string('T', 100));
            }

            rungArray[ladderLength - 1] = String.Join("", new string('G', 50), new string('T', 150));       //add a long rung on the last row to bookend the ladder


            char[] ladderNucleotides = String.Join("", rungArray).ToCharArray();
            Image ladderImage = visualizeDNA(ladderNucleotides);
            return ladderImage;
        }



        public Bitmap MultiVisualize()
        {
            UpdateStatusBar("Calculating nucleotide ratios...");
            ratioImage = RatioImage();
            if (loadedDNALength > 50000)
            {
                UpdateStatusBar("Calculating ladder size...");
                ladderImage = GenerateLadderDNA();
            }
        
            UpdateStatusBar("Abstracting pixel perfect column...");
            visualAbstraction1 = visualizeDNA(loadedNucleotides);
            UpdateStatusBar("Abstracting 20bp column...");
            AbstractDNA(20);
            visualAbstraction20 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 40bp column...");                              //iterate through a set of filter values and produce an image for that corresponding abstraction
            AbstractDNA(40);
            visualAbstraction40 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 80bp column...");
            AbstractDNA(80);
            visualAbstraction80 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 100bp column...");
            AbstractDNA(100);
            visualAbstraction100 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 200bp column...");
            AbstractDNA(200);
            visualAbstraction200 = visualizeDNA(abstractedNucleotides);
            UpdateStatusBar("Abstracting 400bp column...");
            AbstractDNA(400);
            visualAbstraction400 = visualizeDNA(abstractedNucleotides);
            

            Bitmap combinedVisualAbstraction = new Bitmap((visualAbstraction1.Width +
                visualAbstraction20.Width +
                visualAbstraction40.Width +
                visualAbstraction80.Width +
                visualAbstraction100.Width +                                                          
                visualAbstraction200.Width +
                visualAbstraction400.Width +
                ratioImage.Width +
                200),
                visualAbstraction1.Height);

            using (Graphics g = Graphics.FromImage(combinedVisualAbstraction))
            {

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                {
                    g.FillRectangle(brush, 0, 0, combinedVisualAbstraction.Width, combinedVisualAbstraction.Height);
                }

                if (ladderImage != null)
                {
                    g.DrawImage(ladderImage, visualAbstraction1.Width + 5, 0);
                }
                g.DrawImage(visualAbstraction1, visualAbstraction1.Width * 2 + 6, 0);
                g.DrawImage(visualAbstraction20, visualAbstraction1.Width * 3 + 11, 0);
                g.DrawImage(visualAbstraction40, visualAbstraction1.Width * 4 + 16, 0);                //stitch all the abstraction images together in order of increasing filter size on one large image
                g.DrawImage(visualAbstraction80, visualAbstraction1.Width * 5 + 21, 0);                //make a new image large enough to fit all 7 data columns + 5 pixels of spacer between each
                g.DrawImage(visualAbstraction100, visualAbstraction1.Width * 6 + 26, 0);                //ladder image is the only image touching the actual data columns
                g.DrawImage(visualAbstraction200, visualAbstraction1.Width * 7 + 31, 0);
                g.DrawImage(visualAbstraction400, visualAbstraction1.Width * 8 + 36, 0);
                g.DrawImage(ratioImage, 100, 0);
            }

            
            visualAbstraction1.Dispose();
            visualAbstraction20.Dispose();
            visualAbstraction40.Dispose();
            visualAbstraction80.Dispose();
            visualAbstraction100.Dispose();
            visualAbstraction200.Dispose();
            visualAbstraction400.Dispose();
            ratioImage.Dispose();
            if (ladderImage != null)
            {
                ladderImage.Dispose();
            }
            return combinedVisualAbstraction;
        }



        public void UpdateStatusBar(string status)
        {
            txtConsoleOutput.Invoke((MethodInvoker)delegate
            {                                                           //be able to change the GUI console textbox from another thread
                txtConsoleOutput.Text = status;
            });
        }



        public void UpdateVisualizationWindow(Image currentImage)
        {
            picAbstractor.Invoke((MethodInvoker)delegate
            {                                                           //be able to update the picture box gui control from another thread
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
                    btnBatchVisualize.Enabled = false;                      //enable or disable the buttons during processing to avoid accedental cross-thread malarkey
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
                            fileLines = File.ReadAllLines(openFileDialog1.FileName);                //pick the single FASTA .fna file you wish to visualize
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
                inputData = inputData.Trim(new char[] { '\r', '\n', ' ' }); //REMOVE ALL WHITESPACE, CARRIAGE RETURNS, AND NEWLINES
                inputData = inputData.Replace("@", "");
                inputData = inputData.Replace("N", "");
                inputData = inputData.Replace("U", "");
                inputData = inputData.Replace("W", "");
                inputData = inputData.Replace("S", "");
                inputData = inputData.Replace("M", "");
                inputData = inputData.Replace("K", "");             //REMOVE ALL IUPAC NOTATIONS ASIDE FROM A, T, C, AND G
                inputData = inputData.Replace("R", "");
                inputData = inputData.Replace("Y", "");
                inputData = inputData.Replace("B", "");
                inputData = inputData.Replace("D", "");
                inputData = inputData.Replace("H", "");
                inputData = inputData.Replace("V", "");
                inputData = inputData.Replace("Z", "");


                if (inputData.Length > 1000000)

                {
                    loadedDNA = inputData;//.Substring(0, 1000000); //take only the first million basepairs for time's sake. Will move to full size once process is faster
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

                inputData = string.Join("", fileLines);
                UpdateStatusBar("Removing line breaks and whitespace...");
                inputData = inputData.Trim(new char[] { '\r', '\n', ' ' }); //REMOVE ALL NONSPECIFIC NOTATIONS
                inputData = inputData.Replace("@", "");
                inputData = inputData.Replace("N", "");
                inputData = inputData.Replace("U", "");
                inputData = inputData.Replace("W", "");
                inputData = inputData.Replace("S", "");
                inputData = inputData.Replace("M", "");
                inputData = inputData.Replace("K", "");             //REMOVE ALL IUPAC NOTATIONS ASIDE FROM A, T, C, AND G
                inputData = inputData.Replace("R", "");
                inputData = inputData.Replace("Y", "");
                inputData = inputData.Replace("B", "");
                inputData = inputData.Replace("D", "");
                inputData = inputData.Replace("H", "");
                inputData = inputData.Replace("V", "");
                inputData = inputData.Replace("Z", "");

                
            if (inputData.Length > 1000000)

            {
                    loadedDNA = inputData;//.Substring(0, 1000000); //take only the first million basepairs for time's sake. Will move to full size once process is faster
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
                        foreach (FileInfo file in subdirectory.GetFiles("*.fna"))  //make a list of all the FASTA .fna files in all sub-directories and save that info as full path data
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



        public Image RatioImage()
        {
            dnaImageSize = (loadedDNALength + (200 - (loadedDNALength % 200))) / 200;           //logic to make the image height rounded up to the nearest 200

            nucleotideCountA = loadedDNA.Count(x => x == 'A');
            nucleotideCountT = loadedDNA.Count(x => x == 'T');      //count how many of each letter there are in the loadedDNA of dna
            nucleotideCountC = loadedDNA.Count(x => x == 'C');
            nucleotideCountG = loadedDNA.Count(x => x == 'G');
            nucleotideTotal = nucleotideCountA + nucleotideCountC + nucleotideCountT + nucleotideCountG;

            percentA = nucleotideCountA / nucleotideTotal;
            percentT = nucleotideCountT / nucleotideTotal;
            percentC = nucleotideCountC / nucleotideTotal;
            percentG = nucleotideCountG / nucleotideTotal;

            char[] ratioNucleotideSequence = (new string('A', nucleotideCountA) +
                                             new string('T', nucleotideCountT) +
                                             new string('G', nucleotideCountG) +
                                             new string('C', nucleotideCountC)).ToCharArray();

            Image ratioImage = visualizeDNA(ratioNucleotideSequence);

            return ratioImage;
        }



        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (abstractorVisualizer != null)
            abstractorVisualizer.Abort();               //kill threads on exit click
            if (batchVisualizer != null)
            batchVisualizer.Abort();
            this.Close();
        }



        private void GeneticVisualAbstractor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (abstractorVisualizer != null)
                abstractorVisualizer.Abort();               //kill threads during closing
            if (batchVisualizer != null)
                batchVisualizer.Abort();
        }



        private void GeneticVisualAbstractor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (abstractorVisualizer != null)
                abstractorVisualizer.Abort();
            if (batchVisualizer != null)                //kill threads after closing...just in case, lol
                batchVisualizer.Abort();
        }
    }
}
