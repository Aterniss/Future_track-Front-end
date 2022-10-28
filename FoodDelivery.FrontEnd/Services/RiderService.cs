using FoodDelivery.FrontEnd.Models;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class RiderService : IRiderService
    {
        private static readonly HttpClient client;
        static RiderService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147/")
            };
        }
        public async Task Add(Rider rider)
        {
            var url = string.Format($"/riders/");
            var userString = JsonSerializer.Serialize(rider);
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
            var url = string.Format($"/riders/{id}");
            var response = await client.DeleteAsync(url);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Rider>> GetAll()
        {
            var url = string.Format($"/riders");
            var result = new List<Rider>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<Rider>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);
            }
        }

        public async Task<IEnumerable<Rider>> GetAllId(int restaurantId)
        {
            var url = string.Format($"/riders/restaurant/{restaurantId}");
            var result = new List<Rider>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<Rider>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);
            }
        }

        public async Task<Rider> GetById(int id)
        {
            var url = string.Format($"/riders/{id}");
            var result = new Rider();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<Rider>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);

            }
        }

        public async Task Update(Rider rider, int id)
        {
            var url = string.Format($"/riders/{id}");
            var userString = JsonSerializer.Serialize(rider);
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
