

using FoodDelivery.FrontEnd.Services;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddMemoryCache();
builder.Services.AddMvc();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IRestaurantServices, RestaurantServices>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IDishService, DishService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IFoodCategoryService, FoodCategoryService>();
builder.Services.AddSingleton<IZoneService, ZoneService>();
builder.Services.AddSingleton<IRiderService, RiderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
