namespace DomainShare;

public class Order
{
    public Guid Id { get; set; }
    public decimal AmountId { get; set; }
    public decimal AmountOut { get; set; }
    public decimal Rate { get; set; }
}