using AutoMapper;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class RoomTypeService : BaseCRUDService<RoomTypeResponse, RoomType, RoomTypeSearchObject, RoomTypeUpsertRequest, RoomTypeUpsertRequest>, IRoomTypeService
    {
        public RoomTypeService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<RoomType> AddInclude(IQueryable<RoomType> query, RoomTypeSearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            //includedQuery = includedQuery.Include("RoomImages");

            return includedQuery;
        }

        public override IQueryable<RoomType> AddFilter(IQueryable<RoomType> query, RoomTypeSearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if (searchObject.Capacity > 0 && !String.IsNullOrEmpty(searchObject.Comparator))
            {
                switch (searchObject.Comparator)
                {
                    case "=":
                        filteredQuery = filteredQuery.Where(x => x.Capacity == searchObject.Capacity);
                        break;
                    case ">":
                        filteredQuery = filteredQuery.Where(x => x.Capacity > searchObject.Capacity);
                        break;
                    case "<":
                        filteredQuery = filteredQuery.Where(x => x.Capacity < searchObject.Capacity);
                        break;
                    case "<=":
                        filteredQuery = filteredQuery.Where(x => x.Capacity <= searchObject.Capacity);
                        break;
                    case ">=":
                        filteredQuery = filteredQuery.Where(x => x.Capacity >= searchObject.Capacity);
                        break;
                }
            }
            if (!String.IsNullOrEmpty(searchObject.Name))
            {
                filteredQuery = filteredQuery.Where(x => x.Name.ToLower().Contains(searchObject.Name.ToLower()));
            }

            return filteredQuery;
        }
    }
}
