using DomainShare;
using DomainShare.RequestInformation;
using MassTransit;
using PProject.Log;

var builder = WebApplication.CreateBuilder(args);

var bussConnection = builder.Configuration["AzureServiceBusConnection"];

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    
    x.UsingAzureServiceBus((context, config) =>
    {
        config.Host(bussConnection);
       
        config.ConfigureEndpoints(context);
        
        config.Message<Order>(y =>
        {
            y.SetEntityName("Order-Topic");
        });
        config.Message<Contact>(y =>
        {
            y.SetEntityName("ComplyAdvantage-Topic");
        });
        config.Message<RequestInformation>(y =>
        {
            y.SetEntityName("ComplyAdvantage-Topic-Request_Information");
        });
    });
    x.AddConsumer<LogEvent>();
   
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();