using ArkaDoor.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
