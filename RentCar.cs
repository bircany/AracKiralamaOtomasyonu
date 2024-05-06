using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentacar
{
    class RentCar
    {
        SqlConnection connection = new SqlConnection("Data Source=BIRCAN\\SQLEXPRESS;Initial Catalog=Rentacar;Integrated Security=True;Encrypt=False");
        DataTable table = new DataTable();
      
        
        public void delete(String query)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Kayıt Silindi");
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show("Kayıt Silinemedi");
                }
            }
            
        }
        public void getRecords(String query,DataGridView dataGridView)
        {
            table.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(table);
            dataGridView.DataSource = table;
        }
       public void getIdleVehicles(ComboBox combobox,String query)
       {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                combobox.Items.Add(reader["AracID"]);
            }
            connection.Close();
       }
        public void FillComboBox(ComboBox comboBox, string query)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox.DataSource = dt;
                comboBox.DisplayMember = dt.Columns[1].ColumnName; 
                comboBox.ValueMember = dt.Columns[0].ColumnName; 

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ComboBox doldurulurken hata oluştu: " + ex.Message);
            }
        }
    }
}

