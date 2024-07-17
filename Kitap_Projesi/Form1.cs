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
using System.Data.Common;

namespace Kitap_Projesi
{
    public partial class e : Form
    {
        public e()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=EMRE\\SQLEXPRESS01;Initial Catalog=Kitaplar;Integrated Security=True;");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("Select * From Tbl_Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void e_Load(object sender, EventArgs e)
        {
            listele();

        }
        String durum = "";
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kaydet = new SqlCommand("insert into Tbl_Kitaplar (KitapAdi,KitapYazar,KitapSayfa,KitapTur,KitapDurum) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            kaydet.Parameters.AddWithValue("@p1",TxtKitapAdi.Text);
            kaydet.Parameters.AddWithValue("@p2", TxtYazar.Text);
            kaydet.Parameters.AddWithValue("@p3",TxtSayfa.Text);
            kaydet.Parameters.AddWithValue("@p4",CmbTur.Text);
            if (ChcSıfır.Checked == true)
            {
                durum = "1";
            }
            if (Chckullan.Checked == true)
            {
                durum = "0";
            }
            kaydet.Parameters.AddWithValue("@p5", durum);
            kaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Yapıldı");
            listele();
            

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();

        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("Update Tbl_Kitaplar set KitapAdi=@p1,KitapYazar=@p2,KitapTur=@p3,KitapSayfa=@p4,KitapDurum=@p5 where Kitapid=@p6",baglanti);
            guncelle.Parameters.AddWithValue("@p1", TxtKitapAdi.Text);
            guncelle.Parameters.AddWithValue("@p2", TxtYazar.Text);
            guncelle.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            guncelle.Parameters.AddWithValue("@p3", CmbTur.Text);
            guncelle.Parameters.AddWithValue("@p6",Txtid.Text);
            if (ChcSıfır.Checked == true)
            {
                durum = "1";
            }
            if (Chckullan.Checked == true)
            {
                durum = "0";
            }
            guncelle.Parameters.AddWithValue("@p5", durum);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Yapıldı");
            listele();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtKitapAdi.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString() ;
            CmbTur.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="False")
            {
                ChcSıfır.Checked = true;
            }
            else 
            {
                Chckullan.Checked = true;
            }
            listele();

        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("Select * from Tbl_Kitaplar where KitapAdi=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1",TxtKitapAra.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut); 
            da.Fill(dt);
            dataGridView1.DataSource=dt;
            
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Kitaplar where KitapAdi like '%"+TxtKitapAra.Text+"%'", baglanti);
           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnSil_Click(object sender, EventArgs e)

        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("Delete From Tbl_Kitaplar where Kitapid=@p1",baglanti);
            sil.Parameters.AddWithValue("@p1",Txtid.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }
    }
}
