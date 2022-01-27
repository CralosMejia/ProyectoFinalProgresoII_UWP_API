using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaPlusWeb.Models
{
    public class Paginacion
    {
        public int PaginaActual { get; set; }
        public int TotalDeRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }

        public Usuario Usuario { get; set; }
    }
}