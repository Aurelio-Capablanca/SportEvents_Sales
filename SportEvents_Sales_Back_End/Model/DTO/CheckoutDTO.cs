namespace SportEvents_Sales_Back_End.Model.DTO
{
    public class CheckoutDTO
    {

        public int IdTicket { get; set; }
        public int IdPriceTicket { get; set; }
        public int AvailableSeats { get; set; }
        public int InCartTickets { get; set; }
        //public int TotalPriceCart { get; set; }

    }
}
