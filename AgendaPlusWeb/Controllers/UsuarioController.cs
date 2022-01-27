using AgendaPlusWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgendaPlusWeb.Controllers
{
    public class UsuarioController : Controller
    {
        DBAgenda DB = new DBAgenda();

        // GET: Usuario

        //---------------------------------------Login
        public ActionResult Index(String mensaje = "")
        {
            ViewBag.MessageErrorUserExist = mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult Autenticacion(Usuario usuario)
        {
            if (usuario.Correo != null && usuario.Contrasena != null)
            {
                var user = DB.Usuarios.FirstOrDefault(u => u.Correo == usuario.Correo && u.Contrasena == usuario.Contrasena);
                if (user != null)
                {

                    FormsAuthentication.SetAuthCookie(user.Correo, true);
                    return RedirectToAction("Index", "Pendiente", new { usuario = user, pagina = 1, UserID = user.UsuarioID.ToString() });
                }
                else
                {
                    return RedirectToAction("Index", new { mensaje = "User not find or password incorrect" });
                }
            }
            else
            {
                return RedirectToAction("Index", new { mensaje = "Complete this fills" });
            }
        }
        //------------------------------------------Crear nuevo Usuario
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterError(Usuario usuario, string mensaje = "")
        {
            ViewBag.MessageErrorUserExist = mensaje;
            return View(usuario);
        }

        [HttpPost]
        public ActionResult validarRegisterUser(Usuario usuario)
        {
            usuario.Avatar = "https://i.ibb.co/v1QQ7Kd/profile.png";
            if (DB.Usuarios.Any(x => x.NombreUsuario == usuario.NombreUsuario))
            {
                return RedirectToAction("RegisterError", new { mensaje = "Username Already exist" });
            }
            if (DB.Usuarios.Any(x => x.Correo == usuario.Correo))
            {
                return RedirectToAction("RegisterError", new { mensaje = "Email Already exist" });
            }
            if (ModelState.IsValid)
            {
                DB.Usuarios.Add(usuario);
                DB.SaveChanges();
                return RedirectToAction("Index", "Pendiente", usuario);
            }
            return RedirectToAction("RegisterError", usuario);
        }

        //--------------------------------About
        public ActionResult about(String usuarioIDS)
        {
            int idUser = Int32.Parse(usuarioIDS);
            var user = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);
            Paginacion p = new Paginacion();
            p.Usuario = user;
            return View(p);
        }

        //--------------------------------------settings
        public ActionResult settings(String usuarioIDS, String mensajeContra = "", String mensajeAvatar = "")
        {
            int idUser = Int32.Parse(usuarioIDS);
            Usuario usuario = DB.Usuarios.FirstOrDefault(u => u.UsuarioID == idUser);



            ViewBag.MensajeContraseña = mensajeContra;
            ViewBag.MensajeAvatar = mensajeAvatar;

            return View(usuario);
        }
        public ActionResult settingsError(Usuario usuario)
        {
            return View(usuario);
        }

        [ValidateAntiForgeryToken]
        public ActionResult editarContraseña([Bind(Include = "UsuarioID,NombreUsuario,Correo,Contrasena,ConfirmarContrasena,Avatar")] Usuario usuario)
        {
            String usuarioIDS = usuario.UsuarioID.ToString();

            //se verifica que el mode lo cumpla las dataAnnotations

            if (ModelState.IsValid)
            {

                DB.Entry(usuario).State = EntityState.Modified;
                DB.SaveChanges();
                String mensajeContra = "Password has been modified";
                return RedirectToAction("settings", new { usuarioIDS, mensajeContra });
            }
            //se retorna a la vista pero con los errores presentes

            return RedirectToAction("settingsError", usuario);
        }

        [ValidateAntiForgeryToken]
        public ActionResult editarAvatar([Bind(Include = "UsuarioID,NombreUsuario,Correo,Contrasena,ConfirmarContrasena,Avatar")] Usuario usuario)
        {
            String usuarioIDS = usuario.UsuarioID.ToString();
            //se verifica que el mode lo cumpla las dataAnnotations
            if (usuario.Avatar == null)
            {
                String mensajeAvatar = "Avatar is required";
                return RedirectToAction("settings", new { usuarioIDS, mensajeAvatar });
            }
            if (ModelState.IsValid)
            {

                DB.Entry(usuario).State = EntityState.Modified;
                DB.SaveChanges();
                String mensajeAvatar = "Avatar has been modified";
                return RedirectToAction("settings", new { usuarioIDS, mensajeAvatar });
            }
            //se retorna a la vista pero con los errores presentes

            return RedirectToAction("settingsError", usuario);
        }


        //-------------------------log out
        public ActionResult Logout(String mensaje = "")
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", new { message = "Regresa pronto" });
        }

    }
}