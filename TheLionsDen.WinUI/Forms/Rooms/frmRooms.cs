using TheLionsDen.Model.Enums;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Rooms
{
    public partial class frmRooms : Form
    {
        private RoomAPI roomAPI;
        private RoomTypeAPI roomTypeAPI;
        private RoomResponse selectedRoom;
        public frmRooms()
        {
            InitializeComponent();
            roomAPI = new RoomAPI();
            roomTypeAPI = new RoomTypeAPI();
            dgvRooms.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadRooms();
        }

        private void frmRooms_Load(object sender, EventArgs e)
        {
            loadData();
            deactivateButtons();
        }

        private void deactivateButtons()
        {
            btnHide.Enabled = false;
            btnTaken.Enabled = false;
            btnActivate.Enabled = false;
            this.selectedRoom = null;
        }

        private async void loadRooms()
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

            deactivateButtons();
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

            this.selectedRoom = null;

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
            this.selectedRoom = null;
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (this.selectedRoom != null)
            {
                var response = await roomAPI.Activate(this.selectedRoom.RoomId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    loadRooms();
                }
            }
        }

        private async void btnHide_Click(object sender, EventArgs e)
        {
            if (this.selectedRoom != null)
            {
                var response = await roomAPI.Hide(this.selectedRoom.RoomId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    loadRooms();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async void btnTaken_Click(object sender, EventArgs e)
        {
            if (this.selectedRoom != null)
            {
                var response = await roomAPI.SetAsTaken(this.selectedRoom.RoomId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    loadRooms();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private async void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvRooms.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var item = dgvRooms.Rows[e.RowIndex].DataBoundItem as RoomResponse;
                if (item == null)
                {
                    MessageBox.Show("Grid is currently empty, option DELETE is not available!", "Error!!");
                }
                else
                {
                    var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??\n If you proceed all reservations and favourites containing this room will be deleted as well.", "Confirm Delete!!", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        var response = await roomAPI.Delete(item.RoomId);
                        if (!String.IsNullOrEmpty(response))
                        {
                            loadRooms();
                        }
                    }
                }

            }
            else
            {
                if (e.RowIndex >= 0)
                {
                    var item = dgvRooms.Rows[e.RowIndex].DataBoundItem as RoomResponse;
                    if (item == null)
                        setButtons(dgvRooms.Rows[e.RowIndex].DataBoundItem as RoomResponse);
                }
            }
        }

        private void setButtons(RoomResponse? roomResponse)
        {
            this.selectedRoom = roomResponse;
            if (roomResponse != null)
            {
                if (roomResponse.State == RoomState.Hidden.ToString()
                    || roomResponse.State == RoomState.Taken.ToString()
                    || roomResponse.State == RoomState.Draft.ToString())
                {
                    btnActivate.Enabled = true;
                }

                if (roomResponse.State == RoomState.Active.ToString()
                    || roomResponse.State == RoomState.Draft.ToString()
                    || roomResponse.State == RoomState.Taken.ToString())
                {
                    btnHide.Enabled = true;
                }

                if (roomResponse.State == RoomState.Active.ToString())
                {
                    btnTaken.Enabled = true;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new frmRoomAddEdit().ShowDialog();
        }

        private void dgvRooms_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                new frmRoomAddEdit(dgvRooms.Rows[e.RowIndex].DataBoundItem as RoomResponse).ShowDialog();
        }
    }
}
