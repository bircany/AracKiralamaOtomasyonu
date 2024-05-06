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
    public partial class frmSubeListele : Form
    {
        public frmSubeListele()
        {
            InitializeComponent();
        }
        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            _rentaCar.delete("DELETE FROM Subeler WHERE SubeID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Subeler", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Subeler set Subead='" + txtAd.Text + "', tel='" + txtTel.Text + "', adres='" + txtAdres.Text + "' Where SubeID='" + txtBranchID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Subeler", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBranchID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
        private void frmSubeListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Subeler", dataGridView1);
        }
        private void txtBranchIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Subeler WHERE SubeID LIKE '%" + txtBranchIDAra.Text + "%'", dataGridView1);
        }
    }
}
