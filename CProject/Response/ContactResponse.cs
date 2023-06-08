using DomainShare;
using DomainShare.Response;
using DomainShare.ShareData;
using MassTransit;

namespace CProject.Response;

public class ContactResponse:IConsumer<Contact>
{
    public async Task Consume(ConsumeContext<Contact> context)
    {
        var id = context.Message.Id;
        var family = context.Message.Famili;
        var status = family == "Wick" ? ContactStatus.Forbidden : ContactStatus.Active;
        
        await context.RespondAsync<ContactRiskResponse>(new
        {
            Id= id,
            ContactStatus = status
        });
    }
}