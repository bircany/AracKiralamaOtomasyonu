using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentacar
{
    public partial class frmMusteriEkle : Form
    {


        public frmMusteriEkle()
        {
            InitializeComponent();
        }
        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        public void Temizle()
        {
            foreach (Control item in Controls)
                if (item is TextBox)
                    item.Text = "";
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("AddCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ad", txtAd.Text);
                command.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                command.Parameters.AddWithValue("@TC", txtTC.Text);
                command.Parameters.AddWithValue("@DogumTarihi", dbo.Value);
                command.Parameters.AddWithValue("@LicenceNo", txtLicenceNo.Text);
                command.Parameters.AddWithValue("@TelNo", txtTel.Text);
                command.Parameters.AddWithValue("@Email", txtMail.Text);
                command.Parameters.AddWithValue("@Adres", txtAdres.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Müşteri Eklendi");
                

            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) 
                {
                    MessageBox.Show("Müşteri Eklenemedi: " + ex.Message);
                }
                else
                {
                    MessageBox.Show("Beklenmeyen bir hata oluştu: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Müşteri Eklenemedi");
            }

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnEhliyetKontrol_Click(object sender, EventArgs e)
        {

            try
            {
                string licenceNo = txtLicenceNo.Text;
                bool licenceValid = EhliyetKontrol(licenceNo);

                if (licenceValid)
                {
                    MessageBox.Show("Ehliyet kontrolü başarıyla tamamlandı.");
                }
                else
                {
                    MessageBox.Show("Ehliyet geçerliliği sağlanamadı.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                MessageBox.Show("Bir hata oluştu.");
            }

        }
        private bool EhliyetKontrol(string licenceNo)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False"))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Musteriler WHERE LicenceNo = @LicenceNo";
                    cmd.Parameters.AddWithValue("@LicenceNo", licenceNo);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}
