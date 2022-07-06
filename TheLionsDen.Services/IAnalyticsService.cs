using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;

namespace TheLionsDen.Services
{
    public interface IAnalyticsService
    {
        Task<List<ChartResponse>> employeesPerJobType();
        Task<List<ChartResponse>> roomsPerRoomType();
        Task<List<LineChartResponse>> yearlyRevenue();
        Task<List<RevenueChartResponse>> revenuePerRoomType();
    }
}
