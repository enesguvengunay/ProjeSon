using System;
using System.Collections.Generic;
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

namespace VİZE_VE_FİNAL_PROJE
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        double FirstNumber;
        double SecondNumber;
        string Operation;
        
       
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
           
            if (textbox1.Text=="0"&& textbox1.Text == null)
            {

                textbox1.Text = "1";
            }
            else
            {
                textbox1.Text = textbox1.Text + "1";
            }         
        }

        private void btniki_Click(object sender, RoutedEventArgs e)
        {
           
            
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "2";
            }
            else
            {
                textbox1.Text = textbox1.Text + "2";
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "3";
            }
            else
            {
                textbox1.Text = textbox1.Text + "3";
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            
           
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "4";
            }
            else
            {
                textbox1.Text = textbox1.Text + "4";
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "5";
            }
            else
            {
                textbox1.Text = textbox1.Text + "5";
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
           
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "6";
            }
            else
            {
                textbox1.Text = textbox1.Text + "6";
            }
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "7";
            }
            else
            {
                textbox1.Text = textbox1.Text + "7";
            }
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
           
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "8";
            }
            else
            {
                textbox1.Text = textbox1.Text + "8";
            }
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "9";
            }
            else
            {
                textbox1.Text = textbox1.Text + "9";
            }
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {

            
            if (textbox1.Text == "0" && textbox1.Text == null)
            {
                textbox1.Text = "0";
            }
            else
            {
                textbox1.Text = textbox1.Text + "0";
            }
        }

        private void btnvirgül_Click(object sender, RoutedEventArgs e)
        {
            
            textbox1.Text = textbox1.Text + ",";
        }

        private void btneksi_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            FirstNumber = Convert.ToDouble(textbox1.Text);

            textbox1.Text = null;

            Operation = "-";

            }
            catch (Exception)
            {
                MessageBox.Show("Hata");
                
            }
        }

        private void btntopla_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            FirstNumber = Convert.ToDouble(textbox1.Text);
            

            textbox1.Text = null;

            Operation = "+";
            }
            catch (Exception)
            {
                MessageBox.Show("HATA");
                
            }
        }

        private void btnçarpma_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            FirstNumber = Convert.ToDouble(textbox1.Text);

            textbox1.Text = null;

            Operation = "*";
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen 2 Defa çarpmaya basmayın");
                
            }
        }

        private void btnbölme_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            FirstNumber = Convert.ToDouble(textbox1.Text);

            textbox1.Text = null;

            Operation = "/";
            }
            catch (Exception)
            {
                MessageBox.Show("HATA");
                
            }
        }

        private void btneşit_Click(object sender, RoutedEventArgs e)
        {
            
            double Result;
            string Result2;
            

            try
            {
                SecondNumber = Convert.ToDouble(textbox1.Text);

                if (Operation=="+")
            {
                
                Result = (FirstNumber + SecondNumber);
                RESULTBOX.Text = Result.ToString("N3");
                Result2 = $"{FirstNumber} + {SecondNumber}= {Result}";
                resultlistbox.Items.Insert(0, Result2);
            }
                if (Operation == "-")
            {
                Result = (FirstNumber - SecondNumber);
                RESULTBOX.Text = Result.ToString("N3");
                Result2 = $"{FirstNumber} - {SecondNumber}= {Result}";
                resultlistbox.Items.Insert(0, Result2);
            }
                if (Operation == "*")
            {
                Result = (FirstNumber * SecondNumber);
                RESULTBOX.Text = Result.ToString("N3");
                Result2 = $"{FirstNumber} * {SecondNumber}= {Result}";
                resultlistbox.Items.Insert(0, Result2);
            }
                if (Operation == "/")
            {
                if (SecondNumber==0)
                {
                    MessageBox.Show("SAYI SIFIRA BÖLÜNMEZ");
                }
                else
                {
                    Result = (FirstNumber / SecondNumber);
                    RESULTBOX.Text = Result.ToString("N3");
                    Result2 = $"{FirstNumber} / {SecondNumber}= {Result}";
                    resultlistbox.Items.Insert(0, Result2);
                }            
            }
            }
            catch (Exception)
            {
                MessageBox.Show("HATA");
                
            }
        }
        
        private void btnsilme_Click(object sender, RoutedEventArgs e)
        {
            textbox1.Text = null;
        }

        
    }
}
