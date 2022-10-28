using TFoodDelivery.FrontEnd.Models;
using System.Text.Json;
using System.Text;

namespace FoodDelivery.FrontEnd.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        public RestaurantServices(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
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
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);

            }
        }

        public async Task<IEnumerable<RestaurantModel>> GetAll()
        {
            var url = string.Format($"/restaurants");
            var result = new List<RestaurantModel>();
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
        }

        public async Task Add(RestaurantModel restaurant)
        {
            var url = string.Format($"/restaurants/");
            var userString = JsonSerializer.Serialize(restaurant);
            var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, requestContent);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(RestaurantModel restaurant, int id)
        {
            var url = string.Format($"/restaurants/{id}");
            var userString = JsonSerializer.Serialize(restaurant);
            var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, requestContent);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var url = string.Format($"/restaurants/{id}");
            var response = await client.DeleteAsync(url);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
        }
    }
}
