using CoreEntity;
using CoreEntity.RepositoryInterfaces;
using DBAccessLibrary.DBEntities;
using System.Linq;

namespace DBAccessLibrary.Repositories
{
    /// <summary>
    ///  The implemetation of IGenericRepositoryForNamedEntities. 
    ///  Note also that it inherits from GenericRepository.
    /// </summary>
    public class GenericRepositoryForNamedEntities<T>:GenericRepository<T>, IGenericRepositoryForNamedEntities<T> where T : BaseNamedEntity
    {
        public GenericRepositoryForNamedEntities(DBContext context) : base(context)
        {
        }

        public bool CheckIfNameExists(string name, int exceptId = 0)
        {
            return _context.Set<T>().Any((m => m.Name.ToLower() == name.ToLower() && (exceptId == 0 || m.Id != exceptId)));
        }
    }
}
