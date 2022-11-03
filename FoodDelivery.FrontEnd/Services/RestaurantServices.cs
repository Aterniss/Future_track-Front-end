using TFoodDelivery.FrontEnd.Models;
using System.Text.Json;
using System.Text;
using Polly.Retry;
using Polly;

namespace FoodDelivery.FrontEnd.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private const int MaxRetries = 3;
        private const string Message = "Sorry the service is unavailable!";
        private readonly AsyncRetryPolicy _retryPolicy;
        public RestaurantServices(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromMilliseconds(100));
        }
        public async Task<RestaurantModel> GetById(int id = 1)
        {
            var url = string.Format($"/restaurants/{id}");
            var result = new RestaurantModel();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<RestaurantModel>(stringResponse,
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

        public async Task<IEnumerable<RestaurantModel>> GetAll()
        {
            var url = string.Format($"/restaurants");
            var result = new List<RestaurantModel>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<RestaurantModel>>(stringResponse,
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

        public async Task Add(RestaurantModel restaurant)
        {
            var url = string.Format($"/restaurants/");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(restaurant);
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
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
            
        }

        public async Task Update(RestaurantModel restaurant, int id)
        {
            var url = string.Format($"/restaurants/{id}");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(restaurant);
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
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
            
        }

        public async Task Delete(int id)
        {
            var url = string.Format($"/restaurants/{id}");
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
            catch (Exception)
            {
                throw new HttpRequestException(Message);
            }
            
        }
    }
}
