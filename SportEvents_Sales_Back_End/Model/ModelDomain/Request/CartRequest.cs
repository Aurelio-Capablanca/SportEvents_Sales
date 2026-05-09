namespace SportEvents_Sales_Back_End.Model.ModelDomain.Request
{
    public class CartRequest
    {
        public int IdTicket { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int? IdOrder { get; set; }


    }
}
