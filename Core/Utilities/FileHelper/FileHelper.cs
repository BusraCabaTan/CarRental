
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelper
{
    public class FileHelper
    {
        public static string Add(IFormFile file)
        {
            var sourcePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(sourcePath, FileMode.Create))
                {

                    file.CopyTo(stream);
                }
            }

            var result = NewPath(file);

            File.Move(sourcePath, result);

            return result;
        }

        public static string Update(string sourcePath, IFormFile file)
        {
            var result = NewPath(file);

            File.Move(sourcePath, result);

            File.Delete(sourcePath);

            return result;
        }

        public static void Delete(string sorucePath)
        {
            File.Delete(sorucePath);
        }

        public static string NewPath(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);

            string fileExtension = fileInfo.Extension;

            var createUniqFileName = Guid.NewGuid().ToString("N")
                + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day
                + "-" + DateTime.Now.Year + fileExtension;

            string path = Path.Combine(Environment.CurrentDirectory + @"\wwwroot\Uploads");
            string result = $@"{path}\{createUniqFileName}";

            return result;
        }
    }
}
