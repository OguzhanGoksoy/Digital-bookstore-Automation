using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program_kitap
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 fmust = new Form2();
            fmust.ShowDialog();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form1 fmust = new Form1();
            fmust.ShowDialog();
           
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 fmust = new Form4();
            fmust.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 fmust = new Form5();
            fmust.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
