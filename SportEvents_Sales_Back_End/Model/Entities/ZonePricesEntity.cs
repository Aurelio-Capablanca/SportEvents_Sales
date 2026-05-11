namespace SportEvents_Sales_Back_End.Model.Entities
{
    public class ZonePricesEntity
    {

        /*
    id_zona_precio int IDENTITY(1,1) NOT NULL,
	nombre_zona_precio varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	precio_zona decimal(10,2) NOT NULL,
         */
        public int IdZone { get; set; }
        public decimal Price { get; set; }
        public string? ZoneName { get; set; } = "";

    }
}
