using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using WinUI.Helpers;
using WinUI.Services;

namespace WinUI.Forms.Employees
{
    public partial class frmEmployeeAddEdit : Form
    {
        private JobTypeAPI jobTypeAPI;
        private EmployeeAPI employeeAPI;
        private EmployeeResponse employee;
        

        public frmEmployeeAddEdit()
        {
            InitializeComponent();
            jobTypeAPI = new JobTypeAPI();
            employeeAPI = new EmployeeAPI();
        }

        public frmEmployeeAddEdit(EmployeeResponse employee)
        {
            InitializeComponent();
            jobTypeAPI = new JobTypeAPI();
            employeeAPI = new EmployeeAPI();
            this.employee = employee;
        }

        private void frmEmployeeAddEdit_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            var jobTypesList = await jobTypeAPI.Get();

            cmbJobType.DataSource = jobTypesList;
            cmbJobType.DisplayMember = "Name";
            cmbJobType.ValueMember = "JobTypeId";

            cmbStatus.DataSource = StatusHelper.employee;

            if (employee != null)
            {
                populateFields();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                if (employee == null)
                {
                    var request = populateInsertRequest();

                    var result = await employeeAPI.Post(request);

                    if (result != null)
                    {
                        MessageBox.Show($"Employee {result.OutlineText} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.employee = result;
                        populateFields();
                    }
                }
                else
                {
                    var request = populateUpdateRequest();

                    var result = await employeeAPI.Put(employee.EmployeeId, request);

                    if (result != null)
                    {
                        MessageBox.Show($"Employee {result.OutlineText} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.employee = result;
                        populateFields();
                    }
                }
            }
        }

        private void populateFields()
        {
            cmbJobType.SelectedItem = employee.JobType;
            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            txtEmail.Text = employee.Email;
            txtPhoneNumber.Text = employee.PhoneNumber;
            dtpDateOfBirth.Value = employee.BirthDate == null ? DateTime.Now : employee.BirthDate.Value;
            txtGender.Text = employee.Gender;
            txtAddress.Text = employee.Address;
            cmbStatus.SelectedItem = employee.Status;
            dtpEmploymentDate.Value = employee.EmploymentDate;
            dtpEmploymentDate.Enabled = false;
        }

        private EmployeeInsertRequest populateInsertRequest()
        {
            return new EmployeeInsertRequest()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhoneNumber.Text,
                BirthDate = dtpDateOfBirth.Value,
                Gender = txtGender.Text,
                Address = txtAddress.Text,
                EmploymentDate = dtpEmploymentDate.Value,
                JobTypeId = (int)cmbJobType.SelectedValue,
                Status = (string)cmbStatus.SelectedItem
            };
        }

        private EmployeeUpdateRequest populateUpdateRequest()
        {
            return new EmployeeUpdateRequest()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Gender = txtGender.Text,
                Address = txtAddress.Text,
                JobTypeId = (int)cmbJobType.SelectedValue,
                Status = (string)cmbStatus.SelectedItem,
                BirthDate = dtpDateOfBirth.Value
            };
        }

        private void txtFirstName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                error.SetError(txtFirstName, "First name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(txtFirstName, "");
            }
        }

        private void txtLastName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                error.SetError(txtLastName, "Last name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(txtLastName, "");
            }
        }

        private void dtpEmploymentDate_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dtpEmploymentDate.Text))
            {
                e.Cancel = true;
                error.SetError(dtpEmploymentDate, "Employment date should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(dtpEmploymentDate, "");
            }
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                error.SetError(txtEmail, "Email should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(txtEmail, "");
            }
        }

        private void cmbJobType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmbJobType.SelectedItem == null)
            {
                e.Cancel = true;
                error.SetError(cmbJobType, "Please select a job type!");
            }
            else
            {
                e.Cancel = false;
                error.SetError(cmbJobType, "");
            }
        }
    }
}
