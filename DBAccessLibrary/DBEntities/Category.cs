using CoreEntity;
using System;
using System.Collections.Generic;

#nullable disable

namespace DBAccessLibrary.DBEntities
{
    /// <summary>
    ///  ID and Name comes from BaseNamedEntity.
    /// </summary>
    public partial class Category:BaseNamedEntity
    {
        public decimal MinWeight { get; set; }
        public string IconFileName { get; set; }
    }
}
