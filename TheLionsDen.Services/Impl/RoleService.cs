using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class RoleService : BaseService<RoleResponse, Role, BaseSearchObject>, IRoleService
    {
        public RoleService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
