using Project3.Data.Database;
using System.Collections.Generic;

namespace Project3.Service.ServiceBases
{
    public interface ICardService
    {
        int AddCard(string code, int idStd, int status);

        int EditCard(int id, string code, int idStd, int status);

        int DeleteCard(int id);

        RFIDCard GetCard(int id);

        IEnumerable<RFIDCard> GetListAllCard();

        IEnumerable<RFIDCard> GetListManyCard(int id);
    }
}