using CProject.Event;
using CProject.Response;
using DomainShare;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


var bussConnection = builder.Configuration["AzureServiceBusConnection"];

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<ContactResponse>();
    x.AddConsumer<Consumer>();
    
    x.UsingAzureServiceBus((context, config) =>
    {
        config.Host(bussConnection);

        config.ReceiveEndpoint("Compliance_Advantage_Request_Queue", ep =>
        {
            ep.ConfigureConsumer<ContactResponse>(context);
        });
        
        config.SubscriptionEndpoint("ComplyAdvantage-Subscription","ComplyAdvantage-Topic", e =>
        {
           e.ConfigureConsumer<Consumer>(context);
        });
        
    });

   
});


builder.Services.AddMassTransitHostedService();


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