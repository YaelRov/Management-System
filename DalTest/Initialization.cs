

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
        string[] buildingTasks = new string[]
        {
            "Measure and mark the dimensions of the foundation.",
            "Dig the foundation trench to the required depth.",
            "Pour and level the concrete foundation.",
            "Construct the framework for the walls.",
            "Install electrical and plumbing systems.",
            "Insulate the walls and ceiling for energy efficiency.",
            "Install doors, windows, and roofing materials.",
            "Finish the walls with drywall or other materials.",
            "Paint the interior and exterior surfaces.",
            "Install flooring and finish with baseboards.",
            "Connect utilities and perform final inspections.",
            "Set up scaffolding for elevated work areas.",
            "Lay bricks or blocks for walls.",
            "Install insulation in the attic.",
            "Mount cabinets and countertops in the kitchen.",
            "Install lighting fixtures and switches.",
            "Apply exterior siding for weather protection.",
            "Hang interior doors and install hardware.",
            "Install plumbing fixtures in bathrooms.",
            "Apply final coat of paint to all surfaces."
        };
    };
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
