using FoodDelivery.FrontEnd.Models;
using Polly;
using Polly.Retry;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class ZoneService : IZoneService
    {
        private readonly HttpClient client;
        private readonly IConfiguration _configuration;
        private const int MaxRetries = 3;
        private const string Message = "Sorry,the service is unavailable!";
        private readonly AsyncRetryPolicy _retryPolicy;
        public ZoneService(IConfiguration configuration)
        {
            this._configuration = configuration;
            client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["AppSettings:BaseAPIUrl"])
            };
            _retryPolicy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(MaxRetries, t => TimeSpan.FromMilliseconds(100));
        }
        public async Task Add(Zone zone)
        {
            var url = string.Format($"/zones/");
            try
            {
                await _retryPolicy.ExecuteAsync(async () => {
                    var userString = JsonSerializer.Serialize(zone);
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
            var url = string.Format($"/zones/{id}");
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

        public async Task<IEnumerable<Zone>> GetAll()
        {
            var url = string.Format($"/zones");
            var result = new List<Zone>();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var stringResponse = await result.Content.ReadAsStringAsync();

                        return System.Text.Json.JsonSerializer.Deserialize<List<Zone>>(stringResponse,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    }
                    else
                    {
                        var msg = result.Content.ReadAsStringAsync();

                        throw new Exception(msg.Result);
                    }
                });
            }
            catch(HttpRequestException)
            {
                throw new HttpRequestException(Message);
            }

           
            
        }

        public async Task<Zone> GetById(int id)
        {
            var url = string.Format($"/dishes/{id}");
            var result = new Zone();
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        var stringResponse = await result.Content.ReadAsStringAsync();

                        return System.Text.Json.JsonSerializer.Deserialize<Zone>(stringResponse,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    }
                    else
                    {
                        var msg = result.Content.ReadAsStringAsync();

                        throw new Exception(msg.Result);
                    }
                });
            }
            catch (HttpRequestException)
            {
                throw new Exception(Message);
            }
        }

        public async Task Update(Zone zone, int id)
        {
            var url = string.Format($"/zones/{id}");
            try
            {
                 await _retryPolicy.ExecuteAsync(async () =>
                {
                    var userString = JsonSerializer.Serialize(zone);
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
            catch(HttpRequestException)
            {
                throw new Exception(Message);
            }
        }
    }
}
