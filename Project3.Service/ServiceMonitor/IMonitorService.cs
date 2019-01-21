using Project3.Data.Database;

namespace Project3.Service.ServiceMonitor
{
    public interface IMonitorService
    {
        bool checkKey(string key);

        Student GetStudent(string codeCard);

        bool checkSchedule(string code, string idCar, string idStd);

        bool checkScheduleAlone(string code);

        //bool UpdateMonitor(string code, int locationX, int locationY, int speed, int status);

       // bool CalculatorPoint(string code);
    }
}