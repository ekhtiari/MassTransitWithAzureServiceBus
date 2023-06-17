using DomainShare.Bank;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace PProject.Controllers;

public class BankAccountController : Controller
{
    private readonly IBus _bus;

    // GET
    public BankAccountController(IBus bus)
    {
        _bus = bus;
    }

    public IActionResult Send()
    {
        var p1Session = Guid.NewGuid();
        var p2Session = Guid.NewGuid();
        var p1b1 = new BankAccount() { Id = Guid.NewGuid(), Name = "p1b1", SessionId = p1Session };
        var p1b2 = new BankAccount() { Id = Guid.NewGuid(), Name = "p1b2", SessionId = p1Session };
        var p1b3 = new BankAccount() { Id = Guid.NewGuid(), Name = "p1b3", SessionId = p1Session };
        var p2b1 = new BankAccount() { Id = Guid.NewGuid(), Name = "p2b1", SessionId = p2Session };
        var p2b2 = new BankAccount() { Id = Guid.NewGuid(), Name = "p2b1", SessionId = p2Session };


        _bus.Publish(p1b1);
        _bus.Publish(p2b1);
        _bus.Publish(p2b2);
        _bus.Publish(p1b3);
        _bus.Publish(p1b2);

        return Json("all data published");
    }
}