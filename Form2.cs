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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace program_kitap
{
    public partial class Form2 : Form
    {

        SqlConnection bag = new SqlConnection();
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();
        
        public Form2()
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

            dad = new SqlDataAdapter("Select * from alıs", bag);

            dad.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            bag.Close();
        }

        private void güncelle()
        {
            dad = new SqlDataAdapter("Select * from alıs", bag);
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
            alankisi.Text = "";
            girisfiyat.Text = "";


        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Bagla();
          
            gridDoldur();
            button4.Enabled = false;

            dataGridView1.ReadOnly = true; 
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (barkot.Text == "" || isim.Text == "" || adet.Text == "" || fiyat.Text == "" || alankisi.Text == "" || girisfiyat.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakmayınız!!");
            }
            else
            {
                bag.Open();


                string kayit = "insert into alıs (barkod,isim,adet,fiyat,tarih,kayıtyapankisi,girisfiyat) values(@barkod,@isim,@adet,@fiyat,@tarih,@alankisi,@girisfiyat)";



                SqlCommand komut = new SqlCommand(kayit, bag);


                komut.Parameters.AddWithValue("@barkod", barkot.Text);
                komut.Parameters.AddWithValue("@isim", isim.Text);
                komut.Parameters.AddWithValue("@adet", adet.Text);
                komut.Parameters.AddWithValue("@fiyat", fiyat.Text);
                komut.Parameters.AddWithValue("@tarih", tarih.Text);
                komut.Parameters.AddWithValue("@alankisi", alankisi.Text);
                komut.Parameters.AddWithValue("@girisfiyat", girisfiyat.Text);

                komut.ExecuteNonQuery();




                bag.Close();








                güncelle();
                bosalt();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (secim == DialogResult.Yes)
                {
                    Bagla();

                    SqlCommand cmd = new SqlCommand();
                   
                    cmd = new SqlCommand("delete from alıs where id=@id", bag);
                    
                    cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
                    
                    cmd.ExecuteNonQuery();
                   
                    
                   


                    bag.Close();
                    MessageBox.Show("Ürün Silinmiştir");
                    bosalt();

                    güncelle();
                    button1.Enabled = true;
                    button4.Enabled = false;
                }
            }

            catch
            {

                MessageBox.Show("Lütfen Silinecek Ürünü Seçin!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barkot_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            barkot.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            isim.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            adet.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
           
            fiyat.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            alankisi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            girisfiyat.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            

            button1.Enabled = false;
            button4.Enabled = true;

            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (barkot.Text == "" || isim.Text == "" || adet.Text == "" || fiyat.Text == "" || alankisi.Text == "" || girisfiyat.Text == "")
            {
                MessageBox.Show("Boş Alan Bırakmayınız!!");
            }
            else
            {
                bag.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("update alıs set barkod=@barkod ,isim=@isim,adet=@adet, fiyat=@fiyat , tarih=@tarih, kayıtyapankisi=@alankisi, girisfiyat=@girisfiyat where id=@id ", bag);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);

                cmd.Parameters.AddWithValue("@barkod", barkot.Text);
                cmd.Parameters.AddWithValue("@isim", isim.Text);
                cmd.Parameters.AddWithValue("@adet", adet.Text);
                cmd.Parameters.AddWithValue("@fiyat", fiyat.Text);
                cmd.Parameters.AddWithValue("@tarih", tarih.Text);
                cmd.Parameters.AddWithValue("@alankisi", alankisi.Text);
                cmd.Parameters.AddWithValue("@girisfiyat", girisfiyat.Text);
                cmd.ExecuteNonQuery();



                bag.Close();
                güncelle();

                bosalt();

                button1.Enabled = true;
                button4.Enabled = false;
            }
        }

        private void barkot_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            bosalt();
            button4.Enabled = false;
            
        }
    }
}
