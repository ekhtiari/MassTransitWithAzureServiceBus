namespace DomainShare.SessionMessages;

public class Products
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal MinPrice { get; set; }
}