using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBAccessLibrary.DTOs
{    /// <summary>
     ///  The DTO class corresponding the Vehicle entity.
     /// </summary>
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public int ManifacturerId { get; set; }
        public int YearOfManifacture { get; set; }
        public decimal Weight { get; set; }
    }
}
