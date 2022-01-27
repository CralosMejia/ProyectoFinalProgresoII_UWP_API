namespace AgendaPlusWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Nota
    {
        public int NotaID { get; set; }

        public int UsuarioID { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Descripcion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
