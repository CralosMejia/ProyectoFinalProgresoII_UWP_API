namespace AgendaPlusWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contactos")]
    public partial class Contacto
    {
        public int ContactoID { get; set; }

        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "Contact name is required")]
        public string NombreContacto { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Contact email is required")]
        public string CorreoContacto { get; set; }

        [Required(ErrorMessage = "Contact Name is required")]
        public string TelefonoContacto { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
