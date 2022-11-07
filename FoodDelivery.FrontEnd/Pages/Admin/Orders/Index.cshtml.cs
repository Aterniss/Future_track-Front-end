using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _order;
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Order>? Orders { get; set; }
        public Order? OneOrder { get; set; }
        public Account? Account { get; set; }
        public string Message { get; set; }


        public IndexModel(IOrderService order, ILogger<IndexModel> logger)
        {
            this._order = order;
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
                var result = await _order.GetAll();
                Orders = result;
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
