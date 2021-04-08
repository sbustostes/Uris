using System;
using System.Collections.Generic;

namespace Uris.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Colaboradores = new HashSet<Colaboradores>();
            Importesusuarios = new HashSet<Importesusuarios>();
            Proyectos = new HashSet<Proyectos>();
            Proyectosporusuarios = new HashSet<Proyectosporusuarios>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
        public string Rol { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public byte[] File { get; set; }

        public virtual ICollection<Colaboradores> Colaboradores { get; set; }
        public virtual ICollection<Importesusuarios> Importesusuarios { get; set; }
        public virtual ICollection<Proyectos> Proyectos { get; set; }
        public virtual ICollection<Proyectosporusuarios> Proyectosporusuarios { get; set; }
    }
}
