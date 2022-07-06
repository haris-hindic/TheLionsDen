using TheLionsDen.Model.Enums;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.WinUI.Services;
using WinUI.Forms.RoomTypes;

namespace WinUI.Forms.Rooms
{
    public partial class frmRoomAddEdit : Form
    {
        private RoomTypeAPI roomTypeAPI;
        private RoomAPI roomAPI;
        private AmenityAPI amenitiyAPI;
        private RoomResponse room;

        public frmRoomAddEdit()
        {
            InitializeComponent();
            this.roomTypeAPI = new RoomTypeAPI();
            this.roomAPI = new RoomAPI();
            this.amenitiyAPI = new AmenityAPI();
            dgvAmenities.AutoGenerateColumns = false;
        }
        public frmRoomAddEdit(RoomResponse room)
        {
            InitializeComponent();
            this.roomTypeAPI = new RoomTypeAPI();
            this.roomAPI = new RoomAPI();
            this.amenitiyAPI = new AmenityAPI();
            dgvAmenities.AutoGenerateColumns = false;
            this.room = room;
        }

        private void frmRoomAddEdit_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            cmbRoomType.DataSource = await roomTypeAPI.Get();
            cmbRoomType.ValueMember = "RoomTypeId";
            cmbRoomType.DisplayMember = "Name";
            cmbRoomType.SelectedIndex = -1;


            if (this.room == null) loadAmenities();

            if (this.room != null) populateFields();

            if (this.room != null) setButtons(this.room);
        }

        private async void loadAmenities()
        {
            var amenityRequest = new TheLionsDen.Model.SearchObjects.AmenitySearchObject();
            if (room != null) amenityRequest.NotRoomId = room.RoomId;
            clbAmenities.Refresh();
            clbAmenities.DataSource = await amenitiyAPI.Get(amenityRequest);
            clbAmenities.ValueMember = "AmenityId";
            clbAmenities.DisplayMember = "Name";
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if ((cmbRoomType.SelectedItem as RoomTypeResponse) != null)
                new frmRoomTypeAddEdit(cmbRoomType.SelectedItem as RoomTypeResponse, true).ShowDialog();
            else
                MessageBox.Show("Please select a room type first.");
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {

                var request = populateRequest();

                if (this.room == null)
                {

                    var result = await roomAPI.Post(request);

                    if (result != null)
                    {
                        MessageBox.Show($"Room {result.Name} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.room = result;
                        populateFields();
                    }
                }
                else
                {
                    var result = await roomAPI.Put(this.room.RoomId, request);

                    if (result != null)
                    {
                        MessageBox.Show($"Room {result.Name} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.room = result;
                        populateFields();
                    }
                }
            }
        }

        private void populateFields()
        {
            txtName.Text = this.room.Name;
            numPrice.Value = (decimal)this.room.Price;
            numFloor.Value = (decimal)this.room.Floor;
            numRoomNo.Value = (decimal)this.room.Number;
            cmbRoomType.SelectedValue = this.room.RoomTypeId;
            dgvAmenities.DataSource = this.room.RoomAmenities;

            for (int i = 0; i < clbAmenities.Items.Count; i++)
            {
                clbAmenities.SetItemChecked(i, false);
            }

            loadAmenities();

            btnHide.Enabled = false;
            btnTaken.Enabled = false;
            btnActivate.Enabled = false;

            setButtons(room);
        }

        RoomUpsertRequest populateRequest() => new RoomUpsertRequest
        {
            Name = txtName.Text,
            Price = (float)numPrice.Value,
            Floor = (int?)numFloor.Value,
            Number = (int?)numRoomNo.Value,
            RoomTypeId = (int)cmbRoomType.SelectedValue,
            AmenityIds = clbAmenities.CheckedItems.Cast<AmenityResponse>().Select(x => x.AmenityId).ToList()
        };

        private async void dgvAmenities_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAmenities.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                var confirmResult = MessageBox.Show("Are you sure that you want to remove this item ??", "Confirm remove!!", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var item = dgvAmenities.Rows[e.RowIndex].DataBoundItem as RoomAmenityResponse;
                    var response = await roomAPI.RemoveAmenity(this.room.RoomId, item.AmenityId);
                    if (response != null)
                    {
                        this.room = response;
                        populateFields();
                    }
                }

            }
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (this.room != null)
            {
                var response = await roomAPI.Activate(this.room.RoomId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    loadRoom();
                }
            }
        }

        private async void loadRoom()
        {
            var room = await roomAPI.GetById(this.room.RoomId);
            this.room = room;
            populateFields();
        }

        private async void btnHide_Click(object sender, EventArgs e)
        {
            if (this.room != null)
            {
                var response = await roomAPI.Hide(this.room.RoomId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    loadRoom();
                }
            }
        }

        private async void btnTaken_Click(object sender, EventArgs e)
        {
            if (this.room != null)
            {
                var response = await roomAPI.SetAsTaken(this.room.RoomId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    loadRoom();
                }
            }
        }

        private void setButtons(RoomResponse roomResponse)
        {
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

        private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                e.Cancel = true;
                error.SetError(txtName, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(txtName, "");
            }
        }

        private void numPrice_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (numPrice.Value <= 0)
            {
                e.Cancel = true;
                error.SetError(numPrice, "Price should not be left blank or equal/below zero!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(numPrice, "");
            }
        }

        private void cmbRoomType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbRoomType.SelectedValue == null)
            {
                e.Cancel = true;
                error.SetError(cmbRoomType, "Please select a room type!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(cmbRoomType, "");
            }
        }
    }
}
