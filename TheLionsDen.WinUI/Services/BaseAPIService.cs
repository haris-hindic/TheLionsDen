﻿using Flurl;
using Flurl.Http;
using System.Text;
using System.Text.Json;
using TheLionsDen.Model;
using WinUI.Helpers;

namespace WinUI.Services
{
    public class BaseAPIService<T, TSearch>
        where T : class where TSearch : class 
    {
        public readonly string resourceName;
        public readonly string endpoint = "https://localhost:7070";

        public BaseAPIService(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public async Task<IEnumerable<T>> Get(TSearch search = null)
        {
            try
            {
                var query = "";
                if (search != null)
                {
                    query = await search.ToQueryString();
                }
                var list = await $"{endpoint}/{resourceName}".SetQueryParams(query).WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<IEnumerable<T>>();

                return list;
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
                return default(IEnumerable<T>);
            }

        }

        public async Task<T> GetById(int id)
        {
            try
            {
                var entity = await $"{endpoint}/{resourceName}/{id}".WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<T>();

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
                return default(T);
            }
        }
    }
}