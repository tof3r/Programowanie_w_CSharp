using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SimpleBase
{
    public partial class Form1 : Form
    {
        private List<string> surnames = new List<string>();
        private Dictionary<string,List<string>> bisurnames = new Dictionary<string, List<string>>();
        private Dictionary<string,List<string>> trisurnames = new Dictionary<string,List<string>>();

        public Form1()
        {
            InitializeComponent();
            loadFile(@"nazwiska.txt");
            listBox1.DataSource = null;
        }


        public void loadFile(String filePath)
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();
            string[] lines = File.ReadAllLines(filePath);
            timer.Stop();
            label1.Text = "Database load time in miliseconds: "+timer.ElapsedMilliseconds;
            timer.Reset();
           
            foreach (string line in lines)
            {
                var items = line.Split(' ');
                surnames.Add(items[1]);
            }

            create2Surnames();
            create3Surnames();
        }

        public void create2Surnames()
        {            
            List<string> matches = new List<string>();
            Stopwatch timer = new Stopwatch();

            timer.Start();
            foreach (string sur2 in surnames)
            {
                if (sur2.Length > 2)
                {
                    string tmp = sur2.Substring(0, 2);

                    if (bisurnames.ContainsKey(tmp))
                    {
                        matches = bisurnames[tmp];
                        matches.Add(sur2);
                        bisurnames[tmp] = matches;
                    }
                    else
                    {
                        List<string> matches2 = new List<string>();
                        matches2.Add(sur2);
                        bisurnames[tmp] = matches2;
                    }
                }
            }
            timer.Stop();
            label1.Text += "\n2 letter dictionary create time in miliseconds: " + timer.ElapsedMilliseconds;
            timer.Reset();
        }

        public void create3Surnames()
        {
            List<string> matches = new List<string>();
            Stopwatch timer = new Stopwatch();

            timer.Start();
            foreach (string sur3 in surnames)
            {
                if (sur3.Length > 3)
                {
                    string tmp = sur3.Substring(0, 3);

                    if (trisurnames.ContainsKey(tmp))
                    {
                        matches = trisurnames[tmp];
                        matches.Add(sur3);
                        trisurnames[tmp] = matches;
                    }
                    else
                    {
                        List<string> matches3 = new List<string>();
                        matches3.Add(sur3);
                        trisurnames[tmp] = matches3;
                    }
                }
            }
            timer.Stop();
            label1.Text += "\n3 letter dictionary create time in miliseconds: " + timer.ElapsedMilliseconds;
            timer.Reset();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string[] error = {"This key doesn't exist in dictionary."};
            TextBox textB = (TextBox)sender;
            string key= textBox1.Text;

            try
            {
                if (key.Length < 2)
                    listBox1.DataSource = null;
                else if (key.Length == 2)
                {
                    listBox1.DataSource = bisurnames[key];
                    textBox1.Text = bisurnames[key][0];
                    textBox1.Select(2,textBox1.Text.Length);
                }
                else if (key.Length == 3)
                {
                    listBox1.DataSource = trisurnames[key];
                    textBox1.Text = trisurnames[key][0];
                    textBox1.Select(3,textBox1.Text.Length);
                }
            }
            catch (KeyNotFoundException) 
            {
                listBox1.DataSource = error;
            }
        }
    }
}
