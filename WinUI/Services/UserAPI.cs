using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using WinUI.Helpers;

namespace WinUI.Services
{
    public class UserAPI : CRUDAPIService<UserResponse, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        public UserAPI(string resourceName="user") : base(resourceName)
        {
        }
        public async Task<UserResponse> Login()
        {
            var entity = await $"{endpoint}/{resourceName}/login".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<UserResponse>();

            return entity;
        }
    }
}
