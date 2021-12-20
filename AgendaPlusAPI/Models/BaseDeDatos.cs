using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AgendaPlusAPI.Models
{
    public partial class BaseDeDatos : DbContext
    {
        public BaseDeDatos()
            : base("name=BaseDeDatos")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<FechasImportante> FechasImportantes { get; set; }
        public virtual DbSet<Nota> Notas { get; set; }
        public virtual DbSet<Pendiente> Pendientes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
