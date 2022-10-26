using System.Text;
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

        public async Task Add(Order order)
        {
            var url = string.Format($"/orders/");
            var userString = JsonSerializer.Serialize(order);
            var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, requestContent);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var url = string.Format($"/orders/{id}");
            var response = await client.DeleteAsync(url);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var url = string.Format($"/orders");
            var result = new List<Order>();
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
        }

        public async Task<Order> GetById(int id)
        {
            var url = string.Format($"/orders/{id}");
            var result = new Order();
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
        }

        public async Task Update(Order order, int id)
        {
            var url = string.Format($"/orders/{id}");
            var userString = JsonSerializer.Serialize(order);
            var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, requestContent);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }
    }

        
    }

