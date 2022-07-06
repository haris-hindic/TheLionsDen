using Flurl.Http;
using System.Text;
using System.Text.Json;
using TheLionsDen.WinUI.Helpers;

namespace TheLionsDen.WinUI.Services
{
    public class CRUDAPIService<T, TSearch, TInsert, TUpdate> : BaseAPIService<T, TSearch>
        where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        public CRUDAPIService(string resourceName) : base(resourceName)
        {
        }

        public virtual async Task<string> Delete(int id)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}/{id}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).DeleteAsync().ReceiveString();

                return entity;
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseJsonAsync<Dictionary<string, dynamic>>();

                var errors = errorResponse.First(x => x.Key == "errors");

                string errorsJsonString = string.Join(",", errors.Value);

                Dictionary<string, string[]> errorsMap = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsJsonString);

                var stringBuilder = new StringBuilder();
                foreach (var error in errorsMap)
                {
                    stringBuilder.AppendLine($"{error.Key}:\n{string.Join("\n", error.Value)}");
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public async Task<T> Post(TInsert request)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PostJsonAsync(request).ReceiveJson<T>();

                return entity;
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseJsonAsync<Dictionary<string, dynamic>>();

                var errors = errorResponse.First(x => x.Key == "errors");

                string errorsJsonString = string.Join(",", errors.Value);

                Dictionary<string, string[]> errorsMap = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsJsonString);

                var stringBuilder = new StringBuilder();
                foreach (var error in errorsMap)
                {
                    stringBuilder.AppendLine($"{error.Key}:\n{string.Join("\n", error.Value)}");
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
        }

        public virtual async Task<T> Put(object id, TUpdate request)
        {
            try
            {

                var entity = await $"{endpoint}/{resourceName}/{id}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutJsonAsync(request).ReceiveJson<T>();

                return entity;
            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseJsonAsync<Dictionary<string, dynamic>>();

                var errors = errorResponse.First(x => x.Key == "errors");

                string errorsJsonString = string.Join(",", errors.Value);

                Dictionary<string, string[]> errorsMap = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsJsonString);

                var stringBuilder = new StringBuilder();
                foreach (var error in errorsMap)
                {
                    stringBuilder.AppendLine($"{error.Key}:\n{string.Join("\n", error.Value)}");
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
        }
    }
}
