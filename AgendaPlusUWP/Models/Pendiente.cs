using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Models
{
    class Pendiente
    {
        public int PendienteID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        public int Prioridad { get; set; }

        public string ColorPrioridad { get; set; }

        public bool Estado { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
