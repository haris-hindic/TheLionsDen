using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TheLionsDen.Model;
using TheLionsDen.Model.Enums;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class ReservationService : BaseCRUDService<ReservationResponse, Reservation, ReservationSearchObject, ReservationInsertRequest, object>, IReservationService
    {
        public ReservationService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override void BeforeInsert(ReservationInsertRequest request, Reservation entity)
        {
            entity.Status = "Active";

            base.BeforeInsert(request, entity);
        }

        public async override Task<ReservationResponse> Insert(ReservationInsertRequest request)
        {
            var entity = await base.Insert(request);

            var facilites = new List<ReservationFacilities>();
            foreach (var id in request.FacilityIds)
            {
                facilites.Add(new ReservationFacilities
                {
                    ReservationId = entity.ReservationId,
                    FacilityId = id
                });

            }
            await context.ReservationFacilities.AddRangeAsync(facilites);
            await context.SaveChangesAsync();

            return await GetById(entity.ReservationId);
        }

        public override Task<string> Delete(int id)
        {
            var reservationFacilities = context.ReservationFacilities.Where(x => x.ReservationId == id);

            context.RemoveRange(reservationFacilities);

            return base.Delete(id);
        }

        public override async Task<ReservationResponse> GetById(int id)
        {
            validateGetByIdRequest(id);
            var entity = await context.Reservations.Include("User.Role").Include("ReservationFacilities.Facility")
                .Include("Room.RoomType").Include("PaymentDetails").FirstOrDefaultAsync(x => x.ReservationId == id);

            return mapper.Map<ReservationResponse>(entity);
        }

        public override IQueryable<Reservation> AddFilter(IQueryable<Reservation> query, ReservationSearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (searchObject.Date != null && !String.IsNullOrEmpty(searchObject.Comparator))
            {
                switch (searchObject.Comparator)
                {
                    case "=":
                        filteredQuery = filteredQuery.Where(x => x.Arrival == searchObject.Date &&
                                                        x.Departure == searchObject.Date);
                        break;
                    case ">":
                        filteredQuery = filteredQuery.Where(x => x.Arrival >= searchObject.Date &&
                                                        x.Departure >= searchObject.Date);
                        break;
                    case "<":
                        filteredQuery = filteredQuery.Where(x => x.Arrival <= searchObject.Date &&
                                                        x.Departure <= searchObject.Date);
                        break;
                }
            }
            if (!String.IsNullOrEmpty(searchObject.Status))
            {
                filteredQuery = filteredQuery.Where(x => x.Status.ToLower().Equals(searchObject.Status.ToLower()));
            }
            if (searchObject.UserId>0)
            {
                filteredQuery = filteredQuery.Where(x => x.UserId == searchObject.UserId);
            }

            return filteredQuery;
        }

        public override IQueryable<Reservation> AddInclude(IQueryable<Reservation> query, ReservationSearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            if (searchObject.IncludeUser)
            {
                includedQuery = includedQuery.Include("User.Role");
            }
            if (searchObject.IncludeRoom)
            {
                includedQuery = includedQuery.Include("Room.RoomType");
            }
            if (searchObject.IncludePaymentDetails)
            {
                includedQuery = includedQuery.Include("PaymentDetails");
            }
            if (searchObject.IncludeFacilites)
            {
                includedQuery = includedQuery.Include("ReservationFacilities.Facility");
            }

            return includedQuery;
        }

        public async Task<string> Confirm(int id)
        {
            validateConfirmCancelRequest(id);
            var reservation = context.Reservations.FirstOrDefault(x => x.ReservationId == id);

            reservation.Status = ReservationStatus.Confirmed.ToString();
            await context.SaveChangesAsync();

            return "Reservation confirmed successfully!";
        }

        public async Task<string> Cancel(int id)
        {
            validateConfirmCancelRequest(id);

            var reservation = context.Reservations.FirstOrDefault(x => x.ReservationId == id);

            reservation.Status = ReservationStatus.Cancelled.ToString();
            await context.SaveChangesAsync();

            return "Reservation cancelled successfully!";
        }

        #region VALIDATIONS

        public override void validateInsertRequest(ReservationInsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateUserExist(request.UserId, errorMessage);
            validatRoomExist(request.RoomId, errorMessage);
            validatFacilitiesExist(request.FacilityIds, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateGetByIdRequest(int reservationId)
        {
            var errorMessage = new StringBuilder();

            validateReservationExist(reservationId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public void validateConfirmCancelRequest(int reservationId)
        {
            var errorMessage = new StringBuilder();

            validateReservationExist(reservationId, errorMessage);
            validateIsActive(reservationId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }


        private void validateReservationExist(int reservationId, StringBuilder errorMessage)
        {
            var reservation = context.Reservations.FirstOrDefault(x => x.ReservationId == reservationId);
            if (reservation == null)
                errorMessage.Append("You entered a non existent reservation!\n");
        }

        private void validatFacilitiesExist(List<int> facilityIds, StringBuilder errorMessage)
        {
            var amenities = context.Facilities.Where(x => facilityIds.Contains(x.FacilityId));
            if (amenities.Count() != facilityIds.Count)
                errorMessage.Append("You entered non existent amenities!\n");
        }

        private void validatRoomExist(int roomId, StringBuilder errorMessage)
        {
            var room = context.Rooms.FirstOrDefault(x => x.RoomId == roomId);
            if (room == null)
                errorMessage.Append("You entered a non existent room!\n");
        }

        private void validateIsActive(int reservationId, StringBuilder errorMessage)
        {
            var reservation = context.Reservations.FirstOrDefault(x => x.ReservationId == reservationId);
            if (reservation == null)
                errorMessage.Append("The reservation needs to be active!\n");
        }

        private void validateUserExist(int userId, StringBuilder errorMessage)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                errorMessage.Append("You entered a non existent user!\n");
        }

        #endregion
    }
}
