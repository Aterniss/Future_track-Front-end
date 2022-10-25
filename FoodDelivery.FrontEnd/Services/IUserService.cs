using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByName(string name);
        Task Add(User user);
        Task Update(User user, int id);
        Task Delete(int id);
    }
}
