using SGAIJE.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGAIJE.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(SGAIJE.DataContext.Usuario Usuario)
        {
            using (SGAIJEEntities db = new SGAIJEEntities())
            {
                var DetallesUsuario = db.Usuario.Where(x => x.vchUsuario == Usuario.vchUsuario && x.vchClave == Usuario.vchClave).FirstOrDefault();

                if(DetallesUsuario == null)
                {
                    return View("Index", Usuario);
                }
                else
                {
                    Session["Usuario"] = DetallesUsuario.vchUsuario;
                    return RedirectToAction("Index", "Home");
                }
            }
        
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            using (SGAIJEEntities db = new SGAIJEEntities())
            {
                if(db.Usuario.Any(x => x.vchUsuario == usuario.vchUsuario))
                {
                    return RedirectToAction("Index", "Login");
                }
                db.Usuario.Add(usuario);
                db.SaveChanges();
            }
            Session["Usuario"] = usuario.vchUsuario;
            ModelState.Clear();
            return RedirectToAction("Index", "Home");
        }


    }


}