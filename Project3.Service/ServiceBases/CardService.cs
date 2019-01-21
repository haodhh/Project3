using Project3.Data.Database;
using System.Collections.Generic;
using System.Linq;

namespace Project3.Service.ServiceBases
{
    public class CardService : ICardService
    {
        private ProjectDbContext dbContext = new ProjectDbContext();

        public int AddCard(string code, int idStd, int status)
        {
            try
            {
                if (idStd != 0)
                {
                    var std = dbContext.Students.SingleOrDefault(x => x.ID == idStd);
                    std.Status = 1;
                }
                if (dbContext.RFIDCards.Where(x => x.CodeCard == code).Count() > 0)
                    return 1;

                var card = new RFIDCard();

                card.CodeCard = code;
                card.IDStudent = idStd;
                card.Status = status;
                dbContext.RFIDCards.Add(card);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int EditCard(int id, string code, int idStd, int status)
        {
            try
            {
                var card = GetCard(id);
                if (card == null)
                    return 1;
                if (card.CodeCard != code && dbContext.RFIDCards.Where(x => x.CodeCard == code).Count() > 0)
                    return 1;
                if (card.IDStudent != 0)
                {
                    dbContext.Students.SingleOrDefault(x => x.ID == card.IDStudent).Status = 0;
                }
                if (idStd != 0)
                {
                    dbContext.Students.SingleOrDefault(x => x.ID == idStd).Status = 1;
                }
                card.IDStudent = idStd;
                card.CodeCard = code;
                card.Status = status;
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public int DeleteCard(int id)
        {
            try
            {
                var card = GetCard(id);
                if (card == null)
                    return 1;
                if (card.IDStudent != 0)
                {
                    dbContext.Students.SingleOrDefault(x => x.ID == card.IDStudent).Status = 0;
                }
                dbContext.RFIDCards.Remove(card);
                dbContext.SaveChanges();
                return 0;
            }
            catch
            {
                return 2;
            }
        }

        public RFIDCard GetCard(int id)
        {
            return dbContext.RFIDCards.SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<RFIDCard> GetListAllCard()
        {
            return dbContext.RFIDCards.OrderBy(x => x.CodeCard).ToList();
        }

        public IEnumerable<RFIDCard> GetListManyCard(int id)
        {
            return dbContext.RFIDCards.Where(x => x.ID == id).ToList();
        }
    }
}