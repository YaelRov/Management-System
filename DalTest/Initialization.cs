
namespace DalTest;

using DalApi;
using DO;

public static class Initialization
{
    //defining variables for the interfaces
    private static IDal? s_dal;


    private static readonly Random s_rand = new();//a variable for randomal numbers

    #region functions
    /// <summary>
    /// a function that create 6 engineers
    /// the function doesn't get parameters, and doesn't returns parameters
    /// </summary>
    private static void createEngineers()
    {
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        string[] names =
        {
            "Yoni Stern", "Avraham Silver", "Itzchak Elkias", "Yael Rov",
            "Refael Toledano", "Aharon Zusman"
        };
        //going over the names array, and creating an engineer with each name
        foreach (var name in names)
        {
            int id;
            do
                id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dal!.Engineer.Read(id) != null);//checking if the random id doesn't exists
            double cost = s_rand.Next(30, 100);
            EngineerExperience level = (EngineerExperience)s_rand.Next(0, 3);
            string email = name.Replace(" ", "");
            email += "@gmail.com";
            Engineer eng = new(id, name, email, level, cost);//creating a new engineer with all the details
            s_dal.Engineer.Create(eng);//adding the engineer to the engineer list
        }
    }
    /// <summary>
    /// a function that create 20 tasks
    /// the function doesn't get parameters, and doesn't returns parameters
    /// </summary>
    private static void createTasks()
    {
        //creating an object of details for 20 tasks
        var engineeringTasks = new[]
        {
            new { description = "Design and implement software solutions.", alias = "Software Design", deliverable = "software solutions" },
            new { description = "Write clean and efficient code.", alias = "Code Writing", deliverable = "clean and efficient code" },
            new { description = "Test and debug software applications.", alias = "Software Testing", deliverable = "tested and debugged applications" },
            new { description = "Collaborate with cross-functional teams.", alias = "Team Collaboration", deliverable = "cross-functional collaboration" },
            new { description = "Conduct code reviews and provide feedback.", alias = "Code Reviews", deliverable = "code reviews and feedback" },
            new { description = "Maintain and update existing software systems.", alias = "Software Maintenance", deliverable = "maintained and updated systems" },
            new { description = "Troubleshoot and resolve software issues.", alias = "Issue Resolution", deliverable = "resolved software issues" },
            new { description = "Participate in software planning and estimation.", alias = "Planning and Estimation", deliverable = "software planning and estimation" },
            new { description = "Research and implement new technologies.", alias = "Technology Research", deliverable = "implemented new technologies" },
            new { description = "Document software design and specifications.", alias = "Documentation", deliverable = "software design and specifications" },
            new { description = "Optimize software performance and scalability.", alias = "Performance Optimization", deliverable = "optimized performance and scalability" },
            new { description = "Create and maintain technical documentation.", alias = "Technical Documentation", deliverable = "technical documentation" },
            new { description = "Implement and maintain software version control.", alias = "Version Control", deliverable = "software version control" },
            new { description = "Implement and maintain database systems.", alias = "Database Management", deliverable = "Efficient database systems" },
            new { description = "Perform code refactoring for improved code quality.", alias = "Code Refactoring", deliverable = "High-quality code" },
            new { description = "Develop and execute unit tests.", alias = "Unit Testing", deliverable = "Thoroughly tested code" },
            new { description = "Implement security measures in software solutions.", alias = "Security Implementation", deliverable = "Secure software solutions" },
            new { description = "Provide technical support and assistance.", alias = "Technical Support", deliverable = "Effective technical support" },
            new { description = "Monitor and optimize software performance.", alias = "Performance Monitoring", deliverable = "Optimized performance" },
            new { description = "Plan and execute software deployments.", alias = "Software Deployment", deliverable = "Successful software deployments" }
        };

        string[] remarksArray = { "Be careful", "It's easy", "It's hard", "Do it accurately" };

        //going over the engineeringTasks array, and creating an task with each details object
        foreach (var task in engineeringTasks)
        {
            string description = task.description;
            string alias = task.alias;
            bool milestone = (s_rand.Next() % 2) == 0 ? true : false;

            DateTime createdAt = new DateTime(s_rand.Next(2020, 2025), s_rand.Next(1, 13), s_rand.Next(1, 29));
            TimeSpan requiredOffertTime = new TimeSpan(s_rand.Next(1, 21), 0, 0, 0);
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
            List<Engineer> engineers = s_dal!.Engineer.ReadAll().ToList()!;
            int engineerId = engineers[s_rand.Next(0, Engineer.counterEngineers)].Id; 

            // creating a new task with all the details
            Task newTask = new(-1,description, alias, milestone, createdAt, requiredOffertTime, start, scheduledDate, deadline, complete, deliverables, remarks, engineerId, complexityLevel);
            s_dal!.Task.Create(newTask);//adding the new task to the tasks list
        }
    }

    /// <summary>
    /// a function that create 40 dependencies
    /// the function doesn't get parameters, and doesn't returns parameters
    /// </summary>
    private static void createDependencies()
    { 
        List<Task> Tasks = s_dal!.Task.ReadAll().ToList()!;//getting all the tasks list
        int index = 0;
        //going over the tasks list, and creating dependencies according to the tasks
        foreach (var task in Tasks)
        {
            if (index > 2)
            {
                //creating 3 dependencies for each task with index>2
                for (int j = index - 3; j < index; j++)
                {
                    Dependency dep = new(-1,task.Id, Tasks[j].Id);//creating a new dependency
                    s_dal!.Dependency.Create(dep);//adding the dependency to the dependencies list
                }
            }
            index++;
        }
    }
    /// <summary>
    /// a function that initializes the database by calling the functions that creates the entities
    /// </summary>
    /// <param name="dalEngineer">interface variable</param>
    /// <param name="dalTask">interface variable</param>
    /// <param name="dalDependency">interface variable</param>
    /// <exception cref="NullReferenceException"></exception>
    public static void Do()
    {
       // s_dal=dal ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = Factory.Get ?? throw new NullReferenceException("DAL can not be null!");
        //createEngineers();
        //createTasks();
        //createDependencies();
    }
    #endregion
}

