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
    public partial class ClientForm : Form
    {
        private int _userId;

        public ClientForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Добро пожаловать, клиент (ID: {_userId})!";
        }
    }
}