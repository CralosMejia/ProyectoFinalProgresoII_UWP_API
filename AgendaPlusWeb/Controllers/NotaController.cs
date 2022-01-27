using AgendaPlusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaPlusWeb.Controllers
{
    public class NotaController : Controller
    {

        DBAgenda DB = new DBAgenda();
        // GET: Nota
        public ActionResult Index(Usuario usuario, int pagina = 1, String UserID = "", String rPorPagina = "6", String busqueda = "")
        {
            int rPorPaginaInt = Int32.Parse(rPorPagina);
            var cantidadRegistrosPorPagina = rPorPaginaInt;
            List<Nota> ListaPendiente = null;
            int totalRegistros = 0;

            if (UserID != "")
            {
                int idUser = Int32.Parse(UserID);
                usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);
            }
            if (busqueda != "")
            {

                ListaPendiente = DB.Notas.Where(p => p.UsuarioID == usuario.UsuarioID && p.Titulo.Contains(busqueda))
                .OrderBy(x => x.NotaID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Notas.Where(p => p.UsuarioID == usuario.UsuarioID && p.Titulo.Contains(busqueda)).Count();
            }
            else
            {
                ListaPendiente = DB.Notas.Where(p => p.UsuarioID == usuario.UsuarioID)
               .OrderBy(x => x.NotaID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
               .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Notas.Where(p => p.UsuarioID == usuario.UsuarioID).Count();
            }




            Paginacion paginacion = new Paginacion();

            paginacion.Usuario = usuario;
            paginacion.Usuario.Notas = ListaPendiente;

            paginacion.PaginaActual = pagina;
            paginacion.TotalDeRegistros = totalRegistros;
            paginacion.RegistrosPorPagina = cantidadRegistrosPorPagina;

            return View(paginacion);
        }


        //-----------------------------------------------Crear Nota
        public ActionResult Crear(String UserID)
        {
            int idUser = Int32.Parse(UserID);
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);

            ViewBag.UserId = UserID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.Title = "";
            Nota nota = new Nota();
            nota.Usuario = user;
            return View(nota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearModel(Nota nota, string UserID)
        {
            int idUser = Int32.Parse(UserID);
            nota.UsuarioID = idUser;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == nota.UsuarioID);
            nota.Usuario = user;


            if (ModelState.IsValid)
            {
                DB.Notas.Add(nota);
                DB.SaveChanges();
                return RedirectToAction("Index", "Nota", new { usuario = user, pagina = 1, UserID = user.UsuarioID.ToString() });
            }
            return RedirectToAction("CrearError", nota);

        }

        public ActionResult CrearError(Nota nota)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == nota.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;

            nota.Usuario = user;

            return View(nota);
        }

        //----------------------------------------------------Editar Nota
        public ActionResult editar(String notaID)
        {
            int idNota = Int32.Parse(notaID);
            var nota = DB.Notas.FirstOrDefault(u => u.NotaID == idNota);
            nota.Usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == nota.UsuarioID);

            ViewBag.Nombre = nota.Usuario.NombreUsuario;
            ViewBag.UserId = nota.Usuario.UsuarioID;
            return View(nota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarModel(Nota nota, string UserID,String notaID)
        {
            int idUser = Int32.Parse(UserID);
            int idnotaInt = Int32.Parse(notaID);
            nota.UsuarioID = idUser;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == nota.UsuarioID);
            nota.Usuario = user;


            var notaEditar = DB.Notas.Find(idnotaInt);

            if (ModelState.IsValid)
            {
                notaEditar.Titulo = nota.Titulo;
                notaEditar.Descripcion = nota.Descripcion;
                notaEditar.UsuarioID = nota.UsuarioID;


                DB.Entry(notaEditar).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "Nota", nota.Usuario);
            }
            return RedirectToAction("editarError", nota);
        }

        public ActionResult editarError(Nota nota)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == nota.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;

            nota.Usuario = user;


            return View(nota);
        }
        //------------------------------------eliminar
        public ActionResult eliminar(String del, String notaID)
        {
            int notaIDint = Int32.Parse(notaID);
            Nota nota = DB.Notas.Find(notaIDint);
            Usuario usuario = DB.Usuarios.Find(nota.UsuarioID);
            if (del.CompareTo("yes") == 0)
            {
                DB.Notas.Remove(nota);
                DB.SaveChanges();
                return RedirectToAction("Index", "Nota", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
            }
            else
            {
                return RedirectToAction("Index", "Nota", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        //Funciones que se necesitan
        private string FechaActual()
        {
            return String.Format("{0:yyyy-MM-ddTHH:mm}", DateTime.Now);
        }

        private string color(int prioridad)
        {
            string resultado = "#";

            switch (prioridad)
            {
                case 1:
                    resultado = resultado + "FF8C78";
                    break;
                case 2:
                    resultado = resultado + "FBAB62";
                    break;
                case 3:
                    resultado = resultado + "FFF85B";
                    break;
            }

            return resultado;
        }

        private string Fecha(DateTime fecha)
        {


            return String.Format("{0:yyyy-MM-ddTHH:mm}", fecha);

        }

    }
}