using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IRestaurantServices
    {
        Task<IEnumerable<RestaurantModel>> GetAll();
        Task<RestaurantModel> GetById(int id);
        Task Add(RestaurantModel restaurant);
        Task Update(RestaurantModel restaurant, int id);
        Task Delete(int id);
    }
}
