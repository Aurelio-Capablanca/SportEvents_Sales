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
        public DbSet<ZonePricesEntity> ZonePrices => Set<ZonePricesEntity>();
        public DbSet<StadiumEntity> Stadiums => Set<StadiumEntity>();
        public DbSet<GameEntity> Games => Set<GameEntity>();
        public DbSet<TicketPriceEntity> TicketPrice => Set<TicketPriceEntity>();

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
                entity.Property(o => o.TotalPrice).HasColumnName("total_price");
                entity.HasOne(o => o.Client).WithMany().HasForeignKey(o => o.IdClient);
            });

            modelBuilder.Entity<TicketOrderEntity>(entity =>
            {
                entity.ToTable("orden_ticket");
                entity.HasKey(ot => new { ot.IdOrder, ot.IdTicket });
                entity.Property(ot => ot.IdOrder).HasColumnName("id_order");
                entity.Property(ot => ot.IdTicket).HasColumnName("id_ticket");
                entity.HasOne(ot => ot.Tickets).WithMany().HasForeignKey(ot => ot.IdTicket);
                entity.HasOne(ot => ot.Order).WithMany().HasForeignKey(ot => ot.IdOrder);
            });

            modelBuilder.Entity<TicketEntity>(entity =>
            {
                entity.ToTable("tickets");
                entity.HasKey(t => t.IDTicket);
                entity.Property(t => t.IDTicket).HasColumnName("id_ticket");//.ValueGeneratedOnAddOrUpdate();
                entity.Property(t => t.HasDiscount).HasColumnName("has_discount");
                entity.Property(t => t.Discount).HasColumnName("percentage");
                entity.Property(t => t.CodeTicket).HasColumnName("code_ticket");
                entity.Property(t => t.IdGame).HasColumnName("id_partido");
                entity.Property(t => t.AvailableTotal).HasColumnName("cupo_disponible");
                entity.Property(t => t.TotalPrice).HasColumnName("total_price");
                entity.HasOne(t => t.Game).WithMany().HasForeignKey(t => t.IdGame);
            });

            modelBuilder.Entity<ZonePricesEntity>(entity =>
            {
                entity.ToTable("zonas_precios");
                entity.HasKey(pz => pz.IdZone);
                entity.Property(pz => pz.IdZone).HasColumnName("id_zona_precio");
                entity.Property(pz => pz.ZoneName).HasColumnName("nombre_zona_precio");
                entity.Property(pz => pz.Price).HasColumnName("precio_zona");
            });

            modelBuilder.Entity<StadiumEntity>(entity =>
            {
                entity.ToTable("estadio");
                entity.HasKey(s => s.IdStadium);
                entity.Property(s => s.IdStadium).HasColumnName("id_estadio");
                entity.Property(s => s.Name).HasColumnName("nombre_estadio");
                entity.Property(s => s.Location).HasColumnName("ubicacion_estadio");
                entity.Property(s => s.Capacity).HasColumnName("capacidad_total");
            });

            modelBuilder.Entity<GameEntity>(entity =>
            {
                entity.ToTable("partidos");
                entity.HasKey(g => g.IdGame);
                entity.Property(g => g.IdGame).HasColumnName("id_partido");
                entity.Property(g => g.LocalTeam).HasColumnName("equipo_local");
                entity.Property(g => g.VisitorTeam).HasColumnName("equipo_visitante");
                entity.Property(g => g.TimeGame).HasColumnName("fecha_hora");
                entity.Property(g => g.IdStadium).HasColumnName("id_estadio");
                entity.Property(g => g.Status).HasColumnName("estado");
                entity.Property(g => g.Tournament).HasColumnName("torneo");
                entity.HasOne(g => g.Stadium).WithMany().HasForeignKey(g => g.IdStadium);
            });

            modelBuilder.Entity<TicketPriceEntity>(entity =>
            {
                entity.ToTable("ticket_price");
                entity.HasKey(pt => new { pt.IdPriceZone, pt.IdTicket });
                entity.Property(pt => pt.IdPriceZone).HasColumnName("id_price_zone");
                entity.Property(pt => pt.IdTicket).HasColumnName("id_ticket");
                entity.Property(pt => pt.Price).HasColumnName("price");
                entity.HasOne(pt => pt.Tickets).WithMany().HasForeignKey(ot => ot.IdTicket);
                entity.HasOne(pt => pt.ZonePrice).WithMany().HasForeignKey(ot => ot.IdPriceZone);
            });

        }




    }
}
