using DomainShare;
using DomainShare.RequestInformation;
using DomainShare.Response;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PProject.Controllers;

public class ContactController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;

    private readonly IRequestClient<Contact> _requestClient;
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

    public async Task<JsonResult> GetContactState(string family,CancellationToken cancellationToken)
    {
        var request = new RequestInformation() { Id = Guid.NewGuid(), Family = family };
        var response =await _requestClient.GetResponse<ContactRiskResponse>(request, cancellationToken);
        var result = response.Message.ContactStatus.ToString();
        return Json(result);
    }
}
