using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
   
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_odsz(object sender, EventArgs e)
       
        {
            try
            {
                if (textBox3.Text.Length < 8)
                {
                    MessageBox.Show("Klucz musi się składać z 8 znaków");

                    textBox3.Focus(); //przeniesienie kursora na klucz
                    return;
                }


                string password = textBox3.Text;
                string szyfrText = textBox1.Text;
                byte[] szyfrBytes = Convert.FromBase64String(szyfrText); //Konwertuje tekst z textBox1, gdzie koduje dane binarne jako cyfry Base64, do równoważnej 8-bitowej nieoznaczonej liczby całkowitej

                DESCryptoServiceProvider Alg = new DESCryptoServiceProvider();

                //  8 bitowy  tajny klucz DES
                //GetBytes  - koduje znaki w bajty
                Alg.Key = ASCIIEncoding.ASCII.GetBytes(password); //ASCII - kodowanie znaków Unicode aby nie stracic danych które występują poza zakresem ASCII
                Alg.IV = ASCIIEncoding.ASCII.GetBytes(password);  //wektor inicjalizacji ( IV )


                MemoryStream txtDeciphered = new MemoryStream(); // Przechowywanie odszyfrowanych danych

                CryptoStream txtCrypto = new CryptoStream(txtDeciphered, Alg.CreateDecryptor(), CryptoStreamMode.Write);  //łączenie strumieni danych z transformacjami kryptograficznymi

                txtCrypto.Write(szyfrBytes, 0, szyfrBytes.Length); //przesuwanie bieżącą pozycję w strumieniu o liczbę zapisanych bajtówi zapisuje sekwencje bajtów do obecnego CryptoStream
                txtCrypto.Close();
                textBox2.Text = Encoding.UTF8.GetString(txtDeciphered.ToArray()); //dekodowanie sekwencji bajtów na format ciągu UTF8 i zapisanie zawartości strumienia w tablicy bajtów

                txtDeciphered.Close();

             
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niepoprawny klucz zabezpieczający !" + ex.Message); // wyświetlenie informacji o niepoprawnym wprowadzeniu hasła do odkodowania

            }

        }

        private void btn_szyf_Click(object sender, EventArgs e)
        {

            if (textBox3.Text.Length < 8)
            {
                MessageBox.Show("Klucz musi się składać z 8 znaków");

                textBox3.Focus(); //przeniesienie kursora na klucz
                return;
            }

            try

            {
                string password = textBox3.Text;
                string plainText = textBox1.Text;
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

              
                DESCryptoServiceProvider Alg = new DESCryptoServiceProvider();

                //  8 bitowy  tajny klucz DES
                Alg.Key = ASCIIEncoding.ASCII.GetBytes(password);
                Alg.IV = ASCIIEncoding.ASCII.GetBytes(password);

                MemoryStream strCiphered = new MemoryStream(); //To Store Encrypted Data

                CryptoStream strCrypto = new CryptoStream(strCiphered,
                    Alg.CreateEncryptor(), CryptoStreamMode.Write);

                strCrypto.Write(plainBytes, 0, plainBytes.Length);
                strCrypto.Close();
                textBox2.Text = Convert.ToBase64String(strCiphered.ToArray());

                strCiphered.Close();
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niepoprawny klucz zabezpieczający !" + ex.Message);

            }
        }

        private void key_txt(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
