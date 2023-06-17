using System.Reflection;
using CProject.Event;
using CProject.Response;
using DomainShare;
using DomainShare.Bank;
using DomainShare.RequestInformation;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);


var bussConnection = builder.Configuration["AzureServiceBusConnection"];

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<ContactResponse>().Endpoint(cfg =>
    {
        cfg.Name = "Compliance_Advantage_Request_Queue";
    });
    x.AddConsumer<Consumer>();
    x.AddConsumer<BankAccountConsumer>();
    
    var entryAssembly = Assembly.GetEntryAssembly();
    
    x.AddSagaStateMachines(entryAssembly);
    x.AddSagas(entryAssembly);
    x.AddActivities(entryAssembly);
    
    x.UsingAzureServiceBus((context, config) =>
    {
        config.Host(bussConnection);
        

        config.SubscriptionEndpoint("Compliance_Advantage_Request_Queue","ComplyAdvantage-Topic-Request_Information", ep =>
        {
            ep.ConfigureConsumer<ContactResponse>(context);
        });
        
        config.SubscriptionEndpoint("ComplyAdvantage-Subscription","ComplyAdvantage-Topic", e =>
        {
           e.ConfigureConsumer<Consumer>(context);
        });
        
        config.SubscriptionEndpoint("ComplyAdvantage-Subscription", "ComplyAdvantage-Topic-BankAccount",e =>
        {
            e.RequiresSession = true;
            e.Consumer<BankAccountConsumer>();
        });
        
        config.ConfigureEndpoints(context);
        
    });
x.AddRequestClient<RequestInformation>();
   
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