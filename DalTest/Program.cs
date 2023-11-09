using Dal;
using DalApi;

namespace DalTest
{
    internal class Program
    {
        private static IEngineer? s_dalEngineer =new EngineerImplementation();
        private static ITask? s_dalTask = new TaskImplementation();
        private static IDependency? s_dalDependency = new DependencyImplementation();
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
                Console.WriteLine("Hello, World!");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }    
        }

        int createEntity(int entityChoice)
        {
           switch(entityChoice)
           {
                case 1:
                    Console.ReadLine("enter the details for engineer\n");
                    break;
                case 2:
                    Console.ReadLine();
                    break;
                case 3:
                    Console.ReadLine();
                    break;
            }
            return 1;
        }
        void engineerMenu(int entityChoice)
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());
        }
        void taskMenu(int entityChoice)
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());
        }
        void dependencyMenu(int entityChoice)
        {
            Console.WriteLine("Choose the method that you want to execute:\n 1 to exit\n 2 to Create\n 3 to Read\n 4 to ReadAll\n 5 to Update\n 6 to Delete");
            int methodChoice = Convert.ToInt32(Console.ReadLine());
        }
        void mainMenu()
        {
            Console.WriteLine("Choose an entity that you whant to check:\n 0 to exit\n 1 to Engineer\n 2 to Task\n 3 to Dependency\n");
            int entityChoice = Convert.ToInt32(Console.ReadLine());
            switch(entityChoice)
            {
                case
            }
        }


    }
}