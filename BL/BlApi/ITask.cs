

namespace BlApi;
/// <summary>
/// Task Interface
/// </summary>
public interface ITask
{
    /// <summary>
    /// Creates new entity object of Task
    /// </summary>
    /// <param name="item">BO.Task</param>
    /// <returns>Id number of Task,type int</returns>
    int Create(BO.Task item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">Task Id</param>
    /// <returns>type BO.Task? </returns>
    BO.Task? Read(int id);
    /// <summary>
    /// Reads entity object according to given condition
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>type BO.Task?</returns>
    BO.Task? Read(Func<BO.Task, bool> filter);
    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">a condition, type Func<BO.Task, bool>?</param>
    /// <returns>type IEnumerable<BO.Task?></returns>
    IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null);
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">BO.Task</param>
    void Update(BO.Task item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">Task Id</param>
    void Delete(int id);
}
