using Project3.Common;
using Project3.Data.Database;
using System.Linq;

namespace Project3.Service.ServiceLogics
{
    public class ChangePassService : IChangePassService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public bool ChangePass(string user, string passOld, string passNew)
        {
            try
            {
                var acc = dbContext.Accounts.SingleOrDefault(x => x.User == user);
                passOld = CryptoSHA.GenerateSHA512String(passOld);
                if (acc.Pass != passOld)
                    return false;
                passNew = CryptoSHA.GenerateSHA512String(passNew);
                acc.Pass = passNew;
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}