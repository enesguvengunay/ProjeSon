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

        public static bool şifreveıdkontrol(string kullanıcıadı, string şifre)
        {
            string saklanmışşifre = ŞifreSaklama.ComputeSha256Hash(şifre);
            bool varşifre = false;
            foreach (var vrline in File.ReadLines("users.txt"))
            {
                var vrLinesplt = vrline.Split(':');

                if (vrLinesplt[0] == kullanıcıadı)
                {

                    if (vrLinesplt[1] == saklanmışşifre)
                    {
                        varşifre = true;
                        break;
                    }

                }
            }
                return varşifre;
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
        public static void appendToLogFile(string srKullanıcıadı, string srText)
        {
            File.AppendAllText(srKullanıcıadı + ".txt", srText + "\r\n");
        }
        public static void readToListBox(MainWindow mm, string srUserName)
        {
            if (!File.Exists(srUserName + ".txt"))
                return;
            mm.lstsonişlem.Items.Clear();
            List<string> lstUserLogs = File.ReadAllLines(srUserName + ".txt").ToList();
            lstUserLogs.Reverse();
            foreach (var item in lstUserLogs)
            {
                mm.lstsonişlem.Items.Add(item);
            }
        }

    }
}
