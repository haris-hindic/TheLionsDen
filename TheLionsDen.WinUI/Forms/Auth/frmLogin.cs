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

                AuthHelper.Role = result.RoleName;

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
