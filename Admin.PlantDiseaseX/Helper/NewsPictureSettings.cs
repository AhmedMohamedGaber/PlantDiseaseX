using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Admin.PlantDiseaseX.Helper
{
    public class NewsPictureSettings
    {

        // Upload
        public static string UploadFile(IFormFile file, string folderName)
        {
            // Get Folder Path 
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

            // Set FileName Unique 
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Get File Path
            var filePath = Path.Combine(FolderPath, fileName);

            // Save File As Stream
            var fs = new FileStream(filePath, FileMode.Create);

            // Copy File Into Stream
            file.CopyTo(fs);

            // Return FileName
            // Reaturn FileName
            return Path.Combine("images\\newsarticle", fileName);
            // return Path.Combine("images/newsarticle/", fileName);
            // return fileName;
        }


        // Delete
        public static void DeleteFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
