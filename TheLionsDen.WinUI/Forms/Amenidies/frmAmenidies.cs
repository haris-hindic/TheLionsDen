using System.ComponentModel;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Amenidies
{
    public partial class frmAmenidies : Form
    {
        private AmenityAPI api;
        public AmenityResponse selectedItem { get; set; }
        public frmAmenidies()
        {
            InitializeComponent();
            this.api = new AmenityAPI();
            dgvAmenities.AutoGenerateColumns = false;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAmenities.Columns["Delete"].Index)
            {
                var item = dgvAmenities.Rows[e.RowIndex].DataBoundItem as AmenityResponse;
                if (item == null)
                {
                    MessageBox.Show("Grid is currently empty, option DELETE is not available!", "Error!!");
                }
                else
                {
                    var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        var response = await api.Delete(item.AmenityId);
                        if (!String.IsNullOrEmpty(response))
                        {
                            await LoadData();
                        }
                    }
                }

            }
            else
            {
                populateFields(dgvAmenities.Rows[e.RowIndex].DataBoundItem as AmenityResponse);
            }
        }

        private void populateFields(AmenityResponse item)
        {
            if (item != null)
            {
                selectedItem = item;
                txtName.Text = item.Name;
                txtDescription.Text = item.Description;
            }
        }

        private void clearFields()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            this.selectedItem = null;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var search = new AmenitySearchObject
            {
                Name = txtNameSearch.Text
            };

            var amenities = await api.Get(search);

            dgvAmenities.DataSource = amenities;
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                var request = new AmenityUpsertRequest
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text
                };

                if (selectedItem != null)
                {
                    var result = await api.Put(selectedItem.AmenityId, request);

                    if (request != null)
                    {
                        MessageBox.Show($"Amenity {result.Name} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    LoadData();
                    clearFields();
                }
                else
                {
                    var result = await api.Post(request);

                    if (request != null)
                    {
                        MessageBox.Show($"Amenity {result.Name} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    LoadData();
                    clearFields();

                }
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
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

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }
}
