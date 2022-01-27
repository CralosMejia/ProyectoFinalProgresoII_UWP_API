using AgendaPlusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaPlusWeb.Controllers
{
    public class PendienteController : Controller
    {
        DBAgenda DB = new DBAgenda();

        // GET: Pendiente
        public ActionResult Index(Usuario usuario, int pagina = 1, String UserID = "", String rPorPagina = "6", String busqueda = "", String ordenar = "", String completTask= "false")
        {

            ViewBag.tareasCompletas = Convert.ToBoolean(completTask);
            int rPorPaginaInt = Int32.Parse(rPorPagina);
            var cantidadRegistrosPorPagina = rPorPaginaInt;
            List<Pendiente> ListaPendiente = null;
            int totalRegistros = 0;

            if (UserID != "")
            {
                int idUser = Int32.Parse(UserID);
                usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);
            }
            if (busqueda != "")
            {

                ListaPendiente = DB.Pendientes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Estado == false && p.Titulo.Contains(busqueda))
                .OrderBy(x => x.PendienteID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Pendientes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Estado == false && p.Titulo.Contains(busqueda)).Count();
            }
            else if (ordenar != "")
            {
                ListaPendiente = DB.Pendientes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Estado == false && p.Titulo.Contains(busqueda))
                .OrderBy(x => x.Prioridad).Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Pendientes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Estado == false && p.Titulo.Contains(busqueda)).Count();
            }
            else
            {
                ListaPendiente = DB.Pendientes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Estado == false)
               .OrderBy(x => x.PendienteID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
               .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Pendientes.Where(p => p.UsuarioID == usuario.UsuarioID && p.Estado == false).Count();
            }




            Paginacion paginacion = new Paginacion();

            paginacion.Usuario = usuario;
            paginacion.Usuario.Pendientes = ListaPendiente;

            paginacion.PaginaActual = pagina;
            paginacion.TotalDeRegistros = totalRegistros;
            paginacion.RegistrosPorPagina = cantidadRegistrosPorPagina;

            return View(paginacion);
        }


        
        //-----------------------------------------------Crear Pendiente
        public ActionResult Crear(String UserID)
        {
            int idUser = Int32.Parse(UserID);
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);

            ViewBag.UserId = UserID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.FechaActual = FechaActual();
            ViewBag.Title = "";
            Pendiente pendiente = new Pendiente();
            pendiente.Usuario = user;
            return View(pendiente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearModel(Pendiente pendiente, string UserID, String priority)
        {
            int idUser = Int32.Parse(UserID);
            int priorityInt = Int32.Parse(priority);
            pendiente.UsuarioID = idUser;
            pendiente.ColorPrioridad = color(priorityInt);
            pendiente.Prioridad = priorityInt;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == pendiente.UsuarioID);
            pendiente.Usuario = user;


            if (ModelState.IsValid)
            {
                DB.Pendientes.Add(pendiente);
                DB.SaveChanges();
                return RedirectToAction("Index", "Nota", new { usuario = user, pagina = 1, UserID = user.UsuarioID.ToString() });
            }
            return RedirectToAction("CrearError", pendiente);

        }

        public ActionResult CrearError(Pendiente pendiente)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == pendiente.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.FechaActual = FechaActual();

            pendiente.Usuario = user;

            return View(pendiente);
        }

        //--------------------------------------------------Cambia el estado de las tareas

        public ActionResult CompletedTask(List<String> check, String UserID, int pagina = 1)
        {
            int idUser = Int32.Parse(UserID);
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);
            if (check == null)
            {
                return RedirectToAction("Index", new { user, pagina, UserID });
            }
            foreach (String a in check)
            {
                int b = Int32.Parse(a);
                var pen = DB.Pendientes.Find(b);
                pen.Estado = true;
                DB.Entry(pen).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
            }

            return RedirectToAction("Index", new { user, pagina, UserID });
        }

        //----------------------------------------------------Editar Pendiente
        public ActionResult editar(String pendienteID)
        {
            int idPendiente = Int32.Parse(pendienteID);
            var pendiente = DB.Pendientes.FirstOrDefault(u => u.PendienteID == idPendiente);
            pendiente.Usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == pendiente.UsuarioID);

            ViewBag.Nombre = pendiente.Usuario.NombreUsuario;
            ViewBag.UserId = pendiente.Usuario.UsuarioID;
            ViewBag.FechaActual = Fecha(pendiente.FechaLimite);
            return View(pendiente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarModel(Pendiente pendiente, string UserID, String priority, String pendienteID)
        {
            int idUser = Int32.Parse(UserID);
            int idpendienteInt = Int32.Parse(pendienteID);
            int priorityInt = Int32.Parse(priority);
            pendiente.UsuarioID = idUser;
            pendiente.ColorPrioridad = color(priorityInt);
            pendiente.Prioridad = priorityInt;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == pendiente.UsuarioID);
            pendiente.Usuario = user;


            var pendienteEditar = DB.Pendientes.Find(idpendienteInt);

            if (ModelState.IsValid)
            {
                pendienteEditar.Titulo = pendiente.Titulo;
                pendienteEditar.Descripcion = pendiente.Descripcion;
                pendienteEditar.FechaLimite = pendiente.FechaLimite;
                pendienteEditar.ColorPrioridad = pendiente.ColorPrioridad;
                pendienteEditar.Prioridad = pendiente.Prioridad;
                pendienteEditar.UsuarioID = pendiente.UsuarioID;


                DB.Entry(pendienteEditar).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "Pendiente", pendiente.Usuario);
            }
            return RedirectToAction("editarError", pendiente);
        }

        public ActionResult editarError(Pendiente pendiente)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == pendiente.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.FechaActual = Fecha(pendiente.FechaLimite);

            pendiente.Usuario = user;


            return View(pendiente);
        }

        //--------------------------------------------------------Ver pendiente
        public ActionResult ver(String pendienteID)
        {
            int idPendiente = Int32.Parse(pendienteID);
            var pendiente = DB.Pendientes.FirstOrDefault(u => u.PendienteID == idPendiente);
            pendiente.Usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == pendiente.UsuarioID);

            @ViewBag.Nombre = pendiente.Usuario.NombreUsuario;
            ViewBag.UserId = pendiente.Usuario.UsuarioID;
            ViewBag.FechaActual = Fecha(pendiente.FechaLimite);

            return View(pendiente);
        }

        //------------------------------------eliminar
        public ActionResult eliminar(String del, String pendienteID)
        {
            int pendienteIDint = Int32.Parse(pendienteID);
            Pendiente pendiente = DB.Pendientes.Find(pendienteIDint);
            Usuario usuario = DB.Usuarios.Find(pendiente.UsuarioID);
            if (del.CompareTo("yes") == 0)
            {
                DB.Pendientes.Remove(pendiente);
                DB.SaveChanges();
                return RedirectToAction("Index", "Pendiente", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
            }
            else
            {
                return RedirectToAction("Index", "Pendiente", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
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