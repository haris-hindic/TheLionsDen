using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Facilites
{
    public partial class frmFacilityAddEdit : Form
    {
        private FacilityAPI facilityAPI;
        private EmployeeAPI employeeAPI;
        private FacilityResponse facility;

        public frmFacilityAddEdit()
        {
            InitializeComponent();
            this.facilityAPI = new FacilityAPI();
            this.employeeAPI = new EmployeeAPI();
            dgvEmployees.AutoGenerateColumns = false;
        }

        public frmFacilityAddEdit(FacilityResponse facility)
        {
            InitializeComponent();
            this.facilityAPI = new FacilityAPI();
            this.employeeAPI = new EmployeeAPI();
            this.facility = facility;
            dgvEmployees.AutoGenerateColumns = false;
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.png;)|*.jpg;*.jpeg;.*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pbImage.Image = new Bitmap(opnfd.FileName);
            }
        }

        private void frmFacilityAddEdit_Load(object sender, EventArgs e)
        {
            cmbStatus.DataSource = cmbHelper.facility;
            if (this.facility != null)
            {
                populateFields();
                loadEmployees();
            }
        }

        private async void loadEmployees()
        {
            var avaliableEmployeesRequest = new EmployeeSearchObject()
            {
                AvaliableForFacilityId = this.facility.FacilityId,
                IncludeJobType = true,
                IncludeFacility = true
            };

            var facilityEmployeesRequest = new EmployeeSearchObject()
            {
                FacilityId = this.facility.FacilityId,
                IncludeJobType = true,
                IncludeFacility = true
            };


            var avaliableEmployeesResponse = await employeeAPI.Get(avaliableEmployeesRequest);
            var facilityEmployeesResponse = await employeeAPI.Get(facilityEmployeesRequest);

            dgvEmployees.DataSource = facilityEmployeesResponse;


            cmbAvaliableEmployees.DataSource = avaliableEmployeesResponse;
            cmbAvaliableEmployees.DisplayMember = "OutlineText";
            cmbAvaliableEmployees.ValueMember = "EmployeeId";
            cmbAvaliableEmployees.SelectedIndex = -1;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren() && ValidateImage())
            {
                var request = populateRequest();
                var img = pbImage.Image;

                if (this.facility == null)
                {
                    var result = await facilityAPI.Post(request);

                    if (result != null)
                    {
                        MessageBox.Show($"Facility {result.Name} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.facility = result;
                        populateFields();
                    }
                }
                else
                {
                    var result = await facilityAPI.Put(this.facility.FacilityId, request);

                    if (result != null)
                    {
                        MessageBox.Show($"Facility {result.Name} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.facility = result;
                        populateFields();
                    }
                }
            }
        }

        private FacilityUpsertRequest populateRequest() => new FacilityUpsertRequest()
        {
            Name = txtName.Text,
            Description = txtDescription.Text,
            Price = (float)nudPrice.Value,
            Status = (string)cmbStatus.SelectedValue,
            Image = ImageHelper.ImageToByteArray(pbImage.Image)
        };

        private void populateFields()
        {
            txtDescription.Text = this.facility.Description;
            txtName.Text = this.facility.Name;
            nudPrice.Value = (decimal)this.facility.Price;
            if(this.facility.Image.Length>1)
                pbImage.Image = ImageHelper.ByteArrayToImage(this.facility.Image);
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            cmbStatus.SelectedItem = this.facility.Status;
            loadEmployees();
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

        private void nudPrice_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(nudPrice.Value > 0))
            {
                e.Cancel = true;
                error.SetError(nudPrice, "Price should not be left blank and it should be above zero!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(nudPrice, "");
            }
        }

        private void cmbStatus_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbStatus.SelectedValue.ToString()))
            {
                e.Cancel = true;
                error.SetError(cmbStatus, "Please select a status!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(cmbStatus, "");
            }
        }

        public bool ValidateImage()
        {
            if (pbImage.Image == null)
            {
                error.SetError(pbImage, "Please upload an image!");
                return false;
            }
            error.Clear();
            return true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private async void btnAssign_Click(object sender, EventArgs e)
        {
            if (cmbAvaliableEmployees.SelectedItem != null)
            {
                var employeeId = (cmbAvaliableEmployees.SelectedItem as EmployeeResponse).EmployeeId;
                var faclitiyId = facility.FacilityId;

                var result = await employeeAPI.AssignToFacility(employeeId, faclitiyId);

                loadEmployees();
            }
            else
            {
                MessageBox.Show("Please select an employee.", "Info", MessageBoxButtons.OK);
            }
        }

        private async void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvEmployees.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var item = dgvEmployees.Rows[e.RowIndex].DataBoundItem as EmployeeResponse;
                    var response = await employeeAPI.RemoveFromFacility(item.EmployeeId);
                    if (!String.IsNullOrEmpty(response))
                    {
                        loadEmployees();
                    }
                }

            }
        }
    }
}
