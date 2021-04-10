using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public bool Delete(string uploadDirectory, string fileName)
        {
            var path = Path.Combine(environment.WebRootPath, uploadDirectory);
            var filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);

            return !File.Exists(filePath);
        }

        public (bool, string) Upload(IFormFile file, string uploadDirectory, string fileName="")
        {
            try
            {
                var uploadPath = Path.Combine(environment.WebRootPath, uploadDirectory);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                fileName = (string.IsNullOrEmpty(fileName) ? Guid.NewGuid() : fileName) + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);
                Delete(uploadPath,fileName);
                using var stream = File.Create(filePath);
                file.CopyTo(stream);
            }
            catch (Exception exception)
            {
                return (false,exception.Message);
            }
            return (true,fileName);
        }

    }
}
