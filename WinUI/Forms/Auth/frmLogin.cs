using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.Helpers;
using WinUI.Services;

namespace WinUI.Forms.Auth
{
    public partial class frmLogin : Form
    {
        private readonly UserAPI api;

        public frmLogin()
        {
            InitializeComponent();
            this.api = new UserAPI();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            AuthHelper.Username = txtUsername.Text;
            AuthHelper.Password = txtPassword.Text;

            try
            {
                var result = await api.Login();

                //AuthHelper.Roles = result.RoleNames;

                this.Hide();
                new MainForm().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong username or password!");
            }
        }
    }
}
