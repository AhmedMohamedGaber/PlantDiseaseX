
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Admin.PlantDiseaseX.Helper
{





    public class PictureSettings
    {

        // Base path to API's wwwroot directory

      //  private static readonly string ApiWwwrootPath = @"..\PlantDiseaseX.API\wwwroot";



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
            return Path.Combine("images\\plants", fileName);
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
