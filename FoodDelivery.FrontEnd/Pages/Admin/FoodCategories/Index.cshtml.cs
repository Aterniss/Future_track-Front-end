using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.FoodCategories
{
    public class IndexModel : PageModel
    {
        private readonly IFoodCategoryService _category;
        public IEnumerable<FoodCategory> Categories { get; set; }
        public Account? Account { get; set; }
       

        public IndexModel(IFoodCategoryService category)
        {
            this._category = category;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            var result = await _category.GetAll();
            Categories = result;
            Account = check;
            return Page();

        }
    }
}
