using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using WinUI.Services;

namespace WinUI.Forms.Amenidies
{
    public partial class frmAmenidies : Form
    {
        private AmenityAPI api;
        public frmAmenidies()
        {
            InitializeComponent();
            this.api = new AmenityAPI();
            dgvAmenities.AutoGenerateColumns = false;
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAmenities.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                var confirmResult = MessageBox.Show("Are you sure that you want to delete this item ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var item = dgvAmenities.Rows[e.RowIndex].DataBoundItem as AmenityResponse;
                    var response = await api.Delete(item.AmenityId);
                    if (!String.IsNullOrEmpty(response))
                    {
                        await LoadData();
                    }
                }

            }
            else
            {
                var item = dgvAmenities.Rows[e.RowIndex].DataBoundItem as AmenityResponse;
                new frmAmenityDetails(item).ShowDialog();
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var search = new AmenitySearchObject
            {
                Name = txtName.Text
            };

            var amenities = await api.Get(search);

            dgvAmenities.DataSource = amenities;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            new frmAmenityDetails().ShowDialog();
        }


    }
}
