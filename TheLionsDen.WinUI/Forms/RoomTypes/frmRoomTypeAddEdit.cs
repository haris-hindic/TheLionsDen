using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.RoomTypes
{
    public partial class frmRoomTypeAddEdit : Form
    {
        private RoomTypeAPI roomTypeAPI;
        private RoomImageAPI roomImageAPI;
        private RoomTypeResponse roomType;
        private List<RoomImageResponse> images = new List<RoomImageResponse>();
        private int imageIndex = 0;
        private bool isPreview = false;
        public frmRoomTypeAddEdit()
        {
            InitializeComponent();
            this.roomTypeAPI = new RoomTypeAPI();
            this.roomImageAPI = new RoomImageAPI();
        }

        public frmRoomTypeAddEdit(RoomTypeResponse roomType)
        {
            InitializeComponent();
            this.roomTypeAPI = new RoomTypeAPI();
            this.roomImageAPI = new RoomImageAPI();
            this.roomType = roomType;
        }
        public frmRoomTypeAddEdit(RoomTypeResponse roomType, bool preview)
        {
            InitializeComponent();
            this.roomTypeAPI = new RoomTypeAPI();
            this.roomImageAPI = new RoomImageAPI();
            this.roomType = roomType;
            this.isPreview = preview;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                var request = populateRequest();

                if (this.roomType == null)
                {
                    var result = await roomTypeAPI.Post(request);

                    if (result != null)
                    {
                        MessageBox.Show($"Room Type {result.Name} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.roomType = result;
                        populateFields();
                    }
                }
                else
                {
                    var result = await roomTypeAPI.Put(this.roomType.RoomTypeId, request);

                    if (result != null)
                    {
                        MessageBox.Show($"Room Type {result.Name} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.roomType = result;
                        populateFields();
                    }
                }
            }
        }

        private void populateFields()
        {
            txtName.Text = this.roomType.Name;
            txtRules.Text = this.roomType.Rules;
            txtDescription.Text = this.roomType.Description;
            numCapacity.Value = this.roomType.Capacity;
            numSize.Value = this.roomType.Size;

            if (this.roomType.RoomImages != null)
            {

                images = (List<RoomImageResponse>)this.roomType.RoomImages;
                imageIndex = 0;

                if (images.Count() > 0)
                {
                    pbImages.Image = ImageHelper.ByteArrayToImage(images[imageIndex].Image);
                }

                btnUploadImage.Enabled = true;
                btnSaveImage.Enabled = true;
                btnNext.Enabled = true;
                btnPrevious.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private RoomTypeUpsertRequest populateRequest() => new RoomTypeUpsertRequest()
        {
            Name = txtName.Text,
            Description = txtDescription.Text,
            Rules = txtRules.Text,
            Capacity = (int)numCapacity.Value,
            Size = (int)numSize.Value
        };

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (images.Count() == 0) MessageBox.Show("No images!");

            if (images.Count() > (imageIndex + 1))
            {
                imageIndex++;
                pbImages.Image = ImageHelper.ByteArrayToImage(images[imageIndex].Image);
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
                pbImages.Image = ImageHelper.ByteArrayToImage(images[imageIndex].Image);
            }
            else
            {
                MessageBox.Show("This is the first image.");
            }
        }

        private void frmRoomTypeAddEdit_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            if (this.roomType != null)
            {
                var request = new RoomImageSearchObject() { RoomTypeId = roomType.RoomTypeId };

                roomType.RoomImages = (ICollection<RoomImageResponse>)await roomImageAPI.Get(request);
                populateFields();
            }
            else
            {
                btnUploadImage.Enabled = false;
                btnSaveImage.Enabled = false;
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
                btnDelete.Enabled = false;
            }
            if (isPreview)
                disableField();
        }

        private void disableField()
        {
            btnUploadImage.Enabled = false;
            btnSaveImage.Enabled = false;
            btnDelete.Enabled = false;
            txtDescription.ReadOnly = true;
            txtName.ReadOnly = true;
            numCapacity.ReadOnly = true;
            numSize.ReadOnly = true;
            txtRules.ReadOnly = true;
            btnSave.Enabled = false;
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

        private void numCapacity_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (numCapacity.Value <= 0)
            {
                e.Cancel = true;
                error.SetError(numCapacity, "Capacity should not be left blank or equal/below zero!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(numCapacity, "");
            }
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.png;)|*.jpg;*.jpeg;.*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pbNewImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pbNewImage.Image = new Bitmap(opnfd.FileName);
            }
        }

        private async void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (pbNewImage.Image != null)
            {
                var request = new RoomImageInsertRequest()
                {
                    RoomTypeId = this.roomType.RoomTypeId,
                    Image = ImageHelper.ImageToByteArray(pbNewImage.Image)
                };

                var result = await roomImageAPI.Post(request);

                if (result != null)
                {
                    MessageBox.Show($"Image successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.roomType.RoomImages.Add(result);
                    pbNewImage.Image = null;
                    populateFields();
                }
            }
            else
            {
                MessageBox.Show("Please select an image first.");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (images.Count() == 0) MessageBox.Show("No images!");

            var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {

                var result = await roomImageAPI.Delete(images[imageIndex].RoomImageId);

                if (result != null)
                {
                    MessageBox.Show(result);
                    pbImages.Image = null;
                    loadData();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
