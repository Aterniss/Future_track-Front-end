using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IRiderService
    {
        Task<IEnumerable<Rider>> GetAll();
        Task<Rider> GetById(int id);
        Task Add(Rider rider);
        Task Update(Rider rider, int id);
        Task Delete(int id);
    }
}
