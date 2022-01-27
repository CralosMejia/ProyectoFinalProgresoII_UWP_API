using AgendaPlusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaPlusWeb.Controllers
{
    public class FechaImportanteController : Controller
    {
        // GET: FechaImportante
        DBAgenda DB = new DBAgenda();

        public ActionResult Index(Usuario usuario, int pagina = 1, String UserID = "", String rPorPagina = "6", String busqueda = "")
        {
            int rPorPaginaInt = Int32.Parse(rPorPagina);
            var cantidadRegistrosPorPagina = rPorPaginaInt;
            List<FechasImportante> ListaFecha = null;
            int totalRegistros = 0;

            if (UserID != "")
            {
                int idUser = Int32.Parse(UserID);
                usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);
            }
            if (busqueda != "")
            {

                ListaFecha = DB.FechasImportantes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Titulo.Contains(busqueda))
                .OrderBy(x => x.FechasImportantesID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.FechasImportantes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Titulo.Contains(busqueda)).Count();
            }
            else
            {
                ListaFecha = DB.FechasImportantes.Where(p => p.UsuarioID == usuario.UsuarioID)
               .OrderBy(x => x.FechasImportantesID ).Skip((pagina - 1) * cantidadRegistrosPorPagina)
               .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.FechasImportantes.Where(p => p.UsuarioID == usuario.UsuarioID).Count();
            }




            Paginacion paginacion = new Paginacion();

            paginacion.Usuario = usuario;
            paginacion.Usuario.FechasImportantes = ListaFecha;

            paginacion.PaginaActual = pagina;
            paginacion.TotalDeRegistros = totalRegistros;
            paginacion.RegistrosPorPagina = cantidadRegistrosPorPagina;

            return View(paginacion);
        }

        //-----------------------------------------------Crear fecha
        public ActionResult Crear(String UserID)
        {
            int idUser = Int32.Parse(UserID);
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);

            ViewBag.UserId = UserID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.FechaActual = FechaActual();
            ViewBag.Title = "";
            FechasImportante fecha = new FechasImportante();
            fecha.Usuario = user;
            return View(fecha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearModel(FechasImportante fecha, string UserID)
        {
            int idUser = Int32.Parse(UserID);
            fecha.UsuarioID = idUser;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == fecha.UsuarioID);
            fecha.Usuario = user;


            if (ModelState.IsValid)
            {
                DB.FechasImportantes.Add(fecha);
                DB.SaveChanges();
                return RedirectToAction("Index", "FechaImportante", new { usuario = user, pagina = 1, UserID = user.UsuarioID.ToString() });
            }
            return RedirectToAction("CrearError", fecha);

        }

        public ActionResult CrearError(FechasImportante fecha)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == fecha.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.FechaActual = FechaActual();

            fecha.Usuario = user;

            return View(fecha);
        }
        //----------------------------------------------------Editar Fecha
        public ActionResult editar(String fechaID)
        {
            int idFecha = Int32.Parse(fechaID);
            var fecha = DB.FechasImportantes.FirstOrDefault(u => u.FechasImportantesID == idFecha);
            fecha.Usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == fecha.UsuarioID);

            ViewBag.Nombre = fecha.Usuario.NombreUsuario;
            ViewBag.UserId = fecha.Usuario.UsuarioID;
            ViewBag.FechaActual = Fecha(fecha.FechaLimite);
            return View(fecha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarModel(FechasImportante fecha, string UserID,String fechaID)
        {
            int idUser = Int32.Parse(UserID);
            int idfechaInt = Int32.Parse(fechaID);
            fecha.UsuarioID = idUser;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == fecha.UsuarioID);
            fecha.Usuario = user;


            var fechaEditar = DB.Pendientes.Find(idfechaInt);

            if (ModelState.IsValid)
            {
                fechaEditar.Titulo = fecha.Titulo;
                fechaEditar.Descripcion = fecha.Descripcion;
                fechaEditar.FechaLimite = fecha.FechaLimite;
                fechaEditar.UsuarioID = fecha.UsuarioID;


                DB.Entry(fechaEditar).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "FechaImportante", fecha.Usuario);
            }
            return RedirectToAction("editarError", fecha);
        }

        public ActionResult editarError(FechasImportante fecha)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == fecha.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.FechaActual = Fecha(fecha.FechaLimite);

            fecha.Usuario = user;


            return View(fecha);
        }



        //--------------------------------------------------------Ver pendiente
        public ActionResult ver(String fechaID)
        {
            int idFecha = Int32.Parse(fechaID);
            var fecha = DB.FechasImportantes.FirstOrDefault(u => u.FechasImportantesID == idFecha);
            fecha.Usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == fecha.UsuarioID);

            @ViewBag.Nombre = fecha.Usuario.NombreUsuario;
            ViewBag.UserId = fecha.Usuario.UsuarioID;
            ViewBag.FechaActual = Fecha(fecha.FechaLimite);

            return View(fecha);
        }
        //------------------------------------eliminar
        public ActionResult eliminar(String del, String fechaID)
        {
            int fechaIDint = Int32.Parse(fechaID);
            FechasImportante fecha = DB.FechasImportantes.Find(fechaIDint);
            Usuario usuario = DB.Usuarios.Find(fecha.UsuarioID);
            if (del.CompareTo("yes") == 0)
            {
                DB.FechasImportantes.Remove(fecha);
                DB.SaveChanges();
                return RedirectToAction("Index", "FechaImportante", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
            }
            else
            {
                return RedirectToAction("Index", "FechaImportante", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
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