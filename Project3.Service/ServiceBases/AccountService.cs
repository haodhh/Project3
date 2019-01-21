using Project3.Common;
using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceBases
{
    public class AccountService : IAccountService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public int AddAccount(Account account)
        {
            try
            {
                account.Pass = CryptoSHA.GenerateSHA512String(account.Pass);
                dbContext.Accounts.Add(account);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int EditAccount(string user, Account account)
        {
            try
            {
                var acc = GetAccount(user);
                if (acc == null)
                    return 1;
                acc.Pass = CryptoSHA.GenerateSHA512String(account.Pass);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int DeleteAccount(string user)
        {
            try
            {
                var acc = GetAccount(user);
                if (acc == null)
                    return 1;
                dbContext.Accounts.Remove(acc);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public Account GetAccount(string user)
        {
            return dbContext.Accounts.SingleOrDefault(x => x.User == user);
        }

        public IEnumerable<Account> GetListAllAccount()
        {
            return dbContext.Accounts.OrderBy(x => x.User).ToList();
        }

        public IEnumerable<Account> GetListManyAccount(string user)
        {
            return dbContext.Accounts.Where(x => x.User == user).ToList();
        }
    }
}