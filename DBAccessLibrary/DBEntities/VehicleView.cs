using CoreEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessLibrary.DBEntities
{    /// <summary>
     ///  This class represents the VehicleView in the DB. This way we dont need to create such a model using EF/Linq in memory.
     ///  The view holds all the required additional fields or computed fields.
     /// </summary>
    public class VehicleView:BaseEntity
    {
        public string OwnerName { get; set; }

        public string Manifacturer { get; set; }

        public int ManifacturerId { get; set; }

        public int YearOfManifacture { get; set; }

        public int CategoryId { get; set; }

        public decimal Weight { get; set; }

        public string CategoryName { get; set; }

        public string IconFileName { get; set; }
    }
}
