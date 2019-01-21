namespace Project3.Service.ServiceLogics
{
    public interface IChangePassService
    {
        bool ChangePass(string user, string passOld, string passNew);
    }
}