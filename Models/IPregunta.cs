namespace Inspecciones.Models
{
    public class IPregunta{
        public int Idpregunta { get; set; }
        public int IdTpregunt { get; set; }
        public string PNombre { get; set; }
        public string PDescri { get; set; }
        public bool PEstado { get; set; }

        public virtual ITPregunta IdTpreguntNavigation { get; set; } = null!;
    }
}