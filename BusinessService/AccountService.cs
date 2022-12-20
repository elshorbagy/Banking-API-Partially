using Core.Config;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusinessService
{
    public class AccountService : IAccountService
    {
        readonly ICachedRepository<Account> _repository;
        readonly ILogger<Account> _logger;
       
        readonly string className;
        readonly decimal minimumBalance;

        public AccountService(IOptions<AccountConfig> accountConfig, ICachedRepository<Account> repository, ILogger<Account> logger)
        {
            _repository = repository;
            className = nameof(AccountService);
            _logger = logger;
            minimumBalance = accountConfig.Value.Minimum;
        }

        public async Task<bool> CreateAccount(Account account)
        {
            try
            {
                _logger.LogInformation(className, account);
                _logger.LogInformation("Creating Account Started");

                var validattionError = ValidateAccount(account);

                if(!string.IsNullOrEmpty(validattionError))
                {
                    _logger.LogError(validattionError);
                    throw new Exception(validattionError);
                }

                account.LastTransactionId = Guid.NewGuid().ToString();

                account.TransactionToAccountNavigations.Add(CreateTransactionRecord(account));

                var success = await _repository.AddAsync(account);

                _logger.LogInformation($"Creating Account Finished, result: {success}");
                _logger.LogInformation($"Exiting {className}");
                return success;
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<decimal> GetAccount(int accountId)
        {
            _logger.LogInformation(className, accountId);
            _logger.LogInformation($"Getting Account {accountId}");

            var account = await _repository.GetById(accountId);

            if (account == null)
            {
                var message = $"Account {accountId} was not found";
                _logger.LogError(message);
                throw new Exception(message);
            }

            _logger.LogInformation($"Exiting {className}");
            return account.Balance;
        }

        private Transaction CreateTransactionRecord(Account account)
        {
            return new Transaction
            {
                FromAccount = account.AccountId,
                ToAccount = account.AccountId,
                TransactionAmount = account.Balance,
                TransactionDate = account.CreateOn,
                TransactionId = account.LastTransactionId
            };
        }

        private string ValidateAccount(Account account)
        {
            if (account is null) {
                return "Account is null";
            }

            if (account.Balance >= minimumBalance)
            {
                return "Account Balance is below Minimum";
            }

            if (string.IsNullOrWhiteSpace(account.LastTransactionId))
            {
                return "Account LastTransactionId is invalid";
            }
           if (account.AccountId <= 0)
            {
                return "Account is invalid";
            }

            return string.Empty;
        }
    }
}