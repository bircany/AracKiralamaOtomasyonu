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
    public partial class frmAracServisListeleme : Form
    {
        public frmAracServisListeleme()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void Temizle()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = "";
            }
        }
        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            _rentaCar.delete("DELETE FROM AracServisler WHERE AracServisID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM AracServisler", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
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

                string selectedServiceName = comboSigortalar.SelectedItem.ToString();
                string query2 = "SELECT ServisID FROM Servisler WHERE ServisAd = @ServisAd";
                SqlCommand command3 = new SqlCommand(query2, connection);
                command3.Parameters.AddWithValue("@ServisAd", selectedServiceName);
                int servisID = (int)command3.ExecuteScalar();

                String query3 = "UPDATE AracServisler SET AracID=@AracID, ServisID=@ServisID, BaslangicTarihi=@BaslangicTarihi, BitisTarihi=@BitisTarihi WHERE AracServisID=@AracServisID";

                SqlCommand command = new SqlCommand(query3, connection);
                command.Parameters.AddWithValue("@AracID", aracID);
                command.Parameters.AddWithValue("@ServisID", servisID);
                command.Parameters.AddWithValue("@BaslangicTarihi", BeginDate.Value);
                command.Parameters.AddWithValue("@BitisTarihi", EndDate.Value);
                command.Parameters.AddWithValue("@AracServisID", txtAracServisID.Text);

                command.ExecuteNonQuery();
                _rentaCar.getRecords("SELECT * FROM AracServisler", dataGridView1);
                connection.Close();
                MessageBox.Show("Güncelleme başarıyla tamamlandı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message);
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAracServisID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboAraclar.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboSigortalar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            BeginDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            EndDate.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
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
                string sigortaQuery = "SELECT SigortaID, SigortaAd FROM Sigortalar";

                SqlCommand aracCommand = new SqlCommand(aracQuery, connection);
                SqlCommand sigortaCommand = new SqlCommand(sigortaQuery, connection);

                SqlDataReader aracReader = aracCommand.ExecuteReader();
                comboAraclar.ValueMember = "AracID";
                while (aracReader.Read())
                {
                    comboAraclar.Items.Add(aracReader["AracAd"].ToString());
                }
                aracReader.Close();

                SqlDataReader sigortaReader = sigortaCommand.ExecuteReader();
                comboSigortalar.ValueMember = "SigortaID";
                while (sigortaReader.Read())
                {
                    comboSigortalar.Items.Add(sigortaReader["SigortaAd"].ToString());
                }
                sigortaReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void txtAracServisIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM AracServisler WHERE AracServisID LIKE '%" + txtAracServisIDAra.Text + "%'", dataGridView1);

        }

        private void frmAracServisListeleme_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from AracServisler", dataGridView1);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
