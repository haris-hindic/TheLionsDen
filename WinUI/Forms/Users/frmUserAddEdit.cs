using System.ComponentModel;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using WinUI.Helpers;
using WinUI.Services;

namespace WinUI.Forms.Users
{
    public partial class frmUserAddEdit : Form
    {
        private RoleAPI roleAPI;
        private UserAPI userAPI;
        private UserResponse user;
        public frmUserAddEdit()
        {
            InitializeComponent();
            this.roleAPI = new RoleAPI();
            this.userAPI = new UserAPI();
        }

        public frmUserAddEdit(UserResponse user)
        {
            InitializeComponent();
            this.roleAPI = new RoleAPI();
            this.userAPI = new UserAPI();
            this.user = user;
        }

        private async void frmUserAddEdit_Load(object sender, EventArgs e)
        {
            loadRoles();

            if (user != null)
            {
                populateFields();
            }
        }

        private async void loadRoles()
        {
            var roles = await roleAPI.Get();

            cbRole.DataSource = roles;
            cbRole.ValueMember = "RoleId";
            cbRole.DisplayMember = "Name";
        }

        private void populateFields()
        {
            cbRole.SelectedItem = user.Role;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtUsername.Text = user.Username;
            txtUsername.Enabled = false;
            txtEmail.Text = user.Email;
            txtPhoneNumber.Text = user.PhoneNumber;
            dtpDateOfBirth.Value = user.DateOfBirth == null ? DateTime.Now : user.DateOfBirth.Value;
            txtGender.Text = user.Gender;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                if (user == null)
                {
                    var request = populateInsertRequest();

                    var result = await userAPI.Post(request);

                    if (result != null)
                    {
                        MessageBox.Show($"User {result.Username} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.user = result;
                        populateFields();
                    }
                }
                else
                {
                    var request = populateUpdateRequest();

                    if (this.user.Username.Equals(AuthHelper.Username))
                    {
                        var confirmResult = MessageBox.Show("If you edit your credentials you will have to authenticate again.", "Are you sure?", MessageBoxButtons.YesNo);

                        if (confirmResult == DialogResult.Yes)
                        {

                            var result = await userAPI.Put(user.UserId, request);

                            if (result != null)
                            {
                                MessageBox.Show($"User {result.Username} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.user = result;
                                if (this.user.Username.Equals(AuthHelper.Username))
                                {
                                    AuthHelper.Username = null;
                                    AuthHelper.Password = null;
                                    AuthHelper.Roles = null;
                                    Application.Restart();
                                }
                                populateFields();
                            }
                        }
                    }
                }
            }
        }

        private UserInsertRequest populateInsertRequest()
        {
            return new UserInsertRequest
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhoneNumber.Text,
                DateOfBirth = dtpDateOfBirth.Value,
                Gender = txtGender.Text,
                PasswordConfirmation = txtConfirmPassword.Text,
                RoleId = (int?)cbRole.SelectedValue
            };
        }

        private UserUpdateRequest populateUpdateRequest()
        {
            return new UserUpdateRequest
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Password = txtPassword.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhoneNumber.Text,
                DateOfBirth = dtpDateOfBirth.Value,
                Gender = txtGender.Text,
                PasswordConfirmation = txtConfirmPassword.Text,
                RoleId = (int?)cbRole.SelectedValue
            };
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                e.Cancel = true;
                error.SetError(txtUsername, "Username should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(txtUsername, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (user == null)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    e.Cancel = true;
                    error.SetError(txtPassword, "Password should not be left blank!");
                }
                else
                {
                    e.Cancel = false;
                    error.SetError(txtPassword, "");
                }
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (user == null)
            {
                if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    e.Cancel = true;
                    error.SetError(txtConfirmPassword, "Password confirmation should not be left blank!");
                }
                else
                {
                    e.Cancel = false;
                    error.SetError(txtConfirmPassword, "");
                }
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                error.SetError(txtEmail, "Email should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(txtEmail, "");
            }
        }
    }
}
