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
    public class JobTypeService : BaseService<JobTypeResponse, JobType, BaseSearchObject>, IJobTypeService
    {
        public JobTypeService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        #region VALIDATIONS
        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateJobTypeExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateJobTypeExist(int id, StringBuilder errorMessage)
        {
            var user = context.JobTypes.FirstOrDefault(x => x.JobTypeId == id);
            if (user == null)
                errorMessage.Append("You entered a non existent job type!\n");
        }
        #endregion
    }
}
