using Authentication.Api.Models.Roles;
using Authentication.Api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Api.Data;

public class AuthenticationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }
    
    
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.HasMany(e => e.Roles)
                .WithMany(e => e.Users);

            b.Property(user => user.UserName).IsRequired();
            b.Property(user => user.DisplayName).IsRequired();
            b.Property(user => user.PasswordHash).IsRequired();
        });
        
        modelBuilder.Entity<Role>(b =>
        {
            b.Property(role => role.Name).IsRequired();
        });
    }
}