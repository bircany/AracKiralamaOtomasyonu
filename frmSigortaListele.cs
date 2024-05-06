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
    public partial class frmSigortaListele : Form
    {
        public frmSigortaListele()
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
            _rentaCar.delete("DELETE FROM Sigortalar WHERE SigortaID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Sigortalar", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Sigortalar set SirketAd='" + txtSirket.Text + "', SigortaAd='" + txtSigorta.Text + "', Fiyat='" + txtFiyat.Text + "' Where SigortaID='" + txtSigortaID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Sigortalar", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSigortaID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtSirket.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSigorta.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtFiyat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void txtSigortaIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Sigortalar WHERE SigortaID LIKE '%" + txtSigortaIDAra.Text + "%'", dataGridView1);

        }

        private void frmSigortaListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Sigortalar", dataGridView1);

        }
    }
}
