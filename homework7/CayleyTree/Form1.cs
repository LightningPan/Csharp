using System;
using System.Drawing;
using System.Windows.Forms;

namespace CayleyTree
{
    public partial class Form1 : Form
    {
        public int n = 10;
        public double leng = 100;
        public double th1 = 30 * Math.PI / 180;
        public double th2 = 20 * Math.PI / 180;
        public double per1 = 0.6;
        public double per2 = 0.7;
        private Graphics graphics;
        private Pen pen;
        public void drawCayleyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);
            drawLine(x0, y0, x1, y1);
            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);
        }
        public void drawLine(double x0, double y0, double x1, double y1)
        {
            graphics.DrawLine(pen, (int)x0, (int)y0, (int)x1, (int)y1);
        }



        public Form1()
        {
           
            InitializeComponent();
            pen = new Pen(Color.Blue);
            graphics = panel1.CreateGraphics();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            n = trackBar1.Value;
            graphics.Clear(BackColor);
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            leng = trackBar2.Value;
            graphics.Clear(BackColor);
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            per1 =(double)trackBar3.Value / 10;
            graphics.Clear(BackColor);
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            per2 = (double) trackBar4.Value/10;
            graphics.Clear(BackColor);
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            th1 = trackBar5.Value * Math.PI / 180;
            graphics.Clear(BackColor);
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        
    }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            th2 = trackBar6.Value * Math.PI / 180;
            graphics.Clear(BackColor);
            drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text) {
                case "Blue": {
                        pen.Color = Color.Blue;
                        graphics.Clear(BackColor);
                        drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
                        break;
                    }
                case "Black":
                    {
                        pen.Color = Color.Black;
                        graphics.Clear(BackColor);
                        drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
                        break;
                    }
                case "Red":
                    {
                        pen.Color = Color.Red;
                        graphics.Clear(BackColor);
                        drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
                        break;
                    }
                case "Silver":
                    {
                        pen.Color = Color.Silver;
                        graphics.Clear(BackColor);
                        drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
                        break;
                    }
                case "Gray":
                    {
                        pen.Color = Color.Gray;
                        graphics.Clear(BackColor);
                        drawCayleyTree(n, 200, 310, leng, -Math.PI / 2);
                        break;
                    }
            }
        }
    }
}
