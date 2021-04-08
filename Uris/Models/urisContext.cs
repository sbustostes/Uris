using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Uris.Models
{
    public partial class urisContext : DbContext
    {
        public urisContext()
        {
        }

        public urisContext(DbContextOptions<urisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Colaboradores> Colaboradores { get; set; }
        public virtual DbSet<Importesusuarios> Importesusuarios { get; set; }
        public virtual DbSet<Proyectos> Proyectos { get; set; }
        public virtual DbSet<Proyectosporusuarios> Proyectosporusuarios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Visitasproyectos> Visitasproyectos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();


            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL(configuration.GetConnectionString("DevConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.IdCategorias)
                    .HasName("PRIMARY");

                entity.ToTable("categorias");

                entity.Property(e => e.IdCategorias)
                    .HasColumnName("idCategorias")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Icono)
                    .HasColumnName("icono")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colaboradores>(entity =>
            {
                entity.HasKey(e => new { e.UsuariosId, e.ProyectosIdProyecto })
                    .HasName("PRIMARY");

                entity.ToTable("colaboradores");

                entity.HasIndex(e => e.ProyectosIdProyecto)
                    .HasName("fk_usuarios_has_proyectos1_proyectos1_idx");

                entity.HasIndex(e => e.UsuariosId)
                    .HasName("fk_usuarios_has_proyectos1_usuarios1_idx");

                entity.Property(e => e.UsuariosId)
                    .HasColumnName("usuarios_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProyectosIdProyecto)
                    .HasColumnName("proyectos_idProyecto")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ProyectosIdProyectoNavigation)
                    .WithMany(p => p.Colaboradores)
                    .HasForeignKey(d => d.ProyectosIdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuarios_has_proyectos1_proyectos1");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Colaboradores)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuarios_has_proyectos1_usuarios1");
            });

            modelBuilder.Entity<Importesusuarios>(entity =>
            {
                entity.ToTable("importesusuarios");

                entity.HasIndex(e => e.IdProyecto)
                    .HasName("idProyecto");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("idUsuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("idProyecto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("idUsuario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Importesusuarios)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("importesusuarios_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Importesusuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("importesusuarios_ibfk_1");
            });

            modelBuilder.Entity<Proyectos>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PRIMARY");

                entity.ToTable("proyectos");

                entity.HasIndex(e => e.CategoriasIdCategorias)
                    .HasName("fk_proyectos_categorias_idx");

                entity.HasIndex(e => e.IdUsuarios)
                    .HasName("fk_proyectos_usuarios");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("idProyecto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoriasIdCategorias)
                    .HasColumnName("categorias_idCategorias")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Clasificacion)
                    .HasColumnName("clasificacion")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ContentType)
                    .HasColumnName("contentType")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuarios)
                    .HasColumnName("idUsuarios")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasColumnType("longblob");

                entity.Property(e => e.Meta).HasColumnName("meta");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PagoInteres)
                    .HasColumnName("pagoInteres")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PeriodoDeGraciaInteres)
                    .HasColumnName("periodoDeGraciaInteres")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PeriodoDeGraciaPerCapita).HasColumnType("int(11)");

                entity.Property(e => e.Plazo)
                    .HasColumnName("plazo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Sector)
                    .HasColumnName("sector")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Tasa).HasColumnName("tasa");

                entity.Property(e => e.TipoTasa)
                    .HasColumnName("tipoTasa")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UlrYoutube)
                    .HasColumnName("ulrYoutube")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoriasIdCategoriasNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.CategoriasIdCategorias)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_proyectos_categorias");

                entity.HasOne(d => d.IdUsuariosNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdUsuarios)
                    .HasConstraintName("fk_proyectos_usuarios");
            });

            modelBuilder.Entity<Proyectosporusuarios>(entity =>
            {
                entity.HasKey(e => new { e.UsuariosId, e.ProyectosIdProyecto })
                    .HasName("PRIMARY");

                entity.ToTable("proyectosporusuarios");

                entity.HasIndex(e => e.ProyectosIdProyecto)
                    .HasName("fk_usuarios_has_proyectos_proyectos1_idx");

                entity.HasIndex(e => e.UsuariosId)
                    .HasName("fk_usuarios_has_proyectos_usuarios_idx");

                entity.Property(e => e.UsuariosId)
                    .HasColumnName("usuarios_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProyectosIdProyecto)
                    .HasColumnName("proyectos_idProyecto")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ProyectosIdProyectoNavigation)
                    .WithMany(p => p.Proyectosporusuarios)
                    .HasForeignKey(d => d.ProyectosIdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuarios_has_proyectos_proyectos1");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Proyectosporusuarios)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuarios_has_proyectos_usuarios");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.Id)
                    .HasName("id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FechaDeNacimiento)
                    .HasColumnName("fechaDeNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.File)
                    .HasColumnName("file")
                    .HasColumnType("mediumblob");

                entity.Property(e => e.Linkedin)
                    .HasColumnName("linkedin")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Twitter)
                    .HasColumnName("twitter")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visitasproyectos>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PRIMARY");

                entity.ToTable("visitasproyectos");

                entity.Property(e => e.IdProyecto)
                    .HasColumnName("idProyecto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithOne(p => p.Visitasproyectos)
                    .HasForeignKey<Visitasproyectos>(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visitasproyectos_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
