
namespace DalTest;

using DalApi;
using DO;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;
    private static IDependency? s_dalDependency;

    private static int counterEngineers = 0;
    private static int counterTasks = 0;
    private static int counterDependencies = 0;

    private static readonly Random s_rand = new();
    #region methods
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
            while (s_dalEngineer!.Read(id) != null);
            double cost = s_rand.Next(30, 100);
            EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);
            string email = name.Replace(" ", "");
            email += "@gmail.com";
            Engineer eng = new(id, name, email, level, cost);
            s_dalEngineer.Create(eng);
            counterEngineers++;
        }
    }
    private static void createTasks()
    {
        var buildingTasks = new[]
        {
            new{ description = "Measure and mark the dimensions of the foundation.", alias = "Foundation Dimensions", deliverable = "measurement" },
            new{ description = "Dig the foundation trench to the required depth.", alias = "Foundation Trench", deliverable = "excavation" },
            new{ description = "Pour and level the concrete foundation.", alias = "Concrete Foundation", deliverable = "concrete foundation" },
            new{ description = "Construct the framework for the walls.", alias = "Wall Framework", deliverable = "wall framework" },
            new{ description = "Install electrical and plumbing systems.", alias = "Electrical Plumbing", deliverable = "electrical and plumbing systems" },
            new{ description = "Insulate the walls and ceiling for energy efficiency.", alias = "Wall Insulation", deliverable = "wall insulation" },
            new{ description = "Install doors, windows, and roofing materials.", alias = "Doors Windows", deliverable = "doors, windows, and roofing materials" },
            new{ description = "Finish the walls with drywall or other materials.", alias = "Wall Finishing", deliverable = "finished walls" },
            new{ description = "Paint the interior and exterior surfaces.", alias = "Surface Painting", deliverable = "painted surfaces" },
            new{ description = "Install flooring and finish with baseboards.", alias = "Flooring Installation", deliverable = "flooring installation" },
            new{ description = "Connect utilities and perform final inspections.", alias = "Utilities Inspections", deliverable = "connected utilities and inspections" },
            new{ description = "Set up scaffolding for elevated work areas.", alias = "Scaffolding Setup", deliverable = "scaffolding setup" },
            new{ description = "Lay bricks or blocks for walls.", alias = "Wall Construction", deliverable = "wall construction" },
            new{ description = "Install insulation in the attic.", alias = "Attic Insulation", deliverable = "attic insulation" },
            new{ description = "Mount cabinets and countertops in the kitchen.", alias = "Cabinets Countertops", deliverable = "cabinets and countertops" },
            new{ description = "Install lighting fixtures and electrical outlets.", alias = "Lighting and Outlets", deliverable = "lighting fixtures and electrical outlets" },
            new{ description = "Install plumbing fixtures in bathrooms and kitchen.", alias = "Plumbing Fixtures", deliverable = "plumbing fixtures" },
            new{ description = "Install heating, ventilation, and air conditioning systems.", alias = "HVAC Systems", deliverable = "heating, ventilation, and air conditioning systems" },
            new{ description = "Apply decorative finishes to walls and ceilings.", alias = "Decorative Finishes", deliverable = "decorative finishes" },
            new{ description = "Install appliances in the kitchen and laundry area.", alias = "Appliance Installation", deliverable = "appliance installation" },
        };

        string[] remarksArray = { "Be carful", "It's easy", "It's hard", "Do it accurately" };
        foreach (var task in buildingTasks)
        {
            string description = task.description;
            string alias = task.alias;
            bool milestone = (s_rand.Next() % 2) == 0 ? true : false;

            DateTime createdAt = new DateTime(s_rand.Next(2020, 2025), s_rand.Next(1, 13), s_rand.Next(1, 30));
            TimeSpan ts = new TimeSpan(s_rand.Next(0, 1800), 0, 0);
            DateTime start = createdAt.Add(ts);
            ts = new TimeSpan(s_rand.Next(24, 336), 0, 0);
            DateTime scheduledDate = start.Add(ts);
            ts = new TimeSpan(s_rand.Next(24, 336), 0, 0);
            DateTime forecastDate = scheduledDate.Add(ts);
            ts = new TimeSpan(72, 0, 0);
            DateTime deadline = forecastDate.Add(ts);
            ts = new TimeSpan(s_rand.Next(-72, 0), 0, 0);
            DateTime complete = deadline.Add(ts);

            string deliverables = task.deliverable;
            string remarks = remarksArray[s_rand.Next(0, 4)];
            EngineerExperience complexityLevel = (EngineerExperience)s_rand.Next(0, 5);
            List<Engineer> engineers = s_dalEngineer.ReadAll();
            int engineerId = engineers[s_rand.Next(0, counterEngineers)].Id;

            Task newTask = new(description, alias, milestone, createdAt, start, scheduledDate, forecastDate, deadline, complete, deliverables, remarks, engineerId, complexityLevel);
            s_dalTask.Create(newTask);
            counterTasks++;
        }
    }
    private static void createDependencies()
    {
        List<Task> Tasks = s_dalTask.ReadAll();
        int index = 0;
        foreach (var task in Tasks)
        {
            if (index > 2)
            {
                for (int j = index - 3; j < index; j++)
                {
                    Dependency dep = new(task.Id, Tasks[j].Id);
                    s_dalDependency.Create(dep);
                    counterDependencies++;
                }
            }
            index++;
        }
    }
    public static void Do(IEngineer dalEngineer, ITask dalTask, IDependency dalDependency)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createEngineers();
        createTasks();
        createDependencies();
    }
    #endregion
}

