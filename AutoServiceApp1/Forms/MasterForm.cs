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

namespace AutoServiceApp1
{
    public partial class MasterForm : Form
    {
        private int masterId;
        private string connectionString = "Server=star6sql2;Database=master;User Id=user64;Password=94696;";

        public MasterForm(int id)
        {
            InitializeComponent();
            masterId = id;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT a.id, c.FullName, s.ServiceName, a.AppointmentDate, a.Status
                                 FROM appointments a
                                 JOIN clients c ON a.ClientId = c.id
                                 JOIN services s ON a.ServiceId = s.id
                                 WHERE a.MasterId = @MasterId";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@MasterId", masterId);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridViewAppointments.DataSource = table;
            }
        }

        private void btnMarkCompleted_Click(object sender, EventArgs e)
        {
            if (dataGridViewAppointments.CurrentRow != null)
            {
                int appointmentId = Convert.ToInt32(dataGridViewAppointments.CurrentRow.Cells["id"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE appointments SET Status = 'Завершено' WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", appointmentId);
                    cmd.ExecuteNonQuery();
                }

                LoadAppointments();
                MessageBox.Show("Работа отмечена как завершенная.");
            }
        }
    }
}