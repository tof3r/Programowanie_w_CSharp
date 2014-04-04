using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyPaint
{
    class MyLine
    {
        Color color = Color.Black;
        Bitmap buffer;
        Bitmap previousBuffer;
        bool keyPressed = false;
        Point start;
        Point end;

        public MyLine(Color _color, Bitmap _buffer)
        {
            color = _color;
            buffer = _buffer;
        }

        public void MousePressed(object sender, MouseEventArgs e)
        {
            keyPressed = true;
            start = e.Location;
            previousBuffer = new Bitmap(buffer);
        }

        public void MouseMoving(object sender, MouseEventArgs e)
        {
            if(keyPressed)
            {
                end = e.Location;

                using(Graphics onBuffer = Graphics.FromImage(buffer))
                {
                    onBuffer.Clear(Color.White);
                    onBuffer.DrawImageUnscaled(previousBuffer, 0, 0);
                    onBuffer.DrawLine(new Pen(getColor), start.X, start.Y, end.X, end.Y);
                }
            }
        }

        public void MouseReleased(object sender, MouseEventArgs e)
        {
            keyPressed = false;
            using(Graphics onBuffer = Graphics.FromImage(buffer))
            {
                onBuffer.DrawLine(new Pen(getColor),start.X,start.Y,end.X,end.Y);
            }
        }

        public Bitmap getBuffer
        {
            get { return buffer; }
        }

        public Color getColor
        {
            get { return color; }
            set { color = value; }
        }

    }
}
