using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace program_kitap
{


    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;



        void bosalt()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox2.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox2.PasswordChar = '#';
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;
            con = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=kitapcı;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM giris where KullaniciAdi='" + textBox1.Text + "' AND KullaniciSifresi='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            bool success = dr.Read(); dr.Close();
            if (success)
            {
                bosalt();

                this.Hide();
                program_kitap.menu fmust = new program_kitap.menu();
                fmust.ShowDialog();
                this.Show();
                con.Close(); return;
            }
                 else
            {
                bosalt();
            }
            bosalt();
            }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
             {
                //checkBox işaretli ise
                if (checkBox1.Checked)
                {
                    //karakteri göster.
                    textBox2.PasswordChar = '\0';
                }
                //değilse karakterlerin yerine * koy.
                else
                {
                    textBox2.PasswordChar = '#';
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
            this.Close();
        }
        }
    }

