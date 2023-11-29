using Dal;
using DalApi;
using DO;
using System.Data.SqlTypes;

namespace DalTest;
internal class Program
{
    //defining interface variable that gets the implementation
    //static readonly IDal s_dal = new DalList();//stage 2
    static readonly IDal s_dal = new DalXml(); //stage 3
    static void Main(string[] args)
    {
        try
        {
            //calling the method that initializes the database
            Initialization.Do(s_dal);
            mainMenu();//calling the method of the main menu
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #region create methods
    /// <summary>
    /// cteate a new entity of engineer
    /// </summary>
    /// <returns>the id of the new engineer</returns>
    static int createEngineer()
    {
        Console.WriteLine("Enter engineer's details: id, name, email, level and cost:\n");
        int _id = Convert.ToInt32(Console.ReadLine());//get the details
        string _name = Console.ReadLine()!;
        string _email = Console.ReadLine()!;
        EngineerExperience _level =(EngineerExperience)Enum.Parse(typeof(EngineerExperience),Console.ReadLine()!);
        double _cost = Convert.ToDouble(Console.ReadLine());
        Engineer newEng = new(_id, _name, _email, _level, _cost);//create new enginner
        return s_dal!.Engineer.Create(newEng);//add to the list
    }
    /// <summary>
    ///cteate a new entity of task and
    /// </summary>
    /// <returns>the id of the new task</returns>
    /// <exception cref="Exception"></exception>
    static int createTask()
    {
        //Console.WriteLine("Enter task's details: description, alias, milestone.\n dates of: creating, start, scheduled date, forecast , deadline and complete.\n deliverables, remarks, engineer's id and complexity level:\n");
        //string _description = Console.ReadLine()!;//get details
        //string _alias = Console.ReadLine()!;
        //bool _milestone = Convert.ToBoolean(Console.ReadLine()!);
        //DateTime _createdAt = Convert.ToDateTime(Console.ReadLine()!);
        //DateTime? _start = Convert.ToDateTime(Console.ReadLine());
        //DateTime? _scheduledDate = Convert.ToDateTime(Console.ReadLine());
        //DateTime? _forecastDate = Convert.ToDateTime(Console.ReadLine());
        //DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
        //DateTime? _complete = Convert.ToDateTime(Console.ReadLine());
        //string? _deliverables = Console.ReadLine();
        //string? _remarks = Console.ReadLine();
        //int? _engineerId = Convert.ToInt32(Console.ReadLine());
        //Engineer checkExistingEngineer = s_dal!.Engineer.Read((int)_engineerId!) ?? throw new DalDoesNotExistException($"An object of type Engineer with ID {_engineerId} does not exist");
        //EngineerExperience _complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
        //DO.Task newTask = new(-1, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);
        DateTime d = new();
        DO.Task newTask = new(-1, "a", "b", true,d, null, null, null, null, null, null, null, null, null);
        return s_dal!.Task.Create(newTask);//add to the list
    }

    /// <summary>
    /// cteate a new entity of dependency
    /// </summary>
    /// <returns>the id of the new dependency</returns>
    /// <exception cref="Exception"></exception>
    static int createDependency()
    {
        Console.WriteLine("Enter dependency's details: dependent task and depends-on task:\n");
        int? _dependentTask = Convert.ToInt32(Console.ReadLine());//get details
        DO.Task checkExisting1 = s_dal!.Task.Read((int)_dependentTask!) ?? throw new DalDoesNotExistException($"An object of type Task with ID {_dependentTask} does not exist");//check if the id of the task exsists
        int? _dependsOnTask = Convert.ToInt32(Console.ReadLine());
        DO.Task checkExisting2 = s_dal!.Task.Read((int)_dependentTask!) ?? throw new DalDoesNotExistException($"An object of type Task with ID {_dependentTask} does not exist");//check if the id of the task exsists
        Dependency newDep = new(-1, _dependentTask, _dependsOnTask);
        return s_dal!.Dependency.Create(newDep);//add to the list
    }
    #endregion

    #region read methods
    /// <summary>
    /// function for reading specific engineer by it's id
    /// </summary>
    /// <returns>an object of the reguested engineer</returns>
    /// <exception cref="Exception"></exception>
    static Engineer? readEngineer()
    {
        Console.WriteLine("Enter engineer's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Engineer? returnedEng = s_dal!.Engineer.Read(id);//call the read function
        if(returnedEng is null)//if the wanted object does not exist
            throw new DalDoesNotExistException($"An object of type Engineer with ID {id} does not exist");
        return returnedEng;//return the found object
    }
    /// <summary>
    /// function for reading specific task by it's id
    /// </summary>
    /// <returns> an object of the requested task</returns>
    /// <exception cref="Exception"></exception>
    static DO.Task? readTask()
    {
        Console.WriteLine("Enter task's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        DO.Task? returnedTask = s_dal!.Task.Read(id);//call the read function
        if ( returnedTask is null)//if the wanted object does not exist
            throw new DalDoesNotExistException($"An object of type Task with ID {id} does not exist");
        return returnedTask;//return the found object
    }
    /// <summary>
    /// function for reading specific dependency by it's id
    /// </summary>
    /// <returns>an object of the requested dependency</returns>
    /// <exception cref="Exception"></exception>
    static Dependency? readDependency()
    {
        Console.WriteLine("Enter dependency's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Dependency? returnedDep= s_dal!.Dependency.Read(id);//call the read function
        if (returnedDep is null)//if the wanted object does not exist
            throw new DalDoesNotExistException($"An object of type Dependency with ID {id} does not exist");
        return returnedDep;//return the found object
    }
    #endregion

    #region readAll methods
    /// <summary>
    /// function for reading all the engineers
    /// </summary>
    /// <returns>a copy of the engineers list</returns>
    static List<Engineer> readAllEngineers()
    {
        return s_dal!.Engineer.ReadAll().ToList()!;
    }

    /// <summary>
    /// function for reading all the tasks
    /// </summary>
    /// <returns>a copy of the tasks list</returns>
    static List<DO.Task> readAllTasks()
    {
        return s_dal!.Task.ReadAll().ToList()!;
    }

    /// <summary>
    /// function for reading all the dependencies
    /// </summary>
    /// <returns>a copy of the dependencies list</returns>
    static List<Dependency> readAllDependencies()
    {
        return s_dal!.Dependency.ReadAll().ToList()!;
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
        Engineer baseEng = s_dal!.Engineer.Read(id) ?? throw new DalDoesNotExistException("Engineer with this id does not exist");//reading the engineer with the enterd id
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
        s_dal!.Engineer.Update(updateEng);//updating the engineers list
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
        DO.Task baseTask = s_dal!.Task.Read(id) ?? throw new DalDoesNotExistException("Task with this id does not exist");//reading the task with the enterd id
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
        Engineer checkExistingEngineer = s_dal!.Engineer.Read((int)_engineerId!) ?? throw new DalDoesNotExistException($"An object of type Engineer with ID {_engineerId} does not exist");


        Console.WriteLine("complexity level:");
        userInput = Console.ReadLine()!;
        EngineerExperience? _complexityLevel = string.IsNullOrEmpty(userInput) ? baseTask.ComplexityLevel : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), userInput); ;
        DO.Task updateTask = new (id, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _forecastDate, _deadline, _complete, _deliverables, _remarks, _engineerId, _complexityLevel);//creating a new entity of task with the details
        s_dal!.Task.Update(updateTask);//updating the tasks list
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
        Dependency baseDep = s_dal!.Dependency.Read(id) ?? throw new DalDoesNotExistException("Dependency with this id does not exist");//reading the dependency with the enterd id
        Console.WriteLine(baseDep);//printing the details of the dependency
        Console.WriteLine("Enter dependency's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the dependency for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("dependent task:");
        userInput = Console.ReadLine()!;
        int? _dependentTask = string.IsNullOrEmpty(userInput) ? baseDep.DependentTask : Convert.ToInt32(userInput);
        //checking if the task with the entered id is exists
        DO.Task checkExisting1 = s_dal!.Task.Read((int)_dependentTask!) ?? throw new DalDoesNotExistException($"An object of type Task with ID {_dependentTask} does not exist");


        Console.WriteLine("depends on task:");
        userInput = Console.ReadLine()!;
        int? _dependsOnTask = string.IsNullOrEmpty(userInput) ? baseDep.DependsOnTask : Convert.ToInt32(userInput);
        //checking if the task with the entered id is exists
        DO.Task checkExisting2 = s_dal!.Task.Read((int)_dependentTask!) ?? throw new DalDoesNotExistException($"An object of type Task with ID {_dependentTask} does not exist");

        Dependency updateDep = new(id, _dependentTask, _dependsOnTask);//creating a new entity of dependency with the details
        s_dal!.Dependency.Update(updateDep);//updating the dependencies list
    }
    #endregion

    #region delete methods
    /// <summary>
    /// delete a requested engineer
    /// </summary>
    static void deleteEngineer()
    {
        Console.WriteLine("Enter engineer's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_dal!.Engineer.Delete(id);//call the Delete function
    }
    /// <summary>
    ///delete a reguested task
    /// </summary>
    static void deleteTask()
    {
        Console.WriteLine("Enter task's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_dal!.Task.Delete(id);//call the Delete function
    }
    /// <summary>
    ///delete a requested dependency
    /// </summary>
    static void deleteDependency()
    {
        Console.WriteLine("Enter dependency's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_dal!.Dependency.Delete(id);//call the Delete function
    }
    #endregion

    /// <summary>
    /// the menu of operations on engineers
    /// </summary>
    static void engineerMenu()
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
    /// <summary>
    /// the menu of operations on tasks
    /// </summary>
    static void taskMenu()
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
    /// <summary>
    /// the menu of operations on dependencies
    /// </summary>
    static void dependencyMenu()
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