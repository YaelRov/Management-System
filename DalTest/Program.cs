using Dal;
using DalApi;
using DO;

namespace DalTest;
internal class Program
{
    //defining interface variables that gets the implementation
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();
    static void Main(string[] args)
    {
        try
        {
            //calling the method that initializes the database
            Initialization.Do(s_dalEngineer!, s_dalTask!, s_dalDependency!);
            mainMenu();//calling the method of the main menu
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #region create methods
    static int createEngineer()
    {
        Console.WriteLine("Enter engineer's details: id, name, email, level and cost:\n");
        int _id = Convert.ToInt32(Console.ReadLine());
        string _name = Console.ReadLine()!;
        string _email = Console.ReadLine()!;
        EngineerExperience _level =(EngineerExperience)Enum.Parse(typeof(EngineerExperience),Console.ReadLine()!);
        double _cost = Convert.ToDouble(Console.ReadLine());
        Engineer newEng = new(_id, _name, _email, _level, _cost);
        return s_dalEngineer!.Create(newEng);
    }
    static int createTask()
    {
        Console.WriteLine("Enter task's details: description, alias, milestone.\n dates of: creating, start, scheduled date, forecast , deadline and complete.\n deliverables, remarks, engineer's id and complexity level:\n");
        string _description = Console.ReadLine()!;
        string _alias = Console.ReadLine()!;
        bool _milestone = Convert.ToBoolean(Console.ReadLine()!);
        DateTime _createdAt = Convert.ToDateTime(Console.ReadLine()!);
        DateTime? _start = Convert.ToDateTime(Console.ReadLine());
        DateTime? _scheduledDate = Convert.ToDateTime(Console.ReadLine());
        DateTime? _forecastDate = Convert.ToDateTime(Console.ReadLine());
        DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
        DateTime? _complete = Convert.ToDateTime(Console.ReadLine());
        string? _deliverables = Console.ReadLine();
        string? _remarks = Console.ReadLine();
        int? _engineerId = Convert.ToInt32(Console.ReadLine());
        Engineer checkExistingEngineer = s_dalEngineer!.Read((int)_engineerId!) ?? throw new Exception($"An object of type Engineer with ID {_engineerId} does not exist");
        EngineerExperience _complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
        DO.Task newTask = new(-1, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);
        return s_dalTask!.Create(newTask);
    }
    static int createDependency()
    {
        Console.WriteLine("Enter dependency's details: dependent task and depends-on task:\n");
        int? _dependentTask = Convert.ToInt32(Console.ReadLine());
        DO.Task checkExisting1 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");
        int? _dependsOnTask = Convert.ToInt32(Console.ReadLine());
        DO.Task checkExisting2 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");
        Dependency newDep = new(-1, _dependentTask, _dependsOnTask);
        return s_dalDependency!.Create(newDep);
    }
    #endregion

    #region read methods
    static Engineer? readEngineer()
    {
        Console.WriteLine("Enter engineer's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Engineer? returnedEng = s_dalEngineer!.Read(id);
        if(returnedEng is null)
            throw new Exception($"An object of type Engineer with ID {id} does not exist");
        return returnedEng;
    }

    static DO.Task? readTask()
    {
        Console.WriteLine("Enter task's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        DO.Task? returnedTask = s_dalTask!.Read(id);
        if( returnedTask is null )
            throw new Exception($"An object of type Task with ID {id} does not exist");
        return returnedTask;
    }

    static Dependency? readDependency()
    {
        Console.WriteLine("Enter dependency's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Dependency? returnedDep= s_dalDependency!.Read(id);
        if(returnedDep is null)
            throw new Exception($"An object of type Dependency with ID {id} does not exist");
        return returnedDep;
    }
    #endregion

    #region readAll methods
    static List<Engineer> readAllEngineers()
    {
        return s_dalEngineer!.ReadAll();
    }

    static List<DO.Task> readAllTasks()
    {
        return s_dalTask!.ReadAll();
    }

    static List<Dependency> readAllDependencies()
    {
        return s_dalDependency!.ReadAll();
    }
    #endregion

    #region update methods
    /// <summary>
    /// a function that updates the details of engineer, doesn't return anything
    /// </summary>
    /// <exception cref="Exception"></exception>
    static void updateEngineers()
    {
        string userInput;//a variable that the input that the user enters assign in it
        Console.WriteLine("Enter id of engineer to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Engineer baseEng = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with this id does not exist");//reading the engineer with the enterd id
        Console.WriteLine(baseEng);//printing the details of the engineer
        Console.WriteLine("Enter engineer's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the engineer for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("name:");
        userInput = Console.ReadLine()!;
        string _name = string.IsNullOrEmpty(userInput) ? baseEng.Name :userInput;

        Console.WriteLine("email:");
        userInput = Console.ReadLine()!;
        string _email = string.IsNullOrEmpty(userInput) ? baseEng.Email : userInput;

        Console.WriteLine("level:");
        userInput = Console.ReadLine()!;
        EngineerExperience _level = string.IsNullOrEmpty(userInput) ? baseEng.Level : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), userInput);

        Console.WriteLine("cost:");
        userInput = Console.ReadLine()!;
        double _cost = string.IsNullOrEmpty(userInput) ? baseEng.Cost : Convert.ToDouble(userInput);

        Engineer updateEng = new(id, _name,_email, _level,_cost);//creating a new entity of engineer with the details
        s_dalEngineer!.Update(updateEng);//updating the engineers list
    }
    /// <summary>
    /// a function that updates the details of task, doesn't return anything
    /// </summary>
    /// <exception cref="Exception"></exception>
    static void updateTasks()
    {
        string userInput;//a variable that the input that the user enters assign in it
        Console.WriteLine("Enter id of task to update");
        int id = Convert.ToInt32(Console.ReadLine());
        DO.Task baseTask = s_dalTask!.Read(id) ?? throw new Exception("Task with this id does not exist");//reading the task with the enterd id
        Console.WriteLine(baseTask);//printing the details of the task

        Console.WriteLine("Enter task's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the task for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("description:");
        userInput = Console.ReadLine()!;
        string _description =  string.IsNullOrEmpty(userInput) ? baseTask.Description : userInput;

        Console.WriteLine("alias:");
        userInput = Console.ReadLine()!;
        string _alias = string.IsNullOrEmpty(userInput) ? baseTask.Alias : userInput;

        Console.WriteLine("milestone:");
        userInput = Console.ReadLine()!;
        bool _milestone = string.IsNullOrEmpty(userInput) ? baseTask.Milestone : Convert.ToBoolean(userInput);

        Console.WriteLine("date of creating:");
        userInput = Console.ReadLine()!;
        DateTime _createdAt = string.IsNullOrEmpty(userInput) ? baseTask.CreatedAt : Convert.ToDateTime(userInput);

        Console.WriteLine("date of starting:");
        userInput = Console.ReadLine()!;
        DateTime? _start = string.IsNullOrEmpty(userInput) ? baseTask.Start : Convert.ToDateTime(userInput);

        Console.WriteLine("scheduled date:");
        userInput = Console.ReadLine()!;
        DateTime? _scheduledDate = string.IsNullOrEmpty(userInput) ? baseTask.ScheduledDate : Convert.ToDateTime(userInput);
       
        Console.WriteLine("forecast date:");
        userInput = Console.ReadLine()!;
        DateTime? _forecastDate = string.IsNullOrEmpty(userInput) ? baseTask.ForecastDate : Convert.ToDateTime(userInput);

        Console.WriteLine("date of deadline:");
        userInput = Console.ReadLine()!;
        DateTime? _deadline = string.IsNullOrEmpty(userInput) ? baseTask.Deadline : Convert.ToDateTime(userInput);

        Console.WriteLine("date of complete:");
        userInput = Console.ReadLine()!;
        DateTime? _complete = string.IsNullOrEmpty(userInput) ? baseTask.Complete : Convert.ToDateTime(userInput);

        Console.WriteLine("deliverables:");
        userInput = Console.ReadLine()!;
        string? _deliverables = string.IsNullOrEmpty(userInput) ? baseTask.Deliverables : userInput;

        Console.WriteLine("remarks:");
        userInput = Console.ReadLine()!;
        string? _remarks = string.IsNullOrEmpty(userInput) ? baseTask.Remarks : userInput;

        Console.WriteLine("engineer's id:");
        userInput = Console.ReadLine()!;
        int? _engineerId = string.IsNullOrEmpty(userInput) ? baseTask.EngineerId : Convert.ToInt32(userInput);
        //checking if the engineer with the entered id is exists
        Engineer checkExistingEngineer = s_dalEngineer!.Read((int)_engineerId!) ?? throw new Exception($"An object of type Engineer with ID {_engineerId} does not exist");


        Console.WriteLine("complexity level:");
        userInput = Console.ReadLine()!;
        EngineerExperience? _complexityLevel = string.IsNullOrEmpty(userInput) ? baseTask.ComplexityLevel : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), userInput); ;
        DO.Task updateTask = new (id, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);//creating a new entity of task with the details
        s_dalTask!.Update(updateTask);//updating the tasks list
    }
    /// <summary>
    /// a function that updates the details of dependency, doesn't return anything
    /// </summary>
    /// <exception cref="Exception"></exception>
    static void updateDependenies()
    {
        string userInput;//a variable that the input that the user enters assign in it
        Console.WriteLine("Enter id of dependency to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Dependency baseDep = s_dalDependency!.Read(id) ?? throw new Exception("Dependency with this id does not exist");//reading the dependency with the enterd id
        Console.WriteLine(baseDep);//printing the details of the dependency
        Console.WriteLine("Enter dependency's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the dependency for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("dependent task:");
        userInput = Console.ReadLine()!;
        int? _dependentTask = string.IsNullOrEmpty(userInput) ? baseDep.DependentTask : Convert.ToInt32(userInput);
        //checking if the task with the entered id is exists
        DO.Task checkExisting1 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");


        Console.WriteLine("depends on task:");
        userInput = Console.ReadLine()!;
        int? _dependsOnTask = string.IsNullOrEmpty(userInput) ? baseDep.DependsOnTask : Convert.ToInt32(userInput);
        //checking if the task with the entered id is exists
        DO.Task checkExisting2 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");

        Dependency updateDep = new(id, _dependentTask, _dependsOnTask);//creating a new entity of dependency with the details
        s_dalDependency!.Update(updateDep);//updating the dependencies list
    }
    #endregion

    #region delete methods
    static void deleteEngineer()
    {
        Console.WriteLine("Enter engineer's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        s_dalEngineer!.Delete(id);
    }

    static void deleteTask()
    {
        Console.WriteLine("Enter task's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        s_dalTask!.Delete(id);
    }

    static void deleteDependency()
    {
        Console.WriteLine("Enter dependency's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        s_dalDependency!.Delete(id);
    }
    #endregion
    static void engineerMenu()
    {
        while (true)
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (methodChoice)
                {
                    case 1:
                        return;
                    case (int)CRUD.CREATE:
                        int returnedId = createEngineer();
                        Console.WriteLine($"The engineer's id: {returnedId}\n");
                        break;
                    case (int)CRUD.READ:
                        Console.WriteLine(readEngineer());
                        break;
                    case (int)CRUD.READALL:
                        List<Engineer> returnedList = readAllEngineers();
                        foreach (var en in returnedList)
                            Console.WriteLine(en);
                        break;
                    case (int)CRUD.UPDATE:
                        updateEngineers();
                        break;
                    case (int)CRUD.DELETE:
                        deleteEngineer();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    static void taskMenu()
    {
        while (true)
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (methodChoice)
                {
                    case 1:
                        return;
                    case (int)CRUD.CREATE:
                        int returnedId = createTask();
                        Console.WriteLine($"The task's id: {returnedId}\n");
                        break;
                    case (int)CRUD.READ:
                        Console.WriteLine(readTask());
                        break;
                    case (int)CRUD.READALL:
                        List<DO.Task> returnedList = readAllTasks();
                        foreach (var task in returnedList)
                            Console.WriteLine(task);
                        break;
                    case (int)CRUD.UPDATE:
                        updateTasks();
                        break;
                    case (int)CRUD.DELETE:
                        deleteTask();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    static void dependencyMenu()
    {
        while (true)
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (methodChoice)
                {
                    case 1:
                        return;
                    case (int)CRUD.CREATE:
                        int returnedId = createDependency();
                        Console.WriteLine($"The dependency's id: {returnedId} \n");
                        break;
                    case (int)CRUD.READ:
                        Console.WriteLine(readDependency());
                        break;
                    case (int)CRUD.READALL:
                        List<Dependency> returnedList = readAllDependencies();
                        foreach (var dep in returnedList)
                            Console.WriteLine(dep);
                        break;
                    case (int)CRUD.UPDATE:
                        updateDependenies();
                        break;
                    case (int)CRUD.DELETE:
                        deleteDependency();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    /// <summary>
    /// A menu function that manages access to the database, by calling the menu of the requested entity
    /// </summary>
    static void mainMenu()
    {
        //run till the user entered '0' to exit
        while (true)
        {
            Console.WriteLine("Choose an entity that you whant to check:\n 0 to exit\n 1 to Engineer\n 2 to Task\n 3 to Dependency\n");
            int entityChoice = Convert.ToInt32(Console.ReadLine());//geting the choice from the user
            switch (entityChoice)
            {
                case 0://exit
                    return;
                case (int)EntityType.ENGINEER://1
                    engineerMenu();//calling the function that manages the engineer menu
                    break;
                case (int)EntityType.TASK://2
                    taskMenu();//calling the function that manages the task menu
                    break;
                case (int)EntityType.DEPENDENCY://3
                    dependencyMenu();//calling the function that manages the dependency menu
                    break;
                default:
                    break;
            }
        }
    }
}