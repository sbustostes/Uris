using System;
using System.Collections.Generic;

namespace Uris.Models
{
    public partial class Proyectos
    {
        public Proyectos()
        {
            Colaboradores = new HashSet<Colaboradores>();
            Importesusuarios = new HashSet<Importesusuarios>();
            Proyectosporusuarios = new HashSet<Proyectosporusuarios>();
        }

        public int? IdProyecto { get; set; }
        public string? Nombre { get; set; }
        public float? Meta { get; set; }
        public string TipoTasa { get; set; }
        public float? Tasa { get; set; }
        public int? Plazo { get; set; }
        public string? Clasificacion { get; set; }
        public string? Sector { get; set; }
        public int? PagoInteres { get; set; }
        public int? PeriodoDeGraciaInteres { get; set; }
        public int? PeriodoDeGraciaPerCapita { get; set; }
        public string? UlrYoutube { get; set; }
        public byte[]? Imagen { get; set; }
        public string? ContentType { get; set; }
        public string? Descripcion { get; set; }
        public int? CategoriasIdCategorias { get; set; }
        public int? IdUsuarios { get; set; }

        public virtual Categorias CategoriasIdCategoriasNavigation { get; set; }
        public virtual Usuarios IdUsuariosNavigation { get; set; }
        public virtual Visitasproyectos Visitasproyectos { get; set; }
        public virtual ICollection<Colaboradores> Colaboradores { get; set; }
        public virtual ICollection<Importesusuarios> Importesusuarios { get; set; }
        public virtual ICollection<Proyectosporusuarios> Proyectosporusuarios { get; set; }
    }
}
