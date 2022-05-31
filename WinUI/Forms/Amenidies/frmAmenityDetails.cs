using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using WinUI.Services;

namespace WinUI.Forms.Amenidies
{
    public partial class frmAmenityDetails : Form
    {
        private readonly AmenityResponse item;
        private AmenityAPI api;
        public frmAmenityDetails()
        {
            InitializeComponent();
            this.api = new AmenityAPI();
        }

        public frmAmenityDetails(AmenityResponse item)
        {
            InitializeComponent();
            this.api = new AmenityAPI();
            this.item = item;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                var request = new AmenityUpsertRequest
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text
                };

                if(item != null)
                {
                    var result = await api.Put(item.AmenityId,request);

                    if (request != null)
                    {
                        MessageBox.Show($"Amenity {result.Name} successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var result = await api.Post(request);

                    if(request != null)
                    {
                        MessageBox.Show($"Amenity {result.Name} successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void frmAmenityDetails_Load(object sender, EventArgs e)
        {
            if(item != null)
            {
                txtName.Text = item.Name;
                txtDescription.Text = item.Description;
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
    }
}
