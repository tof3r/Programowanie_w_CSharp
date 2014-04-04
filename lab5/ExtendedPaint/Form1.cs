using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        List<Bitmap> buffers = new List<Bitmap>();

        MyLine line;
        MyPoint point;
        MyRectangle rectangle;

        TabControl tabGroup;
        TabPage new_draw;
        
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.White;
            buffers.Add(new Bitmap(panel1.Width, panel1.Height));
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;           
            line = new MyLine(Color.White, buffers[tabControl1.SelectedIndex]);
            point = new MyPoint(Color.White, buffers[tabControl1.SelectedIndex]);
            rectangle = new MyRectangle(Color.White, buffers[tabControl1.SelectedIndex]);
            tabGroup = new TabControl();
            new_draw = new TabPage();

            tabGroup.TabPages.Add(new_draw);
            tabGroup.Location = new Point(0, 0);
            tabGroup.Size = new Size(panel1.Width, panel1.Height);
            this.Controls.Add(tabGroup);

            this.tabControl1.MouseClick += tabControl1_MouseClick;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(buffers[tabControl1.SelectedIndex], Point.Empty);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    line.MousePressed(sender, e);
                    panel1.Invalidate();
                    break;
                case 1:
                    rectangle.MousePressed(sender, e);
                    panel1.Invalidate();
                    break;
                case 2:
                    point.MousePressed(sender, e);
                    panel1.Invalidate();
                    break;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    if (e.Button == MouseButtons.Left)
                    {
                        line.MouseMoving(sender, e);
                        panel1.Invalidate();
                        status.Text = "Mouse Coordinates X: " + e.X + " Y: " + e.Y;
                    }
                    break;
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {
                        rectangle.MouseMoving(sender, e);
                        panel1.Invalidate();
                        status.Text = "Mouse Coordinates X: " + e.X + " Y: " + e.Y;
                    }
                    break;
                case 2:
                    if (e.Button == MouseButtons.Left)
                    {
                        point.MouseMoving(sender, e);
                        panel1.Invalidate();
                        status.Text = "Mouse Coordinates X: " + e.X + " Y: " + e.Y;
                    }
                    break;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    line.MouseReleased(sender, e);
                    panel1.Invalidate();
                    break;
                case 1:
                    rectangle.MouseReleased(sender, e);
                    panel1.Invalidate();
                    break;
                case 2:
                    point.MouseReleased(sender, e);
                    panel1.Invalidate();
                    break;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    line = new MyLine(Color.Black, buffers[tabControl1.SelectedIndex]);
                    break;
                case 1:
                    rectangle = new MyRectangle(Color.Black, buffers[tabControl1.SelectedIndex]);
                    break;
                case 2:
                    point = new MyPoint(Color.Black, buffers[tabControl1.SelectedIndex]);
                    break;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox2.SelectedIndex)
            {
                case 0:
                    switch (listBox1.SelectedIndex)
                    {
                        case 0:
                            line.getColor = Color.Black;
                            break;
                        case 1:
                            rectangle.getColor = Color.Black;
                            break;
                        case 2:
                            point.getColor = Color.Black;
                            break;
                    }
                    break;
                case 1:
                    switch (listBox1.SelectedIndex)
                    {
                        case 0:
                            line.getColor = Color.Red;
                            break;
                        case 1:
                            rectangle.getColor = Color.Red;
                            break;
                        case 2:
                            point.getColor = Color.Red;
                            break;
                    }
                    break;
                case 2:
                    switch (listBox1.SelectedIndex)
                    {
                        case 0:
                            line.getColor = Color.Green;
                            break;
                        case 1:
                            rectangle.getColor = Color.Green;
                            break;
                        case 2:
                            point.getColor = Color.Green;
                            break;
                    }
                    break;
                case 3:
                    switch (listBox1.SelectedIndex)
                    {
                        case 0:
                            line.getColor = Color.Blue;
                            break;
                        case 1:
                            rectangle.getColor = Color.Blue;
                            break;
                        case 2:
                            point.getColor = Color.Blue;
                            break;
                    }
                    break;
            }
        }

        private void addNewTab_Click(object sender, EventArgs e)
        {
            string title = "tabPage " + (tabControl1.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            tabControl1.TabPages.Add(myTabPage);
            tabControl1.SelectedTab = myTabPage;
            buffers.Add(new Bitmap(panel1.Width, panel1.Height));

        }

        /*
        Constants in Windows API
        0x84 = WM_NCHITTEST - Mouse Capture Test
        0x1 = HTCLIENT - Application Client Area
        0x2 = HTCAPTION - Application Title Bar
        */

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            var tabs = tabControl.TabPages;

            if (e.Button == MouseButtons.Middle)
            {
                tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location)).First());
            }

            if (tabControl1.TabCount.Equals(0))
            {
                Application.Exit();
            }

        }        

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.TabCount.Equals(0))
            {
                Application.Exit();
            }
            else
            {
                panel1.Parent = tabControl1.TabPages[tabControl1.SelectedIndex];
            }
        }

        private void loadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileL = new OpenFileDialog();
            fileL.Title = "Open Image";
            fileL.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp";

            if (fileL.ShowDialog() == DialogResult.OK)
            {
                buffers[tabControl1.SelectedIndex] = new Bitmap(panel1.Width, panel1.Height);
                Bitmap tmp = new Bitmap(Image.FromFile(fileL.FileName));
                buffers[tabControl1.SelectedIndex] = tmp;
                panel1.Invalidate();
            }
        }

        private void saveImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileS = new SaveFileDialog();
            fileS.Title = "Save an image";
            fileS.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp";

            if (fileS.ShowDialog() == DialogResult.OK)
            {
                string file = fileS.FileName;
                buffers[tabControl1.SelectedIndex].Save(file);
                panel1.Parent = tabControl1.TabPages[tabControl1.SelectedIndex];
            }

        }
    }
}
