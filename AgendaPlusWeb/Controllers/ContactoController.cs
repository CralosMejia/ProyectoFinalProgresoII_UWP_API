using AgendaPlusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaPlusWeb.Controllers
{
    public class ContactoController : Controller
    {
        DBAgenda DB = new DBAgenda();
        // GET: Contacto
        public ActionResult Index(Usuario usuario, int pagina = 1, String UserID = "", String rPorPagina = "6", String busqueda = "")
        {
            int rPorPaginaInt = Int32.Parse(rPorPagina);
            var cantidadRegistrosPorPagina = rPorPaginaInt;
            List<Contacto> ListaContacto = null;
            int totalRegistros = 0;

            if (UserID != "")
            {
                int idUser = Int32.Parse(UserID);
                usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);
            }
            if (busqueda != "")
            {

                ListaContacto = DB.Contactos.Where(p => p.UsuarioID == usuario.UsuarioID && p.NombreContacto.Contains(busqueda))
                .OrderBy(x => x.ContactoID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Contactos.Where(p => p.UsuarioID == usuario.UsuarioID && p.NombreContacto.Contains(busqueda)).Count();
            }
            else
            {
                ListaContacto = DB.Contactos.Where(p => p.UsuarioID == usuario.UsuarioID)
               .OrderBy(x => x.ContactoID).Skip((pagina - 1) * cantidadRegistrosPorPagina)
               .Take(cantidadRegistrosPorPagina).ToList();

                totalRegistros = DB.Contactos.Where(p => p.UsuarioID == usuario.UsuarioID).Count();
            }




            Paginacion paginacion = new Paginacion();

            paginacion.Usuario = usuario;
            paginacion.Usuario.Contactos = ListaContacto;

            paginacion.PaginaActual = pagina;
            paginacion.TotalDeRegistros = totalRegistros;
            paginacion.RegistrosPorPagina = cantidadRegistrosPorPagina;

            return View(paginacion);
        }

        //-----------------------------------------------Crear contacto
        public ActionResult Crear(String UserID)
        {
            int idUser = Int32.Parse(UserID);
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);

            ViewBag.UserId = UserID;
            ViewBag.Nombre = user.NombreUsuario;
            ViewBag.Title = "";
            Contacto contacto = new Contacto();
            contacto.Usuario = user;
            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearModel(Contacto contacto, string UserID)
        {
            int idUser = Int32.Parse(UserID);
            contacto.UsuarioID = idUser;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == contacto.UsuarioID);
            contacto.Usuario = user;


            if (ModelState.IsValid)
            {
                DB.Contactos.Add(contacto);
                DB.SaveChanges();
                return RedirectToAction("Index", "Contacto", new { usuario = user, pagina = 1, UserID = user.UsuarioID.ToString() });
            }
            return RedirectToAction("CrearError", contacto);

        }

        public ActionResult CrearError(Contacto contacto)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == contacto.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;

            contacto.Usuario = user;

            return View(contacto);
        }

        //----------------------------------------------------Editar contacto
        public ActionResult editar(String contactoID)
        {
            int idContacto = Int32.Parse(contactoID);
            var contacto = DB.Contactos.FirstOrDefault(u => u.ContactoID == idContacto);
            contacto.Usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == contacto.UsuarioID);

            ViewBag.Nombre = contacto.Usuario.NombreUsuario;
            ViewBag.UserId = contacto.Usuario.UsuarioID;
            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarModel(Contacto contacto, string UserID, String contactoID)
        {
            int idUser = Int32.Parse(UserID);
            int idcontactoInt = Int32.Parse(contactoID);
            contacto.UsuarioID = idUser;
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == contacto.UsuarioID);
            contacto.Usuario = user;


            var contactoEditar = DB.Contactos.Find(idcontactoInt);

            if (ModelState.IsValid)
            {
                contactoEditar.NombreContacto = contacto.NombreContacto;
                contactoEditar.CorreoContacto= contacto.CorreoContacto;
                contactoEditar.TelefonoContacto = contacto.TelefonoContacto;
                contactoEditar.UsuarioID = contacto.UsuarioID;


                DB.Entry(contactoEditar).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "Contacto", contacto.Usuario);
            }
            return RedirectToAction("editarError", contacto);
        }

        public ActionResult editarError(Contacto contacto)
        {
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == contacto.UsuarioID);

            ViewBag.UserId = user.UsuarioID;
            ViewBag.Nombre = user.NombreUsuario;

            contacto.Usuario = user;


            return View(contacto);
        }


        public ActionResult eliminar(String del, String contactoID)
        {
            int contactoIDint = Int32.Parse(contactoID);
            Contacto contacto = DB.Contactos.Find(contactoIDint);
            Usuario usuario = DB.Usuarios.Find(contacto.UsuarioID);
            if (del.CompareTo("yes") == 0)
            {
                DB.Contactos.Remove(contacto);
                DB.SaveChanges();
                return RedirectToAction("Index", "Contacto", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
            }
            else
            {
                return RedirectToAction("Index", "Contacto", new { usuario = usuario, pagina = 1, UserID = usuario.UsuarioID.ToString() });
            }
        }
    }
}