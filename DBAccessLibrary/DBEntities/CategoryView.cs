using CoreEntity;
using System;
using System.Collections.Generic;

#nullable disable

namespace DBAccessLibrary.DBEntities
{    /// <summary>
     ///  This class represents the CategoryView in the DB.
     /// </summary>
    public class CategoryView:BaseEntity
    {
        public string Name { get; set; }
        public decimal MinWeight { get; set; }
        public decimal UpTo { get; set; }
        public string IconFileName { get; set; }
    }
}
