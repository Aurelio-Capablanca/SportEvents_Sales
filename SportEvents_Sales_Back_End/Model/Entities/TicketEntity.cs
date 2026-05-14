namespace SportEvents_Sales_Back_End.Model.Entities
{
    /*
     tickets

create table tickets (
id_ticket int primary key identity(1,1),
has_discount bit default 0, 
percentage decimal(4,2) default 0.0,
code_ticket varchar(100) not null,
id_zona int not null,
id_partido int not null,
cupo_disponible int not null,
sole_price decimal(8,2) not NULL ,
total_price  decimal(8,2) not NULL,
constraint FK_zona_ticket FOREIGN key (id_zona) references zonas_precios(id_zona_precio),
constraint FK_partido_ticket Foreign key (id_partido) references partidos(id_partido)
)
     */

    public class TicketEntity
    {

        public int IDTicket { get; set; }
        public Boolean HasDiscount { get; set; }
        public decimal Discount { get; set; }
        public string? CodeTicket { get; set; } = string.Empty;

        // Navigation Properties
        //public virtual ZonePricesEntity ZonePrice { get; set; } 
        
        //public int IdZone { get; set; }

        public virtual GameEntity Game { get; set; }

        public int IdGame { get; set; }
        public int AvailableTotal { get; set; }        
        public decimal TotalPrice { get; set; }
    }
}
