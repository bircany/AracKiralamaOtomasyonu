using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Rentacar
{
    public partial class frmSozlesme : Form
    {
        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");

        public frmSozlesme()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void frmSozlesme_Load(object sender, EventArgs e)
        {
            String query = "SELECT * FROM Araclar Where durumu = 'BOŞ'";
            _rentaCar.getIdleVehicles(comboAraclar, query);
            String query2 = "SELECT * FROM Sozlesmeler";
            _rentaCar.getRecords(query2, dataGridView1);

        }

        private void txtMusteriID_Changed(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand("GetMusteriLisans", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MusteriID", txtMusteriID.Text);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtMusteriID.Text = reader["MusteriID"].ToString();
                    txtTC.Text = reader["TC"].ToString();
                    txtAd.Text = reader["Ad"].ToString();
                    txtSoyad.Text = reader["Soyad"].ToString();
                    txtDbo.Text = reader["DogumTarihi"].ToString();
                    txtTel.Text = reader["TelNo"].ToString();
                    txtAdres.Text = reader["Adres"].ToString();
                    txtLicenceNo.Text = reader["LicenceNo"].ToString();
                    txtETarih.Text = reader["ETarih"].ToString();
                    txtEYer.Text = reader["EYer"].ToString();
                }
                else
                {
                    MessageBox.Show("Müşteri bilgileri bulunamadı");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgu hatası: " + ex.Message);
            }
        }


        private void btnSozlesmeEkle_Click(object sender, EventArgs e)
        {

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string selectedCarName = comboAraclar.SelectedItem.ToString();
                string query = "SELECT AracID FROM Araclar WHERE AracAd = @AracAd";
                SqlCommand command2 = new SqlCommand(query, connection);
                command2.Parameters.AddWithValue("@AracAd", selectedCarName);
                int aracID = (int)command2.ExecuteScalar();

                SqlCommand command = new SqlCommand("AddSozlesme", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AracID", aracID);
                command.Parameters.AddWithValue("@MusteriID", txtMusteriID.Text);
                command.Parameters.AddWithValue("@BaslangicTarihi", rentBeginDate.Value);
                command.Parameters.AddWithValue("@BitisTarihi", rentEndDate.Value);
                command.Parameters.AddWithValue("@KiraSekli", comboBox7.SelectedItem.ToString());
                command.Parameters.AddWithValue("@TotalUcret", txtTotal.Text);
                command.ExecuteNonQuery();

                connection.Close();
                MessageBox.Show("Sozlesme Eklendi");  //tetikleyici calıstı araıcn durumu guncellendi. trgUpdateAracDurumu under the sozlesmeler
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Sozlesme Eklenemedi");
            }
        }



        private void EhliyetKontrol_Click(object sender, EventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand("CheckLicenceAndAgeForRent", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@LicenceNo", txtLicenceNo.Text);
            command.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime(txtDbo.Text)); //2002
            command.Parameters.AddWithValue("@MusteriID", Convert.ToInt32(txtMusteriID.Text));
            try
            {
                string result = (string)command.ExecuteScalar();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgu hatası: " + ex.Message);
            }
        }

        private void comboAraclar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            try
            {
                string query = "SELECT * FROM Araclar WHERE AracAd = @AracAd";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AracAd", comboAraclar.SelectedItem.ToString());

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    txtAracTipi.Text = reader["AracTipiID"].ToString();
                    txtMarka.Text = reader["MarkaID"].ToString();
                    txtSube.Text = reader["SubeID"].ToString();
                    txtAracAdı.Text = reader["AracAd"].ToString();
                    txtRenk.Text = reader["AracRenk"].ToString();
                    txtKm.Text = reader["Km"].ToString();
                    txtYakitTipi.Text = reader["YakıtTuru"].ToString();
                    txtVitesTipi.Text = reader["VitesTuru"].ToString();
                    txtPlaka.Text = reader["PlakaNo"].ToString();
                    txtYıl.Text = reader["uretimyili"].ToString();
                    txtkiraUcreti.Text = reader["kiraUcreti"].ToString();

                }
                else
                {
                    MessageBox.Show("Müşteri bilgileri bulunamadı");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgu hatası: " + ex.Message);
            }
        }


        private void FillComboBoxes()
        {

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                string query = "SELECT AracID, AracAd FROM Araclar";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                comboAraclar.ValueMember = "AracID";
                while (reader.Read())
                {
                    comboAraclar.Items.Add(reader["AracAd"].ToString());
                }
                reader.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }

        private void btnAracKiralamaGecmisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                if (comboAraclar.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir araç seçiniz");
                    return;
                }
                SqlCommand command = new SqlCommand("GetCarRentHistory", connection);
                command.CommandType = CommandType.StoredProcedure;

                string selectedCarName = comboAraclar.SelectedItem.ToString();
                string query = "SELECT AracID FROM Araclar WHERE AracAd = @AracAd";
                SqlCommand command2 = new SqlCommand(query, connection);
                command2.Parameters.AddWithValue("@AracAd", selectedCarName);
                int aracID = (int)command2.ExecuteScalar();
                command.Parameters.AddWithValue("@AracID", aracID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Seçilen araca ait kiralama geçmişi bulunamadı");
                    return;
                }

                StringBuilder message = new StringBuilder();
                message.AppendLine("Kiralama Geçmişi:\n");
                foreach (DataRow row in dataTable.Rows)
                {
                    message.AppendLine($"Sözleşme ID: {row["SozlesmeID"]}");
                    message.AppendLine($"Başlangıç Tarihi: {row["BaslangicTarihi"]}");
                    message.AppendLine($"Bitiş Tarihi: {row["BitisTarihi"]}");
                    message.AppendLine($"Kira Şekli: {row["KiraSekli"]}");
                    message.AppendLine($"Toplam Ücret: {row["TotalUcret"]}");
                    message.AppendLine($"Arac Adı: {row["AracAd"]}");
                    message.AppendLine($"Arac Renk: {row["AracRenk"]}");
                    message.AppendLine($"Km: {row["Km"]}");
                    message.AppendLine($"Yakıt Türü: {row["YakıtTuru"]}");
                    message.AppendLine($"Vites Türü: {row["VitesTuru"]}");
                    message.AppendLine($"Plaka No: {row["PlakaNo"]}");
                    message.AppendLine($"Üretim Yılı: {row["uretimyili"]}");
                    message.AppendLine($"Müşteri Adı: {row["Ad"]} {row["Soyad"]}");
                    message.AppendLine($"TC: {row["TC"]}");
                    message.AppendLine($"Doğum Tarihi: {row["DogumTarihi"]}");
                    message.AppendLine($"Ehliyet No: {row["LicenceNo"]}");
                    message.AppendLine($"Tel No: {row["TelNo"]}");
                    message.AppendLine($"Adres: {row["Adres"]}\n");
                }

                MessageBox.Show(message.ToString(), "Kiralama Geçmişi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnMusteriKiralamaGecmisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                if (string.IsNullOrEmpty(txtMusteriID.Text))
                {
                    MessageBox.Show("Lütfen bir müşteri ID'si giriniz");
                    return;
                }

                SqlCommand command = new SqlCommand("GetCustomerRentHistory", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MusteriID", Convert.ToInt32(txtMusteriID.Text));

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Seçilen müşteriye ait kiralama geçmişi bulunamadı");
                    return;
                }

                StringBuilder message = new StringBuilder();
                message.AppendLine("Kiralama Geçmişi:\n");
                foreach (DataRow row in dataTable.Rows)
                {
                    message.AppendLine($"Sözleşme ID: {row["SozlesmeID"]}");
                    message.AppendLine($"Başlangıç Tarihi: {row["BaslangicTarihi"]}");
                    message.AppendLine($"Bitiş Tarihi: {row["BitisTarihi"]}");
                    message.AppendLine($"Kira Şekli: {row["KiraSekli"]}");
                    message.AppendLine($"Toplam Ücret: {row["TotalUcret"]}");
                    message.AppendLine($"Arac Adı: {row["AracAd"]}");
                    message.AppendLine($"Arac Renk: {row["AracRenk"]}");
                    message.AppendLine($"Km: {row["Km"]}");
                    message.AppendLine($"Yakıt Türü: {row["YakıtTuru"]}");
                    message.AppendLine($"Vites Türü: {row["VitesTuru"]}");
                    message.AppendLine($"Plaka No: {row["PlakaNo"]}");
                    message.AppendLine($"Üretim Yılı: {row["uretimyili"]}");
                    message.AppendLine($"Müşteri Adı: {row["Ad"]} {row["Soyad"]}");
                    message.AppendLine($"TC: {row["TC"]}");
                    message.AppendLine($"Doğum Tarihi: {row["DogumTarihi"]}");
                    message.AppendLine($"Ehliyet No: {row["LicenceNo"]}");
                    message.AppendLine($"Tel No: {row["TelNo"]}");
                    message.AppendLine($"Adres: {row["Adres"]}\n");
                }

                MessageBox.Show(message.ToString(), "Kiralama Geçmişi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private decimal CalculateTotalUcret(DateTime kiraBaslangic, DateTime kiraBitis, string kiraSekli, int kiraUcreti)
        {
            decimal totalUcret = 0;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("SELECT dbo.CalculateTotalUcret(@kiraBaslangic, @kiraBitis, @kiraSekli, @kiraUcreti)", connection);
                command.Parameters.AddWithValue("@kiraBaslangic", kiraBaslangic);
                command.Parameters.AddWithValue("@kiraBitis", kiraBitis);
                command.Parameters.AddWithValue("@kiraSekli", kiraSekli);
                command.Parameters.AddWithValue("@kiraUcreti", kiraUcreti);
                totalUcret = (decimal)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            return totalUcret;
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            DateTime kiraBaslangic = rentBeginDate.Value;
            DateTime kiraBitis = rentEndDate.Value;
            string kiraSekli = comboBox7.SelectedItem.ToString();
            int kiraUcreti = Convert.ToInt32(txtkiraUcreti.Text);

            decimal totalUcret = CalculateTotalUcret(kiraBaslangic, kiraBitis, kiraSekli, kiraUcreti);
            txtGun.Text = ((int)(kiraBitis - kiraBaslangic).TotalDays).ToString();
            txtTotal.Text = totalUcret.ToString();
        }

        private void btnSozlesmeleriListele_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand command = new SqlCommand("ListSozlesmeler", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnSozlesmeGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            String query = "UPDATE Sozlesmeler SET BaslangicTarihi = @BaslangicTarihi, BitisTarihi = @BitisTarihi, KiraSekli = @KiraSekli, TotalUcret = @TotalUcret WHERE SozlesmeID = @SozlesmeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BaslangicTarihi", rentBeginDate.Value);
            command.Parameters.AddWithValue("@BitisTarihi", rentEndDate.Value);
            command.Parameters.AddWithValue("@KiraSekli", comboBox7.SelectedItem.ToString());
            command.Parameters.AddWithValue("@TotalUcret", Convert.ToDecimal(txtTotal.Text));
            command.Parameters.AddWithValue("@SozlesmeID", txtSozlesmeID.Text);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Sozlesmeler", dataGridView1);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAracKontrol_Click(object sender, EventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            
            try
            {
                SqlCommand command = new SqlCommand("transactionAracDurumKontrolleri", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AracID", Convert.ToInt32(comboAraclar.SelectedValue));
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    string sigortaDurumu = reader["SigortaDurumu"].ToString();
                    string servisDurumu = reader["ServisDurumu"].ToString();
                    string kiralamaDurumu = reader["KiralamaDurumu"].ToString();
                    if (sigortaDurumu == "Aracın Aktif Sigortası Mevcut" && servisDurumu == "Aracın Bakımı Yapılmış" && kiralamaDurumu == "Kiralanabilir")
                        MessageBox.Show("Araç kiralama işlemi gerçekleştirilebilir.");
                    else
                        MessageBox.Show("Araç kiralama işlemi gerçekleştirilemez.");
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgu hatası: " + ex.Message);
            }

        }

        private void btnOdemeYap_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {

                MessageBox.Show("Ödeme başarılı şekilde gerçekleşti");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme yapılamadı: " + ex.Message);
            }
        
          

               



        }

       
    }
}
