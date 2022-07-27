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
            //Room Type
            CreateMap<RoomType, RoomTypeResponse>();
            CreateMap<RoomTypeUpsertRequest,RoomType>();
            //Room Image
            CreateMap<RoomImage, RoomImageResponse>();
            CreateMap<RoomImageInsertRequest, RoomImage>();
            //Room
            CreateMap<Room, RoomResponse>()
                .ForMember(x => x.Amenities, opts => opts.MapFrom(y => String.Join(", ",y.RoomAmenities.Select(j=>j.Amenity.Name))))
                .ForMember(x => x.RoomTypeName, opts => opts.MapFrom(y => $"{y.RoomType.Name} ({y.RoomType.Capacity} persons, {y.RoomType.Size} m2)" ))
                .ForMember(x => x.CoverImage, opts => opts.MapFrom(y => y.RoomType.RoomImages.FirstOrDefault().Image));
            CreateMap<RoomUpsertRequest, Room>();
            //RoomAmenity
            CreateMap<RoomAmenity, RoomAmenityResponse>();
            //Reservation
            CreateMap<Reservation, ReservationResponse>()
                .ForMember(x=>x.RoomName, opts => opts.MapFrom(y=> $"{y.Room.Name} ({y.Room.RoomType.Name})"))
                .ForMember(x=>x.UserFullName, opts => opts.MapFrom(y=> $"{y.User.FirstName} {y.User.LastName}"))
                .ForMember(x=>x.FacilityNames, opts => opts.MapFrom(y=> String.Join(", ",y.ReservationFacilities.Select(z=>z.Facility.Name))));
            CreateMap<ReservationInsertRequest, Reservation>();
            //PaymentDetails
            CreateMap<PaymentDetail, PaymentDetailResponse>();
            CreateMap<PaymentDetailsInsertRequest, PaymentDetail>();
            //Reservation Facilities
            CreateMap<ReservationFacilities, ReservationFaciliteResponse>();
        }
    }
}
