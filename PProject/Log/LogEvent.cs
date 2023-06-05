using DomainShare;
using MassTransit;

namespace PProject.Log;

public class LogEvent:IConsumer<Order>
{
    public Task Consume(ConsumeContext<Order> context)
    {
        Console.WriteLine("New Order Registered"+ context.Message.Id);
        return Task.CompletedTask;
    }
}