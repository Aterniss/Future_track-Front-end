using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.FoodCategories
{
    public class DeleteModel : PageModel
    {
        private readonly IFoodCategoryService _category;
        public string Name { get; set; }

        public DeleteModel(IFoodCategoryService category)
        {
            this._category = category;
        }

        public Account? Account { get; set; }

        public IActionResult OnGetAsync(string name)
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Name = name;
            return Page();
        }
        public IActionResult OnPostBack()
        {
            return Redirect("/Admin/FoodCategories");
        }
        public async Task<IActionResult> OnPostDelete(string name)
        {
            await _category.Delete(name);
            return Redirect("/Admin/FoodCategories");

        }
    }
}
