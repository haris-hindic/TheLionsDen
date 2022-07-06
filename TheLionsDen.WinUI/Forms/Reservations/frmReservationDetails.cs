using TheLionsDen.Model.Enums;
using TheLionsDen.Model.Responses;
using TheLionsDen.WinUI.Services;

namespace TheLionsDen.WinUI.Forms.Reservations
{
    public partial class frmReservationDetails : Form
    {
        private ReservationResponse reservation;
        private ReservationAPI reservationAPI;
        public frmReservationDetails()
        {
            InitializeComponent();
        }

        public frmReservationDetails(ReservationResponse reservation)
        {
            InitializeComponent();
            this.reservation = reservation;
            reservationAPI = new ReservationAPI();

        }

        private void frmReservationDetails_Load(object sender, EventArgs e)
        {
            populateFields();
            checkButtons();
        }

        private void checkButtons()
        {
            if (reservation != null && reservation.Status != ReservationStatus.Active.ToString())
            {
                deactivateButtons();
            }
        }

        private void deactivateButtons()
        {
            btnCancel.Enabled = false;
            btnConfirm.Enabled = false;
        }

        private void populateFields()
        {
            txtName.Text = reservation.UserFullName;
            txtEmail.Text = reservation.User.Email;
            txtPhone.Text = reservation.User.PhoneNumber;
            txtRoom.Text = reservation.RoomName;
            numRoomNo.Value = (decimal)reservation.Room.Number;
            dtpArrival.Value = reservation.Arrival;
            dtpDeparture.Value = reservation.Departure;
            txtEstArrTime.Text = reservation.EstimatedArrivalTime;
            txtSpecialReq.Text = reservation.SpecialRequests;
            txtFacilities.Text = reservation.FacilityNames;
            lblPrice.Text = $"Total price: {reservation.TotalPrice}$";
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure that you want to confirm this reservation ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                var response = await reservationAPI.Confirm(reservation.ReservationId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    deactivateButtons();
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure that you want to cancel this reservation ??", "Confirm Delete!!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                var response = await reservationAPI.Cancel(reservation.ReservationId);
                if (!String.IsNullOrEmpty(response))
                {
                    MessageBox.Show(response);
                    deactivateButtons();
                }
            }
        }
    }
}
