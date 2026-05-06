namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class OrderEntity
    {

        public int Id { get; set; }
        public Boolean Status { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int IdClient { get; set; }   
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }  




    }
}
