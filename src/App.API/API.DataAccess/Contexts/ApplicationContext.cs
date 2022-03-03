using App.API.DataAccess.Models;
using App.API.DataAccess.Models.Auth;

using Microsoft.EntityFrameworkCore;

namespace App.API.DataAccess.Contexts;

/// <summary>
/// The application context.
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
    /// </summary>
    /// <param name="options">
    /// The options.
    /// </param>
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the items.
    /// </summary>
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users { get; set; }


    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<Language> Languages { get; set; }

    /// <summary>
    /// The on model creating.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(
            b =>
                {
                    b.Property("Id");
                    b.HasKey("Id");
                    b.Property(e => e.Name);
                    b.HasMany(e => e.Tags).WithOne().IsRequired();
                });

        modelBuilder.Entity<Tag>(
            b =>
                {
                    b.Property("Id");
                    b.HasKey("Id");
                    b.Property(e => e.Label);
                });

        modelBuilder.Entity<User>(
            b =>
                {
                    b.Property("Id");
                    b.HasKey("Id");
                    b.Property(e => e.Username).IsRequired();
                    b.Property(e => e.FirstName).IsRequired();
                    b.Property(e => e.LastName);
                    b.Property(e => e.Role).IsRequired();
                    b.Property(e => e.PasswordHash).IsRequired();
                    b.Property(e => e.Created);
                    b.Property(e => e.Updated);
                    b.Property(e => e.IsDefault);
                });

        modelBuilder.Entity<Language>(
            b =>
                {
                    b.Property("Id");
                    b.HasKey("Id");
                    b.Property(e => e.Name).IsRequired();
                    b.Property(e => e.Localization).IsRequired();
                    b.Property(e => e.IsActive).HasDefaultValue(false);
                });
    }
}
