using FoodDelivery.FrontEnd.Models;
using Polly;
using Polly.Retry;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class FoodCategoryService : IFoodCategoryService
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private const int MaxRetries = 3;
        private const string Message = "Sorry,the service is unavailable!";
        private readonly AsyncRetryPolicy _retryPolicy;
        public FoodCategoryService(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromMilliseconds(100));
        }

        public async Task Add(FoodCategory category)
        {
            var url = string.Format($"/food-category/");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(category);
                    var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, requestContent);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }
            
        }

        public async Task Delete(string name)
        {
            var url = string.Format($"/food-category/{name}");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.DeleteAsync(url);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }
            
        }

        public async Task<IEnumerable<FoodCategory>> GetAll()
        {
            var url = string.Format($"/food-category");
            var result = new List<FoodCategory>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<FoodCategory>>(stringResponse,
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
            catch (HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }
            
        }

        public async Task<FoodCategory> GetByName(string name)
        {
            var url = string.Format($"/food-category/{name}");
            var result = new FoodCategory();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<FoodCategory>(stringResponse,
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
            catch (HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }
            
        }

        public async Task Update(FoodCategory dish, string name)
        {
            var url = string.Format($"/food-category/{name}");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(dish);
                    var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url, requestContent);
                    var msg = response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode == false)
                    {
                        throw new Exception(msg.Result);
                    }
                    response.EnsureSuccessStatusCode();
                });
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }
            
        }
    }
}
