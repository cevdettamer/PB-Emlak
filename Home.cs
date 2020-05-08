using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace PB_Emlak
{
    public partial class Home : Form
    {
        MySqlConnection baglanti;
        DataTable dt;
        MySqlDataAdapter da;
        MySqlCommand cmd;
        private static string connectionString = ConfigurationManager.ConnectionStrings["PBEmlak"].ConnectionString;
        int ekle = 1;
        int guncelle = 2;
        
        public Home()
        {
            InitializeComponent();
            doldur();
            btn_ara2.Visible = false;
            this.Width = 960; 
            this.Height = 594; 
        }
        public void doldur()
        {            
            baglanti = new MySqlConnection(connectionString);
            baglanti.Open();
            string query = "Select *From ozellikler";
            cmd = new MySqlCommand(query, baglanti);
            da = new MySqlDataAdapter(query, baglanti);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            //Process formu açılıyor
            Process frm = new Process();
            frm.deger = ekle;
            frm.ShowDialog(); 
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        { 
            //Home formundaki seçili bilgiler Process formunda gösterilecek.
            Process frm = new Process();
            //datagriddeki seçili satırın id si alınacak diğer formda o id ye karşılık gelen değerler doldurulacak.
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            //Console.WriteLine("Home id: " + id.ToString());
            frm.id = id;
            frm.deger = guncelle;
            frm.ShowDialog();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void tb_ara_TextChanged(object sender, EventArgs e)
        {
            baglanti = new MySqlConnection(connectionString);
            baglanti.Open();
            dt = new DataTable();           
            MySqlDataAdapter aramayap = new MySqlDataAdapter("select * from ozellikler where ilce like '%" + tb_ara.Text + "%' or mahalle like '%" + tb_ara.Text + "%'", baglanti);
            aramayap.Fill(dt);
           
            baglanti.Close();
            dataGridView1.DataSource = dt;
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void btn_cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int hareket, x, y;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X-x, MousePosition.Y-y);
            }
        }
        
        private void btn_sil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                string query = "delete from ozellikler where Id='"+id+"' ";
                baglanti.Open();
                cmd = new MySqlCommand(query, baglanti);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                MessageBox.Show("Veriler başarıyla silindi :)");
                doldur();
                while (dr.Read())
                {
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void btn_ara_Click(object sender, EventArgs e)
        {
            this.Width = 1194;
            btn_ara.Visible = false;
            btn_ara2.Visible = true;
        }

        private void btn_ara2_Click(object sender, EventArgs e)
        {
            this.Width = 962;
            btn_ara.Visible = true;
            btn_ara2.Visible = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = 1;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = 0;
            
        }
    }
}
