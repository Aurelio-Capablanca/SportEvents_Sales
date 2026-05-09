using SportEvents_Sales_Back_End.Model.DTO;

namespace SportEvents_Sales_Back_End.Model.ModelDomain.Response
{
    public class CartResponse
    {

        public Boolean Status { get; set; }
        public int IdClient { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public List<TicketDTO>? ListTickets { get; set; } = Enumerable.Empty<TicketDTO>().ToList();
    }
}
