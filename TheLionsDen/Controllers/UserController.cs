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
    public class UserController : BaseCRUDController<UserResponse, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        private readonly IUserService service;

        public UserController(IUserService service) : base(service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Administrator")]
        public override Task<UserResponse> Insert([FromBody] UserInsertRequest request)
        {
            return base.Insert(request);
        }
        [Authorize(Roles = "Administrator")]
        public override Task<UserResponse> Update(int id, [FromBody] UserUpdateRequest request)
        {
            return base.Update(id, request);
        }
        [Authorize(Roles = "Administrator")]
        public override Task<IEnumerable<UserResponse>> Get([FromQuery] UserSearchObject searchObject)
        {
            return base.Get(searchObject);
        }
        [Authorize(Roles = "Administrator,Customer")]
        public override Task<UserResponse> GetById(int id)
        {
            return base.GetById(id);
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

        [HttpPut("customer/{id}"),Authorize(Roles = "Customer,Administrator")]
        public async Task<UserResponse> CustomerUpdate(int id,[FromBody] UserUpdateRequest request)
        {
            return await service.CustomerUpdate(id,request);
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
