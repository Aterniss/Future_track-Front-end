using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Dishes
{
    public class IndexModel : PageModel
    {
        private readonly IDishService _dish;
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Dish>? Dishes { get; set; }
        public Dish? OneDish { get; set; }
        public Account? Account { get; set; }
        public string Message { get; set; }


        public IndexModel(IDishService dish, ILogger<IndexModel> logger)
        {
            this._dish = dish;
            this._logger = logger;
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
                var result = await _dish.GetAll();
                Dishes = result;
                Account = check;
                return Page();
            }
            catch(HttpRequestException e)
            {
                _logger.LogError(e.Message);
                Message = e.Message;
                return Page();
            }
            

        }
    }
}
