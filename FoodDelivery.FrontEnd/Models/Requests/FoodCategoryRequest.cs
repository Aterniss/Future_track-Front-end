using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.FrontEnd.Models.Requests
{
    public class FoodCategoryRequest
    {
        [BindProperty]
        public string CategoryName { get; set; } = null!;
        [BindProperty]
        public string CategoryDescription { get; set; } = null!;
    }
}
