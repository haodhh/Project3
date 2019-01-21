using Project3.Data.Database;

namespace Project3.Service.ServiceLogics
{
    public interface ILoginService
    {
        bool CheckLogin(string user, string pass);

        Account GetAccount(string user);
    }
}