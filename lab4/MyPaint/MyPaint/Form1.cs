using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        Bitmap buffer;

        MyLine line;
        MyPoint point;
        MyRectangle rectangle;

        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.White;
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            buffer = new Bitmap(panel1.Width,panel1.Height);
            line = new MyLine(Color.White, buffer);
            point = new MyPoint(Color.White,buffer);
            rectangle = new MyRectangle(Color.White,buffer);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            switch(listBox1.SelectedIndex)
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
                        status.Text = "Mouse Coordinates X: "+e.X+" Y: "+e.Y;
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
                    line = new MyLine(Color.Black,buffer);
                    break;
                case 1:
                    rectangle = new MyRectangle(Color.Black, buffer);
                    break;
                case 2:
                    point = new MyPoint(Color.Black, buffer);
                    break;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox2.SelectedIndex)
            {
                case 0:
                    switch(listBox1.SelectedIndex)
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


    }
}
