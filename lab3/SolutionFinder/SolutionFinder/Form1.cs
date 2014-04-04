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
using System.Text.RegularExpressions;
using System.Xml;

namespace SolutionFinder
{
    public partial class SolutionFinder : Form
    {
        FolderBrowserDialog root_folder = new FolderBrowserDialog();
       
        string solutionSource = null;
        string slnContent = null;

        XmlDocument xmlProj = null;
        string xmlAsString = null;
        /*path to .sln file*/
        string[] slnPath = null;
        /*paths to .csproj files*/
        List<string> projectsPaths = new List<string>();
        /*paths to files included to each .csproj*/
        List<string> innerFiles = new List<string>();
        /*project folder names*/
        List<string> projectFolderName = new List<string>();
        /*list with filenames*/
        List<string> fileList = new List<string>();
        /*List with all files*/
        List<string> content = new List<string>();
        string pattern = @"[0-9a-zA-Z_]*\\[0-9a-zA-Z_]*.csproj";
        string filesPattern = @"Include=""[0-9a-zA-Z_]*[.\\]?[0-9a-zA-Z_]*\.?[a-z]*\.?[0-9a-zA-Z_]*\.?[a-z]*""";

        MatchCollection matches = null;
        MatchCollection filesMatches = null;
        int i = 0;
        int project = 0;

        public SolutionFinder()
        {
            InitializeComponent();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            simplyCleanup();
            readFromSln();
            selectProjects();
            listBox1.DataSource = projectsPaths;
            findAndCopyIncludedFiles();
            listBox2.DataSource = content;
            pathTo.Text = slnPath[0];
        }

        private void simplyCleanup()
        {
            if (pathTo.Text.Length > 0)
            {
                pathTo.Text = "";
                listBox1.DataSource = null;
                listBox2.DataSource = null;
                projectsPaths.Clear();
                innerFiles.Clear();
                projectFolderName.Clear();
                fileList.Clear();
            }
        }

        private void readFromSln()
        {
            root_folder.ShowDialog();

            solutionSource = root_folder.SelectedPath;
            slnContent = null;

            slnPath = Directory.GetFiles(solutionSource);

            if (slnPath.Length == 0)
            {
                pathTo.Text = "Choosen directory doesn't contain .sln file. Choose another one.";
                root_folder.ShowDialog();
                solutionSource = root_folder.SelectedPath;
                slnPath = Directory.GetFiles(solutionSource);
                slnContent = File.ReadAllText(slnPath[0]);
                
            }
            else
            {
                if (slnPath[0].EndsWith(".sln"))
                {
                    slnContent = File.ReadAllText(slnPath[0]);
                    if (Directory.Exists(root_folder.SelectedPath + @"\kopia") != false)
                        Directory.Delete(root_folder.SelectedPath + @"\kopia", true);
                }
            }
            /*else 
            {
                pathTo.Text = "Choosen directory doesn't contain .sln file. Choose another.";
                root_folder.ShowDialog();
            }*/

        }

        private void selectProjects()
        {
            matches = Regex.Matches(slnContent, pattern);

            foreach (Match match in matches)
            {
                int end = match.ToString().IndexOf(@"\");
                projectFolderName.Add(match.ToString().Substring(0, end));

                projectsPaths.Add(root_folder.SelectedPath + @"\" + match.ToString());
                Console.WriteLine("\nProject: " + match.ToString() + "\n");
            }
        }

        private void findAndCopyIncludedFiles()
        {
            xmlProj = new XmlDocument();

            createFolders();
            copySolutionAndProjects();

            foreach (string xml in projectsPaths)
            {
                xmlProj.Load(xml);
                xmlAsString = GetXMLAsString(xmlProj);
                filesMatches = Regex.Matches(xmlAsString, filesPattern);

                foreach (Match filesM in filesMatches)
                {
                    if (filesM.ToString().EndsWith(".cs\"") || filesM.ToString().EndsWith(".resx\"") || filesM.ToString().EndsWith(".settings\"") || filesM.ToString().EndsWith(".config\""))
                    {
                        int end = filesM.ToString().LastIndexOf("\"");

                        innerFiles.Add(root_folder.SelectedPath + @"\" + projectFolderName[i] + @"\" + filesM.ToString().Substring(9, end - 9));
                        content.Add(root_folder.SelectedPath + @"\" + projectFolderName[i] + @"\" + filesM.ToString().Substring(9, end - 9));
                        fileList.Add(filesM.ToString().Substring(9, end - 9));

                        File.Copy(root_folder.SelectedPath + @"\"+ projectFolderName[i] + @"\" + filesM.ToString().Substring(9, end - 9), root_folder.SelectedPath + @"\kopia\" + projectFolderName[i] + @"\" + filesM.ToString().Substring(9, end - 9));
                    }
                }
                i++;
                innerFiles.Clear();

                label.Text = "Files copied to: " + root_folder.SelectedPath + @"\kopia";
            }
            
        }

        private void createFolders() 
        {
            Directory.CreateDirectory(root_folder.SelectedPath + @"\kopia");

            foreach (string backupFolder in projectFolderName)
            {
                Directory.CreateDirectory(root_folder.SelectedPath + @"\kopia\" + backupFolder);
                Directory.CreateDirectory(root_folder.SelectedPath + @"\kopia\" + backupFolder + @"\Properties");
            }
        }

        private void copySolutionAndProjects() 
        {
            File.Copy(slnPath[0], root_folder.SelectedPath + @"\kopia\" + projectFolderName[0] + ".sln",true);

            foreach (string copyProject in projectsPaths)
            {
                 File.Copy(copyProject, root_folder.SelectedPath + @"\kopia\" + projectFolderName[project] + @"\" + projectFolderName[project] + ".csproj");
                 project++;
            }
        }

        private string GetXMLAsString(XmlDocument xmlDoc)
        {
            return xmlDoc.OuterXml;
        }
    }
}
