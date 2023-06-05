using DomainShare.ShareData;

namespace DomainShare.Response;

public class ContactRiskResponse
{
    public Guid Id { get; set; }
    public ContactStatus ContactStatus { get; set; }
}