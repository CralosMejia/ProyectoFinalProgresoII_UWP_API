using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AgendaPlusWeb.Models
{
    public partial class DBAgenda : DbContext
    {
        public DBAgenda()
            : base("name=DBAgenda")
        {
        }

        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<FechasImportante> FechasImportantes { get; set; }
        public virtual DbSet<Nota> Notas { get; set; }
        public virtual DbSet<Pendiente> Pendientes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
