using System.Text;
using System.Text.Json;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
        }

        public async Task Add(User user)
        {
            var url = string.Format($"/users/");
            var userString = JsonSerializer.Serialize(user);
            var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, requestContent);
            var msg = response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
        }

        public async Task Delete(int id)
        {
            var url = string.Format($"/users/{id}");
            var response = await client.DeleteAsync(url);
            var msg = response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var url = string.Format($"users");
            var result = new List<User>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<User>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();
                throw new Exception(msg.Result);
            } 
        }

        public async Task<User> GetById(int id)
        {
            var url = string.Format($"/users/{id}");
            var result = new User();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<User>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();
                throw new Exception(msg.Result);
            }

            
        }

        public async Task<User> GetByName(string name)
        {
            var url = string.Format($"/users/get-by-name/{name}");
            var result = new User();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<User>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();
                throw new Exception(msg.Result);
            }

            
        }

        public async Task Update(User user, int id)
        {
            var url = string.Format($"/users/{id}");
            var userString = JsonSerializer.Serialize(user);
            var requestContent = new StringContent(userString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, requestContent);
            var msg = response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }

        }
    }
}
