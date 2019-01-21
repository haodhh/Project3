using Project3.Common;
using Project3.Data.Database;
using Project3.Service.ServiceBases;

namespace Project3.Service.ServiceLogics
{
    public class LoginService : ILoginService
    {
        private IAccountService accountService = new AccountService();

        public bool CheckLogin(string user, string pass)
        {
            try
            {
                Account account = accountService.GetAccount(user);
                if (CryptoSHA.GenerateSHA512String(pass) == account.Pass)
                    if (account.Status == 1)
                        return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Account GetAccount(string user)
        {
            return accountService.GetAccount(user);
        }
    }
}