namespace Core.Models;

public class Transaction
{
    public string TransactionId { get; set; } = null!;

    public decimal TransactionAmount { get; set; }

    public DateTime TransactionDate { get; set; }

    public int FromAccount { get; set; }

    public int ToAccount { get; set; }

    public virtual Account FromAccountNavigation { get; set; } = null!;

    public virtual Account ToAccountNavigation { get; set; } = null!;
}
