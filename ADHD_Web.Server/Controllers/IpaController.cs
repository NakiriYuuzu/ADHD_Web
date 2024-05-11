using Microsoft.AspNetCore.Mvc;

namespace ADHD_Web.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IpaController : ControllerBase
{
    private static readonly string StorageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "StoredFiles");

    public IpaController()
    {
        if (!Directory.Exists(StorageDirectory))
        {
            Directory.CreateDirectory(StorageDirectory);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadIpa(IFormFile ipa, IFormFile crt)
    {
        var ipaPath = Path.Combine(StorageDirectory, ipa.FileName);
        await using var ipaFileStream = new FileStream(ipaPath, FileMode.Create);
        await ipa.CopyToAsync(ipaFileStream);

        var crtPath = Path.Combine(StorageDirectory, crt.FileName);
        await using var crtFileStream = new FileStream(crtPath, FileMode.Create);
        await crt.CopyToAsync(crtFileStream);

        return Ok(new { message = "Files uploaded successfully!" });
    }

    [HttpGet]
    public IActionResult GetAllIpaFiles()
    {
        var files = Directory.GetFiles(StorageDirectory);
        return Ok(files.Select(Path.GetFileName));
    }
    
    [HttpDelete("{fileName}")]
    public IActionResult DeleteIpa(string fileName)
    {
        var filePath = Path.Combine(StorageDirectory, fileName);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            return Ok(new { message = "File deleted successfully!" });
        }
        return NotFound(new { message = "File not found!" });
    }

    [HttpGet("download/{fileName}")]
    public IActionResult DownloadIpa(string fileName)
    {
        var filePath = Path.Combine(StorageDirectory, fileName);
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        return File(fileBytes, "application/octet-stream", fileName);
    }
}