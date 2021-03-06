namespace AgendaPlusAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FechasImportante
    {
        [Key]
        public int FechasImportantesID { get; set; }

        public int UsuarioID { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
