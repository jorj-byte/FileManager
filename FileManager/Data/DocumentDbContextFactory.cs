using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FileManager.Data;

public class DocumentDbContextFactory:IDesignTimeDbContextFactory<DocumentDbContext>
{
    public DocumentDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DocumentDbContext>();
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FileManager;User ID=sa;Password=987654321;MultipleActiveResultSets=true;Encrypt=False");
        return new DocumentDbContext(optionsBuilder.Options);
    }
}