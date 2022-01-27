using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaPlusXamarin.Models
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
