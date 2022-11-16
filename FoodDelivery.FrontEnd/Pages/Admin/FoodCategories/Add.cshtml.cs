using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.FoodCategories
{
    public class AddModel : PageModel
    {
        private readonly IFoodCategoryService _category;
        public Account? Account { get; set; }
        public FoodCategoryRequest? Request { get; set; }
        public string Message { get; set; }

        public AddModel(IFoodCategoryService category)
        {
            this._category = category;
        }
        public IActionResult OnGet()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Account = check;
            return Page();

        }
        public async Task<IActionResult> OnPostSubmit(FoodCategoryRequest request)
        {
            try
            {
                var category = new FoodCategory()
                {
                    CategoryName = request.CategoryName,
                    CategoryDescription = request.CategoryDescription
                };
                await _category.Add(category);
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
