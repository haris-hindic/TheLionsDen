using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Services.Database;

namespace TheLionsDen.Services.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Amenity
            CreateMap<Amenity, AmenityResponse>();
            CreateMap<AmenityUpsertRequest, Amenity>();
        }
    }
}
