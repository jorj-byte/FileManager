using FileManager.Domain;
using FileManager.Domain.Dtos;

namespace FileManager.Contract;

public interface IDocumentService
{
     Task UploadFileAsync(UploadedDocument input);
     Task<List<Document>> GetDocumentsAsync(string userId, DocumentCriteria filter); // اضافه کردن فیلتر
     Task<Document> GetDocumentByIdAsync(int id);
     Task<byte[]> DownloadFileAsync(int id);
}