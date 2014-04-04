using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeneratorHTML
{
    class Program
    {
        public static void generate2x2(String file) 
        {
            List<List<string>> tdata = new List<List<string>>();
            for(int i = 0; i<2; i++)
            {
                List<string> trow = new List<string>();
                for (int j = 0; j < 2; j++ )
                {
                    trow.Add((j+3).ToString());
                }
                tdata.Add(trow);
            }
            Generator generator = new Generator(tdata);
            generator.saveFile(file);
        }

        public static void generate2x3(String file)
        {
            List<List<string>> tdata = new List<List<string>>();
            for (int i = 0; i < 2; i++)
            {
                List<string> trow = new List<string>();
                for (int j = 0; j < 3; j++)
                {
                    trow.Add("Zachodniopomorski Uniwersytet Technologiczny");
                }
                tdata.Add(trow);
            }
            Generator generator = new Generator(tdata);
            generator.saveFile(file);
        }

        public static void generate2x5(String file)
        {
            List<List<string>> tdata = new List<List<string>>();
            for (int i = 0; i < 2; i++)
            {
                List<string> trow = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    trow.Add("Dawid Glinski");
                }
                tdata.Add(trow);
            }
            Generator generator = new Generator(tdata);
            generator.addHeader("Header");
            generator.addFooter("Footer");
            generator.saveFile(file);
        }

        static void Main(string[] args)
        {
            generate2x2(@"table2x2.html");
            generate2x3(@"table2x3.html");
            generate2x5(@"table2x5.html");
            Generator generator = new Generator(@"test.txt");
            generator.saveFile(@"wynik.html");
        }
    }
}
