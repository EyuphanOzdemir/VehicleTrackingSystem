using CoreEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DBAccessLibrary.DBEntities
{    /// <summary>
     ///  The interesting point here is that CategoryId is a computed field defined in SQL Server.
     /// </summary>
    public partial class Vehicle : BaseEntity
    {
        public string OwnerName { get; set; }
        public int ManifacturerId { get; set; }
        public int YearOfManifacture { get; set; }
        public decimal Weight { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? CategoryId { get; set; }

        public virtual Manifacturer Manifacturer { get; set; }
    }
}
