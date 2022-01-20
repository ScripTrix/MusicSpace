using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Service.FileService
{
    public enum FileType { Song = 0, Cover, Performer }
    public interface IFileService
    {
        string Upload(IFormFile file, FileType type);
        void DeleteUpload(string fileName, FileType type);
    }
}
