using DomainShare;
using DomainShare.Response;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PProject.Controllers;

public class ContactController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;

    private IRequestClient<Contact> _requestClient;
    public ContactController(IPublishEndpoint publishEndpoint, IRequestClient<Contact> requestClient)
    {
        _publishEndpoint = publishEndpoint;
        _requestClient = requestClient;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var newContact = new Contact() { Id = Guid.NewGuid(), Name = "John", Famili = "Week", };

        await _publishEndpoint.Publish(newContact);
        return Json("ok");
    }

    public async Task<JsonResult> GetContactState(string famili,CancellationToken cancellationToken)
    {
        var response =await _requestClient.GetResponse<ContactRiskResponse>(new { Famili = famili }, cancellationToken);
        var result = response.Message.ContactStatus.ToString();
        return Json(response);
    }
}