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
    public partial class frmAracServisEkleme : Form
    {
        public frmAracServisEkleme()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        private void Temizle()
        {
            foreach (Control item in Controls)
                if (item is TextBox)
                    item.Text = "";
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
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
                if (comboAraclar.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen bir araç seçin.");
                    return;
                }

                if (comboSigortalar.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen bir sigorta seçin.");
                    return;
                }

                string selectedCarName = comboAraclar.SelectedItem.ToString();
                string query = "SELECT AracID FROM Araclar WHERE AracAd = @AracAd";
                SqlCommand command2 = new SqlCommand(query, connection);
                command2.Parameters.AddWithValue("@AracAd", selectedCarName);
                int aracID = (int)command2.ExecuteScalar();

                string selectedServiceName = comboSigortalar.SelectedItem.ToString();
                string query2 = "SELECT ServisID FROM Servisler WHERE ServisAd = @ServisAd"; 
                SqlCommand command3 = new SqlCommand(query2, connection); 
                command3.Parameters.AddWithValue("@ServisAd", selectedServiceName);
                int servisID = (int)command3.ExecuteScalar();

                SqlCommand command = new SqlCommand("AddAracServisi", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AracID",aracID );
                command.Parameters.AddWithValue("@ServisID",servisID );
                command.Parameters.AddWithValue("@BaslangicTarihi", BeginDate.Value);
                command.Parameters.AddWithValue("@BitisTarihi", EndDate.Value);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Servis Eklendi");
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Servis Eklenemedi");
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

                string aracQuery = "SELECT AracID, AracAd FROM Araclar";
                string servisQuery = "SELECT ServisID, ServisAd FROM Servisler";

                SqlCommand aracCommand = new SqlCommand(aracQuery, connection);
                SqlCommand servisCommand = new SqlCommand(servisQuery, connection);

                SqlDataReader aracReader = aracCommand.ExecuteReader();
                comboAraclar.ValueMember = "AracID";
                comboAraclar.DisplayMember = "AracAd";
                while (aracReader.Read())
                {
                    comboAraclar.Items.Add(aracReader["AracAd"].ToString());
                }
                aracReader.Close();

                SqlDataReader servisReader = servisCommand.ExecuteReader();
                comboSigortalar.ValueMember = "SigortaID";
                comboSigortalar.DisplayMember = "ServisAd";
                while (servisReader.Read())
                {
                    comboSigortalar.Items.Add(servisReader["ServisAd"].ToString());
                }
                servisReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}

