using AutoMapper;
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
            //Facility
            CreateMap<Facility, FacilityResponse>();
            CreateMap<FacilityUpsertRequest, Facility>();
            //User
            CreateMap<User, UserResponse>();
            CreateMap<UserInsertRequest, User>();
            CreateMap<UserUpdateRequest, User>();
            //Role
            CreateMap<Role, RoleResponse>();
            //Job Type
            CreateMap<JobType, JobTypeResponse>();
            //Employee
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(x => x.OutlineText, opts => opts.MapFrom(y => $"{y.FirstName} {y.LastName} ({y.JobType.Name},{y.Gender},{DateTime.Now.Year - y.BirthDate.Value.Year})"))
                .ForMember(x => x.FacilityName, opts => opts.MapFrom(y => $"{y.Facility.Name}" ?? "Not Assigned"))
                .ForMember(x => x.JobName, opts => opts.MapFrom(y => y.JobType.Name));
            CreateMap<EmployeeInsertRequest, Employee>();
            CreateMap<EmployeeUpdateRequest, Employee>();
        }
    }
}
