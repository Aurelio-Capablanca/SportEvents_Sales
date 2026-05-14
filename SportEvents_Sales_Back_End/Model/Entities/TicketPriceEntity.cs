namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class TicketPriceEntity
    {


        public virtual TicketEntity Tickets { get; set; }
        public int IdTicket { get; set; }
        public virtual ZonePricesEntity ZonePrice { get; set; }
        public int IdPriceZone { get; set; }
        public decimal Price { get; set; }

    }
}
