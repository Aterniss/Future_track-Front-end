using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.FoodCategories
{
    public class IndexModel : PageModel
    {
        private readonly IFoodCategoryService _category;
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<FoodCategory> Categories { get; set; }
        public Account? Account { get; set; }
        public string Message { get; set; }


        public IndexModel(IFoodCategoryService category, ILogger<IndexModel> logger)
        {
            this._category = category;
            _logger = logger;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            try
            {
                var result = await _category.GetAll();
                Categories = result;
                Account = check;
                return Page();
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message);
                Message = e.Message;
                return Page();
            }
            

        }
    }
}
