using System;
using System.Collections.Generic;

namespace Uris.Models
{
    public partial class Colaboradores
    {
        public int UsuariosId { get; set; }
        public int ProyectosIdProyecto { get; set; }

        public virtual Proyectos ProyectosIdProyectoNavigation { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
