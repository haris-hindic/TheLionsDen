using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model;
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

        #region VALIDATIONS
        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateRoleExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateRoleExist(int id, StringBuilder errorMessage)
        {
            var role = context.Roles.FirstOrDefault(x => x.RoleId == id);
            if (role == null)
                errorMessage.Append("You entered a non existent role!\n");
        }
        #endregion
    }
}
