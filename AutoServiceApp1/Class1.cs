using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Dapper; // Подключаем Dapper

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

                    // SQL-запрос с использованием параметров
                    string query = @"
                        SELECT id, FullName, 'мастер' AS Role FROM masters 
                        WHERE Email = @Email AND Passwords = @Password
                        UNION ALL
                        SELECT id, FullName, 'клиент' AS Role FROM clients 
                        WHERE Email = @Email AND Passwords = @Password";

                    // Выполняем запрос с помощью Dapper
                    var user = conn.QueryFirstOrDefault(query, new { Email = email, Password = password });

                    if (user != null)
                    {
                        // Если пользователь найден, отображаем сообщение и открываем соответствующую форму
                        MessageBox.Show($"Добро пожаловать, {user.FullName}!", "Успешная авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();

                        if (user.Role == "мастер")
                        {
                            MasterForm masterForm = new MasterForm(user.id);
                            masterForm.ShowDialog();
                        }
                        else
                        {
                            ClientForm clientForm = new ClientForm(user.id);
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
