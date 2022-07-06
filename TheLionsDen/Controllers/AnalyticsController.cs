using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;
using TheLionsDen.Services;

namespace TheLionsDen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService service;

        public AnalyticsController(IAnalyticsService service)
        {
            this.service = service;
        }

        [HttpGet("employee-jobtype")]
        public async Task<List<ChartResponse>> EmployeesPerRoomType()
        {
            return await service.employeesPerJobType();
        }

        [HttpGet("room-roomtype")]
        public async Task<List<ChartResponse>> RoomsPerRoomType()
        {
            return await service.roomsPerRoomType();
        }

        [HttpGet("yearly-revenue")]
        public async Task<List<LineChartResponse>> YearlyRevenue()
        {
            return await service.yearlyRevenue();
        }

        [HttpGet("roomtype-revenue")]
        public async Task<List<RevenueChartResponse>> RevenuePerRoomType()
        {
            return await service.revenuePerRoomType();
        }
    }
}
