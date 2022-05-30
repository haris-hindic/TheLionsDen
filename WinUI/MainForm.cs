using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.Forms.Amenidies;
using WinUI.Forms.Analytics;
using WinUI.Forms.Auth;
using WinUI.Forms.Employees;
using WinUI.Forms.Facilites;
using WinUI.Forms.Reservations;
using WinUI.Forms.Rooms;
using WinUI.Forms.RoomTypes;
using WinUI.Forms.Users;
using WinUI.Helpers;

namespace WinUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            loadForm(new frmUsers());
        }

        private void loadForm(object form)
        {
            if (this.panelMain.Controls.Count > 0)
            {
                this.panelMain.Controls.RemoveAt(0);
            }
            Form frm = form as Form;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(frm);
            this.panelMain.Tag = frm;
            frm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            loadForm(new frmUsers());
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            loadForm(new frmEmployees());
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            loadForm(new frmRooms());
        }

        private void btnRoomTypes_Click(object sender, EventArgs e)
        {
            loadForm(new frmRoomTypes());
        }

        private void btnFacilites_Click(object sender, EventArgs e)
        {
            loadForm(new frmFacilites());
        }

        private void btnAmenidies_Click(object sender, EventArgs e)
        {
            loadForm(new frmAmenidies());
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            loadForm(new frmReservations());
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            loadForm(new frmAnalytics());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblLoggedUser.Text = $"Welcome {AuthHelper.Username}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AuthHelper.Username = null;
            AuthHelper.Password= null;
            AuthHelper.Roles= null;
            this.Hide();
            new frmLogin().Show();
        }
    }
}
