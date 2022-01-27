namespace AgendaPlusWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Contactos = new HashSet<Contacto>();
            FechasImportantes = new HashSet<FechasImportante>();
            Notas = new HashSet<Nota>();
            Pendientes = new HashSet<Pendiente>();
        }

        public int UsuarioID { get; set; }

        [DisplayName("Username")]
        [MinLength(8)]
        [Required(ErrorMessage = "Username is required")]
        public string NombreUsuario { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Correo { get; set; }

        [RegularExpression(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}", ErrorMessage = @"Error. Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.")]
        [MinLength(8)]
        [Required(ErrorMessage = "This is required")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Contrasena { get; set; }

        [RegularExpression(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}", ErrorMessage = @"Error. Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.")]
        [MinLength(8)]
        [Required(ErrorMessage = "This is required")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Contrasena")]
        public string ConfirmarContrasena { get; set; }

        public string Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contacto> Contactos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FechasImportante> FechasImportantes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Nota> Notas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pendiente> Pendientes { get; set; }
    }
}
