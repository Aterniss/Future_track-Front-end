using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Riders
{
    public class IndexModel : PageModel
    {
        private readonly IRiderService _rider;
        public IEnumerable<Dish>? Dishes { get; set; }
        public Dish? OneDish { get; set; }
        public Account? Account { get; set; }


        public IndexModel(IDishService dish)
        {
            this._dish = dish;
        }


        public async Task<IActionResult> OnGetAsync()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            var result = await _dish.GetAll();
            Dishes = result;
            Account = check;
            return Page();

        }
    }
}
