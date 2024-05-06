using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rentacar
{
    public partial class frmOdemeler : Form
    {
        public frmOdemeler()
        {
            InitializeComponent();
        }

        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        public void Temizle()
        {
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                    control.Text = "";
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            _rentaCar.delete("DELETE FROM Odemeler WHERE OdemeID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Odemeler", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Odemeler set SozlesmeID='" + txtSozlesmeID.Text + "', MusteriID='" + txtMusteriID.Text + "', OdemeTarihi='" + OdemeTarih.Value + "' , OdemeMiktari='" + txtMiktar.Text + "', Aciklama='" + txtAciklama.Text + "' Where OdemeID='" + txtOdemeID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Odemeler", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtOdemeID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtSozlesmeID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtMusteriID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            string odemeTarihStr = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            DateTime odemeTarih;
            if (DateTime.TryParseExact(odemeTarihStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out odemeTarih))
            {
                OdemeTarih.Value = odemeTarih;
            }
            else
            {
                MessageBox.Show("Geçerli bir tarih formatı bekleniyor.");
            }
            txtMiktar.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
       
      
        private void frmOdemeler_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Odemeler", dataGridView1);

        }

        private void txtOdemeIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Odemeler WHERE OdemeID LIKE '%" + txtOdemeIDAra.Text + "%'", dataGridView1);

        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("AddOdeme", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SozlesmeID", txtSozlesmeID.Text);
                command.Parameters.AddWithValue("@MusteriID", txtMusteriID.Text);
                command.Parameters.AddWithValue("@OdemeTarihi",OdemeTarih.Value);
                command.Parameters.AddWithValue("@OdemeMiktar", txtMiktar.Text);
                command.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Ödeme Eklendi");
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Ödeme Eklenemedi");
            }
        }
    }
}

