using FileManager.Contract;
using FileManager.Data;
using FileManager.Domain;
using FileManager.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FileManager.Infrastructure.Services;

internal class DocumentService : IDocumentService
{
    private readonly DocumentDbContext _context;
    private readonly IHostEnvironment  _env;

    protected DocumentService(DocumentDbContext context, IHostEnvironment  env)
    {
        _context = context;
        _env = env;
    }

     public async Task UploadFileAsync(UploadedDocument uploadedDocument)
    {
        if (uploadedDocument.FileStream != null && uploadedDocument.FileStream.Length > 0)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await uploadedDocument.FileStream.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var document = new Document
            {
                FileName = uploadedDocument.FileName,
                ContentType = uploadedDocument.ContentType,
                FileData = fileBytes, // Store the byte array
                EntityName = uploadedDocument.EntityName,
                EntityId = uploadedDocument.EntityId,
                UploadDate = DateTime.Now,
                UserId = uploadedDocument.UserId
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Document>> GetDocumentsAsync(string userId, DocumentCriteria filter)
    {
        IQueryable<Document> query = _context.Documents.Where(d => d.UserId == userId);

        if (filter is not null)
        {
            if (!string.IsNullOrEmpty(filter.EntityName))
                query = query.Where(d => d.EntityName.Contains(filter.EntityName)); // فیلتر بر اساس نام فایل و نام موجودیت
            if (!string.IsNullOrEmpty(filter.FileName))
                query = query.Where(d => d.EntityName.Contains(filter.FileName)); // فیلتر بر اساس نام فایل و نام موجودی
            if (!string.IsNullOrEmpty(filter.ContentType))
                query = query.Where(d => d.ContentType==filter.ContentType); // فیلتر بر اساس نام فایل و نام موجودیت
            if (filter.FromUploadDate!=default)
                query = query.Where(d => d.UploadDate>=filter.FromUploadDate); // فیلتر بر اساس نام فایل و نام موجودیت
            if (filter.ToUploadDate!=default)
                query = query.Where(d => d.UploadDate<=filter.ToUploadDate); // فیلتر بر اساس نام فایل و نام موجودیت
        }
        return await query.ToListAsync();
    }

    public async Task<Document> GetDocumentByIdAsync(int id)
    {
        return await _context.Documents.FindAsync(id);
    }

    public async Task<byte[]> DownloadFileAsync(int id)
    {
        var document = await GetDocumentByIdAsync(id);
        return document?.FileData;
    }
}