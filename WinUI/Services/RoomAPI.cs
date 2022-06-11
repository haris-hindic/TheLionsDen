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
    public class RoomAPI : CRUDAPIService<RoomResponse, RoomSearchObject, RoomUpsertRequest, RoomUpsertRequest>
    {
        public RoomAPI(string resourceName="room") : base(resourceName)
        {
        }

        public async Task<string> Activate(int id)
        {
            try
            {

                var entity = await $"{endpoint}/{resourceName}/{id}/Activate".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutAsync(null).ReceiveString();

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

        public async Task<string> Hide(int id)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}/{id}/Hide".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutAsync(null).ReceiveString();

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

        public async Task<string> SetAsTaken(int id)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}/{id}/Taken".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutAsync(null).ReceiveString();

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
