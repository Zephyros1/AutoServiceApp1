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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Data Source=star6sql2;Initial Catalog=master;User ID=user64;Password=94696";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT id, FullName, 'мастер' AS Role FROM masters 
                        WHERE Email = @Email AND Passwords = @Password
                        UNION ALL
                        SELECT id, FullName, 'клиент' AS Role FROM clients 
                        WHERE Email = @Email AND Passwords = @Password";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["id"]);
                        string fullName = reader["FullName"].ToString();
                        string role = reader["Role"].ToString();

                        MessageBox.Show($"Добро пожаловать, {fullName}!", "Успешная авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();

                        if (role == "мастер")
                        {
                            MasterForm masterForm = new MasterForm(userId);
                            masterForm.ShowDialog();
                        }
                        else
                        {
                            ClientForm clientForm = new ClientForm(userId);
                            clientForm.ShowDialog();
                        }

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
                }
            }
        }
    }
}