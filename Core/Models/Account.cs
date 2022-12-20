using System.Text.Json.Serialization;

namespace Core.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public int CustomerId { get; set; }

    public string LastTransactionId { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreateOn { get; set; }

    //[JsonIgnore]
    //public virtual Customer Customer { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Transaction> TransactionFromAccountNavigations { get; } = new List<Transaction>();
    [JsonIgnore]
    public virtual ICollection<Transaction> TransactionToAccountNavigations { get; } = new List<Transaction>();
}
