namespace SportEvents_Sales_Back_End.Model.DTO
{
    public class TicketDTO
    {

        public Boolean HasDiscount { get; set; }
        public decimal Discount { get; set; }
        public int IdZone { get; set; }
        /*Get more from Zone*/
        public string? ZonePrice { get; set; } = "";
        public decimal Price { get; set; }
        /*from Zone*/
        public int IdGame { get; set; }
        /*Get more from Game*/
        public string? Stadium { get; set; } = "";
        public string? LocalTeam { get; set; } = "";
        public string? VisitorTeam { get; set; } = "";
        /*End for game*/
        public decimal SolePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalBuy { get; set; }
    }
}
