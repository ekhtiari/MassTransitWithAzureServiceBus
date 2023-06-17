using DomainShare.Bank;
using MassTransit;

namespace CProject.Event;

public class BankAccountConsumer:IConsumer<BankAccount>
{
    public async Task Consume(ConsumeContext<BankAccount> context)
    {
        Console.WriteLine("Bank Account Id "+ context.Message.Id+"  "+ context.Message.Name);
        
    }
}