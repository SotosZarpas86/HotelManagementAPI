using Anixe.Core.Entities;
using Anixe.Core.Models;
using AutoMapper;

namespace Anixe.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelModel>();
            CreateMap<HotelCreateModel, Hotel>();

            CreateMap<Booking, BookingModel>().ReverseMap();
            CreateMap<BookingCreateModel, Booking>();
        }
    }
}
