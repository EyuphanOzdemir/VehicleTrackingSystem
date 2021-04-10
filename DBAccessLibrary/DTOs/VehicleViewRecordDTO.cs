using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBAccessLibrary.DTOs
{    /// <summary>
     ///  The DTO class corresponding the Vehicle view records.
     /// </summary>
    public class VehicleViewRecordDTO
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }

        public int ManifacturerId { get; set; }
        public string Manifacturer { get; set; }

        public int YearOfManifacture { get; set; }

        public int CategoryId { get; set; }

        public decimal Weight { get; set; }

        public string CategoryName { get; set; }

        public string IconFileName { get; set; }
    }
}
