using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

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

                AuthHelper.Role = result.RoleName;

                if (AuthHelper.Role == "Customer")
                {
                    MessageBox.Show("Access denied!");
                    clearCredentials();
                }
                else
                {
                    this.Hide();
                    new MainForm().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong username or password!");
            }
        }

        private void clearCredentials()
        {
            AuthHelper.Username = null;
            AuthHelper.Password = null;
            AuthHelper.Role = null;
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
