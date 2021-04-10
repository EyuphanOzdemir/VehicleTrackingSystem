using DBAccessLibrary.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBAccessLibrary.Repositories
{
    /// <summary>
    ///  With this, we can use methods for manifacturers from 3 different classes: GenericRepository, GenericRepositoryForNamedEntities and 
    ///  ManifacturerRepository.
    /// </summary>
    public class ManifacturerRepository : GenericRepositoryForNamedEntities<Manifacturer>, IManifacturerRepository
    {
        public ManifacturerRepository(DBContext context) : base(context)
        {
        }

        public bool CheckRelatedVehicles(int manifacturerId)
        {
            return _context.Vehicles.Any(v => v.ManifacturerId == manifacturerId);
        }
    }
}
