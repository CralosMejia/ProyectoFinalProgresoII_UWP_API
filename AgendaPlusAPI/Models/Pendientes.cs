namespace AgendaPlusAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pendientes
    {
        [Key]
        public int PendienteID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public int Prioridad { get; set; }

        public string ColorPrioridad { get; set; }

        public bool Estado { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
