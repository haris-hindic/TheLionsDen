using Flurl;
using Flurl.Http;
using System.Text;
using System.Text.Json;
using TheLionsDen.Model;
using WinUI.Helpers;

namespace WinUI.Services
{
    public class BaseAPIService<T, TSearch, TInsert, TUpdate>
        where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        public readonly string resourceName;
        public readonly string endpoint = "https://localhost:7070";

        public BaseAPIService(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public async Task<IEnumerable<T>> Get(TSearch search = null)
        {
            var query = "";
            if (search != null)
            {
                query = await search.ToQueryString();
            }
            var list = await $"{endpoint}/{resourceName}".SetQueryParams(query).WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<IEnumerable<T>>();

            return list;
        }

        public async Task<T> GetById(int id)
        {
            var entity = await $"{endpoint}/{resourceName}/{id}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<T>();

            return entity;
        }

        public async Task<string> Delete(int id)
        {
            var entity = await $"{endpoint}/{resourceName}/{id}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).DeleteAsync().ReceiveString();

            return entity;
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
                var errors = await ex.GetResponseJsonAsync<Dictionary<string, dynamic>>();

                var err = errors.First(x => x.Key == "errors");

                string s = String.Join(",", err.Value);

                Dictionary<string, string[]> json = JsonSerializer.Deserialize<Dictionary<string, string[]>>(s);

                var stringBuilder = new StringBuilder();
                foreach (var error in json)
                {
                    stringBuilder.AppendLine($"{error.Key}:\n{string.Join("\n", error.Value)}");
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }
        }

        public async Task<T> Put(object id, TUpdate request)
        {
            var entity = await $"{endpoint}/{resourceName}/{id}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).PutJsonAsync(request).ReceiveJson<T>();

            return entity;
        }
    }
}
