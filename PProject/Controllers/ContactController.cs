using DomainShare;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PProject.Controllers;

public class ContactController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ContactController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var newContact = new Contact() { Id = Guid.NewGuid(), Name = "John", Famili = "Week", };

        await _publishEndpoint.Publish(newContact);
        return Json("ok");
    }
}