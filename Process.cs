
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
    public partial class Process : Form
    {
        MySqlConnection baglanti = new MySqlConnection(connectionString);
        private static string connectionString = ConfigurationManager.ConnectionStrings["PBEmlak"].ConnectionString;
        MySqlCommand cmd;
        public int id { get; set; }
        public int deger { get; set; }
        //emlak, durum, ilanBasligi, ilce, mahalle, adres, fiyat, metrekare, odaSayisi, BinaYasi, katSayisi, 
        //  bulunduguKat, emlakSahibi, iletisim, banyo, balkon, esyaDurumu, isitma, kredi, resim;
        public void doldur()
        {
            int[] sayi = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] deger = { "evet", "hayır" };
            string[] ilce = { "Seyhan", "Çukurova", "Yüreğir", "Kozan" };
            string[] mahalle = { "Namık Kemal", "Toros", "Barajyolu" };
            string[] durum = { "Satılık", "Kiralık", "Mülk" };
            string[] emlak = { "Arsa", "İşyeri", "Ev", "Bina" };

            for (int i = 0; i < emlak.Length; i++)
            {
                cb_emlak.Items.Add(emlak[i].ToString());
            }
            for (int i = 0; i < durum.Length; i++)
            {
                cb_durum.Items.Add(durum[i].ToString());
            }
            for (int i = 0; i < ilce.Length; i++)
            {
                cb_ilce.Items.Add(ilce[i].ToString());
            }
            for (int i = 0; i < mahalle.Length; i++)
            {
                cb_mahalle.Items.Add(mahalle[i].ToString());
            }
            tb_adres.Text = "";
            tb_fiyat.Text = "";
            tb_m2.Text = "";
            tb_emlakSahibi.Text = "";
            tb_iletisim.Text = "";
            tb_ilanBasligi.Text = "";
            for (int i = 0; i < sayi.Length; i++)
            {
                cb_odaSayisi.Items.Add(sayi[i].ToString());
                cb_binaYasi.Items.Add(sayi[i].ToString());
                cb_katSayisi.Items.Add(sayi[i].ToString());
                cb_bulunduguKat.Items.Add(sayi[i].ToString());
                cb_banyo.Items.Add(sayi[i].ToString());
                cb_balkon.Items.Add(sayi[i].ToString());
            }
            cb_esyaDurumu.Items.Add(deger[0]);
            cb_esyaDurumu.Items.Add(deger[1]);
            cb_isitma.Items.Add(deger[0]);
            cb_isitma.Items.Add(deger[1]);
            cb_kredi.Items.Add(deger[0]);
            cb_kredi.Items.Add(deger[1]);

        }

        public void guncelleDoldur()
        {
            //Console.WriteLine("Process id:" + id);     
            baglanti.Open();
            string query = "Select * From ozellikler Where Id= '" + id + "'";
            cmd = new MySqlCommand(query, baglanti);
            int Count = Convert.ToInt32(cmd.ExecuteScalar());
            if (Count != 0)
            {
                MySqlDataReader oku = cmd.ExecuteReader();
                while (oku.Read())
                {
                    //texboxlar
                    tb_adres.Text = oku["adres"].ToString();
                    tb_fiyat.Text = oku["fiyat"].ToString();
                    tb_m2.Text = oku["metrekare"].ToString();
                    tb_emlakSahibi.Text = oku["emlakSahibi"].ToString();
                    tb_iletisim.Text = oku["iletisim"].ToString();
                    tb_ilanBasligi.Text = oku["ilanBasligi"].ToString();
                    //comboboxlar
                    cb_odaSayisi.SelectedItem = oku["odaSayisi"].ToString();
                    cb_binaYasi.SelectedItem = oku["binaYasi"].ToString();
                    cb_katSayisi.SelectedItem = oku["katSayisi"].ToString();
                    cb_bulunduguKat.SelectedItem = oku["bulunduguKat"].ToString();
                    cb_banyo.SelectedItem = oku["banyo"].ToString();
                    cb_balkon.SelectedItem = oku["balkon"].ToString();
                    cb_esyaDurumu.SelectedItem = oku["esyaDurumu"].ToString();
                    cb_isitma.SelectedItem = oku["isitma"].ToString();
                    cb_kredi.SelectedItem = oku["kredi"].ToString();
                    cb_emlak.SelectedItem = oku["emlak"].ToString();
                    cb_durum.SelectedItem = oku["durum"].ToString();
                    cb_ilce.SelectedItem = oku["ilce"].ToString();
                    cb_mahalle.SelectedItem = oku["mahalle"].ToString();
                    //picturebox
                    tb_fotoYolu.Text = oku["resim"].ToString();
                    pb_resim.ImageLocation = tb_fotoYolu.Text;
                }
            }

            baglanti.Close();
        }

        public Process()
        {
            InitializeComponent();
        }

        private void Process_Load(object sender, EventArgs e)
        {
            doldur();
            guncelleDoldur();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        //resim yükleme olayı
        private void btn_yukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            string dosyayolu = dosya.FileName;
            tb_fotoYolu.Text = dosyayolu;
            pb_resim.ImageLocation = dosyayolu;
        }

        public void vt_ekle()
        {
            try
            {
                string adres = tb_adres.Text;
                string fiyat = tb_fiyat.Text;
                int m2 = Convert.ToInt32(tb_m2.Text);
                string emlakSahibi = tb_emlakSahibi.Text;
                string iletisim = tb_iletisim.Text;
                string ilanBasligi = tb_ilanBasligi.Text;
                string odaSayisi = cb_odaSayisi.SelectedItem.ToString();
                int binaYasi = Convert.ToInt32(cb_binaYasi.SelectedItem);
                int katSayisi = Convert.ToInt32(cb_katSayisi.SelectedItem);
                int bulunduguKat = Convert.ToInt32(cb_bulunduguKat.SelectedItem);
                int banyo = Convert.ToInt32(cb_banyo.SelectedItem);
                int balkon = Convert.ToInt32(cb_balkon.SelectedItem);
                string esyaDurumu = cb_esyaDurumu.SelectedItem.ToString();
                string isitma = cb_isitma.SelectedItem.ToString();
                string kredi = cb_kredi.SelectedItem.ToString();
                string emlak = cb_emlak.SelectedItem.ToString();
                string durum = cb_durum.SelectedItem.ToString();
                string ilce = cb_ilce.SelectedItem.ToString();
                string mahalle = cb_mahalle.SelectedItem.ToString();
                string resim = tb_fotoYolu.Text;
                string query = "insert into ozellikler (emlak, durum, ilanBasligi, ilce, " +
                    "mahalle, adres, fiyat, metrekare, odaSayisi, binaYasi, katSayisi, bulunduguKat, " +
                    "emlakSahibi, iletisim, banyo, balkon, esyaDurumu, isitma, kredi, resim) VALUES ('" + emlak + "', '" + durum + "', '" + ilanBasligi + "', '" + ilce + "', '" + mahalle + "'," +
                    " '" + adres + "', '" + fiyat + "', '" + m2 + "', '" + odaSayisi + "', '" + binaYasi + "', '" + katSayisi + "', '" + bulunduguKat + "', '" + emlakSahibi + "', '" + iletisim + "', '" + banyo + "', '" + balkon + "', '" + esyaDurumu + "', " +
                    " '" + isitma + "', '" + kredi + "', '" + resim + "')";
                baglanti.Open();
                cmd = new MySqlCommand(query, baglanti);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Veriler başarıyla eklendi :)");
                while (dr.Read())
                {

                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw; //hata ayıklama
                return;
            }
        }

        public void vt_guncelle()
        {
            //update
            try
            {
                string adres = tb_adres.Text;
                string fiyat = tb_fiyat.Text;
                int m2 = Convert.ToInt32(tb_m2.Text);
                string emlakSahibi = tb_emlakSahibi.Text;
                string iletisim = tb_iletisim.Text;
                string ilanBasligi = tb_ilanBasligi.Text;
                string odaSayisi = cb_odaSayisi.SelectedItem.ToString();
                int binaYasi = Convert.ToInt32(cb_binaYasi.SelectedItem);
                int katSayisi = Convert.ToInt32(cb_katSayisi.SelectedItem);
                int bulunduguKat = Convert.ToInt32(cb_bulunduguKat.SelectedItem);
                int banyo = Convert.ToInt32(cb_banyo.SelectedItem);
                int balkon = Convert.ToInt32(cb_balkon.SelectedItem);
                string esyaDurumu = cb_esyaDurumu.SelectedItem.ToString();
                string isitma = cb_isitma.SelectedItem.ToString();
                string kredi = cb_kredi.SelectedItem.ToString();
                string emlak = cb_emlak.SelectedItem.ToString();
                string durum = cb_durum.SelectedItem.ToString();
                string ilce = cb_ilce.SelectedItem.ToString();
                string mahalle = cb_mahalle.SelectedItem.ToString();
                string query = "update ozellikler set emlak='" + emlak + "', durum='" + durum + "', ilanBasligi='" + ilanBasligi + "', ilce='" + ilce + "', mahalle='" + mahalle + "'," +
                    "adres='" + adres + "', fiyat='" + fiyat + "', metrekare='" + m2 + "', odaSayisi='" + odaSayisi + "', binaYasi='" + binaYasi + "', katSayisi='" + katSayisi + "', " +
                    "bulunduguKat='" + bulunduguKat + "', emlakSahibi='" + emlakSahibi + "', iletisim='" + iletisim + "', banyo='" + banyo + "', balkon='" + balkon + "', esyaDurumu='" + esyaDurumu + "', " +
                    "isitma='" + isitma + "', kredi='" + kredi + "', resim='" + tb_fotoYolu.Text + "' where Id='" + id + "' ";
                baglanti.Open();
                cmd = new MySqlCommand(query, baglanti);
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Veriler başarıyla güncellendi :)");
                while (dr.Read())
                {
                }
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
                //throw;
            }
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            kontrol(this);
            if (deger == 1)
            {
                vt_ekle();
            }
            if (deger == 2)
            {
                vt_guncelle();
            }
        }

        private void Process_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home frm = (Home)Application.OpenForms["Home"];
            frm.doldur();
        }

        private void temizle(Control ctl)
        {
            //form elemanlarının içini temizleme
            foreach (Control item in ctl.Controls)
            {
                if (item is ComboBox)
                {
                    ((ComboBox)item).Text = "";
                }
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }
                if (item.Controls.Count > 0)
                {
                    temizle(item);
                }
            }
        }

        private void kontrol(Control ctl)
        {
            //form elemanları boşluk kontrolü
            foreach (Control item in ctl.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Text == "")
                    {
                        MessageBox.Show("Doldurulması gereken yerler var!");
                    }
                }
                if (item is ComboBox)
                {
                    if (item.Text == "")
                    {
                        MessageBox.Show("Doldurulması gereken yerler var!");
                    }
                }
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            temizle(this);
        }
    }
}
