using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH18
{
    public partial class Form1 : Form
    {
        bool Moving = false;
        Rectangle rect = new Rectangle(10, 10, 60, 40);
        Point ViTri;
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(brush, rect);
            }

            using (Pen pen = new Pen(Color.Black, 3)) 
            {
                g.DrawRectangle(pen, rect);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(rect.Contains(e.Location))
            {
                Moving = true;
                ViTri = new Point(e.X - rect.X, e.Y - rect.Y);

            }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Moving)
            {
                rect.X = e.X - ViTri.X;
                rect.Y = e.Y - ViTri.Y;
                this.Invalidate();
            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Moving = false;
        }
    }
}
