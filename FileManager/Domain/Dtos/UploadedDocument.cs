namespace FileManager.Domain.Dtos;

public record UploadedDocument(Stream FileStream, string FileName, string ContentType, string EntityName, string EntityId, string UserId);