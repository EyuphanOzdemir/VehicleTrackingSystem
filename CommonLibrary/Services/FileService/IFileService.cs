using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Services.FileService
{
    public interface IFileService
    {
        (bool, string) Upload(IFormFile file, string uploadDirectory, string fileName="");

        bool Delete(string uploadDirectory, string fileName);
    }
}
