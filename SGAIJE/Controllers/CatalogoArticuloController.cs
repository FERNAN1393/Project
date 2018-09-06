using SGAIJE.DataContext;
using SGAIJE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace SGAIJE.Controllers
{
    public class CatalogoArticuloController : Controller
    {
        SGAIJEEntities db = new SGAIJEEntities();
        public List<CatalogoArticulo> GetArticulo(Guid? id)
        {
            List<CatalogoArticulo> Articulo = new List<CatalogoArticulo>();
            Articulo = db.prGetArticulo(id).ToList().Select(x => new CatalogoArticulo()
            {
                GenArticuloKey = x.unqGenArticuloKey,
                GenTipoArticuloLink = x.unqGenTipoArticuloLink,
                TipoArticulo = db.prGetTipoArticulo(x.unqGenTipoArticuloLink).First().vchDescripcion,
                CodigoArticulo = x.vchCodigoArticulo,
                Descripcion = x.vchDescripcion,
                Precio = x.decPrecio,
                Descuento = x.decDescuento,
                Existencia = x.intExistencia,
                CantidadMaxima = x.intCantidadMaxima,
                CantidadMinima = x.intCantidadMinima

            }).ToList();
            return Articulo;

        }

        private void InitControls()
        {
            ViewBag.DsArticulo = (from x in db.prGetArticulo(null).ToList() select new { x.unqGenArticuloKey, x.unqGenTipoArticuloLink, x.vchDescripcion }).ToList();
            //ViewBag.DsTipoArticulos = (from x in db.prGetTipoArticulo(null).ToList() select x.unqGenTipoArticuloKey, x.vchDescripcion).ToList();
            ViewBag.DsTipoArticulos  = (from x in db.prGetTipoArticulo(null).ToList() select new {x.vchDescripcion, x.unqGenTipoArticuloKey}).ToList();
            //ViewBag.DsTipoArticulos = new SelectList()
        }

        // GET: Articulo
        public ActionResult Index()
        {
            return View(GetArticulo(null));
        }

        // GET: Articulo/Details/5
        public ActionResult Details(Guid id)
        {
            return View(GetArticulo(id).First());
        }

        // GET: Articulo/Create
        public ActionResult Create()
        {
            InitControls();


            return View();
        }

        // POST: Articulo/Create
        [HttpPost]
        public ActionResult Create(CatalogoArticulo CatalogoArticulo, FormCollection TipoArticulo)
        {
            try
            {
                var ArticuloKey = db.prMantenimientoArticulo(Guid.Empty, Guid.Parse(TipoArticulo["cboTipoArticulo"].ToString()), CatalogoArticulo.CodigoArticulo, CatalogoArticulo.Descripcion, CatalogoArticulo.Precio, CatalogoArticulo.Descuento, CatalogoArticulo.Existencia, CatalogoArticulo.CantidadMaxima, CatalogoArticulo.CantidadMinima, 1).First();
                db.SetComentarioXML(ArticuloKey, "Gen", "Articulo", Session["Usuario"].ToString(), "Agrego", "El usuario " + Session["Usuario"].ToString() + " agrego el registro " + CatalogoArticulo.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + CatalogoArticulo.Descripcion + " ha sido capturado correctamente!";
                Session["MessageTipo"] = "success";
           


                return RedirectToAction("Index");
            }
             catch (Exception x)
            {
                if (x.InnerException == null)
                {
                    Session["Message"] = "Registro Incorrecto! ";
                    Session["MessageDatos"] = x.InnerException.ToString();
                    Session["MessageTipo"] = "error";
                    return RedirectToAction("Index");
                }

                Session["Message"] = "Registro Incorrecto! ";
                Session["MessageDatos"] = x.InnerException.Message.ToString();
                Session["MessageTipo"] = "error";
                return RedirectToAction("Index");
            }
        }

        // GET: Articulo/Edit/5
        public ActionResult Edit(Guid id)
        {
            InitControls();
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbArticulo = db.prGetArticulo(id);

            if (dbArticulo == null)
                return HttpNotFound();

            var objTArticulo = dbArticulo.First();

            CatalogoArticulo Articulos = new CatalogoArticulo();
            Articulos = GetArticulo(id).First();
            return View(Articulos);
        }

        // POST: Articulo/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, CatalogoArticulo CatalogoArticulo, FormCollection TipoArticulo)
        {
            try
            {
                
                var TipoClienteKey = db.prMantenimientoArticulo(id, Guid.Parse(TipoArticulo["cboTipoArticulo"].ToString()), CatalogoArticulo.CodigoArticulo, CatalogoArticulo.Descripcion, CatalogoArticulo.Precio, CatalogoArticulo.Descuento, CatalogoArticulo.Existencia, CatalogoArticulo.CantidadMaxima, CatalogoArticulo.CantidadMinima, 2).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "Articulo", Session["Usuario"].ToString(), "cambio", "El usuario " + Session["Usuario"].ToString() + " cambio el registro " + CatalogoArticulo.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + CatalogoArticulo.Descripcion + " ha sido modificado correctamente!";
                Session["MessageTipo"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                if (x.InnerException == null)
                {
                    Session["Message"] = "Registro Incorrecto! ";
                    Session["MessageDatos"] = x.InnerException.ToString();
                    Session["MessageTipo"] = "error";
                    return RedirectToAction("Index");
                }

                Session["Message"] = "Registro Incorrecto! ";
                Session["MessageDatos"] = x.InnerException.Message.ToString();
                Session["MessageTipo"] = "error";
                return RedirectToAction("Index");
            }
        }

        // GET: Articulo/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbArticulo = db.prGetArticulo(id);

            if (dbArticulo == null)
                return HttpNotFound();

            var objCliente = dbArticulo.First();

            CatalogoArticulo Articulo = new CatalogoArticulo();
            Articulo = GetArticulo(id).First();
            return View(Articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, CatalogoArticulo CatalogoArticulo)
        {
            try
            {

                // TODO: Add update logic here
                var TipoClienteKey = db.prMantenimientoArticulo(id, null, null, null, null, null, null, null, null, 3).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "Articulo", Session["Usuario"].ToString(), "cambio", "El usuario " + Session["Usuario"].ToString() + " dio de baja " + CatalogoArticulo.Descripcion);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + CatalogoArticulo.Descripcion + " ha sido eliminado correctamente!";
                Session["MessageTipo"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception x)
            {
                if (x.InnerException == null)
                {
                    Session["Message"] = "Registro Incorrecto! ";
                    Session["MessageDatos"] = x.InnerException.ToString();
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
            var Comentarios = db.prGetComentarioXML(id.ToString(), "Gen", "Articulo").ToList().Select(x => new Comentario() { Accion = x.vchAccion, Coment = x.vchComentario, Fecha = x.dtmFecha, Usuario = x.vchUsuario }).ToList();

            return View(Comentarios);
        }
    }
}
