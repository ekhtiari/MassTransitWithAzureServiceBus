using DomainShare;
using DomainShare.Response;
using DomainShare.ShareData;
using MassTransit;

namespace CProject.Response;

public class ContactResponse:IConsumer<Contact>
{
    public async Task Consume(ConsumeContext<Contact> context)
    {
        await context.RespondAsync<ContactRiskResponse>(new
        {
            CpntactId= context.Message.Id,
            Status = context.Message.Famili=="Wick"? ContactStatus.Forbidden: ContactStatus.Active
        });
    }
}