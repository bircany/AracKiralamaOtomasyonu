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
    public partial class frmMarkaListele : Form
    {
        public frmMarkaListele()
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

            _rentaCar.delete("DELETE FROM Markalar WHERE MarkaID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Markalar", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Markalar set MarkaAdı='" + txtAd.Text + "' Where MarkaID='" + txtBrandID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Markalar", dataGridView1);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBrandID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void frmMarkaListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Markalar", dataGridView1);
        }

        private void txtBrandIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Markalar WHERE MarkaID LIKE '%" + txtBrandIDAra.Text + "%'", dataGridView1);

        }
    }
}
