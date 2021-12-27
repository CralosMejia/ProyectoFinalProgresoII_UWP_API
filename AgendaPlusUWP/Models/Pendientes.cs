using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace AgendaPlusUWP.Models
{
    class Pendientes
    {
     

        [Key]
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

        public virtual Usuarios Usuarios { get; set; }

        public string PrioridadString { get; set; }

        public string EstadoString { get; set; }

        public void calcularPrioridad()
        {
            if (Prioridad == 1)
            {
                PrioridadString = "Severe";
                ColorPrioridad = "FF2D00";
            }
            else if (Prioridad == 2)
            {
                PrioridadString = "Important";
                ColorPrioridad = "FF5935"; 
            }
            else if (Prioridad == 3)
            {
                PrioridadString = "Normal";
                ColorPrioridad = "FF9882"; 
            }


        }

        public void calcularEstado()
        {
            if (Estado)
            {
               EstadoString = "Done";
            }
            else if (!Estado)
            {
                EstadoString = "Pending";
            }
            else
            {
                EstadoString = "Error";
            }
        }

      
    }
}
