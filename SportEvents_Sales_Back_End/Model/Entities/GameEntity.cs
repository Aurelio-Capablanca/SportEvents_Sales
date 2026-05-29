namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class GameEntity
    {
        public int IdGame { get; set; }
        public string? LocalTeam { get; set; }
        public string? VisitorTeam { get; set; }
        public DateTime TimeGame { get; set; }
        public virtual StadiumEntity? Stadium { get; set; }
        public int IdStadium { get; set; }
        public Boolean Status { get; set; }
        public string? Tournament { get; set; } = string.Empty;

    }
}
