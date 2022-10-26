using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IFoodCategoryService
    {
        Task<IEnumerable<FoodCategory>> GetAll();
        Task<FoodCategory> GetByName(string name);
        Task Add(FoodCategory dish);
        Task Update(FoodCategory dish, string name);
        Task Delete(string name);
    }
}
