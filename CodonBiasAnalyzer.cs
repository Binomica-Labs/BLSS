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
    public partial class CodonBiasAnalyzer : Form
    {
        public CodonBiasAnalyzer()
        {
            InitializeComponent();
        }

        public bool kazusaStandardOutputChoice;
        public string monolith;
        public string[] codingSequences;
        public string[] inputSequenceCodons;
        public char[] aminoArray;
        public string[] dominantCodonArray = new string[21];
        public string dominantCodonList;
        public bool biasProcessThreadComplete;
        public string inputData;
        public string[] monolithCodons;
        public string[] individualCodons;
        public string optimizedSequence;
        public string statusUpdateString = "";
        string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);



        #region Count Variables
        public float
            countAAA, //A
            countAAT,
            countAAC,
            countAAG,
            countATA,
            countATT,
            countATC,
            countATG,
            countACA,
            countACT,
            countACC,
            countACG,
            countAGA,
            countAGT,
            countAGC,
            countAGG,
            countTAA, //T
            countTAT,
            countTAC,
            countTAG,
            countTTA,
            countTTT,
            countTTC,
            countTTG,
            countTCA,
            countTCT,
            countTCC,
            countTCG,
            countTGA,
            countTGT,
            countTGC,
            countTGG,
            countCAA, //C
            countCAT,
            countCAC,
            countCAG,
            countCTA,
            countCTT,
            countCTC,
            countCTG,
            countCCA,
            countCCT,
            countCCC,
            countCCG,
            countCGA,
            countCGT,
            countCGC,
            countCGG,
            countGAA, //G
            countGAT,
            countGAC,
            countGAG,
            countGTA,
            countGTT,
            countGTC,
            countGTG,
            countGCA,
            countGCT,
            countGCC,
            countGCG,
            countGGA,
            countGGT,
            countGGC,
            countGGG,
            CDSCount,
            codonCount;
        #endregion




        #region Percent Variables
        public float
            percentAAA, //A
            percentAAT,
            percentAAC,
            percentAAG,
            percentATA,
            percentATT,
            percentATC,
            percentATG,
            percentACA,
            percentACT,
            percentACC,
            percentACG,
            percentAGA,
            percentAGT,
            percentAGC,
            percentAGG,
            percentTAA, //T
            percentTAT,
            percentTAC,
            percentTAG,
            percentTTA,
            percentTTT,
            percentTTC,
            percentTTG,
            percentTCA,
            percentTCT,
            percentTCC,
            percentTCG,
            percentTGA,
            percentTGT,
            percentTGC,
            percentTGG,
            percentCAA, //C
            percentCAT,
            percentCAC,
            percentCAG,
            percentCTA,
            percentCTT,
            percentCTC,
            percentCTG,
            percentCCA,
            percentCCT,
            percentCCC,
            percentCCG,
            percentCGA,
            percentCGT,
            percentCGC,
            percentCGG,
            percentGAA, //G
            percentGAT,
            percentGAC,
            percentGAG,
            percentGTA,
            percentGTT,
            percentGTC,
            percentGTG,
            percentGCA,
            percentGCT,
            percentGCC,
            percentGCG,
            percentGGA,
            percentGGT,
            percentGGC,
            percentGGG;
