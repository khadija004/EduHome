﻿
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace EduHome.Utilities.Extensions
{
    public static class Extensions
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
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
