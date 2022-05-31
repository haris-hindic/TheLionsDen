using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Auth;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;
using TheLionsDen.Services.Helpers;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController,AllowAnonymous]
    public class UserController : BaseCRUDController<UserResponse, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        private readonly IUserService service;

        public UserController(IUserService service) : base(service)
        {
            this.service = service;
        }

        [HttpGet("login")]
        public UserResponse Login()
        {
            var credentials = CredentialsHelper.extractCredentials(Request);

            return service.Login(credentials.Username, credentials.Password);
        }
    }
}
