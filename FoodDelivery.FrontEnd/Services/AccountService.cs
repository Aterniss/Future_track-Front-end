using FoodDelivery.FrontEnd.Models;
using Polly;
using Polly.Retry;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private const int MaxRetries = 3;
        private const string Message = "Sorry the service is unavailable!";
        private readonly AsyncRetryPolicy _retryPolicy;
        public AccountService(IConfiguration configuration)
        {
            this._configuration = configuration;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromMilliseconds(100));
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
            try
            {
                 return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await _client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<Account>(stringResponse,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                });
                
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }

            
        }
    }
}
