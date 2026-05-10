using SportEvents_Sales_Back_End.Model.DTO;

namespace SportEvents_Sales_Back_End.Model.ModelDomain.Request
{
    public class CartRequest
    {
        /*public int IdTicket { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }*/

        public List<TicketDTO> Tickets { get; set; } = Enumerable.Empty<TicketDTO>().ToList();
        public int? IdOrder { get; set; }


    }
}
