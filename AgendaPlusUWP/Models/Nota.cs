using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{
    class Nota
    {
        public int NotaID { get; set; }

        public int UsuarioID { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
