namespace DomainShare.Bank;

public class BankAccount
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SessionId { get; set; }
}