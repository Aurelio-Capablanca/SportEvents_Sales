using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.Model.Entities;

namespace SportEvents_Sales_Back_End.DatabaseAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ClientEntity> Clients => Set<ClientEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("usuario_administrativo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id_usuario");
                entity.Property(e => e.UserName).HasColumnName("usuario_admin");
                entity.Property(e => e.PasswordHash).HasColumnName("clave_admin");
            });

            modelBuilder.Entity<ClientEntity>(entity =>
            {
                entity.ToTable("cliente");
                entity.HasKey(k => k.Id);
                entity.Property(k => k.Id).HasColumnName("id_cliente");
                entity.Property(k => k.Name).HasColumnName("nombre_cliente");
                entity.Property(k => k.LastName).HasColumnName("apellido_cliente");
                entity.Property(k => k.Email).HasColumnName("correo_cliente");
                entity.Property(k => k.Pass).HasColumnName("clave_cliente");
            });
        }




    }
}
