using FileManager.Contract;
using FileManager.Data;
using FileManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Installer;

public static class Installer
{
    public static IServiceCollection AddDocumentManagementModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<DocumentDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("FileManager:DocumentManagementConnection"))); // Or your connection string name
        // Add Services
        services.AddScoped<IDocumentService, DocumentService>();
        // Optionally add other dependencies required by your module
        return services;
    }
} 