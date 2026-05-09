namespace SportEvents_Sales_Back_End.Model.ModelDomain.Request
{
    public class ClientRegistry
    {
        public int? Idclient { get; set; } = null;
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Pass/*no signed*/ { get; set; } = "";
    }
}
