using FoodDelivery.FrontEnd.Models;
using FoodDelivery.FrontEnd.Models.Requests;
using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.FrontEnd.Pages.Admin.Riders
{
    public class AddModel : PageModel
    {
        private readonly IRiderService _rider;
        private readonly IZoneService _zoneService;

        public IEnumerable<Zone> Zones { get; set; }
        public string Message { get; set; }

        public AddModel(IRiderService rider, IZoneService zoneService)
        {
            this._rider = rider;
            this._zoneService = zoneService;
        }
        public async Task<IActionResult> OnGet()
        {

            var check = HttpContext.Session.GetObject<Account>("Admin");
            if (check == null)
            {
                return Redirect("/Index");
            }
            Zones = await _zoneService.GetAll();
            return Page();

        }
        public async Task<IActionResult> OnPostSubmit(RiderRequest request)
        {
            try
            {
                var dish = new Rider()
                {
                    RiderName = request.RiderName,
                    ZoneId = request.ZoneId
                };
                await _rider.Add(dish);
                return Redirect("/Admin/Riders/Index");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }


        }
    }
}
