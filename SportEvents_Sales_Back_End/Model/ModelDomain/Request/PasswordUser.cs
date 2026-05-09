namespace SportEvents_Sales_Back_End.Model.ModelDomain.Request
{
    public class PasswordUser
    {
        public required string User { get; set; }
        public required string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
