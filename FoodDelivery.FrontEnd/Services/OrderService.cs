using System.Text;
using System.Text.Json;
using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using Polly;
using Polly.Retry;

namespace FoodDelivery.FrontEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private const int MaxRetries = 3;
        private const string Message = "Sorry,the service is unavailable!";
        private readonly AsyncRetryPolicy _retryPolicy;
        public OrderService(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromMilliseconds(100));
        }

        public async Task Add(Order order)
        {
            var url = string.Format($"/orders/");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(order);
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
            var url = string.Format($"/orders/{id}");
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

        public async Task<IEnumerable<Order>> GetAll()
        {
            var url = string.Format($"/orders");
            var result = new List<Order>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(stringResponse,
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

        public async Task<IEnumerable<Order>> GetAllId(int restaurantId)
        {
            var url = string.Format($"/orders/restaurant/{restaurantId}");
            var result = new List<Order>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(stringResponse,
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

        public async Task<Order> GetById(int id)
        {
            var url = string.Format($"/orders/{id}");
            var result = new Order();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {

                        var stringResponse = await response.Content.ReadAsStringAsync();

                        result = System.Text.Json.JsonSerializer.Deserialize<Order>(stringResponse,
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

        public async Task Update(OrderUpdateRequest order, int id)
        {
            var url = string.Format($"/orders/{id}");
            try
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(order);
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

