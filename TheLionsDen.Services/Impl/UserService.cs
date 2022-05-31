using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services.Database;
using TheLionsDen.Services.Helpers;

namespace TheLionsDen.Services.Impl
{
    public class UserService : BaseCRUDService<UserResponse, Database.User, UserSearchObject, UserInsertRequest, UserUpdateRequest>, IUserService
    {
        public UserService(TheLionsDenContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<User> AddFilter(IQueryable<User> query, UserSearchObject searchObject = null)
        {
            var filteredQuery = base.AddFilter(query, searchObject);

            if(!String.IsNullOrWhiteSpace(searchObject.Name))
            {
                filteredQuery = filteredQuery.Where(x => x.FirstName.ToLower().Contains(searchObject.Name.ToLower()) ||
                                                       x.LastName.ToLower().Contains(searchObject.Name.ToLower()));
            }
            if (!String.IsNullOrWhiteSpace(searchObject.Username))
            {
                filteredQuery = filteredQuery.Where(x => x.Username.Equals(searchObject.Username));
            }
            if (searchObject.Active)
            {
                filteredQuery = filteredQuery.Where(x => x.Status.Equals("ACTIVE"));

            }

            return filteredQuery;
        }

        public override IQueryable<User> AddInclude(IQueryable<User> query, UserSearchObject searchObject = null)
        {
            var includedQuery = base.AddInclude(query, searchObject);

            if (searchObject.IncludeRoles)
            {
                includedQuery = includedQuery.Include("Role");
            }

            return includedQuery;
        }

        public override void BeforeInsert(UserInsertRequest request, User entitiy)
        {
            var salt = PasswordHelper.GenerateSalt();
            var hash = PasswordHelper.GenerateHash(salt, request.Password);

            entitiy.PasswordSalt = salt;
            entitiy.PasswordHash = hash;

            base.BeforeInsert(request, entitiy);
        }

        public UserResponse Login(string username, string password)
        {
            var entity = context.Users.Include("Role").FirstOrDefault(x => x.Username.Equals(username));

            if (entity == null)
            {
                return null;
            }

            var hash = PasswordHelper.GenerateHash(entity.PasswordSalt, password);

            if (hash != entity.PasswordHash)
            {
                return null;
            }

            return mapper.Map<UserResponse>(entity);
        }
    }
}
