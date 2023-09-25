namespace Inspecciones.Models
{
    public class IMaqPre{
        public int IdMaqPre{ get; set; }
        public int IdMaquina { get; set; }
        public int Idpregunta { get; set; }
        public virtual IMaquina IdMaquinaNavigation { get; set; } = null!;
        public virtual IPregunta IdpreguntaNavigation { get; set; } = null!;
    }
}