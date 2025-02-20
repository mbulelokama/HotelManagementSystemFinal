using AutoMapper;

namespace hotelManagementSystem.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Hotel, Dtos.HotelDto>();
            CreateMap<Models.Room, Dtos.RoomDto>();
            CreateMap<Models.Booking, Dtos.BookingDto>();
            CreateMap<Dtos.HotelDto, Models.Hotel>();
            CreateMap<Dtos.RoomDto, Models.Room>();
            CreateMap<Dtos.BookingDto, Models.Booking>();
        }
    }
}
