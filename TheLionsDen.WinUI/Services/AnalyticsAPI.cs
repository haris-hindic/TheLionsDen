using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;
using TheLionsDen.WinUI.Helpers;

namespace TheLionsDen.WinUI.Services
{
    public class AnalyticsAPI
    {
        public readonly string resourceName;
        public readonly string endpoint = "https://localhost:7070";

        public AnalyticsAPI(string resourceName="analytics")
        {
            this.resourceName = resourceName;
        }

        public async Task<List<ChartResponse>> GetEmployeesPerRoomType()
        {
            try
            {
                var query = "";
                var list = await $"{endpoint}/{resourceName}/employee-jobtype".SetQueryParams(query).WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<List<ChartResponse>>();

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
                return default(List<ChartResponse>);
            }

        }

        public async Task<List<ChartResponse>> RoomsPerRoomType()
        {
            try
            {
                var query = "";
                var list = await $"{endpoint}/{resourceName}/room-roomtype".SetQueryParams(query).WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<List<ChartResponse>>();

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
                return default(List<ChartResponse>);
            }

        }

        public async Task<List<LineChartResponse>> YearlyRevenue()
        {
            try
            {
                var query = "";
                var list = await $"{endpoint}/{resourceName}/yearly-revenue".SetQueryParams(query).WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<List<LineChartResponse>>();

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
                return default(List<LineChartResponse>);
            }

        }

        public async Task<List<RevenueChartResponse>> RevenuePerRoomType()
        {
            try
            {
                var query = "";
                var list = await $"{endpoint}/{resourceName}/roomtype-revenue".SetQueryParams(query).WithBasicAuth(AuthHelper.Username, AuthHelper.Password).GetJsonAsync<List<RevenueChartResponse>>();

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
                return default(List<RevenueChartResponse>);
            }

        }
    }
}
