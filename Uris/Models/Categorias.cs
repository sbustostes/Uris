using System;
using System.Collections.Generic;

namespace Uris.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Proyectos = new HashSet<Proyectos>();
        }

        public int IdCategorias { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Icono { get; set; }

        public virtual ICollection<Proyectos> Proyectos { get; set; }
    }
}
