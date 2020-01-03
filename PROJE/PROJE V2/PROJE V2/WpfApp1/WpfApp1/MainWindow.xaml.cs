using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string regkullanıcı = "Kullanıcı Adı Girin";
        private static string loginkullanıcı2 = "Kullanıcı Adınızı Girin";
        public MainWindow()
        {
            InitializeComponent();
            txtKullanıcıAdı.Text = regkullanıcı;
            logintxtkullanıcıadı.Text = loginkullanıcı2;
        }
        //var kullanıcııd = txtKullanıcıAdı.Text;  txtKullanıcıAdı_TextChanged çağırılmıyor

        private void txtKullanıcıAdı_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtKullanıcıAdı.Text != regkullanıcı)
            {
                return;
            }
            txtKullanıcıAdı.Text = null;

        }
        private void txtKullanıcıAdı_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtKullanıcıAdı.Text.Trim(); //trim kayıt?
        }

        private void Kayıtolbtn_Click(object sender, RoutedEventArgs e)
        {
            var kullanıcııd = txtKullanıcıAdı.Text;//txtKullanıcıAdı_TextChanged çağırılmıyor
            var stpas1 = pas1.Password.ToString();
            var stpas2 = pas2.Password.ToString();
            var çiftnokta = ":";
            var regexpw = Regex.Match(stpas1, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,})");

            if (true)
            {

                if (kullanıcııd.Length < 5)
                {
                    MessageBox.Show("Lütfen en az 6 karakterden olşuturulacak Id giriniz ");
                }
                List<char> lstChars = new List<char>();
                foreach (var vrChar in System.IO.Path.GetInvalidFileNameChars())//Yasaklı karakterleri engelliyor.
                                                                                //boşluk tuşu kullanıcı adı içinde olmamamlı
                                                                                //Trim kullandığımda başta işe yaramıyor
                {
                    lstChars.Add(vrChar);
                }
                if (kullanıcııd.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
                {
                    MessageBox.Show("Yasaklı karakterler var.Tekrar deneyin!");
                    return;
                }
                if (!string.IsNullOrEmpty(stpas1) && !regexpw.Success)
                {
                    MessageBox.Show("Zayıf Parola");
                    return;
                }
                if (stpas1 != stpas2)
                {
                    MessageBox.Show("Şifreler uyuşmuyor tekrar girin");
                    return;
                }
                if (stpas1 == kullanıcııd)
                {
                    MessageBox.Show("Kullanıcı adı ve şifre aynı olamaz.");
                    return;
                }
                if (FONKSİYONALAR.idkontrol(kullanıcııd))
                {
                    MessageBox.Show("Bu kullanıcı adı zaten var!");
                    return;
                }
                if (stpas1 == ":")
                {
                    MessageBox.Show("Şifrede ':' işrati kullanılmaz ");
                    return;
                }
                if (stpas1.Length < 8)
                {
                    MessageBox.Show("Şifre 8 karakterden kısa olamaz.");
                    return;
                }
                
                MessageBox.Show("Kayıt Başarılı.");
            }
            string saklanmışşifre = hashed.ComputeSha256Hash(stpas1);
            File.AppendAllText("users.txt", kullanıcııd + çiftnokta + saklanmışşifre + "\r\n");

        }
        
        private void logintxtkullanıcıadı_GotFocus(object sender, RoutedEventArgs e)
        {
            if (logintxtkullanıcıadı.Text == loginkullanıcı2)
            {
                logintxtkullanıcıadı.Text = null;
            }
            
        }

        private void girişbtn_Click(object sender, RoutedEventArgs e)
        {
            if (FONKSİYONALAR.şifreveıdkontrol(loginpaswordbox.Password.ToString(),logintxtkullanıcıadı.Text))
            {
                MessageBox.Show("Giriş Başarılı");
                //fonksiyon hatalı kontrol et.
            }
           
        }

       
    }

}
