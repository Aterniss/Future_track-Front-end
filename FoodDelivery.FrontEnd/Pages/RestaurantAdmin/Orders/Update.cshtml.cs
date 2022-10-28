using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.RestaurantAdmin.Orders
{
    public class UpdateModel : PageModel
    {
        private readonly IOrderService _order;
        public Order OneOrder;
        public Account? Account { get; set; }
        public string Status { get; set; }
        public int Rider { get; set; }
        public int User { get; set; }
        public string Message { get; set; }


        public UpdateModel(IOrderService order)
        {
            this._order = order;
        }


        public async Task<IActionResult> OnGet(int id, string status, int rider, int user)
        {
            var check = HttpContext.Session.GetObject<Account>("Restaurant");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Status = status;
            Rider = rider;
            User = user;

            var result = await _order.GetById(id);
            OneOrder = result;
            return Page();
        }

        public async Task<IActionResult> OnPostSubmit(OrderUpdateRequest request, int id)
        {
            try
            {
                await _order.Update(request, id);
                Message = $"Succesfully updated!";
                return Redirect("/RestaurantAdmin/Orders");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }

        }
    }
}
