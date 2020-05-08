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
    public partial class Login : Form
    {
        MySqlConnection baglanti;
        MySqlCommand cmd;
        private static string connectionString = ConfigurationManager.ConnectionStrings["PBEmlak"].ConnectionString;

        public Login()
        {
            InitializeComponent();
            tb_password.PasswordChar = '*';
            tb_password.MaxLength = 14;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void bt_cikis_Click(object sender, EventArgs e)
        {     
            Application.Exit();
        }

        private void bt_giris_Click(object sender, EventArgs e)
        {
            try
            {              
                string query = "SELECT * FROM kullanici where kullaniciAdi='" + tb_username.Text + "' AND kullaniciSifre='" + tb_password.Text + "'";
                baglanti = new MySqlConnection(connectionString);
                baglanti.Open();
                cmd = new MySqlCommand(query, baglanti);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();                
                if (dr.Read())
                {
                    MessageBox.Show("Hoş geldiniz, " + tb_username.Text);
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else { MessageBox.Show("Kullanıcı Adı veya Şifre hatalı"); }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        
        int hareket, x, y;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
            }
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
