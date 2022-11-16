using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _account;
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Account>? Accounts { get; set; }
        public string Message { get; set; }

        public IndexModel(IAccountService account, ILogger<IndexModel> logger)
        {
            _account = account;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            try
            {
                Accounts = await _account.GetAll();
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
