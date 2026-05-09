namespace SportEvents_Sales_Back_End.Model.ModelDomain.Domain
{

    public enum Roles { 
        Admin,
        Client,
        ScopedDomain,
        Anon
    }

    public class GlobalSession
    {
        public string Email { get; set; } = "";
        public Roles Role { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(Email);
    }
}
