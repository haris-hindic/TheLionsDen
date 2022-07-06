using TheLionsDen.WinUI.Helpers;
using WinUI.Forms.Amenidies;
using WinUI.Forms.Analytics;
using WinUI.Forms.Auth;
using WinUI.Forms.Employees;
using WinUI.Forms.Facilites;
using WinUI.Forms.Reservations;
using WinUI.Forms.Rooms;
using WinUI.Forms.RoomTypes;
using WinUI.Forms.Users;

namespace WinUI
{
    public partial class MainForm : Form
    {
        private string loadedForm;
        public MainForm()
        {
            InitializeComponent();
            loadForm(new frmUsers(), FormTypes.user);
        }

        private void loadForm(object form, string formName)
        {
            if (loadedForm != formName)
            {
                if (this.panelMain.Controls.Count > 0)
                {
                    this.panelMain.Controls.RemoveAt(0);
                }
                Form frm = form as Form;
                frm.TopLevel = false;
                frm.Dock = DockStyle.Fill;
                loadedForm = formName;
                this.panelMain.Controls.Add(frm);
                this.panelMain.Tag = frm;
                frm.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            loadForm(new frmUsers(), FormTypes.user);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            loadForm(new frmEmployees(), FormTypes.employee);
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            loadForm(new frmRooms(), FormTypes.room);
        }

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            loadForm(new frmRoomTypes(), FormTypes.roomType);
        }

        private void btnFacilites_Click(object sender, EventArgs e)
        {
            loadForm(new frmFacilites(), FormTypes.facility);
        }

        private void btnAmenidies_Click(object sender, EventArgs e)
        {
            loadForm(new frmAmenidies(), FormTypes.amentiy);
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            loadForm(new frmReservations(), FormTypes.reservations);
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            loadForm(new frmAnalytics(), FormTypes.analytics);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblLoggedUser.Text = $"Welcome {AuthHelper.Username}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AuthHelper.Username = null;
            AuthHelper.Password = null;
            AuthHelper.Role = null;
            this.Hide();
            new frmLogin().Show();
        }
    }
}
