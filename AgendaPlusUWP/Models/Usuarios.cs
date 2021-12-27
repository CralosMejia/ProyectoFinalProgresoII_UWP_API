using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{


    class Usuarios
    {
        [Key]
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

  
        public virtual ICollection<Contactos> Contactos { get; set; }

       
        public virtual ICollection<FechasImportantes> FechasImportantes { get; set; }

 
        public virtual ICollection<Notas> Notas { get; set; }

   
        public virtual ICollection<Pendientes> Pendientes { get; set; }
    }
}
