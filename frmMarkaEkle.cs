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
    public partial class frmMarkaEkle : Form
    {
        public frmMarkaEkle()
        {
            InitializeComponent();
        }

        RentCar _rentaCar = new RentCar();
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        public void Temizle()
        {
            foreach (Control item in Controls)
                if (item is TextBox)
                    item.Text = "";
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
                SqlCommand command = new SqlCommand("AddBrand", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MarkaAd", txtAd.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Marka Eklendi");
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Marka Eklenemedi");
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
