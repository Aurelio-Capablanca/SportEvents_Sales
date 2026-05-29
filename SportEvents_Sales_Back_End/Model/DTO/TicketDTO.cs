namespace SportEvents_Sales_Back_End.Model.DTO
{
    public class TicketDTO
    {

        public int IdTicket { get; set; }
        public int? IdOrder { get; set; }
        public int? IdGame { get; set; }
        public int? IdTicketPrice { get; set; }
        public int AvailableSeats { get; set; }
        public decimal Discount { get; set; }
        /*Get more from Zone*/
        public List<PricesDTO>? Prices { get; set; }
        /*Get more from Game*/
        public string? Stadium { get; set; } = "";
        public string? LocalTeam { get; set; } = "";
        public string? VisitorTeam { get; set; } = "";
        public string? Location { get; set; } = "";
        public string? Tournament { get; set; } = "";
        public string? Date { get; set; } = "";
        public string? Time { get; set; } = "";
        /*End for game*/
        public decimal TotalPrice { get; set; }
        public int? TotalBuy { get; set; } = 0;
    }
}
