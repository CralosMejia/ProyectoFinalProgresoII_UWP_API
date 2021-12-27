using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{
    class Contacto
    {
        public int ContactoID { get; set; }

        public int UsuarioID { get; set; }

        public string NombreContacto { get; set; }

        public string CorreoContacto { get; set; }

        public string TelefonoContacto { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
