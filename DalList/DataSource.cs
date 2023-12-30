
namespace Dal;
internal static class DataSource
{
  #region Config
    internal static class Config//set barcodes for tasks and dependencies
    {
        //barcodes for tasks
        internal const int startTaskId = 1000;//barcodes start running from 1000
        private static int nextTaskId = startTaskId;//reset
        internal static int NextTaskId { get => nextTaskId++; }//return the next code and move on
        //barcodes for dependencies
        internal const int startDependId = 10000;//barcodes start running from 10000
        private static int nextDependId = startDependId;//reset
        internal static int NextDependId { get => nextDependId++; }//return the next code and move on
        
        internal static DateTime? startProjectDate=null;
        internal static DateTime? endProjectDate = null;

    }
    #endregion

    internal static List<DO.Engineer> Engineers { get; } = new();//list of engineers, can only be accessed by an implementation function so there is no set function
    internal static List<DO.Task> Tasks { get; } = new();//list of tasks, can only be accessed by an implementation function so there is no set function
    internal static List<DO.Dependency> Dependencies { get; } = new();//list of dependenies, can only be accessed by an implementation function so there is no set function
}
