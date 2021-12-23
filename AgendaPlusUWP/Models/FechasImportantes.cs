using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{
    class FechasImportantes
    {
        public int FechasImportantesID { get; set; }

        public int UsuarioID { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
