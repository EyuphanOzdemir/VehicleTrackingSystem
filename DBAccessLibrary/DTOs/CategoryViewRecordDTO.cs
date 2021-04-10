using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DBAccessLibrary.DTOs
{
    /// <summary>
    ///  The DTO class corresponding the Category view.
    /// </summary>
    public class CategoryViewRecordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinWeight { get; set; }
        public decimal UpTo { get; set; }
        public string IconFileName { get; set; }
    }

    public class CategoryViewRecordWithIconDTO : CategoryViewRecordDTO
    {
        public IFormFile Icon { get; set; }
    }
}
