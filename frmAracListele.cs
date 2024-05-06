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
    public partial class frmAracListele : Form
    {
        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        public frmAracListele()
        {
            InitializeComponent();
            FillComboBoxes();

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            _rentaCar.delete("DELETE FROM Araclar WHERE AracID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Araclar", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            String query = "Update Araclar set arac tipi='" + (int)(comboBox1.SelectedValue) + "', marka='" + (int)(comboBox2.SelectedValue) + "', Şube='" + (int)(comboBox3.SelectedValue) + "', arac ad='" + (int)(comboBox4.SelectedValue)
                + "', renk='" + txtCarColor.Text + "', km'" + txtKm.Text + "', plaka no='" + txtPlateNo.Text + "', yılı='" + txtProductYear.Text + "',durumu='"
                + "', YakıtTipi='" + (int)(comboBox5.SelectedValue) + "', VitesTipi ='" + (int)(comboBox6.SelectedValue) + "', kira ucreti = '" + txtDailyRentalFee.Text + "' Where CarID='" + txtCarID.Text + "'";

            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Araclar", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCarID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtCarColor.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKm.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtPlateNo.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtProductYear.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBox5.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            comboBox6.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            txtDailyRentalFee.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            txtDurum.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }
        private void frmAracListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Araclar", dataGridView1);
        }
        private void txtCarIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Araclar WHERE AracID LIKE '%" + txtCarIDAra.Text + "%'", dataGridView1);
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
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }
    }
}
