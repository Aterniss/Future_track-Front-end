using TFoodDelivery.FrontEnd.Models;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private static readonly HttpClient client;

        static RestaurantServices()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147/")
            };
        }
        public async Task<RestaurantModel> GetById(int id = 1)
        {
            var url = string.Format($"/restaurants/{id}");
            var result = new RestaurantModel();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<RestaurantModel>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        public async Task<IEnumerable<RestaurantModel>> GetAll()
        {
            var url = string.Format("/restaurants");
            var result = new List<RestaurantModel>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<RestaurantModel>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
