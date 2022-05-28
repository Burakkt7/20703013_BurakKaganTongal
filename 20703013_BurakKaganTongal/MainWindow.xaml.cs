using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClassVeri.Model;

namespace WpfClassVeri
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            baglanti = new SqlConnection("Data Source=DESKTOP-2J8KJUC;Initial Catalog=görsel_programlama;Integrated Security=True");
        }
        SqlConnection baglanti;
        SqlDataReader dr;
        SqlCommand komut;
        Musteri m;
        ObservableCollection<Musteri> musteriler;
        
        public void Listele()
        {
            
            musteriler= new ObservableCollection<Musteri>();
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT *FROM musteri";
            baglanti.Open();
            dr = komut.ExecuteReader();
            while(dr.Read())
            {
                m = new Musteri();
                m.No = (int)dr[0];
                m.Ad = dr[1].ToString();
                m.Soyad = dr[2].ToString();
                m.Tarih = (DateTime)dr[3];
                m.Telefon = dr[4].ToString();
                musteriler.Add(m);
            }
            baglanti.Close();
            lstMusteri.ItemsSource = musteriler;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Listele();
        }

        private void lstMusteri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Musteri secilen = new Musteri();
            secilen = (Musteri)lstMusteri.SelectedItem;
            grd1.DataContext = secilen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "INSERT INTO musteri (ad,soyad,dtarih,tel) VALUES (@ad,@soyad,@dtarih,@tel)";
            komut.Parameters.AddWithValue("@ad",txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@dtarih", dp1.SelectedDate.Value);
            komut.Parameters.AddWithValue("@tel",txtTelefon.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Musteri m = new Musteri();
            grd1.DataContext = m;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "DELETE FROM musteri WHERE mno=@mno";
            komut.Parameters.AddWithValue("@mno",Convert.ToInt32(txtNo.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "UPDATE musteri SET ad=@ad,soyad=@soyad,dtarih=@tarih,tel=@telefon WHERE mno=@mno";
            komut.Parameters.AddWithValue("@mno",Convert.ToInt32(txtNo.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@telefon",Convert.ToString( txtTelefon.Text));
            komut.Parameters.AddWithValue("@tarih",Convert.ToDateTime( dp1.SelectedDate.Value));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Musteri> filtre = musteriler.Where(x => x.Ad.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                lstMusteri.ItemsSource = filtre;
            }
            catch
            {

            }
            
        }


    }
}
