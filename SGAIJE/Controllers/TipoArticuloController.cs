using SGAIJE.DataContext;
using SGAIJE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGAIJE.Controllers
{
    public class TipoArticuloController : Controller
    {
        SGAIJEEntities db = new SGAIJEEntities();
       
        public List<TipoArticulo> GetTipoArticulo(Guid? id)
        {
            List<TipoArticulo> TipoArticulo = new List<TipoArticulo>();
            TipoArticulo = db.prGetTipoArticulo(id).ToList().Select(x => new TipoArticulo()
            {
                GenTipoArticuloKey = x.unqGenTipoArticuloKey,
                Descripcion = x.vchDescripcion
            }).ToList();
            return TipoArticulo;
        }

        private void InitControls()
        {
            ViewBag.DsTipoArticulo = (from x in db.prGetTipoArticulo(null).ToList() select x.vchDescripcion).ToList();
        }

        // GET: TipoArticulo
        public ActionResult Index()
        {
            return View(GetTipoArticulo(null));
        }

        // GET: TipoArticulo/Details/5
        public ActionResult Details(Guid id)
        {
            return View(GetTipoArticulo(id).First());
        }

        // GET: TipoArticulo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoArticulo/Create
        [HttpPost]
        public ActionResult Create(TipoArticulo TipoArticulo)
        {
            try
            {

                var TipoArticuloKey = db.prMantenimientoTipoArticulo(Guid.Empty, TipoArticulo.Descripcion,  1).First();
                db.SetComentarioXML(TipoArticuloKey, "Gen", "TipoArticulo", Session["Usuario"].ToString(), "Agrego", "El usuario " + Session["Usuario"].ToString() + " agrego el registro " + TipoArticulo.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + TipoArticulo.Descripcion + " ha sido capturado correctamente!";
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

        // GET: TipoArticulo/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbTipoArticulo = db.prGetTipoArticulo(id);

            if (dbTipoArticulo == null)
                return HttpNotFound();

            var objTipoArticulo = dbTipoArticulo.First();

            TipoArticulo TipoA = new TipoArticulo();
            TipoA = GetTipoArticulo(id).First();
            return View(TipoA);
        }

        // POST: TipoArticulo/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, TipoArticulo TipoArticulo)
        {
            try
            {

                // TODO: Add update logic here
                db.prMantenimientoTipoArticulo(id, TipoArticulo.Descripcion, 2).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "TipoArticulo", Session["Usuario"].ToString(), "Cambio", "El usuario " + Session["Usuario"].ToString() + " cambió el registro " + " por " + TipoArticulo.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + TipoArticulo.Descripcion + " ha sido modificado correctamente!";
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

        // GET: TipoArticulo/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbTipoArticulo = db.prGetTipoArticulo(id);

            if (dbTipoArticulo == null)
                return HttpNotFound();

            var objTipoArticulo = dbTipoArticulo.First();

            TipoArticulo TipoA = new TipoArticulo();
            TipoA = GetTipoArticulo(id).First();
            return View(TipoA);
        }

        // POST: TipoArticulo/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, TipoArticulo TipoArticulo)
        {
            try
            {

                // TODO: Add update logic here
                db.prMantenimientoTipoArticulo(id, TipoArticulo.Descripcion, 3).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "TipoArticulo", Session["Usuario"].ToString(), "Elimino", "El usuario " + Session["Usuario"].ToString() + " elimino el registro " + " por " + TipoArticulo.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + TipoArticulo.Descripcion + " ha sido eliminado correctamente!";
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
            var Comentarios = db.prGetComentarioXML(id.ToString(), "Gen", "TipoArticulo").ToList().Select(x => new Comentario() { Accion = x.vchAccion, Coment = x.vchComentario, Fecha = x.dtmFecha, Usuario = x.vchUsuario }).ToList();

            return View(Comentarios);
        }
    }
}
