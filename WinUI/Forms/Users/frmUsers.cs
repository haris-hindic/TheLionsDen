using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLionsDen.Model.SearchObjects;
using WinUI.Services;

namespace WinUI.Forms.Users
{
    public partial class frmUsers : Form
    {
        private UserAPI api;
        public frmUsers()
        {
            InitializeComponent();
            api = new UserAPI();
            dgvUsers.AutoGenerateColumns = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var request = new UserSearchObject
            {
                Name = txtName.Text,
                Username = txtUsername.Text,
                Active = cbActive.Checked,
                IncludeRoles = true
            };
            var response = await api.Get(request);

            dgvUsers.DataSource = response;
        }
    }
}