#endregion



        //comparative codonomics variables
        public int refSeqCDSCount = 2;
        public int comparativeCDSCount;
        public Bitmap bmpRefSeq;
        public bool[] isDifferent = new bool[21];
        public Graphics grafRef;



        private void CodonGUI_Load(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(desktopPath + "\\Codon Usage Tables");
            FileInfo[] Files = d.GetFiles(); //Getting Text files

            comboBox1.DataSource = Files;
            comboBox1.DisplayMember = "Name";
        }



        private void BtnIndividual_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Processing Individual Codon Bias Table...");
            ProcessIndividualCDSBias();
            UpdateStatusBar("Processing Done!");
        }



        private void BtnLoad_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Loading Genomic Data...");
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            inputData = "";


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
                            CDSCount = File.ReadAllText(openFileDialog1.FileName).Count(x => x == '>');

                            UpdateStatusBar("Removing FASTA comments...");
                            var oldLines = File.ReadAllLines(openFileDialog1.FileName);

                            for (int i = 0; i < oldLines.Length; i++)
                            {
                                if (oldLines[i].Contains('>'))
                                {
                                    oldLines[i] = "@"; //replace entire FASTA comment line with a single @ for character delimited CDS's, not elegant but works
                                }
                            }

                            //var newLines = oldLines.Where(line => !line.Contains('>'));
                            string tempPath = desktopPath + "\\" + txtLatinName.Text + "FormattedCDSCollection " + DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss") + ".FASTA";
                            File.WriteAllLines(tempPath, oldLines);
                            inputData = string.Join("", File.ReadAllLines(tempPath));


                            UpdateStatusBar("Removing line breaks and whitespace...");
                            inputData = inputData.TrimEnd(new char[] { '\r', '\n', ' ' });
                            codingSequences = inputData.Split('@');
                            monolith = inputData.Replace("@", "");
                            UpdateStatusBar("Saving CDS Monolith File...");

                            File.WriteAllText(desktopPath + "\\" + txtLatinName.Text + "CDSMonolith " + DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss") + ".FASTA", monolith);


                            UpdateStatusBar("Genetic Data Loaded!");

                        }
                    }
                }
                catch (Exception)
                {
                    UpdateStatusBar("Error! Can't parse your file. Is it in FASTA format?");
                }
            }

        }



        private void BtnProcess_Click(object sender, EventArgs e)
        {
            ClearCounts();
            UpdateStatusBar("Chopping CDS monolith into codons...");

            monolithCodons = ChunksUpto(monolith, 3).ToArray<string>();
            UpdateStatusBar("Counting codons...");


            for (int i = 0; i < monolithCodons.Length; i++)
            {
                switch (monolithCodons[i])
                {

                    case "AAA":
                        countAAA++;
                        break;

                    case "AAT":
                        countAAT++;
                        break;

                    case "AAC":
                        countAAC++;
                        break;

                    case "AAG":
                        countAAG++;
                        break;

                    case "ATA":
                        countATA++;
                        break;

                    case "ATT":
                        countATT++;
                        break;

                    case "ATC":
                        countATC++;
                        break;

                    case "ATG":
                        countATG++;
                        break;

                    case "ACA":
                        countACA++;
                        break;

                    case "ACT":
                        countACT++;
                        break;

                    case "ACC":
                        countACC++;
                        break;

                    case "ACG":
                        countACG++;
                        break;

                    case "AGA":
                        countAGA++;
                        break;

                    case "AGT":
                        countAGT++;
                        break;

                    case "AGC":
                        countAGC++;
                        break;

                    case "AGG":
                        countAGG++;
                        break;

                    case "TAA":
                        countTAA++;
                        break;

                    case "TAT":
                        countTAT++;
                        break;

                    case "TAC":
                        countTAC++;
                        break;

                    case "TAG":
                        countTAG++;
                        break;

                    case "TTA":
                        countTTA++;
                        break;

                    case "TTT":
                        countTTT++;
                        break;

                    case "TTC":
                        countTTC++;
                        break;

                    case "TTG":
                        countTTG++;
                        break;

                    case "TCA":
                        countTCA++;
                        break;

                    case "TCT":
                        countTCT++;
                        break;

                    case "TCC":
                        countTCC++;
                        break;

                    case "TCG":
                        countTCG++;
                        break;

                    case "TGA":
                        countTGA++;
                        break;

                    case "TGT":
                        countTGT++;
                        break;

                    case "TGC":
                        countTGC++;
                        break;

                    case "TGG":
                        countTGG++;
                        break;

                    case "CAA":
                        countCAA++;
                        break;

                    case "CAT":
                        countCAT++;
                        break;

                    case "CAC":
                        countCAC++;
                        break;

                    case "CAG":
                        countCAG++;
                        break;

                    case "CTA":
                        countCTA++;
                        break;

                    case "CTT":
                        countCTT++;
                        break;

                    case "CTC":
                        countCTC++;
                        break;

                    case "CTG":
                        countCTG++;
                        break;

                    case "CCA":
                        countCCA++;
                        break;

                    case "CCT":
                        countCCT++;
                        break;

                    case "CCC":
                        countCCC++;
                        break;

                    case "CCG":
                        countCCG++;
                        break;

                    case "CGA":
                        countCGA++;
                        break;

                    case "CGT":
                        countCGT++;
                        break;

                    case "CGC":
                        countCGC++;
                        break;

                    case "CGG":
                        countCGG++;
                        break;

                    case "GAA":
                        countGAA++;
                        break;

                    case "GAT":
                        countGAT++;
                        break;

                    case "GAC":
                        countGAC++;
                        break;

                    case "GAG":
                        countGAG++;
                        break;

                    case "GTA":
                        countGTA++;
                        break;

                    case "GTT":
                        countGTT++;
                        break;

                    case "GTC":
                        countGTC++;
                        break;

                    case "GTG":
                        countGTG++;
                        break;

                    case "GCA":
                        countGCA++;
                        break;

                    case "GCT":
                        countGCT++;
                        break;

                    case "GCC":
                        countGCC++;
                        break;

                    case "GCG":
                        countGCG++;
                        break;

                    case "GGA":
                        countGGA++;
                        break;

                    case "GGT":
                        countGGT++;
                        break;

                    case "GGC":
                        countGGC++;
                        break;

                    case "GGG":
                        countGGG++;
                        break;

                    default:
                        break;
                }

            }
            UpdateStatusBar("Counting complete!");

            ProcessPercents();
            /*
              lblAAA.Text = countAAA.ToString();
              lblAAT.Text = countAAT.ToString();
              lblAAC.Text = countAAC.ToString();
              lblAAG.Text = countAAG.ToString();

              lblATA.Text = countATA.ToString();
              lblATT.Text = countATT.ToString();
              lblATC.Text = countATC.ToString();
              lblATG.Text = countATG.ToString();

              lblACA.Text = countACA.ToString();
              lblACT.Text = countACT.ToString();
              lblACC.Text = countACC.ToString();
              lblACG.Text = countACG.ToString();

              lblAGA.Text = countAGA.ToString();
              lblAGT.Text = countAGT.ToString();
              lblAGC.Text = countAGC.ToString();
              lblAGG.Text = countAGG.ToString();

              lblCAA.Text = countCAA.ToString();
              lblCAT.Text = countCAT.ToString();
              lblCAC.Text = countCAC.ToString();
              lblCAG.Text = countCAG.ToString();

              lblCTA.Text = countCTA.ToString();
              lblCTT.Text = countCTT.ToString();
              lblCTC.Text = countCTC.ToString();
              lblCTG.Text = countCTG.ToString();

              lblCCA.Text = countCCA.ToString();
              lblCCT.Text = countCCT.ToString();
              lblCCC.Text = countCCC.ToString();
              lblCCG.Text = countCCG.ToString();

              lblCGA.Text = countCGA.ToString();
              lblCGT.Text = countCGT.ToString();
              lblCGC.Text = countCGC.ToString();
              lblCGG.Text = countCGG.ToString();

              lblTAA.Text = countTAA.ToString();
              lblTAT.Text = countTAT.ToString();
              lblTAC.Text = countTAC.ToString();
              lblTAG.Text = countTAG.ToString();

              lblTTA.Text = countTTA.ToString();
              lblTTT.Text = countTTT.ToString();
              lblTTC.Text = countTTC.ToString();
              lblTTG.Text = countTTG.ToString();

              lblTCA.Text = countTCA.ToString();
              lblTCT.Text = countTCT.ToString();
              lblTCC.Text = countTCC.ToString();
              lblTCG.Text = countTCG.ToString();

              lblTGA.Text = countTGA.ToString();
              lblTGT.Text = countTGT.ToString();
              lblTGC.Text = countTGC.ToString();
              lblTGG.Text = countTGG.ToString();

              lblGAA.Text = countGAA.ToString();
              lblGAT.Text = countGAT.ToString();
              lblGAC.Text = countGAC.ToString();
              lblGAG.Text = countGAG.ToString();

              lblGTA.Text = countGTA.ToString();
              lblGTT.Text = countGTT.ToString();
              lblGTC.Text = countGTC.ToString();
              lblGTG.Text = countGTG.ToString();

              lblGCA.Text = countGCA.ToString();
              lblGCT.Text = countGCT.ToString();
              lblGCC.Text = countGCC.ToString();
              lblGCG.Text = countGCG.ToString();

              lblGGA.Text = countGGA.ToString();
              lblGGT.Text = countGGT.ToString();
              lblGGC.Text = countGGC.ToString();
              lblGGG.Text = countGGG.ToString();
            */

            lblAAA.Text = percentAAA.ToString();
            lblAAT.Text = percentAAT.ToString();
            lblAAC.Text = percentAAC.ToString();
            lblAAG.Text = percentAAG.ToString();

            lblATA.Text = percentATA.ToString();
            lblATT.Text = percentATT.ToString();
            lblATC.Text = percentATC.ToString();
            lblATG.Text = percentATG.ToString();

            lblACA.Text = percentACA.ToString();
            lblACT.Text = percentACT.ToString();
            lblACC.Text = percentACC.ToString();
            lblACG.Text = percentACG.ToString();

            lblAGA.Text = percentAGA.ToString();
            lblAGT.Text = percentAGT.ToString();
            lblAGC.Text = percentAGC.ToString();
            lblAGG.Text = percentAGG.ToString();

            lblCAA.Text = percentCAA.ToString();
            lblCAT.Text = percentCAT.ToString();
            lblCAC.Text = percentCAC.ToString();
            lblCAG.Text = percentCAG.ToString();

            lblCTA.Text = percentCTA.ToString();
            lblCTT.Text = percentCTT.ToString();
            lblCTC.Text = percentCTC.ToString();
            lblCTG.Text = percentCTG.ToString();

            lblCCA.Text = percentCCA.ToString();
            lblCCT.Text = percentCCT.ToString();
            lblCCC.Text = percentCCC.ToString();
            lblCCG.Text = percentCCG.ToString();

            lblCGA.Text = percentCGA.ToString();
            lblCGT.Text = percentCGT.ToString();
            lblCGC.Text = percentCGC.ToString();
            lblCGG.Text = percentCGG.ToString();

            lblTAA.Text = percentTAA.ToString();
            lblTAT.Text = percentTAT.ToString();
            lblTAC.Text = percentTAC.ToString();
            lblTAG.Text = percentTAG.ToString();

            lblTTA.Text = percentTTA.ToString();
            lblTTT.Text = percentTTT.ToString();
            lblTTC.Text = percentTTC.ToString();
            lblTTG.Text = percentTTG.ToString();

            lblTCA.Text = percentTCA.ToString();
            lblTCT.Text = percentTCT.ToString();
            lblTCC.Text = percentTCC.ToString();
            lblTCG.Text = percentTCG.ToString();

            lblTGA.Text = percentTGA.ToString();
            lblTGT.Text = percentTGT.ToString();
            lblTGC.Text = percentTGC.ToString();
            lblTGG.Text = percentTGG.ToString();

            lblGAA.Text = percentGAA.ToString();
            lblGAT.Text = percentGAT.ToString();
            lblGAC.Text = percentGAC.ToString();
            lblGAG.Text = percentGAG.ToString();

            lblGTA.Text = percentGTA.ToString();
            lblGTT.Text = percentGTT.ToString();
            lblGTC.Text = percentGTC.ToString();
            lblGTG.Text = percentGTG.ToString();

            lblGCA.Text = percentGCA.ToString();
            lblGCT.Text = percentGCT.ToString();
            lblGCC.Text = percentGCC.ToString();
            lblGCG.Text = percentGCG.ToString();

            lblGGA.Text = percentGGA.ToString();
            lblGGT.Text = percentGGT.ToString();
            lblGGC.Text = percentGGC.ToString();
            lblGGG.Text = percentGGG.ToString();

            lblCodonCount.Text = monolithCodons.Length.ToString();
            lblCDSCount.Text = CDSCount.ToString();
            UpdateStatusBar("Bias Table Calculated!");
            DisplayMostFrequentCodon();
        }



        private void BtnSave_Click(object sender, EventArgs e)
        {


            UpdateStatusBar("Saving Codon Bias Table...");
            dominantCodonList = "";
            foreach (string dominantCodon in dominantCodonArray)
            {
                dominantCodonList = dominantCodonList + "," + dominantCodon;
            }
            File.WriteAllText(desktopPath + "\\Codon Usage Tables\\" + txtLatinName.Text + ".csv", txtLatinName.Text + dominantCodonList);

            UpdateStatusBar("Codon Bias Table Saved!");



            //for use with apps that need the typical Kazusa japanese database codon frequence table format known as SPSUM
            if (kazusaStandardOutputChoice)
            {
                //SPSUM output code

                /*
                 * 
                 * Kazusa output table format:
                 * 
                 * NCBI-txid:total-number-of-cds<endofline>
                 * CGA CGC CGG CGU AGA AGG 
                 * CUA CUC CUG CUU UUA UUG 
                 * UCA UCC UCG UCU AGC AGU 
                 * ACA ACC ACG ACU CCA CCC   
                 * CCG CCU GCA GCC GCG GCU 
                 * GGA GGC GGG GGU GUA GUC 
                 * GUG GUU AAA AAG AAC AAU 
                 * CAA CAG CAC CAU GAA GAG 
                 * GAC GAU UAC UAU UGC UGU 
                 * UUC UUU AUA AUC AUU AUG 
                 * UGG UAA UAG UGA<endofline>
                 * 
                 */
                using (var codonFile = File.CreateText(desktopPath + "\\Codon Usage Tables" + txtLatinName.Text + "Kazusa Format Table" + DateTime.Now.ToBinary().ToString() + ".spsum"))
                {
                    UpdateStatusBar("Saving Codon Bias Table...");
                    codonFile.WriteLine(txtTaxonID.Text + ":" + txtLatinName.Text + ":" + CDSCount);
                    codonFile.WriteLine(countCGA + " " + countCGC + " " + countCGG + " " + countCGT + " " + countAGA + " " + countAGG + " " +
                                        countCTA + " " + countCTC + " " + countCTG + " " + countCTT + " " + countTTA + " " + countTTG + " " +
                                        countTCA + " " + countTCC + " " + countTCG + " " + countTCT + " " + countAGC + " " + countAGT + " " +
                                        countACA + " " + countACC + " " + countACG + " " + countACT + " " + countCCA + " " + countCCC + " " +
                                        countCCG + " " + countCCT + " " + countGCA + " " + countGCC + " " + countGCG + " " + countGCT + " " +
                                        countGGA + " " + countGGC + " " + countGGG + " " + countGGT + " " + countGTA + " " + countGTC + " " +
                                        countGTG + " " + countGTT + " " + countAAA + " " + countAAG + " " + countAAC + " " + countAAT + " " +
                                        countCAA + " " + countCAG + " " + countCAC + " " + countCAT + " " + countGAA + " " + countGAG + " " +
                                        countGAC + " " + countGAT + " " + countTAC + " " + countTAT + " " + countTGC + " " + countTGT + " " +
                                        countTTC + " " + countTTT + " " + countATA + " " + countATC + " " + countATT + " " + countATG + " " +
                                        countTGG + " " + countTAA + " " + countTAG + " " + countTGA);

                }
            }
        }



        private void BtnOptimizeSequence_Click(object sender, EventArgs e)
        {
            try
            {

                using (var reader = new StreamReader(desktopPath + "\\Codon Usage Tables\\" + comboBox1.Text))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        dominantCodonArray = values.Skip(1).ToArray();
                    }
                }


                if (txtInputSequence.Text != "")
                {
                    txtAminoAcidEquivalent.Text = ConvertToProtein(txtInputSequence.Text);
                }
                aminoArray = txtAminoAcidEquivalent.Text.ToCharArray();
                optimizedSequence = "";
                foreach (char amino in aminoArray)
                {
                    switch (amino)
                    {
                        case 'A':
                            optimizedSequence += dominantCodonArray[0];
                            break;
                        case 'C':
                            optimizedSequence += dominantCodonArray[1];
                            break;
                        case 'D':
                            optimizedSequence += dominantCodonArray[2];
                            break;
                        case 'E':
                            optimizedSequence += dominantCodonArray[3];
                            break;
                        case 'F':
                            optimizedSequence += dominantCodonArray[4];
                            break;
                        case 'G':
                            optimizedSequence += dominantCodonArray[5];
                            break;
                        case 'H':
                            optimizedSequence += dominantCodonArray[6];
                            break;
                        case 'I':
                            optimizedSequence += dominantCodonArray[7];
                            break;
                        case 'K':
                            optimizedSequence += dominantCodonArray[8];
                            break;
                        case 'L':
                            optimizedSequence += dominantCodonArray[9];
                            break;
                        case 'M':
                            optimizedSequence += dominantCodonArray[10];
                            break;
                        case 'N':
                            optimizedSequence += dominantCodonArray[11];
                            break;
                        case 'P':
                            optimizedSequence += dominantCodonArray[12];
                            break;
                        case 'Q':
                            optimizedSequence += dominantCodonArray[13];
                            break;
                        case 'R':
                            optimizedSequence += dominantCodonArray[14];
                            break;
                        case 'S':
                            optimizedSequence += dominantCodonArray[15];
                            break;
                        case 'T':
                            optimizedSequence += dominantCodonArray[16];
                            break;
                        case 'V':
                            optimizedSequence += dominantCodonArray[17];
                            break;
                        case 'W':
                            optimizedSequence += dominantCodonArray[18];
                            break;
                        case 'Y':
                            optimizedSequence += dominantCodonArray[19];
                            break;
                        case '*':
                            optimizedSequence += dominantCodonArray[20];
                            break;

                        default:
                            break;
                    }
                }
                txtCodonOptimizedSequence.Text = optimizedSequence;
            }
            catch (Exception)
            {
                MessageBox.Show("No codon tables detected! Please make a new table using genomic CDS data.");
            }
        }



        private void BtnCompareSequences_Click(object sender, EventArgs e)
        {
            VisualizeComparison();
        }



        private void BtnLoadComparative_Click(object sender, EventArgs e)
        {

        }



        private void BtnLoadRefSeq_Click(object sender, EventArgs e)
        {
            UpdateStatusBar("Loading Reference Data...");
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            inputData = "";

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
                            CDSCount = File.ReadAllText(openFileDialog1.FileName).Count(x => x == '>');

                            UpdateStatusBar("Removing FASTA comments...");
                            var oldLines = File.ReadAllLines(openFileDialog1.FileName);

                            for (int i = 0; i < oldLines.Length; i++)
                            {
                                if (oldLines[i].Contains('>'))
                                {
                                    oldLines[i] = "@";
                                }
                            }

                            //var newLines = oldLines.Where(line => !line.Contains('>'));
                            string tempPath = desktopPath + "\\" + txtLatinName.Text + "FormattedCDSCollection " + DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss") + ".FASTA";
                            File.WriteAllLines(tempPath, oldLines);
                            inputData = string.Join("", File.ReadAllLines(tempPath));

                            UpdateStatusBar("Removing line breaks and whitespace...");
                            inputData = inputData.TrimEnd(new char[] { '\r', '\n', ' ' });
                            codingSequences = inputData.Split('@');
                            monolith = inputData.Replace("@", "");
                            UpdateStatusBar("Saving CDS Monolith File...");

                            File.WriteAllText(desktopPath + "\\" + txtLatinName.Text + "CDSMonolith " + DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss") + ".FASTA", monolith);

                            UpdateStatusBar("Genetic Data Loaded!");
                        }
                    }
                }
                catch (Exception)
                {
                    UpdateStatusBar("Error! Can't parse your file. Is it in FASTA format?");
                }
            }
        }



        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(desktopPath + "\\Codon Usage Tables");
            FileInfo[] Files = d.GetFiles(); //Getting Text files

            comboBox1.DataSource = Files;
            comboBox1.DisplayMember = "Name";
        }



        public static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
            {
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
            }
        }



        public void VisualizeComparison()
        {
            isDifferent[1] = true;
            isDifferent[5] = true;
            isDifferent[6] = true;
            isDifferent[7] = true;
            isDifferent[14] = true;
            isDifferent[19] = true;

            //bmpRefSeq = new Bitmap(21, refSeqCDSCount);
            //grafRef = Graphics.FromImage(bmpRefSeq);
            //grafRef.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            //grafRef.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

            /*   for (int y = 0; y < refSeqCDSCount; y++)
               {
                   for (int i = 0; i < isDifferent.Length; i++)
                   {
                       if (isDifferent[i] == true)
                       {
                           bmpRefSeq.SetPixel(i, y, Color.Yellow);

                       }
                       else
                       {
                           bmpRefSeq.SetPixel(i, y, Color.Cyan);
                       }
                   }
               }
               */

            // picReference.Image = bmpRefSeq;
            picReference.Image = this.Draw(21, refSeqCDSCount);

        }



        public Bitmap Draw(int width, int height)
        {
            var bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var graphics = Graphics.FromImage(bitmap);
            var brushDifferent = new SolidBrush(Color.Red);
            var brushSimilar = new SolidBrush(Color.Yellow);

            //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            //graphics.FillRectangle(new SolidBrush(Color.Tomato), 10, 10, 100, 100);
            for (int y = 0; y < refSeqCDSCount; y++)
            {
                for (int i = 0; i < isDifferent.Length; i++)
                {
                    if (isDifferent[i] == true)
                    {
                        graphics.FillRectangle(brushDifferent, i, y, 1, 1);
                    }
                    else
                    {
                        graphics.FillRectangle(brushSimilar, i, y, 1, 1);
                    }
                }
            }
            return bitmap;
        }



        public void UpdateStatusBar(string status)
        {
            statusUpdateString = status;
            txtConsoleOutput.Invoke((MethodInvoker)delegate
            {

                txtConsoleOutput.Text = statusUpdateString;
            });

        }



        public string ConvertToProtein(string inputDNA)
        {

            string cds = "";
            inputSequenceCodons = ChunksUpto(inputDNA, 3).ToArray<string>();

            for (int i = 0; i < inputSequenceCodons.Length; i++)
            {
                switch (inputSequenceCodons[i])
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
                        cds += "*";
                        break;

                    case "TAG":
                        cds += "*";
                        break;

                    case "TGA":
                        cds += "*";
                        break;

                    default:
                        break;
                }


            }
            return cds;
        }



        public void ClearCounts()
        {
            countAAA = 0; //A
            countAAT = 0;
            countAAC = 0;
            countAAG = 0;
            countATA = 0;
            countATT = 0;
            countATC = 0;
            countATG = 0;
            countACA = 0;
            countACT = 0;
            countACC = 0;
            countACG = 0;
            countAGA = 0;
            countAGT = 0;
            countAGC = 0;
            countAGG = 0;
            countTAA = 0; //T
            countTAT = 0;
            countTAC = 0;
            countTAG = 0;
            countTTA = 0;
            countTTT = 0;
            countTTC = 0;
            countTTG = 0;
            countTCA = 0;
            countTCT = 0;
            countTCC = 0;
            countTCG = 0;
            countTGA = 0;
            countTGT = 0;
            countTGC = 0;
            countTGG = 0;
            countCAA = 0; //C
            countCAT = 0;
            countCAC = 0;
            countCAG = 0;
            countCTA = 0;
            countCTT = 0;
            countCTC = 0;
            countCTG = 0;
            countCCA = 0;
            countCCT = 0;
            countCCC = 0;
            countCCG = 0;
            countCGA = 0;
            countCGT = 0;
            countCGC = 0;
            countCGG = 0;
            countGAA = 0; //G
            countGAT = 0;
            countGAC = 0;
            countGAG = 0;
            countGTA = 0;
            countGTT = 0;
            countGTC = 0;
            countGTG = 0;
            countGCA = 0;
            countGCT = 0;
            countGCC = 0;
            countGCG = 0;
            countGGA = 0;
            countGGT = 0;
            countGGC = 0;
            countGGG = 0;

            //percents
            percentAAA = 0; //A
            percentAAT = 0;
            percentAAC = 0;
            percentAAG = 0;
            percentATA = 0;
            percentATT = 0;
            percentATC = 0;
            percentATG = 0;
            percentACA = 0;
            percentACT = 0;
            percentACC = 0;
            percentACG = 0;
            percentAGA = 0;
            percentAGT = 0;
            percentAGC = 0;
            percentAGG = 0;
            percentTAA = 0; //T
            percentTAT = 0;
            percentTAC = 0;
            percentTAG = 0;
            percentTTA = 0;
            percentTTT = 0;
            percentTTC = 0;
            percentTTG = 0;
            percentTCA = 0;
            percentTCT = 0;
            percentTCC = 0;
            percentTCG = 0;
            percentTGA = 0;
            percentTGT = 0;
            percentTGC = 0;
            percentTGG = 0;
            percentCAA = 0; //C
            percentCAT = 0;
            percentCAC = 0;
            percentCAG = 0;
            percentCTA = 0;
            percentCTT = 0;
            percentCTC = 0;
            percentCTG = 0;
            percentCCA = 0;
            percentCCT = 0;
            percentCCC = 0;
            percentCCG = 0;
            percentCGA = 0;
            percentCGT = 0;
            percentCGC = 0;
            percentCGG = 0;
            percentGAA = 0; //G
            percentGAT = 0;
            percentGAC = 0;
            percentGAG = 0;
            percentGTA = 0;
            percentGTT = 0;
            percentGTC = 0;
            percentGTG = 0;
            percentGCA = 0;
            percentGCT = 0;
            percentGCC = 0;
            percentGCG = 0;
            percentGGA = 0;
            percentGGT = 0;
            percentGGC = 0;
            percentGGG = 0;
        }



        public void ProcessPercents()
        {
            //alanine
            percentGCA = countGCA / (countGCA + countGCC + countGCT + countGCG) * 100;
            percentGCT = countGCT / (countGCA + countGCC + countGCT + countGCG) * 100;
            percentGCC = countGCC / (countGCA + countGCC + countGCT + countGCG) * 100;
            percentGCG = countGCG / (countGCA + countGCC + countGCT + countGCG) * 100;

            //Cysteine
            percentTGT = countTGT / (countTGT + countTGC) * 100;
            percentTGC = countTGC / (countTGT + countTGC) * 100;

            //Aspartic Acid
            percentGAT = countGAT / (countGAT + countGAC) * 100;
            percentGAC = countGAC / (countGAT + countGAC) * 100;

            //Glutamic Acid
            percentGAA = countGAA / (countGAA + countGAG) * 100;
            percentGAG = countGAG / (countGAA + countGAG) * 100;

            //Phenylalanine
            percentTTT = countTTT / (countTTT + countTTC) * 100;
            percentTTC = countTTC / (countTTT + countTTC) * 100;

            //Glycine
            percentGGA = countGGA / (countGGA + countGGT + countGGC + countGGG) * 100;
            percentGGT = countGGT / (countGGA + countGGT + countGGC + countGGG) * 100;
            percentGGC = countGGC / (countGGA + countGGT + countGGC + countGGG) * 100;
            percentGGG = countGGG / (countGGA + countGGT + countGGC + countGGG) * 100;

            //Histidine
            percentCAT = countCAT / (countCAT + countCAC) * 100;
            percentCAC = countCAC / (countCAT + countCAC) * 100;

            //Isoleucine
            percentATA = countATA / (countATT + countATC + countATA) * 100;
            percentATT = countATT / (countATT + countATC + countATA) * 100;
            percentATC = countATC / (countATT + countATC + countATA) * 100;

            //Lysine
            percentAAA = countAAA / (countAAA + countAAG) * 100;
            percentAAG = countAAG / (countAAA + countAAG) * 100;

            //Leucine
            percentTTA = countTTA / (countTTA + countTTG + countCTA + countCTT + countCTC + countCTG) * 100;
            percentTTG = countTTG / (countTTA + countTTG + countCTA + countCTT + countCTC + countCTG) * 100;
            percentCTA = countCTA / (countTTA + countTTG + countCTA + countCTT + countCTC + countCTG) * 100;
            percentCTT = countCTT / (countTTA + countTTG + countCTA + countCTT + countCTC + countCTG) * 100;
            percentCTC = countCTC / (countTTA + countTTG + countCTA + countCTT + countCTC + countCTG) * 100;
            percentCTG = countCTG / (countTTA + countTTG + countCTA + countCTT + countCTC + countCTG) * 100;

            //Methionine
            percentATG = countATG / (countATG) * 100;

            //Asparagine
            percentAAT = countAAT / (countAAT + countAAC) * 100;
            percentAAC = countAAC / (countAAT + countAAC) * 100;

            //Proline
            percentCCA = countCCA / (countCCA + countCCT + countCCC + countCCG) * 100;
            percentCCT = countCCT / (countCCA + countCCT + countCCC + countCCG) * 100;
            percentCCC = countCCC / (countCCA + countCCT + countCCC + countCCG) * 100;
            percentCCG = countCCG / (countCCA + countCCT + countCCC + countCCG) * 100;

            //Glutamine
            percentCAA = countCAA / (countCAA + countCAG) * 100;
            percentCAG = countCAG / (countCAA + countCAG) * 100;

            //Arginine
            percentAGA = countAGA / (countAGA + countAGG + countCGA + countCGT + countCGC + countCGG) * 100;
            percentAGG = countAGG / (countAGA + countAGG + countCGA + countCGT + countCGC + countCGG) * 100;
            percentCGA = countCGA / (countAGA + countAGG + countCGA + countCGT + countCGC + countCGG) * 100;
            percentCGT = countCGT / (countAGA + countAGG + countCGA + countCGT + countCGC + countCGG) * 100;
            percentCGC = countCGC / (countAGA + countAGG + countCGA + countCGT + countCGC + countCGG) * 100;
            percentCGG = countCGG / (countAGA + countAGG + countCGA + countCGT + countCGC + countCGG) * 100;

            //STOP
            percentTAA = countTAA / (countTAA + countTAG + countTGA) * 100;
            percentTAG = countTAG / (countTAA + countTAG + countTGA) * 100;
            percentTGA = countTGA / (countTAA + countTAG + countTGA) * 100;

            //Serine
            percentAGT = countAGT / (countAGT + countAGC + countTCA + countTCT + countTCC + countTCG) * 100;
            percentAGC = countAGC / (countAGT + countAGC + countTCA + countTCT + countTCC + countTCG) * 100;
            percentTCA = countTCA / (countAGT + countAGC + countTCA + countTCT + countTCC + countTCG) * 100;
            percentTCT = countTCT / (countAGT + countAGC + countTCA + countTCT + countTCC + countTCG) * 100;
            percentTCC = countTCC / (countAGT + countAGC + countTCA + countTCT + countTCC + countTCG) * 100;
            percentTCG = countTCG / (countAGT + countAGC + countTCA + countTCT + countTCC + countTCG) * 100;

            //Threonine
            percentACA = countACA / (countACA + countACT + countACC + countACG) * 100;
            percentACT = countACT / (countACA + countACT + countACC + countACG) * 100;
            percentACC = countACC / (countACA + countACT + countACC + countACG) * 100;
            percentACG = countACG / (countACA + countACT + countACC + countACG) * 100;

            //Valine
            percentGTA = countGTA / (countGTA + countGTC + countGTT + countGTG) * 100;
            percentGTC = countGTC / (countGTA + countGTC + countGTT + countGTG) * 100;
            percentGTT = countGTT / (countGTA + countGTC + countGTT + countGTG) * 100;
            percentGTG = countGTG / (countGTA + countGTC + countGTT + countGTG) * 100;

            //Tryptophan
            percentTGG = countTGG / (countTGG) * 100;

            //Tyrosine
            percentTAT = countTAT / (countTAT + countTAC) * 100;
            percentTAC = countTAC / (countTAT + countTAC) * 100;
        }



        public void ProcessIndividualCDSBias()
        {

            foreach (string cds in codingSequences)
            {
                ClearCounts();
                individualCodons = ChunksUpto(cds, 3).ToArray<string>();
                for (int i = 0; i < individualCodons.Length; i++)
                {
                    switch (individualCodons[i])
                    {

                        case "AAA":
                            countAAA++;
                            break;

                        case "AAT":
                            countAAT++;
                            break;

                        case "AAC":
                            countAAC++;
                            break;

                        case "AAG":
                            countAAG++;
                            break;

                        case "ATA":
                            countATA++;
                            break;

                        case "ATT":
                            countATT++;
                            break;

                        case "ATC":
                            countATC++;
                            break;

                        case "ATG":
                            countATG++;
                            break;

                        case "ACA":
                            countACA++;
                            break;

                        case "ACT":
                            countACT++;
                            break;

                        case "ACC":
                            countACC++;
                            break;

                        case "ACG":
                            countACG++;
                            break;

                        case "AGA":
                            countAGA++;
                            break;

                        case "AGT":
                            countAGT++;
                            break;

                        case "AGC":
                            countAGC++;
                            break;

                        case "AGG":
                            countAGG++;
                            break;

                        case "TAA":
                            countTAA++;
                            break;

                        case "TAT":
                            countTAT++;
                            break;

                        case "TAC":
                            countTAC++;
                            break;

                        case "TAG":
                            countTAG++;
                            break;

                        case "TTA":
                            countTTA++;
                            break;

                        case "TTT":
                            countTTT++;
                            break;

                        case "TTC":
                            countTTC++;
                            break;

                        case "TTG":
                            countTTG++;
                            break;

                        case "TCA":
                            countTCA++;
                            break;

                        case "TCT":
                            countTCT++;
                            break;

                        case "TCC":
                            countTCC++;
                            break;

                        case "TCG":
                            countTCG++;
                            break;

                        case "TGA":
                            countTGA++;
                            break;

                        case "TGT":
                            countTGT++;
                            break;

                        case "TGC":
                            countTGC++;
                            break;

                        case "TGG":
                            countTGG++;
                            break;

                        case "CAA":
                            countCAA++;
                            break;

                        case "CAT":
                            countCAT++;
                            break;

                        case "CAC":
                            countCAC++;
                            break;

                        case "CAG":
                            countCAG++;
                            break;

                        case "CTA":
                            countCTA++;
                            break;

                        case "CTT":
                            countCTT++;
                            break;

                        case "CTC":
                            countCTC++;
                            break;

                        case "CTG":
                            countCTG++;
                            break;

                        case "CCA":
                            countCCA++;
                            break;

                        case "CCT":
                            countCCT++;
                            break;

                        case "CCC":
                            countCCC++;
                            break;

                        case "CCG":
                            countCCG++;
                            break;

                        case "CGA":
                            countCGA++;
                            break;

                        case "CGT":
                            countCGT++;
                            break;

                        case "CGC":
                            countCGC++;
                            break;

                        case "CGG":
                            countCGG++;
                            break;

                        case "GAA":
                            countGAA++;
                            break;

                        case "GAT":
                            countGAT++;
                            break;

                        case "GAC":
                            countGAC++;
                            break;

                        case "GAG":
                            countGAG++;
                            break;

                        case "GTA":
                            countGTA++;
                            break;

                        case "GTT":
                            countGTT++;
                            break;

                        case "GTC":
                            countGTC++;
                            break;

                        case "GTG":
                            countGTG++;
                            break;

                        case "GCA":
                            countGCA++;
                            break;

                        case "GCT":
                            countGCT++;
                            break;

                        case "GCC":
                            countGCC++;
                            break;

                        case "GCG":
                            countGCG++;
                            break;

                        case "GGA":
                            countGGA++;
                            break;

                        case "GGT":
                            countGGT++;
                            break;

                        case "GGC":
                            countGGC++;
                            break;

                        case "GGG":
                            countGGG++;
                            break;

                        default:
                            break;
                    }

                }

                using (var individualCodonFile = File.AppendText(desktopPath + "\\" + txtLatinName.Text + "Individual Codon Bias Table Data" + ".csv"))
                {
                    UpdateStatusBar("Saving Codon Bias Table...");

                    individualCodonFile.WriteLine(
                            countAAA + "," +
                countAAC + "," +
                countAAG + "," +
                countAAT + "," +
                countACA + "," +
                countACC + "," +
                countACG + "," +
                countACT + "," +
                countAGA + "," +
                countAGC + "," +
                countAGG + "," +
                countAGT + "," +
                countATA + "," +
                countATC + "," +
                countATG + "," +
                countATT + "," +
                countCAA + "," +
                countCAC + "," +
                countCAG + "," +
                countCAT + "," +
                countCCA + "," +
                countCCC + "," +
                countCCG + "," +
                countCCT + "," +
                countCGA + "," +
                countCGC + "," +
                countCGG + "," +
                countCGT + "," +
                countCTA + "," +
                countCTC + "," +
                countCTG + "," +
                countCTT + "," +
                countGAA + "," +
                countGAC + "," +
                countGAG + "," +
                countGAT + "," +
                countGCA + "," +
                countGCC + "," +
                countGCG + "," +
                countGCT + "," +
                countGGA + "," +
                countGGC + "," +
                countGGG + "," +
                countGGT + "," +
                countGTA + "," +
                countGTC + "," +
                countGTG + "," +
                countGTT + "," +
                countTAA + "," +
                countTAC + "," +
                countTAG + "," +
                countTAT + "," +
                countTCA + "," +
                countTCC + "," +
                countTCG + "," +
                countTCT + "," +
                countTGA + "," +
                countTGC + "," +
                countTGG + "," +
                countTGT + "," +
                countTTA + "," +
                countTTC + "," +
                countTTG + "," +
                countTTT);




                }

            }
        }



        public void DisplayMostFrequentCodon()
        {
            //Alanine
            if (countGCA > countGCC &&
                countGCA > countGCT &&
                countGCA > countGCG)
            {
                lblGCA.BackColor = Color.LimeGreen;
                lblDominantA.Text = "GCA";
                dominantCodonArray[0] = "GCA";
            }
            else if (countGCT > countGCA &&
                     countGCT > countGCC &&
                     countGCT > countGCG)
            {
                lblGCT.BackColor = Color.LimeGreen;
                lblDominantA.Text = "GCT";
                dominantCodonArray[0] = "GCT";
            }
            else if (countGCC > countGCA &&
                     countGCC > countGCT &&
                     countGCC > countGCG)
            {
                lblGCC.BackColor = Color.LimeGreen;
                lblDominantA.Text = "GCC";
                dominantCodonArray[0] = "GCC";
            }
            else
            {
                lblGCG.BackColor = Color.LimeGreen;
                lblDominantA.Text = "GCG";
                dominantCodonArray[0] = "GCG";
            }


            //Cysteine
            if (countTGT > countTGC)
            {
                lblTGT.BackColor = Color.LimeGreen;
                lblDominantC.Text = "TGT";
                dominantCodonArray[1] = "TGT";
            }
            else
            {
                lblTGC.BackColor = Color.LimeGreen;
                lblDominantC.Text = "TGC";
                dominantCodonArray[1] = "TGC";
            }


            //Aspartic Acid
            if (countGAT > countGAG)
            {
                lblGAT.BackColor = Color.LimeGreen;
                lblDominantD.Text = "GAT";
                dominantCodonArray[2] = "GAT";
            }
            else
            {
                lblGAG.BackColor = Color.LimeGreen;
                lblDominantD.Text = "GAG";
                dominantCodonArray[2] = "GAG";
            }


            //Glutamic Acid
            if (countGAA > countGAG)
            {
                lblGAA.BackColor = Color.LimeGreen;
                lblDominantE.Text = "GAA";
                dominantCodonArray[3] = "GAA";
            }
            else
            {
                lblGAG.BackColor = Color.LimeGreen;
                lblDominantE.Text = "GAG";
                dominantCodonArray[3] = "GAG";
            }


            //Phenylalanine
            if (countTTT > countTTC)
            {
                lblTTT.BackColor = Color.LimeGreen;
                lblDominantF.Text = "TTT";
                dominantCodonArray[4] = "TTT";
            }
            else
            {
                lblTTC.BackColor = Color.LimeGreen;
                lblDominantF.Text = "TTC";
                dominantCodonArray[4] = "TTC";

            }


            //Glycine
            if (countGGA > countGGC &&
                countGGA > countGGT &&
                countGGA > countGGG)
            {
                lblGGA.BackColor = Color.LimeGreen;
                lblDominantG.Text = "GGA";
                dominantCodonArray[5] = "GGA";
            }
            else if (countGGT > countGGA &&
                     countGGT > countGGC &&
                     countGGT > countGGG)
            {
                lblGGT.BackColor = Color.LimeGreen;
                lblDominantG.Text = "GGT";
                dominantCodonArray[5] = "GGT";
            }
            else if (countGGC > countGGA &&
                     countGGC > countGGT &&
                     countGGC > countGGG)
            {
                lblGGC.BackColor = Color.LimeGreen;
                lblDominantG.Text = "GGC";
                dominantCodonArray[5] = "GGC";
            }
            else
            {
                lblGGG.BackColor = Color.LimeGreen;
                lblDominantG.Text = "GGG";
                dominantCodonArray[5] = "GGG";
            }


            //Histidine
            if (countCAT > countCAC)
            {
                lblCAT.BackColor = Color.LimeGreen;
                lblDominantH.Text = "CAT";
                dominantCodonArray[6] = "CAT";
            }
            else
            {
                lblCAC.BackColor = Color.LimeGreen;
                lblDominantH.Text = "CAC";
                dominantCodonArray[6] = "CAC";
            }


            //Isoleucine
            if (countATA > countATC &&
                countATA > countATT)
            {
                lblATA.BackColor = Color.LimeGreen;
                lblDominantI.Text = "ATA";
                dominantCodonArray[7] = "ATA";
            }
            else if (countATC > countATA &&
                     countATC > countATT)
            {
                lblATC.BackColor = Color.LimeGreen;
                lblDominantI.Text = "ATC";
                dominantCodonArray[7] = "ATC";
            }
            else
            {
                lblATT.BackColor = Color.LimeGreen;
                lblDominantI.Text = "ATT";
                dominantCodonArray[7] = "ATT";
            }


            //Lysine
            if (countAAA > countAAG)
            {
                lblAAA.BackColor = Color.LimeGreen;
                lblDominantK.Text = "AAA";
                dominantCodonArray[8] = "AAA";
            }
            else
            {
                lblAAG.BackColor = Color.LimeGreen;
                lblDominantK.Text = "AAG";
                dominantCodonArray[8] = "AAG";
            }


            //Leucine
            if (countTTA > countTTG &&
                countTTA > countCTA &&
                countTTA > countCTT &&
                countTTA > countCTC &&
                countTTA > countCTG)
            {
                lblTTA.BackColor = Color.LimeGreen;
                lblDominantL.Text = "TTA";
                dominantCodonArray[9] = "TTA";
            }
            else if (countTTG > countTTA &&
                countTTG > countCTA &&
                countTTG > countCTT &&
                countTTG > countCTC &&
                countTTG > countCTG)
            {
                lblTTG.BackColor = Color.LimeGreen;
                lblDominantL.Text = "TTG";
                dominantCodonArray[9] = "TTG";
            }
            else if (countCTA > countTTA &&
               countCTA > countTTG &&
               countCTA > countCTT &&
               countCTA > countCTC &&
               countCTA > countCTG)
            {
                lblCTA.BackColor = Color.LimeGreen;
                lblDominantL.Text = "CTA";
                dominantCodonArray[9] = "CTA";
            }
            else if (countCTT > countTTA &&
              countCTT > countTTG &&
              countCTT > countCTA &&
              countCTT > countCTC &&
              countCTT > countCTG)
            {
                lblCTT.BackColor = Color.LimeGreen;
                lblDominantL.Text = "CTT";
                dominantCodonArray[9] = "CTT";
            }
            else if (countCTC > countTTA &&
              countCTC > countTTG &&
              countCTC > countCTT &&
              countCTC > countCTA &&
              countCTC > countCTG)
            {
                lblCTC.BackColor = Color.LimeGreen;
                lblDominantL.Text = "CTC";
                dominantCodonArray[9] = "CTC";
            }
            else
            {
                lblCTG.BackColor = Color.LimeGreen;
                lblDominantL.Text = "CTG";
                dominantCodonArray[9] = "CTG";
            }


            //Methionine
            lblATG.BackColor = Color.LimeGreen;
            lblDominantM.Text = "ATG";
            dominantCodonArray[10] = "ATG";



            //Asparagine
            if (countAAT > countAAC)
            {
                lblAAT.BackColor = Color.LimeGreen;
                lblDominantN.Text = "AAT";
                dominantCodonArray[11] = "AAT";
            }
            else
            {
                lblAAC.BackColor = Color.LimeGreen;
                lblDominantN.Text = "AAC";
                dominantCodonArray[11] = "AAC";
            }


            //Proline
            if (countCCA > countCCC &&
                countCCA > countCCT &&
                countCCA > countCCG)
            {
                lblCCA.BackColor = Color.LimeGreen;
                lblDominantP.Text = "CCA";
                dominantCodonArray[12] = "CCA";
            }
            else if (countCCT > countCCA &&
                     countCCT > countCCC &&
                     countCCT > countCCG)
            {
                lblCCT.BackColor = Color.LimeGreen;
                lblDominantP.Text = "CCT";
                dominantCodonArray[12] = "CCT";
            }
            else if (countCCC > countCCA &&
                     countCCC > countCCT &&
                     countCCC > countCCG)
            {
                lblCCC.BackColor = Color.LimeGreen;
                lblDominantP.Text = "CCC";
                dominantCodonArray[12] = "CCC";
            }
            else
            {
                lblCCG.BackColor = Color.LimeGreen;
                lblDominantP.Text = "CCG";
                dominantCodonArray[12] = "CCG";
            }


            //Glutamine
            if (countCAA > countCAG)
            {
                lblCAA.BackColor = Color.LimeGreen;
                lblDominantQ.Text = "CAA";
                dominantCodonArray[13] = "CAA";
            }
            else
            {
                lblCAG.BackColor = Color.LimeGreen;
                lblDominantQ.Text = "CAG";
                dominantCodonArray[13] = "CAG";
            }


            //Arginine
            if (countAGA > countAGG &&
                countAGA > countCGA &&
                countAGA > countCGT &&
                countAGA > countCGC &&
                countAGA > countCGG)
            {
                lblAGA.BackColor = Color.LimeGreen;
                lblDominantR.Text = "AGA";
                dominantCodonArray[14] = "AGA";
            }
            else if (countAGG > countAGA &&
                countAGG > countCGA &&
                countAGG > countCGT &&
                countAGG > countCGC &&
                countAGG > countCGG)
            {
                lblAGG.BackColor = Color.LimeGreen;
                lblDominantR.Text = "AGG";
                dominantCodonArray[14] = "AGG";
            }
            else if (countCGA > countAGA &&
               countCGA > countAGG &&
               countCGA > countCGT &&
               countCGA > countCGC &&
               countCGA > countCGG)
            {
                lblCGA.BackColor = Color.LimeGreen;
                lblDominantR.Text = "CGA";
                dominantCodonArray[14] = "CGA";
            }
            else if (countCGT > countAGA &&
              countCGT > countAGG &&
              countCGT > countCGA &&
              countCGT > countCGC &&
              countCGT > countCGG)
            {
                lblCGT.BackColor = Color.LimeGreen;
                lblDominantR.Text = "CGT";
                dominantCodonArray[14] = "CGT";
            }
            else if (countCGC > countAGA &&
              countCGC > countAGG &&
              countCGC > countCGT &&
              countCGC > countCGA &&
              countCGC > countCGG)
            {
                lblCGC.BackColor = Color.LimeGreen;
                lblDominantR.Text = "CGC";
                dominantCodonArray[14] = "CGC";
            }
            else
            {
                lblCGG.BackColor = Color.LimeGreen;
                lblDominantR.Text = "CGG";
                dominantCodonArray[14] = "CGG";
            }


            //STOP
            if (countTGA > countTAG &&
                countTGA > countTAA)
            {
                lblTGA.BackColor = Color.LimeGreen;
                lblDominantSTOP.Text = "TGA";
                dominantCodonArray[20] = "TGA";
            }
            else if (countTAG > countTGA &&
                     countTAG > countTAA)
            {
                lblTAG.BackColor = Color.LimeGreen;
                lblDominantSTOP.Text = "TAG";
                dominantCodonArray[20] = "TAG";
            }
            else
            {
                lblTAA.BackColor = Color.LimeGreen;
                lblDominantSTOP.Text = "TAA";
                dominantCodonArray[20] = "TAA";
            }


            //Serine
            if (countAGT > countAGC &&
                countAGT > countTCA &&
                countAGT > countTCT &&
                countAGT > countTCC &&
                countAGT > countTCG)
            {
                lblAGT.BackColor = Color.LimeGreen;
                lblDominantS.Text = "AGT";
                dominantCodonArray[15] = "AGT";
            }
            else if (countAGC > countAGT &&
                countAGC > countTCA &&
                countAGC > countTCT &&
                countAGC > countTCC &&
                countAGC > countTCG)
            {
                lblAGC.BackColor = Color.LimeGreen;
                lblDominantS.Text = "AGC";
                dominantCodonArray[15] = "AGC";
            }
            else if (countTCA > countAGT &&
               countTCA > countAGC &&
               countTCA > countTCT &&
               countTCA > countTCC &&
               countTCA > countTCG)
            {
                lblTCA.BackColor = Color.LimeGreen;
                lblDominantS.Text = "TCA";
                dominantCodonArray[15] = "TCA";
            }
            else if (countTCT > countAGT &&
              countTCT > countAGC &&
              countTCT > countTCA &&
              countTCT > countTCC &&
              countTCT > countTCG)
            {
                lblTCT.BackColor = Color.LimeGreen;
                lblDominantS.Text = "TCT";
                dominantCodonArray[15] = "TCT";
            }
            else if (countTCC > countAGT &&
              countTCC > countAGC &&
              countTCC > countTCT &&
              countTCC > countTCA &&
              countTCC > countTCG)
            {
                lblTCC.BackColor = Color.LimeGreen;
                lblDominantS.Text = "TCC";
                dominantCodonArray[15] = "TCC";
            }
            else
            {
                lblTCG.BackColor = Color.LimeGreen;
                lblDominantS.Text = "TCG";
                dominantCodonArray[15] = "TCG";
            }

            //Threonine
            if (countACA > countACT &&
                countACA > countACC &&
                countACA > countACG)
            {
                lblACA.BackColor = Color.LimeGreen;
                lblDominantT.Text = "ACA";
                dominantCodonArray[16] = "ACA";
            }
            else if (countACC > countACA &&
                     countACC > countACT &&
                     countACC > countACG)
            {
                lblACC.BackColor = Color.LimeGreen;
                lblDominantT.Text = "ACC";
                dominantCodonArray[16] = "ACC";
            }
            else if (countACT > countACA &&
                     countACT > countACC &&
                     countACT > countACG)
            {
                lblACT.BackColor = Color.LimeGreen;
                lblDominantT.Text = "ACT";
                dominantCodonArray[16] = "ACT";
            }
            else
            {
                lblACG.BackColor = Color.LimeGreen;
                lblDominantT.Text = "ACG";
                dominantCodonArray[16] = "ACG";
            }


            //Valine
            if (countGTA > countGTT &&
          countGTA > countGTC &&
          countGTA > countGTG)
            {
                lblGTA.BackColor = Color.LimeGreen;
                lblDominantV.Text = "GTA";
                dominantCodonArray[17] = "GTA";
            }
            else if (countGTC > countGTA &&
                     countGTC > countGTT &&
                     countGTC > countGTG)
            {
                lblGTC.BackColor = Color.LimeGreen;
                lblDominantV.Text = "GTC";
                dominantCodonArray[17] = "GTC";
            }
            else if (countGTT > countGTA &&
                     countGTT > countGTC &&
                     countGTT > countGTG)
            {
                lblGTT.BackColor = Color.LimeGreen;
                lblDominantV.Text = "GTT";
                dominantCodonArray[17] = "GTT";
            }
            else
            {
                lblGTG.BackColor = Color.LimeGreen;
                lblDominantV.Text = "GTG";
                dominantCodonArray[17] = "GTG";
            }


            //Tryptophan
            lblTGG.BackColor = Color.LimeGreen;
            lblDominantW.Text = "TGG";
            dominantCodonArray[18] = "TGG";


            //Tyrosine
            if (countTAT > countTAC)
            {
                lblTAT.BackColor = Color.LimeGreen;
                lblDominantY.Text = "TAT";
                dominantCodonArray[19] = "TAT";
            }
            else
            {
                lblTAC.BackColor = Color.LimeGreen;
                lblDominantY.Text = "TAC";
                dominantCodonArray[19] = "TAC";
            }
        }



        public void ParseGenbankFile()
        {

        }
    }
}