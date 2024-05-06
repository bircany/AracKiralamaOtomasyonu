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
    public partial class frmMusteriListele : Form
    { 
        public frmMusteriListele()
        {
            InitializeComponent();
        }
        public void Temizle()
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
            _rentaCar.delete("DELETE FROM Musteriler WHERE MusteriID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Musteriler", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Musteriler set Ad='" + txtAd.Text + "', Soyad='" + txtSoyad.Text + "', TC='" + txtTC.Text + "', dogumTarihi='" + txtDbo.Text + "', licenceNo='" + txtLicenceNo.Text + "', TelNo='" + txtTel.Text + "', Email='" + txtMail.Text + "', Adres='" + txtAdres.Text + "' Where MusteriID='" + txtCustomerID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Musteriler", dataGridView1);
        }
        private void frmMusteriListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Musteriler", dataGridView1);
        }
        private void txtMusteriIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Musteriler WHERE MusteriID LIKE '%" + txtMusteriIDAra.Text + "%'", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTC.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtDbo.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtLicenceNo.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }
    }
}
