using ArkaDoor.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Window.Domain.Entities.Account;

namespace ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;

public class AkaDoorDbContext : DbContext
{
    #region Ctor

 public AkaDoorDbContext(DbContextOptions<AkaDoorDbContext> options)
        : base(options)
    {

    }

    #endregion
   
    #region User

    public DbSet<User> Users { get; set; }

    #endregion

    #region Role

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    #endregion

    #region OnConfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Role Seed Data

        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = 1,
            Title = "Admin",
            RoleUniqueName = "Admin",
            CreateDate = DateTime.Now,
            IsDelete = false
        });

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
