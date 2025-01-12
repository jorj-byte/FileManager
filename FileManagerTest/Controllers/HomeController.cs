using System.Diagnostics;
using FileManager.Contract;
using FileManager.Domain;
using FileManager.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using FileManagerTest.Models;

namespace FileManagerTest.Controllers;

public class HomeController : Controller
{
    private readonly IDocumentService _documentService;
    private readonly IHostEnvironment _env;

    public HomeController(IDocumentService documentService, IHostEnvironment env)
    {
        _documentService = documentService;
        _env = env;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            await _documentService.UploadFileAsync(new UploadedDocument(file.OpenReadStream(), file.FileName, file.ContentType, "MyEntity", "1", "1")); // Replace "1" with actual user ID
            return RedirectToAction("Index"); // Redirect after upload
        }

        return Ok();
    }

    public async Task<IActionResult> Download(int id)
    {
        var fileBytes = await _documentService.DownloadFileAsync(id);
        if (fileBytes == null)
        {
            return NotFound();
        }
        var document = await _documentService.GetDocumentByIdAsync(id);
        return File(fileBytes, document.ContentType, document.FileName);
    }

    public async Task<IActionResult> Manager()
    {
        var userId = "1";
        var documents = await _documentService.GetDocumentsAsync(userId, new DocumentCriteria(){FromUploadDate = DateTime.Now});
        return View(documents);
    }
}