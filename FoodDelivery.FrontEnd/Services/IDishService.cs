using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IDishService
    {
        Task<IEnumerable<Dish>> GetAll();
        Task<Dish> GetById(int id);
        Task Add(Dish dish);
        Task Update(Dish dish, int id);
        Task Delete(int id);
    }
}
