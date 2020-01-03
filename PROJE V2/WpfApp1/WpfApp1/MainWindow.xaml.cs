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
        private static string çiftnokta= ":";
        private static string boşdeğer = "Lütfen Değer Girin";

        public MainWindow()
        {
            InitializeComponent();
            txtKullanıcıAdı.Text = regkullanıcı;
            logintxtkullanıcıadı.Text = loginkullanıcı2;
            txthesaplama.Text = boşdeğer;
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
            var regexpw = Regex.Match(stpas1, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,})");
            
            if (true)//Kayıt başarılı olduğunda bildirim gönderirir.
            {
                

                if (kullanıcııd.Length < 5)//Kullanıcı adı uzunluğunu ayarlıyor.
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
                
                if (stpas1 != stpas2)
                {
                    MessageBox.Show("Şifreler uyuşmuyor tekrar girin");
                    return;
                }
                if (stpas1 == çiftnokta)
                {
                    MessageBox.Show("Şifrede"+çiftnokta+ "işareti kullanılmaz. ");
                    return;
                }

                if (!string.IsNullOrEmpty(stpas1) && !regexpw.Success)
                {
                    MessageBox.Show("Zayıf Parola");
                    return;
                }
                File.AppendAllText("users.txt",null);//ilk kayıtta users dosyası oluşması için yoksa idkontorlda prog. kapanır.
                if (FONKSİYONALAR.idkontrol(kullanıcııd))
                {
                    MessageBox.Show("Bu kullanıcı adı zaten var!");
                    return;
                }
                
                string saklanmışşifre = ŞifreSaklama.ComputeSha256Hash(stpas1);
                File.AppendAllText("users.txt", FONKSİYONALAR.normalizeUserName(kullanıcııd).Trim() + çiftnokta + saklanmışşifre + "\r\n");
                MessageBox.Show("Kayıt Başarılı.");
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
            if (FONKSİYONALAR.şifreveıdkontrol(FONKSİYONALAR.normalizeUserName(logintxtkullanıcıadı.Text).Trim(), loginpaswordbox.Password.ToString()))
            {
                MessageBox.Show("Giriş Başarılı");

            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya Şifre yanlış");//ayrı ayrı ayır.
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
            var vrtext = txthesaplama.Text + "\t: " + dblResult.ToString("N2");

            //FONKSİYONALAR.appendToLogFile(cbmboxkullanıcı.SelectedValue.ToString(), vrtext);
            lstsonişlem.Items.Insert(0, vrtext);
        }

        private void txthesaplama_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txthesaplama.Text==boşdeğer)
            {
                txthesaplama.Text = null;
            }
        }
    }
}
