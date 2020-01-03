using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
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
        private static string çiftnokta = ":";
        private static string boşdeğer = "Lütfen Değer Girin";


        public MainWindow()
        {
            InitializeComponent();
            txtKullanıcıAdı.Text = regkullanıcı;
            logintxtkullanıcıadı.Text = loginkullanıcı2;
            txthesaplama.Text = boşdeğer;
            ((Control)this.KAYIT).IsEnabled = true;
            ((Control)this.GİRİŞ).IsEnabled = true;
            ((Control)this.Hesapmakinası).IsEnabled = false;
        }

        private void txtKullanıcıAdı_GotFocus(object sender, RoutedEventArgs e)//Focus oldğunda metini sıfırlar.
        {

            if (txtKullanıcıAdı.Text != regkullanıcı)
            {
                return;
            }
            txtKullanıcıAdı.Text = null;
        }
        private void Kayıtolbtn_Click(object sender, RoutedEventArgs e)
        {
            var stpas1 = pas1.Password.ToString();
            var stpas2 = pas2.Password.ToString();
            var kullanıcııd = txtKullanıcıAdı.Text;
            var regexpw = Regex.Match(stpas1, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,})");// \d sadece sayı

            if (kullanıcııd.Length < 5)//Kullanıcı adı uzunluğunu ayarlıyor.
            {
                MessageBox.Show("Lütfen en az 6 karakterden olşuturulacak Id giriniz ");
            }
            var regexıd = Regex.Match(kullanıcııd, @"(^\w*$)");// \w sayı ve karekter için * Girilen  veride  karakterler hiç ya da bir veya birden çok tekrar edebilsin
            if (!regexıd.Success)
            {
                MessageBox.Show("Kullanıcı Adında Kullanılmayacak Karakterler var.(@!'^^$.. gibi)");
                return;
            }
            if (stpas1 != stpas2)
            {
                MessageBox.Show("Şifreler uyuşmuyor tekrar girin");
                return;
            }
            if (stpas1 == çiftnokta)
            {
                MessageBox.Show("Şifrede" + çiftnokta + "işareti kullanılmaz. ");
                return;
            }

            if (!string.IsNullOrEmpty(stpas1) && !regexpw.Success)
            {
                MessageBox.Show("Zayıf Parola");
                return;
            }
            var kullanıcı2 = FONKSİYONALAR.normalizeUserName(kullanıcııd).Trim();

            if (FONKSİYONALAR.idkontrol(kullanıcı2))
            {
                MessageBox.Show("Bu kullanıcı adı zaten var!");
                return;
            }
            string saklanmışşifre = ŞifreSaklama.ComputeSha256Hash(stpas1);
            File.AppendAllText("users.txt", kullanıcı2 + çiftnokta + saklanmışşifre + "\r\n");
            if (MessageBox.Show("Kayıt Başarlı.Otomatik Giriş Yapılsın Mı ?", "Soru", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var girişıd = txtKullanıcıAdı.Text;
                ((Control)this.KAYIT).IsEnabled = false;
                ((Control)this.GİRİŞ).IsEnabled = false;
                ((Control)this.Hesapmakinası).IsEnabled = true;
                Hesapmakinası.IsSelected = true;
                cbmboxkullanıcı.Items.Add($"({girişıd})");
                cbmboxkullanıcı.SelectedIndex = 0;
                txthesaplama.Focus();

            }
            else
            {
                ((Control)this.KAYIT).IsEnabled = true;
                ((Control)this.GİRİŞ).IsEnabled = true;
                ((Control)this.Hesapmakinası).IsEnabled = false;
                GİRİŞ.IsSelected = true;
            }
        }

        private void logintxtkullanıcıadı_GotFocus(object sender, RoutedEventArgs e)//Focus oldğunda metini sıfırlar
        {
            if (logintxtkullanıcıadı.Text == loginkullanıcı2)
            {
                logintxtkullanıcıadı.Text = null;
            }
        }
        private void girişbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!FONKSİYONALAR.girişidkontrol(FONKSİYONALAR.normalizeUserName(logintxtkullanıcıadı.Text).Trim()))
            {
                MessageBox.Show("Kullanıcı Adınız Yanlış");
                return;
            }
            if (FONKSİYONALAR.girişşifrekontrol(loginpaswordbox.Password.ToString()))
            {
                MessageBox.Show("Giriş Başarılı");
                ((Control)this.KAYIT).IsEnabled = false;
                ((Control)this.GİRİŞ).IsEnabled = false;
                ((Control)this.Hesapmakinası).IsEnabled = true;
                Hesapmakinası.IsSelected = true;
                var girişıd = logintxtkullanıcıadı.Text;
                cbmboxkullanıcı.Items.Add($"({girişıd})");
                cbmboxkullanıcı.SelectedIndex = 0;
                txthesaplama.Focus();
            }
            else
            {
                MessageBox.Show("Şifreniz Yanlış");
            }

        }

        private void btneşittir_Click(object sender, RoutedEventArgs e)
        {
            double dblResult;
            try
            {
                dblResult = Convert.ToDouble(new DataTable().Compute(txthesaplama.Text, null));
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());
                return;
            }
            var vrtext = txthesaplama.Text + "\t: " + dblResult.ToString("N3");
            FONKSİYONALAR.appendToLogFile(cbmboxkullanıcı.SelectedValue.ToString(), vrtext);
            lstsonişlem.Items.Insert(0, vrtext);
        }

        private void txthesaplama_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
        }

        private void cbmboxkullanıcı_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FONKSİYONALAR.readToListBox(this, cbmboxkullanıcı.SelectedValue.ToString());
        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Çıkış yapıldı.");
            logintxtkullanıcıadı.Text = null;
            loginpaswordbox.Password = null;
            txtKullanıcıAdı.Text = regkullanıcı;
            pas1.Password = null;
            pas2.Password = null;
            txthesaplama.Text = null;
            ((Control)this.KAYIT).IsEnabled = true;
            ((Control)this.GİRİŞ).IsEnabled = true;
            ((Control)this.Hesapmakinası).IsEnabled = false;
            GİRİŞ.IsSelected = true;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "1";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "1";
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "2";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "2";
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {

                txthesaplama.Text = "3";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "3";
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "4";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "4";
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "5";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "5";
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "6";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "6";
            }
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "7";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "7";
            }
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "8";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "8";
            }
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {


                txthesaplama.Text = "9";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "9";
            }
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {


                txthesaplama.Text = "0";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "0";
            }
        }

        private void btntopmala_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "+";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "+";
            }
        }

        private void btnçıkarma_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "-";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "-";
            }
        }

        private void btnbölme_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "/";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "/";
            }
        }

        private void btnçarpma_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = null;
            }
            if (txthesaplama.Text == "0" && txthesaplama.Text == boşdeğer)
            {
                txthesaplama.Text = "*";
            }
            else
            {
                txthesaplama.Text = txthesaplama.Text + "*";
            }
        }

        private void btnsilme_Click(object sender, RoutedEventArgs e)
        {
            txthesaplama.Text = null;
        }

        private void btnce_Click(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text.Length > 0)
            {
                txthesaplama.Text = txthesaplama.Text.Substring(0, txthesaplama.Text.Length - 1);
            }
        }
    }
}
