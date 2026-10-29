using CrowStation.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace CrowStation.Infrastructure.Persistence;

/// <summary>
/// The single EF Core context. Entity configurations live next to it as IEntityTypeConfiguration classes.
/// No entities are mapped yet: the data model is still being planned (see the plan branch).
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public const string Schema = "app";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
