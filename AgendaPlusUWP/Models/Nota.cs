using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{
    class Nota
    {

        [Key]
        public int NotaID { get; set; }

        public int UsuarioID { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public virtual Usuario Usuarios { get; set; }
    }
}
