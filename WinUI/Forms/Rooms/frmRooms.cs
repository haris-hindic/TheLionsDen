using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using WinUI.Helpers;
using WinUI.Services;

namespace WinUI.Forms.Rooms
{
    public partial class frmRooms : Form
    {
        private RoomAPI roomAPI;
        private RoomTypeAPI roomTypeAPI;
        public frmRooms()
        {
            InitializeComponent();
            roomAPI = new RoomAPI();    
            roomTypeAPI = new RoomTypeAPI();
            dgvRooms.AutoGenerateColumns = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var request = populateSearchRequest();

            if (cmbRoomType.SelectedValue != null && cmbRoomType.SelectedIndex != -1)
            {
                request.RoomTypeId = (int)cmbRoomType.SelectedValue;
            }
            if (cmbState.SelectedValue != null && cmbState.SelectedIndex != -1)
            {
                request.State = (string)cmbState.SelectedValue;
            }
            if (cmbComparator.SelectedValue != null && cmbComparator.SelectedIndex != -1)
            {
                request.Comparator = (string)cmbComparator.SelectedValue;
            }

            var response = await roomAPI.Get(request);

            dgvRooms.DataSource = response;
        }

        private void frmRooms_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            cmbComparator.DataSource = cmbHelper.comparator;
            cmbComparator.SelectedIndex = -1;

            cmbState.DataSource = cmbHelper.roomState;
            cmbState.SelectedIndex = -1;

            cmbRoomType.DataSource = await roomTypeAPI.Get();
            cmbRoomType.ValueMember = "RoomTypeId";
            cmbRoomType.DisplayMember = "Name";
            cmbRoomType.SelectedIndex = -1;
        }

        private RoomSearchObject populateSearchRequest() => new RoomSearchObject
        {
            Name = txtName.Text,
            Price = (float)numPrice.Value,
            IncludeAmenities = true,
            IncludeRoomType = true,
        };

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            cmbRoomType.SelectedIndex = -1;
            cmbComparator.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            numPrice.Value = 0;
            txtName.Text = "";

        }
    }
}
