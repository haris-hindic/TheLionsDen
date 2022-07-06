using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using WinUI.Helpers;
using WinUI.Services;

namespace WinUI.Forms.Facilites
{
    public partial class frmFacilites : Form
    {
        private FacilityAPI facilityAPI;

        public frmFacilites()
        {
            InitializeComponent();
            this.facilityAPI = new FacilityAPI();
            dgvFacilites.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            var request = new FacilitySearchObject()
            {
                IncludeEmployees = true,
                Name = txtName.Text,
                Status = (string)cmbStatus.SelectedItem
            };

            var response = await facilityAPI.Get(request);

            dgvFacilites.DataSource = response;
            clearImageEmployees();
        }

        private async void dgvFacilites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            populateFields(dgvFacilites.Rows[e.RowIndex].DataBoundItem as FacilityResponse);
        }

        private void populateFields(FacilityResponse? facilityResponse)
        {
            if (pbImage.Image != null)
                pbImage.Image = null;

            pbImage.Image = ImageHelper.ByteArrayToImage(facilityResponse.Image);
            Employees.DataSource = facilityResponse.Employees;
            Employees.DisplayMember = "OutlineText";
        }

        private void frmFacilites_Load(object sender, EventArgs e)
        {
            cmbStatus.DataSource = cmbHelper.facility;
            cmbStatus.SelectedIndex = -1;
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = -1;
            txtName.Text = "";
            clearImageEmployees();
        }

        private void clearImageEmployees()
        {
            pbImage.Image = null;
            Employees.DataSource = null;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearImageEmployees();
            new frmFacilityAddEdit().ShowDialog();
        }

        private void dgvFacilites_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clearImageEmployees();
            new frmFacilityAddEdit(dgvFacilites.Rows[e.RowIndex].DataBoundItem as FacilityResponse).ShowDialog();
        }
    }
}
