namespace FileManager.Domain;

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] FileData { get; set; } 
    public string EntityName { get; set; }
    public string EntityId { get; set; }
    public DateTime UploadDate { get; set; }
    public string UserId { get; set; } // یا int بسته به نوع Id کاربر
}