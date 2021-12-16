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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }


        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        void griddoldur()
        {
            con = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=kitapcı;Integrated Security=True");
            da = new SqlDataAdapter("Select *From kitap ", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kitapcı");
            dataGridView1.DataSource = ds.Tables["kitapcı"];
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from kitap  where barkod like '%" + textBox1.Text + "%'", con);
            ara.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            dataGridView1.ReadOnly = true; // sadece okunabilir olması yani veri düzenleme kapalı
            dataGridView1.AllowUserToDeleteRows = false;
            griddoldur();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            System.Data.DataTable tbl = new System.Data.DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from kitap  where isim like '%" + textBox2.Text + "%'", con);
            ara.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                int sutun = 1;
                int satir = 1;
                Excel.Application ExcelApp = new Excel.Application();
                ExcelApp.Workbooks.Add();
                ExcelApp.Visible = true; //www.yazilimkodlama.com
                ExcelApp.Worksheets[1].Activate();

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    ExcelApp.Cells[satir, sutun + j].Value = dataGridView1.Columns[j].HeaderText;
                    ExcelApp.Cells[satir, sutun + j].Font.Color = System.Drawing.Color.Black;
                    ExcelApp.Cells[satir, sutun + j].Font.Size = 10;
                    ExcelApp.Cells[satir, sutun + j].ColumnWidth = 10;
                    ExcelApp.Cells[satir, sutun + j].Font.Bold = true;
                    ExcelApp.Cells[satir, sutun + j].Font.Name = "Arial Black";
                }
                satir++;//www.yazilimkodlama.com

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {//www.yazilimkodlama.com
                        ExcelApp.Cells[satir + i, sutun + j].Value = dataGridView1[j, i].Value;
                        if (dataGridView1[j, i].Value.ToString() == "İstanbul")
                        {
                            for (int k = 1; k <= dataGridView1.Columns.Count; k++)
                            {//www.yazilimkodlama.com
                                ExcelApp.Cells[satir + i, k].Interior.Color = System.Drawing.Color.FromArgb(255, 0, 0);
                            }//www.yazilimkodlama.com
                        }
                    }

                }
            }
            catch
            {


            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }
    }
}
