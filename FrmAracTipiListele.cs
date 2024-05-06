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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rentacar
{
    public partial class FrmAracTipiListele : Form
    {
        public FrmAracTipiListele()
        {
            InitializeComponent();
        }
        public void Temizle()
        {
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
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
            _rentaCar.delete("DELETE FROM AracTipleri WHERE AracTipiID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM AracTipleri", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update AracTipleri set AracTipi='" + txtAd.Text + "' Where AracTipiID='" + txtCarTypeID.Text + "'";  
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from AracTipleri", dataGridView1);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCarTypeID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        private void FrmAracTipiListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from AracTipleri", dataGridView1);
        }
        private void txtCarTypeIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM AracTipleri WHERE AracTipiID LIKE '%" + txtCarTypeIDAra.Text + "%'", dataGridView1);

        }
    }
}
