using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caculatorWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text=$"{getResult(1)}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = $"{getResult(2)}";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = $"{getResult(3)}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = $"{getResult(4)}";
        }

        private double getResult(int n) {
            try
            {
                double num1 = checkNum(textBox1.Text);
                double num2 = checkNum(textBox2.Text);
                double result;
                switch (n)
                {
                    case 1:
                        result= num1 + num2;
                        break;
                    case 2:
                        result = num1 - num2;
                        break;
                    case 3:
                        result = num1 * num2;
                        break;
                    case 4:
                        result = num1 / num2;
                        break;
                    default:
                        result = 0;
                        break;
                }
                return result;
            }
            catch (Exception e) {
                
            }
        
        }
        double checkNum(string num)
        {
                double i = Double.Parse(num);
                return i;
        }





    }
}
