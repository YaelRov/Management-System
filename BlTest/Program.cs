using BO;
using DalApi;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do(); //calling the method that initializes the database
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
        bool succesTryParse;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);
        if (!succesTryParse || _id > 0)
            throw new BlInvalidInput("Invalid id number.\n");

        string _name = Console.ReadLine()?? throw new BlInvalidInput("Name cannot be empty string.\n");
        
        string _email = Console.ReadLine()?? throw new BlInvalidInput("Email cannot be empty string.\n");
        
        BO.EngineerExperience _level;
        succesTryParse = Enum.TryParse( Console.ReadLine(), out _level);
        if (!succesTryParse)
            throw new BlInvalidInput("Invalid level.\n");

        double _cost;
        succesTryParse = double.TryParse(Console.ReadLine(), out _cost);
        if (!succesTryParse || _cost > 0)
            throw new BlInvalidInput("Invalid cost number.\n");
        //int? _taskId = Convert.ToInt32(Console.ReadLine()?? null);
        //string? _taskAlias = Console.ReadLine() ?? null;
        //TaskInEngineer? _task = null;
        //if (_taskId is not null && _taskAlias is not null)
        //    _task = new() { Id = (int)_taskId, Alias = _taskAlias };
        //create new enginner
        BO.Engineer newEng = new() { Id=_id, Name=_name, Email=_email, Level=_level, Cost=_cost, Task= null };
        return s_bl!.Engineer.Create(newEng);//add to the list
    }
    /// <summary>
    ///cteate a new entity of task and
    /// </summary>
    /// <returns>the id of the new task</returns>
    /// <exception cref="Exception"></exception>
    static int createTask()
    {
        Console.WriteLine("Enter task's details: description, alias, milestone.\n dates of: creating, start, scheduled date, forecast , deadline and complete.\n deliverables, remarks, engineer's id and complexity level:\n");
        string _description = Console.ReadLine()!;//get details
        string _alias = Console.ReadLine()!;
        DateTime _createdAt = Convert.ToDateTime(Console.ReadLine()!);
        Status? _status = (Status)Enum.Parse(typeof(Status), Console.ReadLine()!);
        int? _milestoneId = Convert.ToInt32(Console.ReadLine() ?? null);
        string? _milestoneAlias = Console.ReadLine() ?? null;
        MilestoneInTask? _milestone = null;
        if (_milestoneId is not null && _milestoneAlias is not null)
            _milestone = new() { Id = (int)_milestoneId, Alias = _milestoneAlias };
        DateTime? _baselineStartDate = Convert.ToDateTime(Console.ReadLine());
        DateTime? _start = Convert.ToDateTime(Console.ReadLine());
        DateTime? _scheduledDate = Convert.ToDateTime(Console.ReadLine());
        DateTime? _forecastDate = Convert.ToDateTime(Console.ReadLine());
        DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
        DateTime? _complete = Convert.ToDateTime(Console.ReadLine());
        string? _deliverables = Console.ReadLine();
        string? _remarks = Console.ReadLine();
        int? _engId = Convert.ToInt32(Console.ReadLine() ?? null);
        Engineer checkExistingEngineer = s_bl!.Engineer.Read((int)_engId!) ?? throw new BlDoesNotExistException($"An object of type Engineer with ID {_engId} does not exist");
        string? _engName= Console.ReadLine() ?? null;
        EngineerInTask? _engineer = null;
        if (_engId is not null && _engName is not null)
            _engineer = new() { Id = (int)_engId, Name = _engName };
        EngineerExperience _complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
        BO.Task newTask = new() { Id = -1, Description = _description, Alias = _alias, CreatedAt = _createdAt, Status = _status, Milestone = _milestone, BaselineStartDate = _baselineStartDate, Start = _start, ScheduledDate = _scheduledDate, ForecastDate = _forecastDate, Deadline = _deadline, Complete = _complete, Deliverables = _deliverables, Remarks = _remarks, Engineer = _engineer, ComplexityLevel = _complexityLevel };
        return s_bl!.Task.Create(newTask);//add to the list
    }

    /// <summary>
    /// cteate a new entity of milestone
    /// </summary>
    /// <returns>the id of the new milestone</returns>
    /// <exception cref="Exception"></exception>
    //static int createMilestone()
    //{
    //    Console.WriteLine("Enter milestone's details: id, description, alias, status,\n created at date, forecast date, deadline date, complete date, copletion, perceltage and remarks\n");
    //    int _id = Convert.ToInt32(Console.ReadLine()!);//get details
    //    string _description = Console.ReadLine()!;//get details
    //    string _alias = Console.ReadLine()!;
    //    Status _status = (Status)Enum.Parse(typeof(Status), Console.ReadLine()!);
    //    DateTime _createdAt = Convert.ToDateTime(Console.ReadLine()!);
    //    DateTime? _forecastDate = Convert.ToDateTime(Console.ReadLine());
    //    DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
    //    DateTime? _complete = Convert.ToDateTime(Console.ReadLine());
    //    double? _perceltage = Convert.ToDouble(Console.ReadLine());
    //    string? _remarks = Console.ReadLine();
    //    Milestone newMilestone = new(_id, _description, _alias, _status, _createdAt, _forecastDate, _deadline, _complete, _perceltage, _remarks);
    //    return s_bl!.Milestone.Create(newMilestone);//add to the list
    //}
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
        Engineer? returnedEng = s_bl!.Engineer.Read(id);//call the read function
        if (returnedEng is null)//if the wanted object does not exist
            throw new BlDoesNotExistException($"An object of type Engineer with ID {id} does not exist");
        return returnedEng;//return the found object
    }
    /// <summary>
    /// function for reading specific task by it's id
    /// </summary>
    /// <returns> an object of the requested task</returns>
    /// <exception cref="Exception"></exception>
    static BO.Task? readTask()
    {
        Console.WriteLine("Enter task's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        BO.Task? returnedTask = s_bl!.Task.Read(id);//call the read function
        if (returnedTask is null)//if the wanted object does not exist
            throw new BlDoesNotExistException($"An object of type Task with ID {id} does not exist");
        return returnedTask;//return the found object
    }
    /// <summary>
    /// function for reading specific milestone by it's id
    /// </summary>
    /// <returns>an object of the requested milestone</returns>
    /// <exception cref="Exception"></exception>
    static Milestone? readMilestone()
    {
        Console.WriteLine("Enter milestone's id for reading:\n");
        int id = Convert.ToInt32(Console.ReadLine());
        Milestone? returnedMil = s_bl!.Milestone.Read(id);//call the read function
        if (returnedMil is null)//if the wanted object does not exist
            throw new BlDoesNotExistException($"An object of type Milestone with ID {id} does not exist");
        return returnedMil;//return the found object
    }
    #endregion

    #region readAll methods
    /// <summary>
    /// function for reading all the engineers
    /// </summary>
    /// <returns>a copy of the engineers list</returns>
    static List<Engineer> readAllEngineers()
    {
        return s_bl!.Engineer.ReadAll().ToList()!;
    }

    /// <summary>
    /// function for reading all the tasks
    /// </summary>
    /// <returns>a copy of the tasks list</returns>
    static List<BO.Task> readAllTasks()
    {
        return s_bl!.Task.ReadAll().ToList()!;
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
        Engineer baseEng = s_bl!.Engineer.Read(id) ?? throw new BlDoesNotExistException("Engineer with this id does not exist");//reading the engineer with the enterd id
        Console.WriteLine(baseEng);//printing the details of the engineer
        Console.WriteLine("Enter engineer's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the engineer for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("name:");
        userInput = Console.ReadLine()!;
        string _name = string.IsNullOrEmpty(userInput) ? baseEng.Name : userInput;

        Console.WriteLine("email:");
        userInput = Console.ReadLine()!;
        string _email = string.IsNullOrEmpty(userInput) ? baseEng.Email : userInput;

        Console.WriteLine("level:");
        userInput = Console.ReadLine()!;
        EngineerExperience _level = string.IsNullOrEmpty(userInput) ? baseEng.Level : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), userInput);

        Console.WriteLine("cost:");
        userInput = Console.ReadLine()!;
        double _cost = string.IsNullOrEmpty(userInput) ? baseEng.Cost : Convert.ToDouble(userInput);

        Console.WriteLine("task id:");
        userInput = Console.ReadLine()!;
        int? _taskId = string.IsNullOrEmpty(userInput) ? (baseEng.Task is not null)? baseEng.Task.Id : null : Convert.ToInt32(userInput);

        Console.WriteLine("task alias:");
        userInput = Console.ReadLine()!;
        string? _taskAlias = string.IsNullOrEmpty(userInput) ? (baseEng.Task is not null) ? baseEng.Task.Alias : null : userInput;

        TaskInEngineer? _task = null;
        if (_taskId is not null && _taskAlias is not null)
            _task = new() { Id = (int)_taskId, Alias = _taskAlias };

        //creating a new entity of engineer with the details
        BO.Engineer updateEng = new() { Id = id, Name = _name, Email = _email, Level = _level, Cost = _cost, Task = _task };

        s_bl!.Engineer.Update(updateEng);//updating the engineers list
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
        BO.Task baseTask = s_bl!.Task.Read(id) ?? throw new BlDoesNotExistException("Task with this id does not exist");//reading the task with the enterd id
        Console.WriteLine(baseTask);//printing the details of the task

        Console.WriteLine("Enter task's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the task for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("description:");
        userInput = Console.ReadLine()!;
        string _description = string.IsNullOrEmpty(userInput) ? baseTask.Description : userInput;

        Console.WriteLine("alias:");
        userInput = Console.ReadLine()!;
        string _alias = string.IsNullOrEmpty(userInput) ? baseTask.Alias : userInput;

        Console.WriteLine("date of creating:");
        userInput = Console.ReadLine()!;
        DateTime _createdAt = string.IsNullOrEmpty(userInput) ? baseTask.CreatedAt : Convert.ToDateTime(userInput);

        Console.WriteLine("status:");
        userInput = Console.ReadLine()!;
        Status? _status = string.IsNullOrEmpty(userInput) ? baseTask.Status : (Status)Enum.Parse(typeof(Status), userInput); ;

        Console.WriteLine("task id:");
        userInput = Console.ReadLine()!;
        int? _milestoneId = string.IsNullOrEmpty(userInput) ? (baseTask.Milestone is not null) ? baseTask.Milestone.Id : null : Convert.ToInt32(userInput);

        Console.WriteLine("task alias:");
        userInput = Console.ReadLine()!;
        string? _milestoneAlias = string.IsNullOrEmpty(userInput) ? (baseTask.Milestone is not null) ? baseTask.Milestone.Alias : null : userInput;


        MilestoneInTask? _milestone = null;
        if (_milestoneId is not null && _milestoneAlias is not null)
            _milestone = new() { Id = (int)_milestoneId, Alias = _milestoneAlias };
        Console.WriteLine("date of baseline start:");
        userInput = Console.ReadLine()!;
        DateTime? _baselineStart = string.IsNullOrEmpty(userInput) ? baseTask.Start : Convert.ToDateTime(userInput);

        Console.WriteLine("date of start:");
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

        Console.WriteLine("engineer id:");
        userInput = Console.ReadLine()!;
        int? _engineerId = string.IsNullOrEmpty(userInput) ? (baseTask.Engineer is not null) ? baseTask.Engineer.Id : null : Convert.ToInt32(userInput);
        //checking if the engineer with the entered id is exists
        Engineer checkExistingEngineer = s_bl!.Engineer.Read((int)_engineerId!) ?? throw new BlDoesNotExistException($"An object of type Engineer with ID {_engineerId} does not exist");

        Console.WriteLine("engineer name:");
        userInput = Console.ReadLine()!;
        string? _engineerName = string.IsNullOrEmpty(userInput) ? (baseTask.Engineer is not null) ? baseTask.Engineer.Name : null : userInput;
        EngineerInTask? _engineer=null;
        if (_engineerId is not null&& _engineerName is not null)
            _engineer = new() { Id = (int)_engineerId, Name=_engineerName};
        Console.WriteLine("complexity level:");
        userInput = Console.ReadLine()!;
        //creating a new entity of task with the details
        EngineerExperience? _complexityLevel = string.IsNullOrEmpty(userInput) ? baseTask.ComplexityLevel : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), userInput); 
        BO.Task updateTask = new() { Id=id, Description=_description, Alias=_alias,CreatedAt= _createdAt, Status=_status, Milestone=_milestone, BaselineStartDate = _baselineStart,Start= _start,ScheduledDate= _scheduledDate, ForecastDate= _forecastDate,  Deadline=_deadline,Complete= _complete,Deliverables= _deliverables,Remarks= _remarks,Engineer= _engineer,ComplexityLevel= _complexityLevel };
        s_bl!.Task.Update(updateTask);//updating the tasks list
    }
    /// <summary>
    /// a function that updates the details of milestone, doesn't return anything
    /// </summary>
    /// <exception cref="Exception"></exception>
    static void updateMilestone()
    {
        string userInput;//a variable that the input that the user enters assign in it
        Console.WriteLine("Enter id of milestone to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Milestone baseMil = s_bl!.Milestone.Read(id) ?? throw new BlDoesNotExistException("Milestone with this id does not exist");//reading the milestone with the enterd id
        Console.WriteLine(baseMil);//printing the details of the milestone
        Console.WriteLine("Enter milestone's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the milestone for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("descriptons:");
        userInput = Console.ReadLine()!;
        string? _descriptions = string.IsNullOrEmpty(userInput) ? baseMil.Description : userInput;
        Console.WriteLine("alias:");
        userInput = Console.ReadLine()!;
        string? _alias = string.IsNullOrEmpty(userInput) ? baseMil.Alias : userInput;
        Console.WriteLine("remarks:");
        userInput = Console.ReadLine()!;
        string? _remarks = string.IsNullOrEmpty(userInput) ? baseMil.Remarks : userInput;
        //creating a new entity of milestone with the details
        Milestone updateMil = new(){Id=id, Description= _descriptions,Alias= _alias,Status= baseMil.Status,CreatedAtDate= baseMil.CreatedAtDate,ForecastDate= baseMil.ForecastDate,DeadlineDate= baseMil.DeadlineDate,CompleteDate= baseMil.CompleteDate,CompletionPercentage= baseMil.CompletionPercentage,Remarks= _remarks};

        s_bl!.Milestone.Update(updateMil);//updating the dependencies list
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
        s_bl!.Engineer.Delete(id);//call the Delete function
    }
    /// <summary>
    ///delete a reguested task
    /// </summary>
    static void deleteTask()
    {
        Console.WriteLine("Enter task's id for deleting:\n");
        int id = Convert.ToInt32(Console.ReadLine());//get the id of the object to delete
        s_bl!.Task.Delete(id);//call the Delete function
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
                        List<BO.Task> returnedList = readAllTasks();//get the copy of the list
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
    static void milestoneMenu()
    {
        while (true)//run till the user enter '1' to exit
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create Projects Schedule \n 3 to Read\n 4 Update\n ");
            int methodChoice = Convert.ToInt32(Console.ReadLine());//get the choise
            try
            {
                switch (methodChoice)
                {
                    case 1://exit
                        return;
                    case 2://Create Projects Schedule
                        s_bl.Milestone.CreateProjectsSchedule();
                        Console.WriteLine("Projects schedule created successfuly");
                        break;
                    case 3://read
                        Console.WriteLine(readMilestone());//print the wanted milestone
                        break;
                    case 4://update
                        updateMilestone();//call the update function
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
            Console.WriteLine("Choose an entity that you whant to check:\n 0 to exit\n 1 to Engineer\n 2 to Task\n 3 to Milestone\n");
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
                case (int)EntityType.MILESTONE://3
                    milestoneMenu();//calling the function that manages the milestone menu
                    break;
                default:
                    break;
            }
        }
    }
}
