using Microsoft.AspNetCore.Mvc;

namespace TFoodDelivery.FrontEnd.Models.Requests;

public class UserRequest
{
    [BindProperty]
    public string FullName { get; set; } = null!;
    [BindProperty]
    public string UserAddress { get; set; } = null!;
    [BindProperty]
    public bool? IsOver18 { get; set; }
}
