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
    public partial class frmPersonelListele : Form
    {
        public frmPersonelListele()
        {
            InitializeComponent();
            FillComboBoxes();
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
            _rentaCar.delete("DELETE FROM Calisanlar WHERE CalisanID='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'");
            _rentaCar.getRecords("SELECT * FROM Calisanlar", dataGridView1);
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
           if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            String query = "Update Calisanlar set ad='" + txtAd.Text + "', soyad='" + txtSoyad.Text + "', Pozisyon='" + txtPozisyon.Text + "' , telNo='" + txtTel.Text + "', email='" + txtMail.Text + "', SubeID ='" +(int)comboBox1.SelectedValue + "' Where CalisanID='" + txtCalisanID.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            _rentaCar.getRecords("select * from Calisanlar", dataGridView1); _rentaCar.getRecords("select * from Calisanlar", dataGridView1);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCalisanID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPozisyon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
        private void frmPersonelListele_Load(object sender, EventArgs e)
        {
            _rentaCar.getRecords("Select * from Calisanlar", dataGridView1);            
        }
        private void txtPersonelIDAra_TextChanged(object sender, EventArgs e)
        {
            _rentaCar.getRecords("SELECT * FROM Calisanlar WHERE CalisanID LIKE '%" + txtPersonelIDAra.Text + "%'", dataGridView1);
        }

        private void FillComboBoxes()
        {
            try
            {                            
                string query = "SELECT SubeID, SubeAd FROM Subeler";
                SqlDataAdapter da3 = new SqlDataAdapter(query, connection);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "SubeAd";
                comboBox1.ValueMember = "SubeID";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
