using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TheLionsDen.Model;
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
            if (searchObject.RoleId > 0)
            {
                filteredQuery = filteredQuery.Where(x => x.RoleId == searchObject.RoleId);

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

        public override void BeforeInsert(UserInsertRequest request, User entity)
        {
            var salt = PasswordHelper.GenerateSalt();
            var hash = PasswordHelper.GenerateHash(salt, request.Password);

            entity.PasswordSalt = salt;
            entity.PasswordHash = hash;

            entity.Status = "New";

            base.BeforeInsert(request, entity);
        }

        public override async Task<UserResponse> Insert(UserInsertRequest request)
        {
            if (request.PasswordConfirmation != request.Password)
                throw new Model.UserException("Password and Confirmation must be the same!");

            var entity = await base.Insert(request);

            await context.SaveChangesAsync();

            return await GetById(entity.UserId);
        }

        public override void BeforeUpdate(UserUpdateRequest request, User entity)
        {
            entity.Status = "Modified";

            if(!String.IsNullOrEmpty(request.Password) && !String.IsNullOrEmpty(request.PasswordConfirmation))
            {
                if (request.PasswordConfirmation != request.Password)
                    throw new Model.UserException("Password and Confirmation must be the same!");

                var salt = PasswordHelper.GenerateSalt();
                var hash = PasswordHelper.GenerateHash(salt, request.Password);

                entity.PasswordSalt = salt;
                entity.PasswordHash = hash;
            }

            base.BeforeUpdate(request, entity);
        }

        public override async Task<UserResponse> Update(int id, UserUpdateRequest request)
        {
            await base.Update(id, request);

            return await GetById(id);
        }

        public override async Task<UserResponse> GetById(int id)
        {
            validateGetByIdRequest(id);

            var entity = await context.Users.Include("Role").FirstOrDefaultAsync(x => x.UserId == id);

            return mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse> Login(string username, string password)
        {
            var entity = await context.Users.Include("Role").FirstOrDefaultAsync(x => x.Username.Equals(username));

            if (entity == null)
            {
                throw new Model.UserException("Not valid credentials!");
            }

            var hash = PasswordHelper.GenerateHash(entity.PasswordSalt, password);

            if (hash != entity.PasswordHash)
            {
                throw new Model.UserException("Not valid credentials!");
            }

            return mapper.Map<UserResponse>(entity);
        }

        public override Task<string> Delete(int id)
        {
            var reservations = context.Reservations.Where(x => x.UserId == id);
            var reservationFacilites = context.Favourites.Where(x => x.UserId == id);

            context.RemoveRange(reservations);
            context.RemoveRange(reservationFacilites);

            return base.Delete(id);
        }

        #region VALIDATIONS

        public override void validateInsertRequest(UserInsertRequest request)
        {
            var errorMessage = new StringBuilder();

            validateRoleExist(request.RoleId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateUpdateRequest(int id, UserUpdateRequest request)
        {
            var errorMessage = new StringBuilder();

            validateUserExist(id,errorMessage);
            validateRoleExist(request.RoleId, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        public override void validateGetByIdRequest(int id)
        {
            var errorMessage = new StringBuilder();

            validateUserExist(id, errorMessage);

            if (errorMessage.Length > 0)
                throw new UserException(errorMessage.ToString());
        }

        private void validateUserExist(int id, StringBuilder errorMessage)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
                errorMessage.Append("You entered a non existent user!\n");
        }
        private void validateRoleExist(int roleId, StringBuilder errorMessage)
        {
            var role = context.Roles.FirstOrDefault(x => x.RoleId == roleId);
            if (role == null)
                errorMessage.Append("You entered a non existent role!\n");
        }
        #endregion
    }
}
