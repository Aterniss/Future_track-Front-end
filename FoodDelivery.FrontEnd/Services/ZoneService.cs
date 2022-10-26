using FoodDelivery.FrontEnd.Models;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class ZoneService : IZoneService
    {
        private static readonly HttpClient client;
        static ZoneService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147/")
            };
        }
        public async Task Add(Zone zone)
        {
            var url = string.Format($"/zones/");
            var userString = JsonSerializer.Serialize(zone);
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
            var url = string.Format($"/zones/{id}");
            var response = await client.DeleteAsync(url);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Zone>> GetAll()
        {
            var url = string.Format($"/zones");
            var result = new List<Zone>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<Zone>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);
            }
        }

        public async Task<Zone> GetById(int id)
        {
            var url = string.Format($"/dishes/{id}");
            var result = new Zone();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<Zone>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);

            }
        }

        public async Task Update(Zone zone, int id)
        {
            var url = string.Format($"/zones/{id}");
            var userString = JsonSerializer.Serialize(zone);
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
