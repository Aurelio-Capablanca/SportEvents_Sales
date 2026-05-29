namespace SportEvents_Sales_Back_End.Model.ModelDomain.Request
{
    public class TicketPriceRequest
    {

        public int Id { get; set; }
        public int IdPriceZone { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }        
    }
}
