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
    public partial class frmServisListele : Form
    {
        public frmServisListele()
        {
            InitializeComponent();
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
            _rentaCar.delete("DELETE FROM Servisler WHERE ServisID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Servisler", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Servisler set ServisAd='" + txtServis.Text + "', Aciklama='" + txtAciklama.Text + "', Fiyat='" + txtFiyat.Text + "' Where ServisID='" + txtSigortaID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Servisler", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSigortaID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtServis.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtFiyat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

       

       
        private void txtServisIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Servisler WHERE ServisID LIKE '%" + txtServisIDAra.Text + "%'", dataGridView1);

        }

        private void frmServisListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Servisler", dataGridView1);

        }
    }
}
