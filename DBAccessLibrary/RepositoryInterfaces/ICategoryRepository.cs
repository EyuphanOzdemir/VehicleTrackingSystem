using CoreEntity.RepositoryInterfaces;
using DBAccessLibrary.DBEntities;

namespace DBAccessLibrary.Repositories
{
    /// <summary>
    ///  Interface for CategoryRepository.
    /// </summary>
    public interface ICategoryRepository: IGenericRepositoryForNamedEntities<Category>
    {
        public bool CheckIfMinWeightExists(decimal minWeight, int exceptId = 0);
    }
}
