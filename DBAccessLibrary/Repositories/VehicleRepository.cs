using DBAccessLibrary.DBEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DBAccessLibrary.Repositories
{    /// <summary>
     ///  With this, we can use methods for vehicles from 3 different classes: GenericRepository, GenericRepositoryForNamedEntities and 
     ///  VehicleRepository.
     /// </summary>
    public class VehicleRepository: GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DBContext context) : base(context)
        {

        }
    }
}
