using DomainShare;
using MassTransit;

namespace CProject.Event;

public class Consumer:IConsumer<Contact>
{
    public Task Consume(ConsumeContext<Contact> context)
    {
        Console.WriteLine("new Item"+context.Message.Id+" "+context.Message.Name+" "+context.Message.Famili);
        return Task.CompletedTask;
    }
}