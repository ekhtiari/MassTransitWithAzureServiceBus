using DomainShare;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PProject.Controllers;

public class OrderController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    // GET
    public IActionResult Index()
    {

        var newOrder = new Order() { Id = Guid.NewGuid(), AmountId = 1000, AmountOut = 100000, Rate = 10 };

        _publishEndpoint.Publish(newOrder);
        
        return Json("ok");
    }
}