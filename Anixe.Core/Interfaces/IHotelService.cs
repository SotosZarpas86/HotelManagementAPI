using Anixe.Core.Models;

namespace AnixeService.Interfaces
{
    public interface IHotelService
    {
        Task<IList<HotelModel>> GetAll();

        Task<IList<HotelModel>> Get(string name);

        Task<HotelModel> Create(HotelCreateModel model);
    }
}
