using DomainShare;
using DomainShare.RequestInformation;
using DomainShare.Response;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PProject.Controllers;

public class ContactController : Controller
{
    private readonly IRequestClient<RequestInformation> _requestClient;
    private readonly IBus _bus;

    public ContactController(IRequestClient<RequestInformation> requestClient, IBus bus)
    {
        _requestClient = requestClient;
        _bus = bus;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var newContact = new Contact() { Id = Guid.NewGuid(), Name = "John", Famili = "Week", };
        await _bus.Publish(newContact);
        return Json("ok");
    }

    public async Task<IActionResult> SendScheduledMessage()
    {
        var newContact = new Contact() { Id = Guid.NewGuid(), Name = "John", Famili = "Week", };
        await _bus.Publish(newContact, config => { config.SetScheduledEnqueueTime(DateTime.Now.AddSeconds(10)); });
        return Json("ok");
    }

    public async Task<JsonResult> GetContactState(string family, CancellationToken cancellationToken)
    {
        var request = new RequestInformation() { Id = Guid.NewGuid(), Family = family };
        var response = await _requestClient.GetResponse<ContactRiskResponse>(request, cancellationToken);
        var result = response.Message.ContactStatus.ToString();
        return Json(result);
    }
}