using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClassVeri.Model
{
    public class Musteri
    {
        public Musteri()
        {
            Tarih = DateTime.Now;
        }
        int _no;

        public int No
        {
            get { return _no; }
            set { _no = value; }
        }
        string _ad;

        public string Ad
        {
            get { return _ad; }
            set { _ad = value; }
        }
        string _soyad;

        public string Soyad
        {
            get { return _soyad; }
            set { _soyad = value; }
        }
        DateTime _tarih;

        public DateTime Tarih
        {
            get { return _tarih; }
            set { _tarih = value; }
        }
        string _telefon;

        public string Telefon
        {
            get { return _telefon; }
            set 
            {
                if (value !="")
                    _telefon = value;
                else
                    _telefon = "Telefon Numarası Yok";
            }
        }

    }
}
