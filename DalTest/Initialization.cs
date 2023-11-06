

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
        string[] firstNames =
        {
            "Avraham", "Yitzchak", "Yaakov", "Moshe", "Aharon", "Yossef",
            "David", "Chaim", "Refael", "Menachem", "Uriel", "Shlomo"
        };
        string[] lastNames =
        {
            "Stern", "Rov", "Gold", "Silver", "Weiss", "Cohen", "Levi",
            "Zusman", "Elkias", "Elchadad", "Shachar", "Maimon", "Toledano"
        };

    }
    #endregion
}
