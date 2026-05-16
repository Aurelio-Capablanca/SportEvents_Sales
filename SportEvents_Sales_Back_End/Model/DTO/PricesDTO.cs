namespace SportEvents_Sales_Back_End.Model.DTO
{
    public class PricesDTO
    {

        public int Id { get; set; }
        public string? ZonePrice { get; set; } = "";
        public decimal Price { get; set; }       
        public int AvailableSeats { get; set; }

    }
}
