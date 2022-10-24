using System.Text.Json;
using FoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public class OrderService : IOrderService
    {
        private static readonly HttpClient client;

        static OrderService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147")
            };
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var url = string.Format("/orders");
            var result = new List<Order>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<Order>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        public Task<Order> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

        
    }

