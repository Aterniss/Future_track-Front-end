using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;

namespace FoodDelivery.FrontEnd.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAll(); 
        Task<Account> GetById(int id);
        Task<Account> Login(string username, string password);
        Task Update(Account account, int accountId);
        Task Delete(int accountId);
        Task Register(AccountRequest request);
        
    }
}
