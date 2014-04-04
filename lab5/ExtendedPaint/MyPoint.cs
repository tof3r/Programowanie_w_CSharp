using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyPaint
{
    class MyPoint
    {
        Color color = Color.Black;
        Bitmap buffer;
        Graphics tempBuffer;
        bool keyPressed = false;
        SolidBrush solid;

        public MyPoint(Color _color, Bitmap _buffer)
        {
            color = _color;
            buffer = _buffer;
        }

        public void MousePressed(object sender, MouseEventArgs e)
        {
            keyPressed = true;
            solid = new SolidBrush(getColor);
            tempBuffer = Graphics.FromImage(buffer);
        }

        public void MouseMoving(object sender, MouseEventArgs e)
        {
            if (keyPressed)
            {
                tempBuffer.DrawRectangle(new Pen(getColor, 2), e.Location.X, e.Location.Y, 2, 2);
                tempBuffer.FillEllipse(solid, e.Location.X, e.Location.Y, 2, 2);
            }
            
        }

        public void MouseReleased(object sender, MouseEventArgs e)
        {
            keyPressed = false;
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
