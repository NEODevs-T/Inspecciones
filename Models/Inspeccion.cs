using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspecciones.Models
{
    public partial class Inspeccion
    {
        public Inspeccion()
        {
            InspecData = new HashSet<InspecDatum>();
        }

        public int IdInspec { get; set; }
        public DateTime Ifecha { get; set; }
        public string Iturno { get; set; } = null!;
        public string Igrupo { get; set; } = null!;

        [Required(ErrorMessage ="Coloque un equipo"),MinLength(6),MaxLength(6)] 
        public string Iequipo { get; set; } = null!;
        public string Iarea { get; set; } = null!; 
        public string Ificha { get; set; } = null!;

        public virtual ICollection<InspecDatum> InspecData { get; set; }
    }
}
