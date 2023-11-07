

namespace DalTest;
using DalApi;
using DO;


public static class Initialization
{
    private static int? s_dalTask;
    private static int? s_dalDependency;
    private static int? s_dalEngineer;
    private static readonly Random s_rand = new();
    #region methods
    private static void createTasks()
    {

    }
    private static void createDependencies()
    {

    }
    private static void createEngineers()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        string[] names =
        {
            "Yoni Stern", "Avraham Silver", "Itzchak Elkias", "Yaakov Beker",
            "Refael Toledano", "Aharon Zusman"
        };
        foreach name in names
        {
            int id;
            do
                id = s_rand.Next(MIN_ID, MAX_ID);
            while (Engineers!.Read(_id) != null);
            double cost = s_rand.Next(30, 100);
            EngineerExperience level = s_rand.Next(0, 3);
            string email = name.Replace(" ", "");
            email += "@gmail.com";
            Engineer eng = new(id, nameof, email, level, cost);
            Engineers.Create(eng);
        }
    }
    #endregion
}
