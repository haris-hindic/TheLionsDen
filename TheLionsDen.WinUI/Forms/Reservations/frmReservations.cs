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
using TheLionsDen.WinUI.Forms.Reservations;
using TheLionsDen.WinUI.Helpers;
using TheLionsDen.WinUI.Services;

namespace WinUI.Forms.Reservations
{
    public partial class frmReservations : Form
    {
        private ReservationAPI reservationAPI;
        public frmReservations()
        {
            InitializeComponent();
            reservationAPI = new ReservationAPI();
            dgvReservations.AutoGenerateColumns = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadReservations();
        }

        private async void loadReservations()
        {
            var request = populateSearchRequest();

            if (cmbStatus.SelectedValue != null && cmbStatus.SelectedIndex != -1)
            {
                request.Status = (string)cmbStatus.SelectedValue;
            }
            if (cmbComparator.SelectedValue != null && cmbComparator.SelectedIndex != -1)
            {
                request.Comparator = (string)cmbComparator.SelectedValue;
            }

            var response = await reservationAPI.Get(request);

            dgvReservations.DataSource = response;
        }

        private ReservationSearchObject populateSearchRequest() => new ReservationSearchObject()
        {
            IncludeFacilites = true,
            IncludePaymentDetails = true,
            IncludeRoom = true,
            IncludeUser = true,
            Date = dtpDate.Value
        };

        private void frmReservations_Load(object sender, EventArgs e)
        {
            cmbComparator.DataSource = cmbHelper.comparatorLite;
            cmbComparator.SelectedIndex = -1;
            cmbStatus.DataSource = cmbHelper.reservationStatus;
            cmbStatus.SelectedIndex = -1;
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = -1;
            cmbComparator.SelectedIndex = -1;
            dtpDate.Value = DateTime.Now;
        }

        private void dgvReservations_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new frmReservationDetails(dgvReservations.Rows[e.RowIndex].DataBoundItem as ReservationResponse).ShowDialog();
        }
    }
}
