using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaPlusXamarin.Models
{
    class FechaImportante
    {
        public int FechasImportantesID { get; set; }

        public int UsuarioID { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
