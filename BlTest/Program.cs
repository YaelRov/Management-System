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
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");

        string _name = Console.ReadLine()?? throw new BlInvalidInput("Name cannot be empty string.\n");
        
        string _email = Console.ReadLine()?? throw new BlInvalidInput("Email cannot be empty string.\n");
        
        BO.EngineerExperience _level;
        succesTryParse = Enum.TryParse( Console.ReadLine(), out _level);
        if (!succesTryParse)
            throw new BlInvalidInput("Invalid level.\n");

        double _cost;
        succesTryParse = double.TryParse(Console.ReadLine(), out _cost);
        if (!succesTryParse || _cost < 0)
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
        Console.WriteLine("Enter task's details: description, alias. Dates of: start, and complete.\n deliverables, remarks, engineer's id and complexity level:\n");

        bool succesTryParse;
        string _description = Console.ReadLine() ?? throw new BlInvalidInput("Description cannot be empty string.\n");//get details
        string _alias = Console.ReadLine() ?? throw new BlInvalidInput("Alias cannot be empty string.\n");

        DateTime _start_nn;        
        DateTime? _start;        
        succesTryParse = DateTime.TryParse(Console.ReadLine(), out _start_nn);
        _start = _start_nn;
        if (!succesTryParse)
            throw new BlInvalidInput("Invalid input.\n");

        DateTime _complete_nn;
        DateTime? _complete;
        succesTryParse = DateTime.TryParse(Console.ReadLine(), out _complete_nn);
        _complete = _complete_nn;
        if (!succesTryParse)
            throw new BlInvalidInput("Invalid input.\n");

        string? _deliverables = Console.ReadLine() ?? null;
        string? _remarks = Console.ReadLine() ?? null;


        int _engId_nn;
        int? _engId = null;
        succesTryParse = int.TryParse(Console.ReadLine(), out _engId_nn);
        if (succesTryParse && _engId_nn > 0)
            _engId = _engId_nn;

        EngineerInTask? _engineer = null;
        Engineer? checkExistingEngineer;
        if (_engId is not null) {
            checkExistingEngineer = s_bl!.Engineer.Read((int)_engId!) ?? throw new BlDoesNotExistException($"An object of type Engineer with ID {_engId} does not exist");
            _engineer = new() { Id = (int)_engId, Name = checkExistingEngineer.Name };
            };

        BO.EngineerExperience _complexityLevel_nn;
        BO.EngineerExperience? _complexityLevel;
        succesTryParse = Enum.TryParse(Console.ReadLine(), out _complexityLevel_nn);
        if (!succesTryParse)
            throw new BlInvalidInput("Invalid complexity level.\n");
        _complexityLevel = _complexityLevel_nn;

        BO.Task newTask = new() {
            Id = -1,
            Description = _description,
            Alias = _alias,
            CreatedAt = DateTime.Now,
            Status = null,
            Milestone = null,
            BaselineStartDate = null,
            Start = _start,
            ScheduledDate = null,
            ForecastDate = null,
            Deadline = null,
            Complete = _complete,
            Deliverables = _deliverables,
            Remarks = _remarks,
            Engineer = _engineer,
            ComplexityLevel = _complexityLevel };
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
        bool succesTryParse;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");
        Engineer? returnedEng = s_bl!.Engineer.Read(_id);//call the read function
        if (returnedEng is null)//if the wanted object does not exist
            throw new BlDoesNotExistException($"An object of type Engineer with ID {_id} does not exist");
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
        bool succesTryParse;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");
        BO.Task? returnedTask = s_bl!.Task.Read(_id);//call the read function
        if (returnedTask is null)//if the wanted object does not exist
            throw new BlDoesNotExistException($"An object of type Task with ID {_id} does not exist");
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
        bool succesTryParse;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");
        Milestone? returnedMil = s_bl!.Milestone.Read(_id);//call the read function
        if (returnedMil is null)//if the wanted object does not exist
            throw new BlDoesNotExistException($"An object of type Milestone with ID {_id} does not exist");
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
        string? userInput;//a variable that the input that the user enters assign in it
        bool succesTryParse;
        Console.WriteLine("Enter id of engineer to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Engineer baseEng = s_bl!.Engineer.Read(id) ?? throw new BlDoesNotExistException("Engineer with this id does not exist");//reading the engineer with the enterd id
        Console.WriteLine(baseEng);//printing the details of the engineer
        Console.WriteLine("Enter engineer's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the engineer for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("name:");
        userInput = Console.ReadLine();
        string _name = string.IsNullOrEmpty(userInput) ? baseEng.Name : userInput;

        Console.WriteLine("email:");
        userInput = Console.ReadLine();
        string _email = string.IsNullOrEmpty(userInput) ? baseEng.Email : userInput;

        Console.WriteLine("level:");
        userInput = Console.ReadLine();
        EngineerExperience _level = baseEng.Level;
        if(!string.IsNullOrEmpty(userInput))
        {
            succesTryParse=Enum.TryParse(userInput, out _level);
            if (succesTryParse is false)
                _level = baseEng.Level;
        }

        Console.WriteLine("cost:");
        userInput = Console.ReadLine();
        double _cost = baseEng.Cost;
        if (!string.IsNullOrEmpty(userInput))
        {
            succesTryParse = double.TryParse(userInput, out _cost);
            if (succesTryParse is false)
                _cost = baseEng.Cost;
        }

        Console.WriteLine("task id:");
        userInput = Console.ReadLine();
        int _taskId;
        if (baseEng.Task is not null)
            _taskId = baseEng.Task.Id;
        TaskInEngineer? _task = baseEng.Task;
        if (!string.IsNullOrEmpty(userInput))
        {
            int _taskId_nn;
            succesTryParse = int.TryParse(userInput, out _taskId_nn);
            if (succesTryParse is true)
            {
                _taskId = _taskId_nn;
                BO.Task checkExistingTask;
                checkExistingTask = s_bl!.Task.Read((int)_taskId!)!;
                _task = new() { Id = _taskId, Alias = checkExistingTask.Alias };
            }
        }

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
        string? userInput = null;//a variable that the input that the user enters assign in it
        Console.WriteLine("Enter id of task to update");
        bool succesTryParse = false;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");
        BO.Task baseTask = s_bl!.Task.Read(_id) ?? throw new BlDoesNotExistException("Task with this id does not exist");//reading the task with the enterd id
        Console.WriteLine(baseTask);//printing the details of the task

        Console.WriteLine("Enter task's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the task for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("description:");
        userInput = Console.ReadLine();
        string _description = string.IsNullOrEmpty(userInput) ? baseTask.Description : userInput;

        Console.WriteLine("alias:");
        userInput = Console.ReadLine();
        string _alias = string.IsNullOrEmpty(userInput) ? baseTask.Alias : userInput;

        Console.WriteLine("date of start:");
        DateTime _start_nn;
        DateTime? _start;
        userInput = Console.ReadLine();
        _start = baseTask.Start;
        if(!string.IsNullOrEmpty(userInput))
        {
            succesTryParse = Enum.TryParse(userInput, out _start_nn);
            if (succesTryParse)
                _start = _start_nn;
        }

        Console.WriteLine("date of complete:");
        DateTime _complete_nn;
        DateTime? _complete;
        userInput = Console.ReadLine();
        _complete = baseTask.Complete;
        if (!string.IsNullOrEmpty(userInput))
        {
            succesTryParse = Enum.TryParse(userInput, out _complete_nn);
            if (succesTryParse)
                _complete = _complete_nn;
        }

        Console.WriteLine("deliverables:");
        userInput = Console.ReadLine()!;
        string? _deliverables = string.IsNullOrEmpty(userInput) ? baseTask.Deliverables : userInput;

        Console.WriteLine("remarks:");
        userInput = Console.ReadLine()!;
        string? _remarks = string.IsNullOrEmpty(userInput) ? baseTask.Remarks : userInput;
       
        Console.WriteLine("engineer id:");
        userInput = Console.ReadLine();
        int _engId;
        if (baseTask.Engineer is not null)
            _engId = baseTask.Engineer.Id;
        EngineerInTask? _eng = baseTask.Engineer;
        if (!string.IsNullOrEmpty(userInput))
        {
            int _engId_nn;
            succesTryParse = int.TryParse(userInput, out _engId_nn);
            if (succesTryParse is true)
            {
                _engId = _engId_nn;
                BO.Engineer checkExistingEng;
                checkExistingEng = s_bl!.Engineer.Read((int)_engId!)!;
                _eng = new() { Id = _engId, Name = checkExistingEng.Name };
            }
        }
        Console.WriteLine("complexity level:");
        userInput = Console.ReadLine();
        EngineerExperience? _complexityLevel = baseTask.ComplexityLevel;
        EngineerExperience _complexityLevel_nn;
        if (!string.IsNullOrEmpty(userInput))
        {
            succesTryParse = Enum.TryParse(userInput, out _complexityLevel_nn);
            if (succesTryParse is false)
                _complexityLevel = baseTask.ComplexityLevel;
            else
                _complexityLevel = _complexityLevel_nn;
        }
        //creating a new entity of task with the details
        BO.Task updateTask = new() { Id=_id, Description=_description, Alias=_alias,CreatedAt= baseTask.CreatedAt, Status = baseTask.Status, Milestone = baseTask.Milestone, BaselineStartDate = baseTask.BaselineStartDate, Start = _start, ScheduledDate= baseTask.ScheduledDate, ForecastDate= baseTask.ForecastDate,  Deadline=baseTask.Deadline,Complete= _complete,Deliverables= _deliverables,Remarks= _remarks,Engineer= _eng,ComplexityLevel= _complexityLevel };
        s_bl!.Task.Update(updateTask);//updating the tasks list
    }
    /// <summary>
    /// a function that updates the details of milestone, doesn't return anything
    /// </summary>
    /// <exception cref="Exception"></exception>
    static void updateMilestone()
    {
        string? userInput;//a variable that the input that the user enters assign in it
        Console.WriteLine("Enter id of milestone to update");
        int id = Convert.ToInt32(Console.ReadLine());
        Milestone baseMil = s_bl!.Milestone.Read(id) ?? throw new BlDoesNotExistException("Milestone with this id does not exist");//reading the milestone with the enterd id
        Console.WriteLine(baseMil);//printing the details of the milestone
        Console.WriteLine("Enter milestone's details to update. If you don't want to change press enter.\n");
        //getting from the user the details for each field of the milestone for string-type variable, 
        //and than: if it empty string, assign the last value of this field, and if not- assign the user input
        Console.WriteLine("descriptons:");
        userInput = Console.ReadLine();
        string? _descriptions = string.IsNullOrEmpty(userInput) ? baseMil.Description : userInput;
        Console.WriteLine("alias:");
        userInput = Console.ReadLine();
        string? _alias = string.IsNullOrEmpty(userInput) ? baseMil.Alias : userInput;
        Console.WriteLine("remarks:");
        userInput = Console.ReadLine();
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
        bool succesTryParse = false;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);//get the id of the object to delete
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");
        s_bl!.Engineer.Delete(_id);//call the Delete function
    }
    /// <summary>
    ///delete a reguested task
    /// </summary>
    static void deleteTask()
    {
        Console.WriteLine("Enter task's id for deleting:\n");
        bool succesTryParse = false;
        int _id;
        succesTryParse = int.TryParse(Console.ReadLine(), out _id);//get the id of the object to delete
        if (!succesTryParse || _id < 0)
            throw new BlInvalidInput("Invalid id number.\n");
        s_bl!.Task.Delete(_id);//call the Delete function
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
