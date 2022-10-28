using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetAllId(int restaurantId);
        Task<Order> GetById(int id);
        Task Add(Order order);
        Task Update(OrderUpdateRequest order, int id);
        Task Delete(int id);
    }
}
