namespace Core.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerFullName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
