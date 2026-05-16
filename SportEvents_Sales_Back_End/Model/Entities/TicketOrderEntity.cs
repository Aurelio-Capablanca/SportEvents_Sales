namespace SportEvents_Sales_Back_End.Model.Entities
{

    /*
      ticket_orders

create table orden_ticket (
id_ticket int not null,
id_order int not null,
constraint PK_order_ticket_orders primary key (id_ticket, id_order),
constraint FK_ticket_orders foreign key (id_ticket) references tickets(id_ticket),
constraint FK_order_tickets foreign key (id_order) references ordenes(id_orden)
);

     */

    public class TicketOrderEntity
    {

        public virtual TicketEntity Tickets { get; set; }
        public int IdTicket { get; set; }
        public virtual OrderEntity Order { get; set; }
        public int IdOrder { get; set; }
        public int BoughtSeats { get; set; }
        public virtual TicketPriceEntity TicketPrice { get; set; }
        public int IdPriceTicket { get; set; }


    }
}
