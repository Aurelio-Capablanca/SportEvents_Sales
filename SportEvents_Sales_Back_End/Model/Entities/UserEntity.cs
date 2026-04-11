namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class UserEntity
    {

        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string PasswordHash { get; set; } = "";

    }
}
