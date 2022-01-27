namespace AgendaPlusWeb.Models
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
        [Required(ErrorMessage = "Title is required")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Deadline is required")]
        public DateTime FechaLimite { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
