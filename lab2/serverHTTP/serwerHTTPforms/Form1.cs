using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serwerHTTPforms
{
    public partial class Form1 : Form
    {
        private MyServer server;
        private int port = 8010;
        private String filePath = @"D:\Programy\Visual Studio Express 2012\PROJEKTY\6 sem\C#\lab2\serverHTTP\serwerHTTPforms\bin\Debug\files";

        public Form1()
        {
            InitializeComponent();
            port_n.Text = port.ToString();
            file_p.Text = filePath;

            stop_s.Enabled = false;
        }

        private void start_s_Click(object sender, EventArgs e)
        {
            server = new MyServer(port_n,file_p);
            server.startOnThread();

            start_s.Enabled = false;
            stop_s.Enabled = true;

            file_p.Enabled = false;
            port_n.Enabled = false;
        }

        private void stop_s_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                server.stop_server();
            }
            start_s.Enabled = true;
            stop_s.Enabled = false;

            file_p.Enabled = true;
            port_n.Enabled = true;

        }

        private void port_n_TextChanged(object sender, EventArgs e)
        {

        }

        private void file_p_TextChanged(object sender, EventArgs e)
        {

        }

        private void log_TextChanged(object sender, EventArgs e)
        {
            //logText.Text = server.log;
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            //Application.Exit();
        }
    }
}
