using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using WinUI.Helpers;
using WinUI.Services;

namespace WinUI.Forms.RoomTypes
{
    public partial class frmRoomTypes : Form
    {
        private RoomTypeAPI roomTypeAPI;
        private RoomImageAPI roomImageAPI;
        private List<RoomImageResponse> images = new List<RoomImageResponse>();
        private int imageIndex = 0;
        public frmRoomTypes()
        {
            InitializeComponent();
            this.roomTypeAPI = new RoomTypeAPI();
            this.roomImageAPI = new RoomImageAPI();
            dgvRoomTypes.AutoGenerateColumns = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {

            loadData();
        }

        private async void loadData()
        {
            var request = new RoomTypeSearchObject()
            {
                Capacity = (int)numCapacity.Value,
                Comparator = (string)cmbComparator.SelectedItem,
                Name = txtName.Text
            };

            var response = await roomTypeAPI.Get(request);

            dgvRoomTypes.DataSource = response;
            
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDesc.Text = "";
            cmbComparator.SelectedIndex = -1;
            pbImage.Image = null;
            images = new List<RoomImageResponse>();
            imageIndex = 0;
        }

        private async void dgvRoomTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvRoomTypes.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var item = dgvRoomTypes.Rows[e.RowIndex].DataBoundItem as RoomTypeResponse;
                    var response = await roomTypeAPI.Delete(item.RoomTypeId);
                    if (!String.IsNullOrEmpty(response))
                    {
                        loadData();
                    }
                }

            }
            else
            {
                populateFields(dgvRoomTypes.Rows[e.RowIndex].DataBoundItem as RoomTypeResponse);
            }
        }

        private async void populateFields(RoomTypeResponse roomTypeResponse)
        {
            var request = new RoomImageSearchObject() { RoomTypeId = roomTypeResponse.RoomTypeId };

            var response = await roomImageAPI.Get(request);

            images = (List<RoomImageResponse>)response;
            imageIndex = 0;

            if (images.Count() > 0)
            {
                pbImage.Image = ImageHelper.ByteArrayToImage(images[imageIndex].Image);
            }

            txtDesc.Text = roomTypeResponse.Description;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (images.Count() == 0) MessageBox.Show("No images!");

            if (images.Count() > (imageIndex + 1))
            {
                imageIndex++;
                pbImage.Image = ImageHelper.ByteArrayToImage(images[imageIndex].Image);
            }
            else
            {
                MessageBox.Show("No more images!");
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (images.Count() == 0) MessageBox.Show("No images!");

            if (imageIndex > 0)
            {
                imageIndex--;
                pbImage.Image = ImageHelper.ByteArrayToImage(images[imageIndex].Image);
            }
            else
            {
                MessageBox.Show("This is the first image.");
            }
        }

        private void frmRoomTypes_Load(object sender, EventArgs e)
        {
            cmbComparator.DataSource = cmbHelper.comparator;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new frmRoomTypeAddEdit().ShowDialog();
        }

        private void dgvRoomTypes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new frmRoomTypeAddEdit(dgvRoomTypes.Rows[e.RowIndex].DataBoundItem as RoomTypeResponse).ShowDialog();
        }
    }
}
