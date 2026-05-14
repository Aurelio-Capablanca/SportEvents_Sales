namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class GameEntity
    {
        /*
    id_partido int IDENTITY(1,1) NOT NULL,
	equipo_local varchar(80)
	equipo_visitante varchar(80)
	fecha_hora datetime DEFAULT getdate() NULL,
	id_estadio int NOT NULL,
	estado bit DEFAULT 1 NULL,
         */

        public int IdGame { get; set; }
        public string? LocalTeam { get; set; }
        public string? VisitorTeam { get; set; }
        public DateTime TimeGame { get; set; }
        public virtual StadiumEntity Stadium { get; set; }
        public int IdStadium { get; set; }
        public Boolean Status { get; set; }
        public string? Tournament { get; set; } = string.Empty;

    }
}
