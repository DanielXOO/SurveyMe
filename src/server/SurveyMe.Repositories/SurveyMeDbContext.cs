using Microsoft.EntityFrameworkCore;
using SurveyMe.DomainModels;

namespace SurveyMe.Repositories;

public class SurveyMeDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Role> Roles { get; set; }

    public SurveyMeDbContext(DbContextOptions<SurveyMeDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.Property(user => user.CreationTime).IsRequired();
            e.Property(user => user.UserName).IsRequired();
            e.Property(user => user.PasswordHash).IsRequired();
            e.HasMany(user => user.Roles)
                .WithMany(role => role.Users);
        });

        modelBuilder.Entity<Role>(e =>
        {
            e.Property(role => role.Name).IsRequired();
        });
    }
}