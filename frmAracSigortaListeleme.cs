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
    public partial class frmAracSigortaListeleme : Form
    {
        public frmAracSigortaListeleme()
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
            _rentaCar.delete("DELETE FROM AracSigortalar WHERE AracSigortaID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM AracSigortalar", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
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

            String query2 = "UPDATE AracSigortalar SET AracID=@AracID, SigortaID=@SigortaID, BaslangicTarihi=@BaslangicTarihi, BitisTarihi=@BitisTarihi WHERE AracSigortaID=@AracSigortaID";

            SqlCommand command = new SqlCommand(query2, connection);
            command.Parameters.AddWithValue("@AracID", Convert.ToInt32(comboAraclar.SelectedValue) + 1);
            command.Parameters.AddWithValue("@SigortaID", Convert.ToInt32(comboSigortalar.SelectedValue) + 1);
            command.Parameters.AddWithValue("@BaslangicTarihi", BeginDate.Value);
            command.Parameters.AddWithValue("@BitisTarihi", EndDate.Value);
            command.Parameters.AddWithValue("@AracSigortaID", txtAracSigortaID.Text);

            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from AracSigortalar", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAracSigortaID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboAraclar.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboSigortalar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            BeginDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            EndDate.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void txtAracSigortaIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM AracSigortalar WHERE AracSigortaID LIKE '%" + txtAracSigortaIDAra.Text + "%'", dataGridView1);
        }

        private void frmAracSigortaListeleme_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from AracSigortalar", dataGridView1);
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
    }
}
