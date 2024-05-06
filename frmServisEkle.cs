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
    public partial class frmServisEkle : Form
    {
        public frmServisEkle()
        {
            InitializeComponent();
        }

        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        private void Temizle()
        {
            foreach (Control item in Controls)
                if (item is TextBox)
                    item.Text = "";
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand("AddServis", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ServisAd", txtServis.Text);
                command.Parameters.AddWithValue("@Fiyat", txtFiyat.Text);
                command.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Servis Eklendi");
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Servis Eklenemedi");
            }
        }

        
    }
}

