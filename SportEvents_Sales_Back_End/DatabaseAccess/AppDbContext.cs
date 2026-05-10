using Microsoft.EntityFrameworkCore;
using SportEvents_Sales_Back_End.Model.Entities;

namespace SportEvents_Sales_Back_End.DatabaseAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ClientEntity> Clients => Set<ClientEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<TicketEntity> Tickets => Set<TicketEntity>();
        public DbSet<TicketOrderEntity> TicketOrder => Set<TicketOrderEntity>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("usuario_administrativo");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id_usuario");//.ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.UserName).HasColumnName("usuario_admin");
                entity.Property(e => e.PasswordHash).HasColumnName("clave_admin");
            });

            modelBuilder.Entity<ClientEntity>(entity =>
            {
                entity.ToTable("cliente");
                entity.HasKey(k => k.Id);
                entity.Property(k => k.Id).HasColumnName("id_cliente");//.ValueGeneratedOnAddOrUpdate();
                entity.Property(k => k.Name).HasColumnName("nombre_cliente");
                entity.Property(k => k.LastName).HasColumnName("apellido_cliente");
                entity.Property(k => k.Email).HasColumnName("correo_cliente");
                entity.Property(k => k.Pass).HasColumnName("clave_cliente");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("ordenes");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).HasColumnName("id_orden").ValueGeneratedOnAdd();//.ValueGeneratedOnAddOrUpdate();
                entity.Property(o => o.Status).HasColumnName("status");//.ValueGeneratedOnAddOrUpdate();
                entity.Property(o => o.DateStart).HasColumnName("datetime_start");
                entity.Property(o => o.DateEnd).HasColumnName("datetime_close");
                entity.Property(o => o.IdClient).HasColumnName("id_cliente");
                entity.Property(o => o.SubTotal).HasColumnName("sub_total");
                entity.Property(o => o.TotalPrice).HasColumnName("total_price");
            });

            modelBuilder.Entity<TicketOrderEntity>(entity =>
            {
                entity.ToTable("orden_ticket");
                entity.HasKey(ot => new { ot.IdOrder, ot.IdTicket });
                entity.Property(ot => ot.IdOrder).HasColumnName("id_order");
                entity.Property(ot => ot.IdTicket).HasColumnName("id_ticket");
            });

            modelBuilder.Entity<TicketEntity>(entity => {
                entity.ToTable("tickets");
                entity.HasKey(t => t.IDTicket);
                entity.Property(t => t.IDTicket).HasColumnName("id_ticket");//.ValueGeneratedOnAddOrUpdate();
                entity.Property(t => t.HasDiscount).HasColumnName("has_discount");
                entity.Property(t => t.Discount).HasColumnName("percentage");
                entity.Property(t => t.CodeTicket).HasColumnName("code_ticket");
                entity.Property(t => t.IdZone).HasColumnName("id_zona");
                entity.Property(t => t.IdGame).HasColumnName("id_partido");
                entity.Property(t => t.AvailableTotal).HasColumnName("cupo_disponible");
                entity.Property(t => t.SolePrice).HasColumnName("sole_price");
                entity.Property(t => t.TotalPrice).HasColumnName("total_price");
            });


        }




    }
}
