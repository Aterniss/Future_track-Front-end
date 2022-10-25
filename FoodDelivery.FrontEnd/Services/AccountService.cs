using FoodDelivery.FrontEnd.Models;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class AccountService : IAccountService
    {
        private static readonly HttpClient _client;
        static AccountService()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147")
            };
        }

        public Task<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> Login(string username, string password)
        {
            var url = string.Format($"/accounts/{username}, {password}");
            var result = new Account();
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<Account>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
