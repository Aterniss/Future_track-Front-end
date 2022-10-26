using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IZoneService
    {
        Task<IEnumerable<Zone>> GetAll();
        Task<Zone> GetById(int id);
        Task Add(Zone zone);
        Task Update(Zone zone, int id);
        Task Delete(int id);
    }
}
