using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaPlusXamarin.Models
{
    class Usuario
    {
        public int UsuarioID { get; set; }

        public string NombreUsuario { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public string ConfirmarContrasena { get; set; }

        public string Avatar { get; set; }

        //public virtual ICollection<Contacto> Contactos { get; set; }

        public virtual ICollection<FechaImportante> FechasImportantes { get; set; }

        public virtual ICollection<Nota> Notas { get; set; }

        //public virtual ICollection<Pendiente> Pendientes { get; set; }
    }
}
