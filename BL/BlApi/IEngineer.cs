

namespace BlApi;
/// <summary>
/// Engineer Interface
/// </summary>
public interface IEngineer
{
    /// <summary>
    /// Creates new entity object of Engineer
    /// </summary>
    /// <param name="item">BO.Engineer</param>
    /// <returns>id number of the engineer , int</returns>
    int Create(BO.Engineer item);
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>BO.Engineer</returns>
    BO.Engineer? Read(int id);
    /// <summary>
    /// Reads entity object according to a condition
    /// </summary>
    /// <param name="filter">function</param>
    /// <returns>BO.Engineer</returns>
    BO.Engineer? Read(Func<BO.Engineer, bool> filter);
    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">function</param>
    /// <returns>IEnumerable BO.Engineer</returns>
    IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null);
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">BO.Engineer</param>
    void Update(BO.Engineer item);
    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">int</param>
    void Delete(int id);
}
