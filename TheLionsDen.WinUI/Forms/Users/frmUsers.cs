using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Users
{
    public partial class frmUsers : Form
    {
        private UserAPI api;
        private RoleAPI roleAPI;
        public frmUsers()
        {
            InitializeComponent();
            api = new UserAPI();
            this.roleAPI = new RoleAPI();
            dgvUsers.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            var request = new UserSearchObject
            {
                Name = txtName.Text,
                Username = txtUsername.Text,
                IncludeRoles = true
            };
            if (cbRole.SelectedValue != null && cbRole.SelectedIndex != -1)
            {
                request.RoleId = (int)cbRole.SelectedValue;
            }

            var response = await api.Get(request);

            dgvUsers.DataSource = response;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new frmUserAddEdit().ShowDialog();
        }

        private async void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvUsers.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var item = dgvUsers.Rows[e.RowIndex].DataBoundItem as UserResponse;
                if (item == null)
                {
                    MessageBox.Show("Grid is currently empty, option DELETE is not available!", "Error!!");
                }
                else
                {

                    if (item.Username.Equals(AuthHelper.Username))
                    {
                        MessageBox.Show("You can't delete your profile!", "Error");
                    }
                    else
                    {
                        var confirmResult = MessageBox.Show("Are you sure that you want to delete this user ??\n If you proceed all reservations from this user will be deleted.", "Confirm Delete!!", MessageBoxButtons.YesNo);

                        if (confirmResult == DialogResult.Yes)
                        {
                            var response = await api.Delete(item.UserId);
                            if (!String.IsNullOrEmpty(response))
                            {
                                loadData();
                            }
                        }
                    }
                }
            }
        }

        private async void dgvUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.ColumnIndex == dgvUsers.Columns["Delete"].Index) && e.RowIndex >= 0)
            {
                var item = dgvUsers.Rows[e.RowIndex].DataBoundItem as UserResponse;
                new frmUserAddEdit(item).ShowDialog();

            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            loadRoles();
        }

        private async void loadRoles()
        {
            var roles = await roleAPI.Get();

            cbRole.DataSource = roles;
            cbRole.ValueMember = "RoleId";
            cbRole.DisplayMember = "Name";
            cbRole.SelectedIndex = -1;
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtUsername.Text = "";
            cbRole.SelectedIndex = -1;
        }
    }
}
