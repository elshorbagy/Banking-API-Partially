using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public class MoneyTransfere
    {
        readonly ICachedRepository<Account> _repository;

        public MoneyTransfere(ICachedRepository<Account> repository)
        {
            _repository = repository;
        }


    }
}
