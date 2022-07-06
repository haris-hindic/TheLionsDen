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
using WinUI.Services;

namespace TheLionsDen.WinUI.Services
{
    public class ReservationAPI : CRUDAPIService<ReservationResponse, ReservationSearchObject, ReservationInsertRequest, object>
    {
        public ReservationAPI(string resourceName="reservation") : base(resourceName)
        {
        }

        public async Task<string> Cancel(int id)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}/{id}/Cancel".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutAsync(null).ReceiveString();

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
                return default(string);
            }
        }

        public async Task<string> Confirm(int id)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}/{id}/Confirm".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutAsync(null).ReceiveString();

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
                return default(string);
            }
        }
    }
}
