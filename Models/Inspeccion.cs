namespace Inspecciones.Models
{
    public class Inspeccion{
        public int IdInspec { get; set; }
        public DateTime Fecha { get; set; }
        public string ITurno { get; set; }
        public string IGrupo { get; set; }
        public string ICodEquipo { get; set; }
        public string IFicha { get; set; }
        public string IArea { get; set; }
    }
}