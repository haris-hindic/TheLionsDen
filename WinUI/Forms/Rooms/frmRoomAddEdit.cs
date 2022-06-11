using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using WinUI.Forms.RoomTypes;
using WinUI.Services;

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
    }
}
