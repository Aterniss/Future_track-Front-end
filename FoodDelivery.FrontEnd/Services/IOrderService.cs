using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task Add(Order order);
        Task Update(Order order, int id);
        Task Delete(int id);
    }
}
