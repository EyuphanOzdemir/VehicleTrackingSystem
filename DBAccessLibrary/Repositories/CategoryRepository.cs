using DBAccessLibrary.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DBAccessLibrary.Repositories
{
    /// <summary>
    ///  With this, we can use methods for categories from 3 different classes: GenericRepository, GenericRepositoryForNamedEntities and 
    ///  CategoryRepository.
    /// </summary>
    public class CategoryRepository: GenericRepositoryForNamedEntities<Category>, ICategoryRepository
    {
        public CategoryRepository(DBContext context) : base(context)
        {
        }

        public bool CheckIfMinWeightExists(decimal minWeight, int exceptId = 0)
        {
            return _context.Categories.Any((m => m.MinWeight == minWeight && (exceptId == 0 || m.Id != exceptId)));
        }
    }
}
