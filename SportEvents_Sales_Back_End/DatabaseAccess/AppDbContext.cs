using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.Model.Entities;

namespace SportEvents_Sales_Back_End.DatabaseAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("usuario_administrativo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id_usuario");
                entity.Property(e => e.UserName).HasColumnName("usuario");
                entity.Property(e => e.PasswordHash).HasColumnName("password");
            });
        }


    }
}
