namespace AgendaPlusAPI.Models
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

        public string NombreContacto { get; set; }

        public string CorreoContacto { get; set; }

        public string TelefonoContacto { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
