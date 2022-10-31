using FoodDelivery.FrontEnd.Models;
using Polly;
using Polly.Retry;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class DishService : IDishService
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private const int MaxRetries = 3;
        private const string Message = "Sorry,the service is unavailable!";
        private readonly AsyncRetryPolicy _retryPolicy;
        public DishService(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromMilliseconds(100));
        }
        public async Task Add(Dish dish)
        {
            var url = string.Format($"/dishes/");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(dish);
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

        public async Task Delete(int id)
        {
            var url = string.Format($"/dishes/{id}");
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

        public async Task<IEnumerable<Dish>> GetAll()
        {
            var url = string.Format($"/dishes");
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var result = new List<Dish>();
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<Dish>>(stringResponse,
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

        public async Task<IEnumerable<Dish>> GetAllId(int restaurantId)
        {
            var url = string.Format($"/dishes/restaurant/{restaurantId}");
            try
            {
                var result = new List<Dish>();
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<Dish>>(stringResponse,
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

        public async Task<Dish> GetById(int id)
        {
            var url = string.Format($"/dishes/{id}");
            var result = new Dish();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<Dish>(stringResponse,
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

        public async Task Update(Dish dish, int id)
        {
           var url = string.Format($"/dishes/{id}");
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
