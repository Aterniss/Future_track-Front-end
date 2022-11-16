using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using Polly;
using Polly.Retry;
using System.Text;
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
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromSeconds(1));
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var url = string.Format($"/accounts");
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var result = new List<Account>();
                    var response = await _client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<Account>>(stringResponse,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        return result;
                    }
                    else
                    {
                        var msg = response.Content.ReadAsStringAsync();

                        throw new Exception(msg.Result);
                    }
                });
            }
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
        }

        public async Task<Account> GetById(int id)
        {
            var url = string.Format($"/accounts/{id}");
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
                        var msg = response.Content.ReadAsStringAsync();

                        throw new Exception(msg.Result);

                    }
                });
            }
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
        }

        public async Task<Account> Login(string username, string password)
        {
            var hashedPassword = PHasher.HashPassword(password);
            var url = string.Format($"/accounts/{username}, {hashedPassword}");
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
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }

            
        }

        public async Task Add(Account account)
        {
            var url = string.Format($"/accounts/");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(account);
                    var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync(url, requestContent);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
        }

        public async Task Update(Account account, int accountId)
        {
            var url = string.Format($"/accounts/{accountId}");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(account);
                    var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
                    var response = await _client.PutAsync(url, requestContent);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
        }

        public async Task Delete(int accountId)
        {
            var url = string.Format($"/accounts/{accountId}");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await _client.DeleteAsync(url);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
        }

        public async Task Register(AccountRequest request)
        {
            var hashedPassword = PHasher.HashPassword(request.UserPassword);
            
            var newAccount = new AccountRequest()
            {
                FullName = request.FullName,
                EmailAddress = request.EmailAddress,
                IsOver18 = request.IsOver18,
                UserName = request.UserName,
                UserPassword = hashedPassword,
                UserAddress = request.UserAddress,
                IdUsers = request.IdUsers,
                RestaurantId = request.RestaurantId,
                Role = request.Role,
                TelNumber = request.TelNumber
            };
            var url = string.Format($"/accounts/register");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(newAccount);
                    var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync(url, requestContent);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
        }
    }
}
