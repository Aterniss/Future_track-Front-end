using System.Text.Json;
using TFoodDelivery.FrontEnd.Models;

namespace FoodDelivery.FrontEnd.Services
{
    public class UserService : IUserService
    {
        private static readonly HttpClient client;

        static UserService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147/")
            };
        }

        public async Task Add(User user)
        {
            try
            {
                var url = string.Format($"/users/");
                await client.GetAsync(url);
            }
            catch(HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public async Task Delete(int id)
        {
            try
            {
                var url = string.Format($"/users/{id}");
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
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
                throw new HttpRequestException(response.ReasonPhrase);
                
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
                throw new HttpRequestException(response.ReasonPhrase);
            }

            
        }

        public async Task Update(User user, int id)
        {
            try
            {
                var url = string.Format($"/users/update/{id}");
                await client.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
