namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class TicketPriceEntity
    {

        public int IdTicketPrice { get; set; }
        public virtual TicketEntity Tickets { get; set; }
        public int IdTicket { get; set; }
        public virtual ZonePricesEntity ZonePrice { get; set; }
        public int IdPriceZone { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }

    }
}
