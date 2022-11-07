using FoodDelivery.FrontEnd.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Console()

    .CreateBootstrapLogger();

try
{


var builder = WebApplication.CreateBuilder(args);

    Log.Information("Starting up");
    //add serilog

    builder.Host.UseSerilog();

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
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);


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

    app.UseSerilogRequestLogging();
    app.MapRazorPages();
    app.Run();
    }
catch(Exception ex)
{
    Log.Fatal(ex, "Unhandeled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}