using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{
    class Usuario
    {
        public int UsuarioID { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }

        [Required]
        public string ConfirmarContrasena { get; set; }

        public string Avatar { get; set; }

        
    }
}
