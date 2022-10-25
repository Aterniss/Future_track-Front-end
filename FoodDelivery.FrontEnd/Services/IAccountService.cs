using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IAccountService
    {
        Task<Account> GetAll(); 
        Task<Account> GetById(int id);
        Task<Account> Login(string username, string password);

    }
}
