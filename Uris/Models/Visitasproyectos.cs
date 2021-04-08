using System;
using System.Collections.Generic;

namespace Uris.Models
{
    public partial class Visitasproyectos
    {
        public int IdProyecto { get; set; }
        public int Cantidad { get; set; }

        public virtual Proyectos IdProyectoNavigation { get; set; }
    }
}
