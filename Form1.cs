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
using System.Data.OleDb;

namespace program_kitap
{
    public partial class Form1 : Form
    {
        int id;

        SqlConnection bag = new SqlConnection();
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();
        
        public Form1()
        {
            InitializeComponent();
        }


        void Bagla()
        {
            bag.ConnectionString = "Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=kitapcı;Integrated Security=True";
            bag.Open();
        }


        void gridDoldur()
        {

            ds.Clear();

            dad = new SqlDataAdapter("Select * from kitap", bag);

            dad.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            bag.Close();
        }

        private void güncelle()
        {
            dad = new SqlDataAdapter("Select * from kitap", bag);
            ds = new DataSet();
            bag.Open();
            dad.Fill(ds, "tablo");
            bag.Close();
            dataGridView1.DataSource = ds.Tables["tablo"];
        }


        void bosalt()
        {

            barkot.Text = "";
            isim.Text = "";
            adet.Text = "";
            fiyat.Text = "";
            satankisi.Text = "";
           


        }

        void bosalt2()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";


        }


    

                    

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void adet_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bagla();

            gridDoldur();

            bosalt();

            dataGridView1.ReadOnly = true; // sadece okunabilir olması yani veri düzenleme kapalı
            dataGridView1.AllowUserToDeleteRows = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (barkot.Text == "" || isim.Text == "" || adet.Text == "" || fiyat.Text == "" || satankisi.Text == "" )
            {
                MessageBox.Show("Boş Alan Bırakmayınız!!");
            }
            else
            {

                bag.Open();


                string kayit = "insert into kitap (barkod,isim,adet,fiyat,tarih,satankisi) values(@barkod,@isim,@adet,@fiyat,@tarih,@satankisi)";


                string sql = string.Format("update alıs set adet=(select adet from alıs where id={0}) - {1} where id={0}",
                            int.Parse(textBox1.Text), int.Parse(adet.Text));
                SqlCommand cmd1 = new SqlCommand(sql, bag); cmd1.ExecuteNonQuery();


                SqlCommand komut = new SqlCommand(kayit, bag);


                komut.Parameters.AddWithValue("@barkod", barkot.Text);
                komut.Parameters.AddWithValue("@isim", isim.Text);
                komut.Parameters.AddWithValue("@adet", adet.Text);
                komut.Parameters.AddWithValue("@fiyat", fiyat.Text);
                komut.Parameters.AddWithValue("@tarih", tarih.Text);
                komut.Parameters.AddWithValue("@satankisi", satankisi.Text);

                komut.ExecuteNonQuery();


                bag.Close();








                güncelle();
                bosalt();
                bosalt2();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Silinecek Ürünü Seçin!!");
            }
            else
            {
            DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {

               


                    Bagla();

                    string sql = string.Format("update alıs set adet=(select adet from alıs where id={0}) + {1} where id={0}",
                           int.Parse(textBox5.Text), int.Parse(textBox2.Text));
                    SqlCommand cmd1 = new SqlCommand(sql, bag); cmd1.ExecuteNonQuery();

                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("delete from kitap where id=@id", bag);
                    cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    gridDoldur();
                    bag.Close();
                    MessageBox.Show("Kullanıcı Silinmiştir");
                    bosalt();

                    bosalt2();
                
                }
            }
        }

        private void barkot_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void barkot_Enter(object sender, EventArgs e)
        {

           
        }

      
        private void barkot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bag.Open();
                DataTable tbl = new DataTable();
                SqlDataAdapter ara = new SqlDataAdapter("Select * from alıs where barkod like '%" + barkot.Text + "%'", bag);
                ara.Fill(tbl);

                
                 textBox1.Text = tbl.Rows[0][0].ToString();
                isim.Text = tbl.Rows[0][2].ToString();
                fiyat.Text = tbl.Rows[0][4].ToString();
                adet.Text =" 1";

                bag.Close();
              
            }
           
        }

        private void barkot_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            bag.Open();
            DataTable tbl = new DataTable();

            SqlDataAdapter ara = new SqlDataAdapter("Select * from alıs where barkod like '%" + textBox4.Text + "%'", bag);
            ara.Fill(tbl);

            textBox5.Text = tbl.Rows[0][0].ToString();


            bag.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
