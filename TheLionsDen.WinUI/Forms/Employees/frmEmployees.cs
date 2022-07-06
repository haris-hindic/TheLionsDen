using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Employees
{
    public partial class frmEmployees : Form
    {
        private EmployeeAPI employeeAPI;
        private JobTypeAPI jobTypeAPI;
        private FacilityAPI facilityAPI;
        public frmEmployees()
        {
            InitializeComponent();
            this.employeeAPI = new EmployeeAPI();
            this.jobTypeAPI = new JobTypeAPI();
            this.facilityAPI = new FacilityAPI();
            dgvEmployees.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            loadFacilites();
            loadJobTypes();
        }

        private async void loadFacilites()
        {
            var response = await facilityAPI.Get();

            cmbFacility.DataSource = response;
            cmbFacility.DisplayMember = "Name";
            cmbFacility.ValueMember = "FacilityId";
            cmbFacility.SelectedIndex = -1;
        }

        private async void loadData()
        {
            var request = new EmployeeSearchObject
            {
                Name = txtName.Text,
                IncludeJobType = true,
                IncludeFacility = true
            };
            if (cmbJobType.SelectedValue != null && cmbJobType.SelectedIndex != -1)
            {
                request.JobTypeId = (int)cmbJobType.SelectedValue;
            }
            if (cmbFacility.SelectedValue != null && cmbFacility.SelectedIndex != -1)
            {
                request.FacilityId = (int)cmbFacility.SelectedValue;
            }

            var response = await employeeAPI.Get(request);

            dgvEmployees.DataSource = response;
        }

        private async void loadJobTypes()
        {
            var jobTypeList = await jobTypeAPI.Get();

            cmbJobType.DataSource = jobTypeList;
            cmbJobType.DisplayMember = "Name";
            cmbJobType.ValueMember = "JobTypeId";
            cmbJobType.SelectedIndex = -1;
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            cmbJobType.SelectedIndex = -1;
            cmbFacility.SelectedIndex = -1;
            txtName.Text = "";
        }

        private async void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvEmployees.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var item = dgvEmployees.Rows[e.RowIndex].DataBoundItem as EmployeeResponse;
                    var response = await employeeAPI.Delete(item.EmployeeId);
                    if (!String.IsNullOrEmpty(response))
                    {
                        loadData();
                    }
                }

            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new frmEmployeeAddEdit().ShowDialog();
        }

        private void dgvEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.ColumnIndex == dgvEmployees.Columns["Delete"].Index) && e.RowIndex >= 0)
            {
                var item = dgvEmployees.Rows[e.RowIndex].DataBoundItem as EmployeeResponse;
                new frmEmployeeAddEdit(item).ShowDialog();
            }
        }
    }
}
