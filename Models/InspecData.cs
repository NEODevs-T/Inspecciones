namespace Inspecciones.Models
{
    public class InspecData{
        public int IdInspecDat { get; set; }
        public int IdMaqPre{ get; set; }
        public int IdInspec { get; set; }
        public bool Data { get; set; }
        public string Observ { get; set; }

        public virtual IMaqPre IdMaqPreNavigation { get; set; } = null!;
        public virtual Inspeccion IdInspecNavigation { get; set; } = null!;
    }
}