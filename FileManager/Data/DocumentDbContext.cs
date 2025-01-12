using FileManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Data;

public class DocumentDbContext:DbContext
{
    public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
    {
    }
    public DbSet<Document> Documents { get; set; }
}