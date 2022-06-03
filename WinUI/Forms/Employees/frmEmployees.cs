using TheLionsDen.Model.Enums;
using TheLionsDen.Model.SearchObjects;
using WinUI.Services;

namespace WinUI.Forms.Employees
{
    public partial class frmEmployees : Form
    {
        private EmployeeAPI employeeAPI;
        public frmEmployees()
        {
            InitializeComponent();
            this.employeeAPI = new EmployeeAPI();
            dgvEmployees.AutoGenerateColumns = false;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var request = new EmployeeSearchObject
            {
                Name = txtName.Text
            };

            var response = await employeeAPI.Get(request);

            dgvEmployees.DataSource = response;
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            loadFacilites();
            loadJobTypes();
        }

        private void loadFacilites()
        {

        }

        private void loadJobTypes()
        {

        }
    }
}
