using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome_Asp.net.Utilities.File
{
    public static class Extentions
    {
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckFileSize(this IFormFile file, long size)
        {
            return file.Length / 1024 < size;
        }

        public async static Task SaveFile(this IFormFile file, string path)
        {
            using (FileStream Stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(Stream);
            }

        }
    }
}










