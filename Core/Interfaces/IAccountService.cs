using Core.Models;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateAccount(Account account);
        Task<decimal> GetAccount(int accountId);
    }
}
