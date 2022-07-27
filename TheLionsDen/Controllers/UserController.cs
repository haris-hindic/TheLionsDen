using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Auth;
using TheLionsDen.Model;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseCRUDController<UserResponse, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        private readonly IUserService service;

        public UserController(IUserService service) : base(service)
        {
            this.service = service;
        }

        [HttpGet("login"), AllowAnonymous]
        public async Task<UserResponse> Login()
        {
            var credentials = CredentialsHelper.extractCredentials(Request);

            return await service.Login(credentials.Username, credentials.Password);
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<UserResponse> Register([FromBody]UserInsertRequest request)
        {
            return await service.Register(request);
        }

        [Authorize(Roles = "Employee,Customer")]
        public override Task<UserResponse> GetById(int id)
        {
            return base.GetById(id);
        }

        public override async Task<string> Delete(int id)
        {
            var credentials = CredentialsHelper.extractCredentials(Request);
            var user = await service.Login(credentials.Username, credentials.Password);
            if (user.UserId == id)
                throw new UserException("You can't delete your profile!");

            return await service.Delete(id);
        }
    }
}
