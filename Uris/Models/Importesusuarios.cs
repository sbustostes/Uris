using System;
using System.Collections.Generic;

namespace Uris.Models
{
    public partial class Importesusuarios
    {
        public int? Id { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdProyecto { get; set; }
        public float? Valor { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Proyectos IdProyectoNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
