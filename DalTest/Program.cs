using Dal;
using DalApi;
using DO;

namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();
    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(s_dalEngineer!, s_dalTask!, s_dalDependency!);
            mainMenu();
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
        int _id = Convert.ToInt32(Console.ReadLine());//get the details
        string _name = Console.ReadLine()!;
        string _email = Console.ReadLine()!;
        EngineerExperience _level = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
        double _cost = Convert.ToDouble(Console.ReadLine());
        Engineer newEng = new(_id, _name, _email, _level, _cost);//create new enginner
        return s_dalEngineer!.Create(newEng);//add to the list
    }//cteate a new object of engineer and return the id number
    static int createTask()
    {
        Console.WriteLine("Enter task's details: description, alias, milestone.\n dates of: creating, start, scheduled date, forecast , deadline and complete.\n deliverables, remarks, engineer's id and complexity level:\n");
        string _description = Console.ReadLine()!;//get details
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
        EngineerExperience? _complexityLevel = (EngineerExperience)Convert.ToInt32(Console.ReadLine());
        DO.Task newTask = new(-1, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);//create new task, the id will be created in the Create function
        return s_dalTask!.Create(newTask);//add to the list
    }//cteate a new object of task and return the id number
    static int createDependency()
    {
        Console.WriteLine("Enter dependency's details: dependent task and depends-on task:\n");
        int? _dependentTask = Convert.ToInt32(Console.ReadLine());//get details
        DO.Task checkExisting1 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");//check if the id of the task exsists
        int? _dependsOnTask = Convert.ToInt32(Console.ReadLine());
        DO.Task checkExisting2 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");//check if the id of the task exsists
        Dependency newDep = new(-1, _dependentTask, _dependsOnTask);//add to the list, the id will be created in the Create function
        return s_dalDependency!.Create(newDep);//add to the list
    }//cteate a new object of dependency and return the id number
    #endregion

    #region read methods
    static Engineer? readEngineer()//return an object of the wanted engineer
    {
        Console.WriteLine("Enter engineer's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Engineer? returnedEng = s_dalEngineer!.Read(id);//call the read function
        if(returnedEng is null)//if the wanted object does not exist
            throw new Exception($"An object of type Engineer with ID {id} does not exist");
        return returnedEng;//return the found object
    }

    static DO.Task? readTask()//return an object of the wanted task
    {
        Console.WriteLine("Enter task's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        DO.Task? returnedTask = s_dalTask!.Read(id);//call the read function
        if ( returnedTask is null)//if the wanted object does not exist
            throw new Exception($"An object of type Task with ID {id} does not exist");
        return returnedTask;//return the found object
    }

    static Dependency? readDependency()//return an object of the wanted dependency
    {
        Console.WriteLine("Enter dependency's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Dependency? returnedDep= s_dalDependency!.Read(id);//call the read function
        if (returnedDep is null)//if the wanted object does not exist
            throw new Exception($"An object of type Dependency with ID {id} does not exist");
        return returnedDep;//return the found object
    }
    #endregion

    #region readAll methods
    static List<Engineer> readAllEngineers()//return a copy of the list
    {
        return s_dalEngineer!.ReadAll();
    }

    static List<DO.Task> readAllTasks()//return a copy of the list
    {
        return s_dalTask!.ReadAll();
    }

    static List<Dependency> readAllDependencies()//return a copy of the list
    {
        return s_dalDependency!.ReadAll();
    }
    #endregion

    #region update methods
    static void updateEngineers()
    {
        string userInput;
        Console.WriteLine("Enter id of engineer to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Engineer baseEng = s_dalEngineer!.Read(id) ?? throw new Exception("Engineer with this id does not exist");
        Console.WriteLine(baseEng);
        Console.WriteLine("Enter engineer's details to update. If you don't want to change press enter.\n");

        Console.WriteLine("name:");
        userInput = Console.ReadLine()!;
        string _name = string.IsNullOrEmpty(userInput) ? baseEng.Name :userInput;

        Console.WriteLine("email:");
        userInput = Console.ReadLine()!;
        string _email = string.IsNullOrEmpty(userInput) ? baseEng.Email : userInput;

        Console.WriteLine("level:");
        userInput = Console.ReadLine()!;
        EngineerExperience _level = string.IsNullOrEmpty(userInput) ? baseEng.Level : (EngineerExperience)Convert.ToInt32(userInput);

        Console.WriteLine("cost:");
        userInput = Console.ReadLine()!;
        double _cost = string.IsNullOrEmpty(userInput) ? baseEng.Cost : Convert.ToDouble(userInput);

        Engineer updateEng = new(id, _name,_email, _level,_cost);
        s_dalEngineer!.Update(updateEng);
    }
    static void updateTasks()
    {
        string userInput;
        Console.WriteLine("Enter id of task to update");
        int id = Convert.ToInt32(Console.ReadLine());
        DO.Task baseTask = s_dalTask!.Read(id) ?? throw new Exception("Task with this id does not exist");
        Console.WriteLine(baseTask);
        
        Console.WriteLine("Enter task's details to update. If you don't want to change press enter.\n");
       
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
        Engineer checkExistingEngineer = s_dalEngineer!.Read((int)_engineerId!) ?? throw new Exception($"An object of type Engineer with ID {_engineerId} does not exist");


        Console.WriteLine("complexity level:");
        userInput = Console.ReadLine()!;
        EngineerExperience? _complexityLevel = string.IsNullOrEmpty(userInput) ? baseTask.ComplexityLevel : (EngineerExperience)(Convert.ToInt32(userInput));
        DO.Task updateTask = new (id, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);
        s_dalTask!.Update(updateTask);
    }
    static void updateDependenies()
    {
        string userInput;
        Console.WriteLine("Enter id of dependency to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Dependency baseDep = s_dalDependency!.Read(id) ?? throw new Exception("Dependency with this id does not exist");
        Console.WriteLine(baseDep);
        Console.WriteLine("Enter dependency's details to update. If you don't want to change press enter.\n");

        Console.WriteLine("dependent task:");
        userInput = Console.ReadLine()!;
        int? _dependentTask = string.IsNullOrEmpty(userInput) ? baseDep.DependentTask : Convert.ToInt32(userInput);
        DO.Task checkExisting1 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");


        Console.WriteLine("depends on task:");
        userInput = Console.ReadLine()!;
        int? _dependsOnTask = string.IsNullOrEmpty(userInput) ? baseDep.DependsOnTask : Convert.ToInt32(userInput);
        DO.Task checkExisting2 = s_dalTask!.Read((int)_dependentTask!) ?? throw new Exception($"An object of type Task with ID {_dependentTask} does not exist");

        Dependency updateDep = new(id, _dependentTask, _dependsOnTask);
        s_dalDependency!.Update(updateDep);
    }
    #endregion

    #region delete methods
    static void deleteEngineer()
    {
        Console.WriteLine("Enter engineer's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_dalEngineer!.Delete(id);//call the Delete function
    }//delete a wanted engineer

    static void deleteTask()
    {
        Console.WriteLine("Enter task's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_dalTask!.Delete(id);//call the Delete function
    }//delete a wanted task

    static void deleteDependency()
    {
        Console.WriteLine("Enter dependency's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_dalDependency!.Delete(id);//call the Delete function
    }//delete a wanted dependency
    #endregion
    static void engineerMenu()//the menu of operations on engineers
    {
        while (true)//run till the user enter '1' to exit
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());//get the choise
            try
            {
                switch (methodChoice)
                {
                    case 1://exit
                        return;
                    case (int)CRUD.CREATE://2
                        int returnedId = createEngineer();//get the id of the created engineer
                        Console.WriteLine($"The engineer's id: {returnedId}\n");
                        break;
                    case (int)CRUD.READ://3
                        Console.WriteLine(readEngineer());//print the wanted engineer
                        break;
                    case (int)CRUD.READALL://4
                        List<Engineer> returnedList = readAllEngineers();//get the copy of the list
                        foreach (var en in returnedList)//print all the object
                            Console.WriteLine(en);
                        break;
                    case (int)CRUD.UPDATE://5
                        updateEngineers();//call the update function
                        break;
                    case (int)CRUD.DELETE://6
                        deleteEngineer();//call the delete function
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)//catch exceptions in this menu
            {
                Console.WriteLine(ex);
            }
        }
    }
    static void taskMenu()//the menu of operations on tasks
    {
        while (true)//run till the user enter '1' to exit
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());//get the choise
            try
            {
                switch (methodChoice)
                {
                    case 1://exit
                        return;
                    case (int)CRUD.CREATE://2
                        int returnedId = createTask();//get the id of the created task
                        Console.WriteLine($"The task's id: {returnedId}\n");
                        break;
                    case (int)CRUD.READ://3
                        Console.WriteLine(readTask());//print the wanted task
                        break;
                    case (int)CRUD.READALL://4
                        List<DO.Task> returnedList = readAllTasks();//get the copy of the list
                        foreach (var task in returnedList)//print all the object
                            Console.WriteLine(task);
                        break;
                    case (int)CRUD.UPDATE://5
                        updateTasks();//call the update function
                        break;
                    case (int)CRUD.DELETE://6
                        deleteTask();//call the delete function
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)//catch exceptions in this menu
            {
                Console.WriteLine(ex);
            }
        }
    }
    static void dependencyMenu()//the menu of operations on dependencies
    {
        while (true)//run till the user enter '1' to exit
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());//get the choise
            try
            {
                switch (methodChoice)
                {
                    case 1://exit
                        return;
                    case (int)CRUD.CREATE://2
                        int returnedId = createDependency();//get the id of the created dependency
                        Console.WriteLine($"The dependency's id: {returnedId} \n");
                        break;
                    case (int)CRUD.READ://3
                        Console.WriteLine(readDependency());//print the wanted dependency
                        break;
                    case (int)CRUD.READALL://4
                        List<Dependency> returnedList = readAllDependencies();//get the copy of the list
                        foreach (var dep in returnedList)//print all the object
                            Console.WriteLine(dep);
                        break;
                    case (int)CRUD.UPDATE://5
                        updateDependenies();//call the update function
                        break;
                    case (int)CRUD.DELETE://6
                        deleteDependency();//call the delete function
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)//catch exceptions in this menu
            {
                Console.WriteLine(ex);
            }
        }
    }
    static void mainMenu()
    {
        while (true)
        {
            Console.WriteLine("Choose an entity that you whant to check:\n 0 to exit\n 1 to Engineer\n 2 to Task\n 3 to Dependency\n");
            int entityChoice = Convert.ToInt32(Console.ReadLine());
            switch (entityChoice)
            {
                case 0:
                    return;
                case (int)EntityType.ENGINEER:
                    engineerMenu();
                    break;
                case (int)EntityType.TASK:
                    taskMenu();
                    break;
                case (int)EntityType.DEPENDENCY:
                    dependencyMenu();
                    break;
            }
        }
    }
}