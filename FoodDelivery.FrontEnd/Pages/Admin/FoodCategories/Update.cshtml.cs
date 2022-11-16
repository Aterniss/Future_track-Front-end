using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.FoodCategories
{
    public class UpdateModel : PageModel
    {
        private readonly IFoodCategoryService _category;
        public Account? Account { get; set; }
        public FoodCategoryRequest? Request { get; set; }
        public string Name { get; set; }
        public FoodCategory Category { get; set; }
        public string Message { get; set; }
        public UpdateModel(IFoodCategoryService category)
        {
            this._category = category;
        }
        public async Task<IActionResult> OnGetAsync(string name)
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");

            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            var category = await _category.GetByName(name);
            Category = category;
            Name = name;
            return Page();
        }
        public async Task<IActionResult> OnPostSubmit(FoodCategoryRequest request, string name)
        {
            try
            {
                var category = new FoodCategory()
                {
                    CategoryName = name,
                    CategoryDescription = request.CategoryDescription
                };
                await _category.Update(category, name);
                return Redirect("/Admin/FoodCategories/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

        }
    }
}
