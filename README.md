# Binomics Labs Software Suite

Pronounced "bliss", this bit of code is a suite of software for use in biological exploration. It contains modules for conventional DNA analysis as well as some quirky ways of visualizing your results. From probing the theoretical limits of evolutionary systems to robotic control systems, this collection of tools is an evergrowing body of computational knowledge and technique designed to help researchers both formal and amateur. The suite is coded in C# using the Visual Studio Community Edition IDE and is intended to be used on Windows-based machines BUT we will shortly transition to the Lua language (Love2D) for portability and aesthetic reasons. There is no reason for science tools to be furrow-browed. A bit of chip-tunes and 2D magic is on the horizon. Feel free to fork to your heart's content and please pardon the paltry coding practices...they will get better with time! 




## Modules
#### Infinite Discovery Machine Simulator 
Produces an in-silico run of the IDM tool chain used to generate random protein sequences from datasets of known and/or random DNA.

#### Codon Bias Analyzer
Analyzes how often each of the 64 DNA triplet codons occur in a given dataset of protein coding sequences. It can also output a codon bias table in the Kazusa format used by many bioinformatics tools.

#### Genomic Visual Abstractor
Converts FASTA formatted genomic DNA data into a pixelized image. Each column in said image is an abstraction of the data through a filter of ever decreasing resolution. Left to right, the image shows a 1:1, 20:1, 40:1, 80:1, 100:1, 200:1, and 400:1 (pixels:averageBasePair) which helps rapidly identify any patterns within the genome. The columns are 200 basepairs wide and vary in height based on the total genome length. To run, simply clikc either the "visualize" or "batch viz" buttons. For a single FASTA file, simply follow the file dialog when prompted and wait for the image to render. Then press "save" to save the single file. To process several files, ensure the directory tree is in the standard genome anotation format. This entails having one main directory with a sub-directory for each genomic file you wish to process. Click the "batch viz" button and select the parent directory where your genome sub-directories are held. Then select the output folder and relax. The app will process each genome, visualize it, and save it to your desired output directory. The black bar at the bottom will output progress as it runs. 
