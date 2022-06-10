using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TheLionsDen.Model;
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
            entity.State = "New";
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

        public override async Task<RoomResponse> GetById(int id)
        {
            validateGetByIdRequest(id);
            var entity = await context.Rooms.Include("RoomType").Include("RoomAmenities.Amenity").FirstOrDefaultAsync(x => x.RoomId == id);

            return mapper.Map<RoomResponse>(entity); ;
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
            //TO DO check existing amenities

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

        private void validateAmenitiesExist(List<int> amenityIds, StringBuilder errorMessage)
        {
            var amenities = context.Amenities.Where(x => amenityIds.Contains(x.AmenityId));
            if (amenities.Count() != amenityIds.Count)
                errorMessage.Append("You entered non existent amenities!\n"); ;
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
        #endregion
    }
}
