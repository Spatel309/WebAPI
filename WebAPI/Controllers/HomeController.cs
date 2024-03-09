using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetNumber")]
        public IActionResult GetNumber()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };





            return new JsonResult(numbers);

            // return BadRequest();
        }

        [HttpPost("savedata")]
        public IActionResult SaveData(IFormCollection formdata)
        {
            var data = formdata;
            if (formdata.Files.Count > 0)
            {
                IFormFile file = formdata.Files[0];
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", file.FileName);
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "newfolder", file.FileName);
                if (file.ContentType.Contains("application/zip-compressed") || file.ContentType.Contains("application/x-zip-compressed") || file.ContentType.Contains("application/x-msdownload"))
                {
                    Stream file1 = file.OpenReadStream();
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        file1.CopyTo(stream);
                    }
                }
                try
                {
                    // Get the base directory of the application
                    string baseDirectory = Directory.GetCurrentDirectory();

                    // Specify the folder name where you want to upload the file
                    string uploadFolderName = "Uploads";

                    // Combine the base directory with the folder name
                    string uploadFolder = Path.Combine(baseDirectory, uploadFolderName);

                    // Combine the folder path with the filename to create the full path
                    string filePath = Path.Combine(uploadFolder, file.FileName);

                    // Create the folder if it doesn't exist
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    Stream data12 = file.OpenReadStream();

                    System.IO.Compression.ZipFile.ExtractToDirectory(path, uploadFolder);


                    // Open the ZIP file using ZipInputStream
                    //using (ZipInputStream zipInputStream = new ZipInputStream(data12))
                    //{
                    //    ZipEntry entry;
                    //    while ((entry = zipInputStream.GetNextEntry()) != null)
                    //    {
                    //        // Combine the extract path with the entry's name
                    //        string entryPath = Path.Combine(uploadFolder, entry.Name);

                    //        // Create the directory if it doesn't exist
                    //        Directory.CreateDirectory(Path.GetDirectoryName(entryPath));

                    //        // Extract the entry's content
                    //        using (FileStream fs = System.IO.File.Create(entryPath))
                    //        {
                    //            zipInputStream.CopyTo(fs);
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    // Handle exceptions, log errors, etc.
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            return new JsonResult(0);
        }

    }
}
