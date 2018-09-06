using SGAIJE.DataContext;
using SGAIJE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGAIJE.Controllers
 
{
    public class TipoClienteController : Controller
    { 
        SGAIJEEntities db = new SGAIJEEntities();

        
        public List<TipoCliente> GetTipoCliente(Guid? id)
        {
            List<TipoCliente> TipoCliente = new List<TipoCliente>();
            TipoCliente = db.prGetTipoCliente(id).ToList().Select(x => new TipoCliente()
            {
                GenTipoClienteKey = x.unqGenTipoClienteKey,
                Descripcion = x.vchDescripcion,
                Descuento = x.decDescuento
            }).ToList();
            return TipoCliente;
        }

        private void InitControls()
        {
            
            ViewBag.DsTipoCliente = (from x in db.prGetTipoCliente(null).ToList() select new SelectListItem { Text = x.vchDescripcion, Value = x.unqGenTipoClienteKey.ToString() }).ToList();
        }


        // GET: TipoCliente
        public ActionResult Index()
        {
            return View(GetTipoCliente(null));
        }

        // GET: TipoCliente/Details/5
        public ActionResult Details(Guid id)
        {
            return View(GetTipoCliente(id).First());
        }

        // GET: TipoCliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCliente/Create
        [HttpPost]
        public ActionResult Create(TipoCliente TipoCliente)
        {
            try
            {

                var TipoClienteKey = db.prMantenimientoTipoCliente(Guid.Empty, TipoCliente.Descripcion, TipoCliente.Descuento, 1).First();
                db.SetComentarioXML(TipoClienteKey, "Gen", "TipoCliente", Session["Usuario"].ToString(), "Agrego", "El usuario " + Session["Usuario"].ToString() + " agrego el registro " + TipoCliente.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + TipoCliente.Descripcion + " ha sido capturado correctamente!";
                Session["MessageTipo"] = "success";

                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                if (x.InnerException == null)
                {
                    Session["Message"] = "Registro Incorrecto! ";
                    Session["MessageDatos"] = x.Message.ToString();
                    Session["MessageTipo"] = "error";
                    return RedirectToAction("Index");
                }

                Session["Message"] = "Registro Incorrecto! ";
                Session["MessageDatos"] = x.InnerException.Message.ToString();
                Session["MessageTipo"] = "error";
                return RedirectToAction("Index");
            }
        }

        // GET: TipoCliente/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbTipoCliente = db.prGetTipoCliente(id);

            if (dbTipoCliente == null)
                return HttpNotFound();

            var objTipoCliente = dbTipoCliente.First();

            TipoCliente TipoC = new TipoCliente();
            TipoC = GetTipoCliente(id).First();
            return View(TipoC);
        }

        // POST: TipoCliente/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, TipoCliente TipoCliente)
        {
            try
            {

                // TODO: Add update logic here
                db.prMantenimientoTipoCliente(id, TipoCliente.Descripcion,TipoCliente.Descuento ,2).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "TipoCliente", Session["Usuario"].ToString(), "Cambio", "El usuario " + Session["Usuario"].ToString() + " cambió el registro " + " por " + TipoCliente.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + TipoCliente.Descripcion + " ha sido modificado correctamente!";
                Session["MessageTipo"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                if (x.InnerException == null)
                {
                    Session["Message"] = "Registro Incorrecto! ";
                    Session["MessageDatos"] = x.Message.ToString();
                    Session["MessageTipo"] = "error";
                    return RedirectToAction("Index");
                }

                Session["Message"] = "Registro Incorrecto! ";
                Session["MessageDatos"] = x.InnerException.Message.ToString();
                Session["MessageTipo"] = "error";
                return RedirectToAction("Index");
            }
        }

        // GET: TipoCliente/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbTipoCliente = db.prGetTipoCliente(id);

            if (dbTipoCliente == null)
                return HttpNotFound();

            var objTipoCliente = dbTipoCliente.First();

            TipoCliente TipoC = new TipoCliente();
            TipoC = GetTipoCliente(id).First();
            return View(TipoC);
        }

        // POST: TipoCliente/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, TipoCliente TipoCliente)
        {
            try
            {

                // TODO: Add update logic here
                db.prMantenimientoTipoCliente(id, TipoCliente.Descripcion, TipoCliente.Descuento, 3).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "TipoCliente", Session["Usuario"].ToString(), "Elimino", "El usuario " + Session["Usuario"].ToString() + "Elimino el registro " + " por " + TipoCliente.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + TipoCliente.Descripcion + " ha sido eliminado correctamente!";
                Session["MessageTipo"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                if (x.InnerException == null)
                {
                    Session["Message"] = "Registro Incorrecto! ";
                    Session["MessageDatos"] = x.Message.ToString();
                    Session["MessageTipo"] = "error";
                    return RedirectToAction("Index");
                }

                Session["Message"] = "Registro Incorrecto! ";
                Session["MessageDatos"] = x.InnerException.Message.ToString();
                Session["MessageTipo"] = "error";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("GetComentario")]
        public ActionResult GetComentario(Guid id)
        {
            var Comentarios = db.prGetComentarioXML(id.ToString(), "Gen", "TipoCliente").ToList().Select(x => new Comentario() { Accion = x.vchAccion, Coment = x.vchComentario, Fecha = x.dtmFecha, Usuario = x.vchUsuario}).ToList();

            return View(Comentarios);
        }


    }
}
