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
    public partial class frmAracEkle : Form
    {      
        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        public frmAracEkle()
        {
            InitializeComponent();
            FillComboBoxes();
        }
        private void Temizle()
        {
            foreach (Control item in Controls)
                if (item is TextBox)
                    item.Text = "";
        }
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("AddCar", connection);



                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AracTipiID", (int)(comboBox1.SelectedValue));
                command.Parameters.AddWithValue("@MarkaID", (int)(comboBox2.SelectedValue));
                command.Parameters.AddWithValue("@SubeID", (int)(comboBox3.SelectedValue));
                command.Parameters.AddWithValue("@AracAdı", comboBox4.Text);
                command.Parameters.AddWithValue("@AracRenk", txtCarColor.Text);
                command.Parameters.AddWithValue("@Km", txtKm.Text);
                command.Parameters.AddWithValue("@PlakaNo", txtPlateNo.Text);
                command.Parameters.AddWithValue("@Yılı", txtProductYear.Text);
                command.Parameters.AddWithValue("@YakıtTipi", comboBox5.Text);
                command.Parameters.AddWithValue("@VitesTipi", comboBox6.Text);
                command.Parameters.AddWithValue("@kiraUcreti", int.Parse(txtDailyRentalFee.Text));
                command.Parameters.AddWithValue("@Durumu", txtDurum.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Araç Eklendi");

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Araç Eklenemedi");
            }
        }
        private void FillComboBoxes()
        {
            try
            {
                _rentaCar.FillComboBox(comboBox1, "SELECT AracTipiID, AracTipi FROM AracTipleri");
                _rentaCar.FillComboBox(comboBox2, "SELECT MarkaID, MarkaAdı FROM Markalar");
                _rentaCar.FillComboBox(comboBox3, "SELECT SubeID, SubeAd FROM Subeler");

                comboBox5.Items.AddRange(new string[] { "Benzin", "Dizel", "LPG" });
                comboBox6.Items.AddRange(new string[] { "Manuel", "Otomatik" });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox4.Items.Clear();
                switch (comboBox2.SelectedIndex)
                {
                    case 0: // Opel
                        comboBox4.Items.AddRange(new string[] { "Astra", "Vectra", "Corsa" });
                        break;
                    case 1: // Renault
                        comboBox4.Items.AddRange(new string[] { "Clio", "Megane", "Symbol" });
                        break;
                    case 2: // Fiat
                        comboBox4.Items.AddRange(new string[] { "Fiesta", "Focus", "Mondeo" });
                        break;
                    case 3: // Ford
                        comboBox4.Items.AddRange(new string[] { "Linea", "Egea", "Doblo" });
                        break;
                    case 4: // Toyota
                        comboBox4.Items.AddRange(new string[] { "Corolla", "Yaris", "Auris" });
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}

