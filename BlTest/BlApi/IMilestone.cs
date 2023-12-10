
namespace BlTest.BlApi;
/// <summary>
/// Milestone Interface
/// </summary>
public interface IMilestone
{
    void CreateProjectsSchedule();
    /// <summary>
    /// Reads entity object by its ID
    /// </summary>
    /// <param name="id">int</param>
    /// <returns>type BO.Milestone?</returns>
    BO.Milestone? Read(int id);
    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">BO.Milestone NN</param>
    void Update(BO.Milestone item); 
}
