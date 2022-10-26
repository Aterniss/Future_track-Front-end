﻿using FoodDelivery.FrontEnd.Models;
using Microsoft.AspNetCore.HttpLogging;
using System.Text;
using System.Text.Json;

namespace FoodDelivery.FrontEnd.Services
{
    public class DishService : IDishService
    {
        private static readonly HttpClient client;
        static DishService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7147/")
            };
        }


        public async Task Add(Dish dish)
        {
            var url = string.Format($"/dishes/");
            var userString = JsonSerializer.Serialize(dish);
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
            
            var url = string.Format($"/dishes/{id}");
            var response = await client.DeleteAsync(url);
            var msg = response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(msg.Result);
            }
            response.EnsureSuccessStatusCode();
            
            
        }

        public async Task<IEnumerable<Dish>> GetAll()
        {
            var url = string.Format($"/dishes");
            var result = new List<Dish>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<Dish>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result); 
            }

            
        }

        public async Task<Dish> GetById(int id)
        {
            var url = string.Format($"/dishes/{id}");
            var result = new Dish();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<Dish>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return result;
            }
            else
            {
                var msg = response.Content.ReadAsStringAsync();

                throw new Exception(msg.Result);

            }
        }

        public async Task Update(Dish dish, int id)
        {
           var url = string.Format($"/dishes/{id}");
           var userString = JsonSerializer.Serialize(dish);
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