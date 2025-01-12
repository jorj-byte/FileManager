namespace FileManager.Domain;

public class DocumentCriteria
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string EntityName { get; set; }
    public string EntityId { get; set; }
    public DateTime ToUploadDate { get; set; }
    public DateTime FromUploadDate { get; set; }
    
}