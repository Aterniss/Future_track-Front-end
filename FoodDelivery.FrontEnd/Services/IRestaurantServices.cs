using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IRestaurantServices
    {
        Task<IEnumerable<RestaurantModel>> GetAll();
        Task<RestaurantModel> GetById(int id);

    }
}
