using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            try
            {
                var entity = await $"{endpoint}/{resourceName}/login".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<UserResponse>();

                return entity;
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseJsonAsync<Dictionary<string, dynamic>>();

                var errors = errorResponse.First(x => x.Key == "errors");

                string errorsJsonString = String.Join(",", errors.Value);

                Dictionary<string, string[]> errorsMap = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsJsonString);

                var stringBuilder = new StringBuilder();
                foreach (var error in errorsMap)
                {
                    stringBuilder.AppendLine($"{error.Key}:\n{string.Join("\n", error.Value)}");
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(UserResponse);
            }
        }
    }
}
