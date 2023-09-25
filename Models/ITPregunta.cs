namespace Inspecciones.Models
{
    public class ITPregunta{
        public int IdTpregunt { get; set; }
        public string TPNombre { get; set; }
        public string TPDescri { get; set; }
        public bool TPEstado { get; set; }
        public virtual ICollection<IPregunta> Pregunta { get; set; } = new List<IPregunta>();
    }
}