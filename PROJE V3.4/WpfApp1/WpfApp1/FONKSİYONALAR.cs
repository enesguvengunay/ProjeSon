using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public static class FONKSİYONALAR
    {
        
        public static bool idkontrol(string kullanıcııd2)
        {
            File.AppendAllText("users.txt", null);//ilk kayıtta users dosyası oluşması için yoksa idkontorlda prog. kapanır.
            bool blExists = false;
            foreach (var vrLine in File.ReadLines("users.txt"))
            {
                if (vrLine.Split(':')[0] == kullanıcııd2)
                {
                    blExists = true;
                    break;
                }
            }
            return blExists;
        }
        public static bool girişşifrekontrol(string şifre)
        {
            File.AppendAllText("users.txt", null);//ilk kayıtta users dosyası oluşması için yoksa idkontorlda prog. kapanır.
            bool blExists = false;
            string saklanmışşifre = ŞifreSaklama.ComputeSha256Hash(şifre);
            foreach (var vrLine in File.ReadLines("users.txt"))
            {
                if (vrLine.Split(':')[1] == saklanmışşifre)
                {
                    blExists = true;
                    return blExists;
                }
            }
            return blExists;
        }
        public static bool girişidkontrol(string kullanıcııd2)
        {
            File.AppendAllText("users.txt", null);//ilk kayıtta users dosyası oluşması için yoksa idkontorlda prog. kapanır.
            bool blExists = false;
            foreach (var vrLine in File.ReadLines("users.txt"))
            {
                if (vrLine.Split(':')[0] == kullanıcııd2)
                {
                    blExists = true;
                    break;
                }
            }
            return blExists;
        }
        public static string normalizeUserName(string srUserNameofUser)
        {
            var vrnormal1 = srUserNameofUser.ToUpper(culture: System.Globalization.CultureInfo.CreateSpecificCulture("tr-TR")).ToLower(culture: System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            return RemoveDiacritics(vrnormal1);
        }
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public static void appendToLogFile(string Kullanıcıadı, string srText)
        {
            
            File.AppendAllText(Kullanıcıadı + ".txt", srText + "\r\n");
        }
        public static void readToListBox(MainWindow mm, string Kullanıcıadı)
        {
            if (!File.Exists(Kullanıcıadı + ".txt"))
                return;
            mm.lstsonişlem.Items.Clear();
            List<string> lstUserLogs = File.ReadAllLines(Kullanıcıadı + ".txt").ToList();
            lstUserLogs.Reverse();
            foreach (var item in lstUserLogs)
            {
                mm.lstsonişlem.Items.Add(item);
            }
        }
    }
}
