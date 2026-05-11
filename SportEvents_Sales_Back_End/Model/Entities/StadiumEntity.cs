namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class StadiumEntity
    {
        /*
    id_estadio int IDENTITY(1,1) NOT NULL,
	nombre_estadio varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ubicacion_estadio varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	capacidad_total int NOT NULL,
         */

        public int IdStadium { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int Capacity { get; set; }

    }
}
