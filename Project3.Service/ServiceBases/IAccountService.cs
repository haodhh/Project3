using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceBases
{
    public interface IAccountService
    {
        int AddAccount(Account account);

        int EditAccount(string user, Account account);

        int DeleteAccount(string user);

        Account GetAccount(string user);

        IEnumerable<Account> GetListAllAccount();

        IEnumerable<Account> GetListManyAccount(string user);
    }
}