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

        public static bool şifreveıdkontrol(string şifre, string kullanıcıadı)
        {
            bool varşifre = false;
            foreach (var vrline in File.ReadLines("users.txt"))
            {
                var vrLinesplt = vrline.Split(':');

                if (vrLinesplt[0] == kullanıcıadı)
                {

                    if (vrLinesplt[1] == şifre)
                    {
                        varşifre = true;
                        break;
                    }

                }

                if (vrLinesplt[0] != kullanıcıadı)
                {
                    MessageBox.Show("Kullanıcı Adı Bulunamadı");
                    break;
                }
                if (vrLinesplt[1] != şifre)
                {
                    MessageBox.Show("Şifre Yanlış");
                    break; 
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
    }
    }
