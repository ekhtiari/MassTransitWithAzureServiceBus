using DomainShare;
using DomainShare.RequestInformation;
using DomainShare.Response;
using DomainShare.ShareData;
using MassTransit;

namespace CProject.Response;

public class ContactResponse:IConsumer<RequestInformation>
{
    public async Task Consume(ConsumeContext<RequestInformation> context)
    {
        var id = context.Message.Id;
        var family = context.Message.Family;
        var status = family == "Wick" ? ContactStatus.Forbidden : ContactStatus.Active;
        Console.WriteLine(DateTime.Now+" New Check Item");
        
        await context.RespondAsync<ContactRiskResponse>(new
        {
            Id= id,
            ContactStatus = status
        });
    }
}