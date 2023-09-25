using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Inspecciones.Models
{
    public class DbNeoContext : DbContext
    {
        public virtual DbSet<Maquina> Maquinas { get; set; }
        public virtual DbSet<Pregunta> Preguntas { get; set; }
        public virtual DbSet<MaqPre> MaqsPres { get; set; }
        public virtual DbSet<TPregunta> Tpreguntas { get; set; }
        public virtual DbSet<Inspeccion> Inspecciones { get; set; }
        public virtual DbSet<InspecData> InspecDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maquina>(maquina =>
            {
                maquina.ToTable("Maquina");
                maquina.HasKey(p => p.IdMaquina);
                maquina.Property(p => p.Nombre).IsRequired().HasMaxLength(50);
                maquina.Property(p => p.descri).IsRequired();
                maquina.Property(p => p.Estado).IsRequired();
            });

            modelBuilder.Entity<TPregunta>(tPregunta =>
            {
                tPregunta.ToTable("TPregunta");
                tPregunta.HasKey(p => p.IdTpregunt);
                tPregunta.Property(p => p.Nombre).IsRequired().HasMaxLength(50).IsUnicode();
                tPregunta.Property(p => p.descri).IsRequired();
                tPregunta.Property(p => p.Estado).IsRequired();
            });

            modelBuilder.Entity<Pregunta>(pregunta =>
            {
                pregunta.ToTable("Pregunta");
                pregunta.HasKey(p => p.Idpregunta);
                //pregunta.HasOne(p => p)
                pregunta.Property(p => p.Nombre).IsRequired().HasMaxLength(50).IsUnicode();
                pregunta.Property(p => p.descri).IsRequired();
                pregunta.Property(p => p.Estado).IsRequired();
            });
        }
    }
}