namespace SportEvents_Sales_Back_End.Model.ModelDomain.Request
{
    public class TicketWrapperRequest
    {
        public int IdTicket { get; set; }        
        public int IdGame { get; set; }
        public List<TicketPriceRequest> Prices { get; set; } = Enumerable.Empty<TicketPriceRequest>().ToList();
    }
}
