using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Responses;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Impl
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly TheLionsDenContext context;
        private readonly IMapper mapper;

        public AnalyticsService(TheLionsDenContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<List<ChartResponse>> employeesPerJobType()
        {
            var employees =await  context.Employees.ToListAsync();
            var jobTypes = await context.JobTypes.ToListAsync();

            var response = new List<ChartResponse>();

            foreach (var job in jobTypes)
            {
                response.Add(new ChartResponse
                {
                    Label = job.Name,
                    Value = employees.Where(x=>x.JobTypeId==job.JobTypeId).Count()
                });
            }


            return response.OrderByDescending(x => x.Value).ToList();
        }

        public async Task<List<RevenueChartResponse>> revenuePerRoomType()
        {
            var roomTypes = await context.RoomTypes.ToListAsync();

            var response = new List<RevenueChartResponse>();

            foreach (var room in roomTypes)
            {
                response.Add(new RevenueChartResponse
                {
                    Label = room.Name,
                    Value = context.Reservations.Include("Room").Where(x => x.Room.RoomTypeId == room.RoomTypeId).Sum(x=>x.TotalPrice)
                });
            }

            return response.OrderByDescending(x => x.Value).ToList();
        }

        public async Task<List<ChartResponse>> roomsPerRoomType()
        {
            var employees = await context.Rooms.ToListAsync();
            var roomTypes = await context.RoomTypes.ToListAsync();

            var response = new List<ChartResponse>();

            foreach (var room in roomTypes)
            {
                response.Add(new ChartResponse
                {
                    Label = room.Name,
                    Value = employees.Where(x => x.RoomTypeId == room.RoomTypeId).Count()
                });
            }


            return response.OrderByDescending(x=>x.Value).ToList();
        }

        public async Task<List<LineChartResponse>> yearlyRevenue()
        {
            List<double> years = getLast5Years();

            var response = new List<LineChartResponse>();

            foreach (var y in years)
            {
                response.Add(new LineChartResponse
                {
                    xValue = y,
                    yValue = context.Reservations.Where(x=> x.Departure.Year == (int)y).Sum(x=>x.TotalPrice)
                });
            }


            return response.OrderByDescending(x => x.xValue).ToList();
        }

        private List<double> getLast5Years()
        {
            var lastFiveYears = new List<double>();
            var thisYear = (double)DateTime.Now.Year;
            lastFiveYears.Add(thisYear);
            for (int i = 1; i < 5; i++)
            {
                lastFiveYears.Add(thisYear - i);
            }
            return lastFiveYears;
        }
    }
}
