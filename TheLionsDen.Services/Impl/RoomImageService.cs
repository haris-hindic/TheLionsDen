using AutoMapper;
using System.Text;
using TheLionsDen.Model;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class RoomImageService : BaseCRUDService<RoomImageResponse, RoomImage, RoomImageSearchObject, RoomImageInsertRequest, RoomImageInsertRequest>, IRoomImageService
    {
        public RoomImageService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<RoomImage> AddFilter(IQueryable<RoomImage> query, RoomImageSearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (searchObject.RoomTypeId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.RoomTypeId == searchObject.RoomTypeId);
            }

            return filteredQuery;
        }
        #region VALIDATIONS

        public override void validateInsertRequest(RoomImageInsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateRoomTypeExist(request.RoomTypeId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateRoomTypeExist(int roomTypeId, StringBuilder errorMessage)
        {
            var roomType = context.RoomTypes.FirstOrDefault(x => x.RoomTypeId == roomTypeId);
            if (roomType == null)
                errorMessage.Append("You entered a non existent room type!\n");
        }
        #endregion
    }
}
