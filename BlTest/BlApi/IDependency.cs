
namespace BlTest.BlApi;

public interface IDependency
{
    int Create(BO.Dependency item); //Creates new entity object in DAL
    BO.Dependency? Read(int id); //Reads entity object by its ID 
    BO.Dependency? Read(Func<BO.Dependency, bool> filter);
    IEnumerable<BO.Dependency?> ReadAll(Func<BO.Dependency, bool>? filter = null);//Reads all entity objects
    void Update(BO.Dependency item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
