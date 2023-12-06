using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftType.DbModels;

namespace ShiftType.Services
{
    public static class ImageHelperService
    {
        public static async Task Upload(IFormFile file, User user, TypingDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var dbFile = new ImageFile()
            {
                Filename = filename,
                RootDirectory = "img",
            };
            var localFilename =
                Path.Combine(webHostEnvironment.WebRootPath, dbFile.RootDirectory, dbFile.Filename);
            using (var localFile = System.IO.File.Open(localFilename, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(localFile);
            }
            context.Images.Add(dbFile);
            user.Logo = dbFile;
            await context.SaveChangesAsync();
        }
    }
}
