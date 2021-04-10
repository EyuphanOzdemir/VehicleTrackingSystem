namespace CoreEntity.RepositoryInterfaces
{
    /// <summary>
    ///  This interface inherits from the IGenericRepository and adds some methods that can work for NamedEntities.
    /// </summary>
    public interface IGenericRepositoryForNamedEntities<T>:IGenericRepository<T> where T:BaseNamedEntity
    {
        bool CheckIfNameExists(string name, int exceptId = 0);
    }
}
