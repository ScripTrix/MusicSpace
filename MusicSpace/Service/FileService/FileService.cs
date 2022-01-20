using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Service.FileService
{ 
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment env;
        public FileService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public string Upload(IFormFile file, FileType type)
        {
            var uploadDirectory = "";
            switch (type)
            {
                case FileType.Song:
                    uploadDirectory = "uploads/songs/";
                    break;
                case FileType.Performer:
                    uploadDirectory = "uploads/performers/";
                    break;
                case FileType.Cover:
                    uploadDirectory = "uploads/covers/";
                    break;
            }
            var uploadPath = Path.Combine(env.WebRootPath, uploadDirectory);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public void DeleteUpload(string fileName, FileType type)
        {
            var uploadDirectory = "";
            switch (type)
            {
                case FileType.Song:
                    uploadDirectory = "uploads/songs/";
                    break;
                case FileType.Performer:
                    uploadDirectory = "uploads/performers/";
                    break;
                case FileType.Cover:
                    uploadDirectory = "uploads/covers/";
                    break;
            }
            var filePath = Path.Combine(env.WebRootPath, uploadDirectory);
            File.Delete(filePath);
        }
    }
}
