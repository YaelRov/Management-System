

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
        var buildingTasks = new[]
{
    { description = "Measure and mark the dimensions of the foundation.", alias = "Foundation Dimensions" },
    { description = "Dig the foundation trench to the required depth.", alias = "Foundation Trench" },
    { description = "Pour and level the concrete foundation.", alias = "Concrete Foundation" },
    { description = "Construct the framework for the walls.", alias = "Wall Framework" },
    { description = "Install electrical and plumbing systems.", alias = "Electrical Plumbing" },
    { description = "Insulate the walls and ceiling for energy efficiency.", alias = "Wall Insulation" },
    { description = "Install doors, windows, and roofing materials.", alias = "Doors Windows" },
    { description = "Finish the walls with drywall or other materials.", alias = "Wall Finishing" },
    { description = "Paint the interior and exterior surfaces.", alias = "Surface Painting" },
    { description = "Install flooring and finish with baseboards.", alias = "Flooring Installation" },
    { description = "Connect utilities and perform final inspections.", alias = "Utilities Inspections" },
    { description = "Set up scaffolding for elevated work areas.", alias = "Scaffolding Setup" },
    { description = "Lay bricks or blocks for walls.", alias = "Wall Construction" },
    { description = "Install insulation in the attic.", alias = "Attic Insulation" },
    { description = "Mount cabinets and countertops in the kitchen.", alias = "Cabinets Countertops" },
    { description = "Install lighting fixtures and switches.", alias = "Lighting Installation" },
    { description = "Apply exterior siding for weather protection.", alias = "Exterior Siding" },
    { description = "Hang interior doors and install hardware.", alias = "Interior Doors" },
    { description = "Install plumbing fixtures in bathrooms.", alias = "Plumbing Fixtures" },
    { description = "Apply final coat of paint to all surfaces.", alias = "Painting" }
};
        string[] deliverables = []
        string[] remarks = ["Be carful", "It's easy", "It's hard", "Do it accurately"]
        foreach (var task in buildingTasks)
        {
            string description=task.description;
            string alias=task.alias;
            bool? milestone = (s_rand() % 2) == 0 ? true : false;

            string deliverables=


        }
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
    foreach (var name in names)
        {
        int id;
        do
            id = s_rand.Next(MIN_ID, MAX_ID);
        while (Engineers!.Read(_id) != null);
        double cost = s_rand.Next(30, 100);
        EngineerExperience level = s_rand.Next(0, 3);
        string email = name.Replace(" ", "");
        email += "@gmail.com";
        Engineer eng = new(id, name, email, level, cost);
        Engineers.Create(eng);
        s_dalEngineer++;
    }
}
    #endregion
}
