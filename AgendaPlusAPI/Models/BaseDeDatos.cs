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

        public virtual DbSet<Contactos> Contactos { get; set; }
        public virtual DbSet<FechasImportantes> FechasImportantes { get; set; }
        public virtual DbSet<Notas> Notas { get; set; }
        public virtual DbSet<Pendientes> Pendientes { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
