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
    public class RoomService : BaseCRUDService<RoomResponse, Room, RoomSearchObject, RoomUpsertRequest, RoomUpsertRequest>, IRoomService
    {

        public RoomService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override void BeforeInsert(RoomUpsertRequest request, Room entity)
        {
            entity.State = RoomState.Draft.ToString();
            base.BeforeInsert(request, entity);
        }
        public override async Task<RoomResponse> Insert(RoomUpsertRequest request)
        {
            var entity = await base.Insert(request);

            var amenities = new List<RoomAmenity>();
            foreach (var id in request.AmenityIds)
            {
                amenities.Add(new RoomAmenity
                {
                    RoomId = entity.RoomId,
                    AmenityId = id
                });

            }
            await context.RoomAmenities.AddRangeAsync(amenities);
            await context.SaveChangesAsync();

            return await GetById(entity.RoomId);
        }

        public override void BeforeUpdate(RoomUpsertRequest request, Room entity)
        {
            base.BeforeUpdate(request, entity);
        }

        public override async Task<RoomResponse> Update(int id, RoomUpsertRequest request)
        {
            var entity = await base.Update(id, request);

            var amenities = new List<RoomAmenity>();
            foreach (var x in request.AmenityIds)
            {
                amenities.Add(new RoomAmenity
                {
                    RoomId = entity.RoomId,
                    AmenityId = x
                });

            }
            await context.RoomAmenities.AddRangeAsync(amenities);
            await context.SaveChangesAsync();

            return await GetById(entity.RoomId);
        }

        public override Task<string> Delete(int id)
        {
            var roomAmenities = context.RoomAmenities.Where(x => x.RoomId == id);

            context.RemoveRange(roomAmenities);

            return base.Delete(id);
        }

        public override async Task<RoomResponse> GetById(int id)
        {
            validateGetByIdRequest(id);
            var entity = await context.Rooms.Include("RoomType").Include("RoomAmenities.Amenity").FirstOrDefaultAsync(x => x.RoomId == id);

            return mapper.Map<RoomResponse>(entity);
        }

        public override IQueryable<Room> AddFilter(IQueryable<Room> query, RoomSearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (searchObject.Price > 0 && !String.IsNullOrEmpty(searchObject.Comparator))
            {
                switch (searchObject.Comparator)
                {
                    case "=":
                        filteredQuery = filteredQuery.Where(x => x.Price == searchObject.Price);
                        break;
                    case ">":
                        filteredQuery = filteredQuery.Where(x => x.Price > searchObject.Price);
                        break;
                    case "<":
                        filteredQuery = filteredQuery.Where(x => x.Price < searchObject.Price);
                        break;
                    case "<=":
                        filteredQuery = filteredQuery.Where(x => x.Price <= searchObject.Price);
                        break;
                    case ">=":
                        filteredQuery = filteredQuery.Where(x => x.Price >= searchObject.Price);
                        break;
                }
            }
            if (!String.IsNullOrEmpty(searchObject.Name))
            {
                filteredQuery = filteredQuery.Where(x => x.Name.ToLower().Contains(searchObject.Name.ToLower()));
            }
            if (searchObject.RoomTypeId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.RoomTypeId == searchObject.RoomTypeId);
            }
            if (!String.IsNullOrEmpty(searchObject.State))
            {
                filteredQuery = filteredQuery.Where(x => x.State.ToLower().Equals(searchObject.State.ToLower()));
            }

            return filteredQuery;
        }

        public override IQueryable<Room> AddInclude(IQueryable<Room> query, RoomSearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            if (searchObject.IncludeAmenities)
            {
                includedQuery = includedQuery.Include("RoomAmenities.Amenity");
            }
            if (searchObject.IncludeRoomType)
            {
                includedQuery = includedQuery.Include("RoomType");
            }

            return includedQuery;
        }

        public async Task<string> Activate(int id)
        {
            validateActivateRequest(id);

            var entity = await context.Rooms.FirstOrDefaultAsync(x => x.RoomId == id);

            entity.State = RoomState.Active.ToString();

            await context.SaveChangesAsync();

            return $"Room {entity.Name} has been activated successfully!";
        }

        public async Task<string> Hide(int id)
        {
            validateHideRequest(id);

            var entity = await context.Rooms.FirstOrDefaultAsync(x => x.RoomId == id);

            entity.State = RoomState.Hidden.ToString();

            await context.SaveChangesAsync();

            return $"Room {entity.Name} has been hidden successfully!";

        }
        public async Task<string> SetAsTaken(int id)
        {
            validateSetAsTakenRequest(id);

            var entity = await context.Rooms.FirstOrDefaultAsync(x => x.RoomId == id);

            entity.State = RoomState.Taken.ToString();

            await context.SaveChangesAsync();

            return $"Room {entity.Name} has been set as taken successfully!";
        }
        public async Task<RoomResponse> RemoveAmenity(int roomId, int amenityId)
        {
            validateRemoveAmenityRequest(roomId,amenityId);

            var roomAmentiy = context.RoomAmenities.FirstOrDefault(x => x.RoomId == roomId && x.AmenityId == amenityId);

            context.Remove(roomAmentiy);
            await context.SaveChangesAsync();

            return await GetById(roomId);
        }

        #region VALIDATIONS

        public override void validateInsertRequest(RoomUpsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateAmenitiesExist(request.AmenityIds, errorMessage);
            validatRoomTypeExist(request.RoomTypeId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());

        }

        public override void validateUpdateRequest(int id, RoomUpsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateRoomExist(id, errorMessage);
            validateAmenitiesExist(request.AmenityIds, errorMessage);
            validatRoomTypeExist(request.RoomTypeId, errorMessage);
            validateIsActive(id, errorMessage);
            validateAmenitiesAlreadyAssigned(request.AmenityIds, id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateRoomExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateActivateRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateRoomExist(id, errorMessage);
            validateIsNotActive(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateHideRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateRoomExist(id, errorMessage);
            validateIsNotHidden(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateSetAsTakenRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateRoomExist(id, errorMessage);
            validateIsNotTaken(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateRemoveAmenityRequest(int roomId, int amenityId)
        {
            var errorMessage = new StringBuilder();

            validateRoomExist(roomId, errorMessage);
            validateAmenitiesExist(new List<int>(amenityId), errorMessage);
            validateRoomAmenityExist(roomId, amenityId, errorMessage);
            validateIsActive(roomId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateIsNotTaken(int id, StringBuilder errorMessage)
        {
            var room = context.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room != null && room.State == RoomState.Taken.ToString())
                errorMessage.Append("The room is already taken!\n");
        }

        private void validateAmenitiesExist(List<int> amenityIds, StringBuilder errorMessage)
        {
            var amenities = context.Amenities.Where(x => amenityIds.Contains(x.AmenityId));
            if (amenities.Count() != amenityIds.Count)
                errorMessage.Append("You entered non existent amenities!\n");
        }
        private void validateAmenitiesAlreadyAssigned(List<int> amenityIds, int roomId, StringBuilder errorMessage)
        {
            var amenities = context.RoomAmenities.Where(x => x.RoomId == roomId && amenityIds.Contains(x.AmenityId));
            if (amenities.Count() > 0)
                errorMessage.Append("You entered already assigned amenities!\n");
        }

        private void validatRoomTypeExist(int id, StringBuilder errorMessage)
        {
            var roomType = context.RoomTypes.FirstOrDefault(x => x.RoomTypeId == id);
            if (roomType == null)
                errorMessage.Append("You entered a non existent room type!\n");
        }

        private void validateRoomExist(int id, StringBuilder errorMessage)
        {
            var room = context.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room == null)
                errorMessage.Append("You entered a non existent room!\n");
        }
        private void validateIsNotActive(int id, StringBuilder errorMessage)
        {
            var room = context.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room != null && room.State == RoomState.Active.ToString())
                errorMessage.Append("The room is already active!\n");
        }

        private void validateIsActive(int id, StringBuilder errorMessage)
        {
            var room = context.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room != null && room.State == RoomState.Active.ToString())
                errorMessage.Append("You can't edit active rooms!\n");
        }
        private void validateIsNotHidden(int id, StringBuilder errorMessage)
        {
            var room = context.Rooms.FirstOrDefault(x => x.RoomId == id);
            if (room!=null && room.State == RoomState.Hidden.ToString())
                errorMessage.Append("The room is already hidden!\n");
        }
        private void validateRoomAmenityExist(int roomId, int amenityId, StringBuilder errorMessage)
        {
            var roomAmenity = context.RoomAmenities.FirstOrDefault(x => x.RoomId == roomId && x.AmenityId == amenityId);
            if (roomAmenity == null)
                errorMessage.Append("The room does't have this amenity!\n");
        }

        #endregion
    }
}
