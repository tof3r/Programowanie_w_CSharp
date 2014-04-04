using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorHTML
{
    class Generator
    {
        private List<List<string>> content;
        private String header = "";
        private String footer = "";

        public Generator(String file) { 
            content = new List<List<string>>();
            loadFile(file);
        }

        public Generator(List<List<string>> table)
        {
            content = table;
        }

        public void loadFile(String file) {
            string[] lines = File.ReadAllLines(file);

            foreach(string line in lines){
                List<string> items = new List<string>(line.Split(' '));
                addRow(items);
            }
        }

        public void saveFile(String filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            writer.WriteLine(generteHTML());
            writer.Close();
        }

        public void addRow(List<string> trow) 
        {
            content.Add(trow);
        }

        public void addHeader(String hdr) 
        {
            header = hdr;
        }

        public void addFooter(String ftr) 
        {
            footer = ftr;
        }

        public string generteHTML() 
        {
            String htmlCode = "<!DOCTYPE html>\n";
            htmlCode += "<table border=1>\n";

            if(!String.IsNullOrEmpty(header))
            {
                htmlCode += "\t<thead><tr><th>"+header+"</th></tr></thead>\n";
            }

            htmlCode += "\t<tbody>\n";

            foreach(List<string> trow in content)
            {
                htmlCode += "\t\t<tr>\n";
                foreach(string tdata in trow)
                {
                    htmlCode += "\t\t\t<td>"+tdata+"</td>\n";
                }
                htmlCode += "\t\t</tr>\n";
            }

            htmlCode += "\t</tbody>\n";

            if (!String.IsNullOrEmpty(footer))
            {
                htmlCode += "\t<tfoot><tr>";
                for (int i = 0; i < content[0].Count;i++ )
                    htmlCode += "<th>";

                htmlCode += footer;

                for (int i = 0; i < content[0].Count; i++)
                    htmlCode += "</th>";
                    
                htmlCode += "</tr></tfoot>\n";
            }

            htmlCode += "</table>\n";

            return htmlCode;
        }

    }
}
