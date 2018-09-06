using SGAIJE.DataContext;
using SGAIJE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGAIJE.Controllers
{
    public class ClienteController : Controller
    {
        SGAIJEEntities db = new SGAIJEEntities();

        public List<Cliente> GetCliente(Guid? Id)
        {
            List<Cliente> Cliente = new List<Cliente>();
            Cliente = db.prGetCliente(Id).ToList().Select(x => new Cliente()
            {
                GenClienteKey = x.unqGenClienteKey,
                GenTipoClienteLink = x.unqGenTipoClienteLink,
                DescripcionTipoCliente = db.prGetTipoCliente(x.unqGenTipoClienteLink).First().vchDescripcion,
                Nombre = x.vchNombre,
                ApellidoP = x.vchApellidoP,
                ApellidoM = x.vchApellidoM,
                NombreCompleto = x.vchNombre + " " + x.vchApellidoP + " " + x.vchApellidoM,
                RFC = x.vchRFC,
                DomicilioCompleto = x.vchCalle + " No Ext." + x.vchNoExterior + " " + x.vchColonia + " " + x.vchCodigoPostal,
                Calle = x.vchCalle,
                NoExterior = x.vchNoExterior,
                Colonia = x.vchColonia,
                CodigoPostal = x.vchCodigoPostal,
                Telefono = x.vchTelefono,
                Correo = x.vchCorreo
            }).ToList();
            return Cliente;
        }

        private void InitControls()
        {
            ViewBag.DSCliente = (from x in db.prGetCliente(null).ToList() select x.vchNombre).ToList();
            //ViewBag.DsTipoCliente = (from x in db.prGetTipoCliente(null).ToList() select new SelectListItem { Text = x.vchDescripcion, Value = x.unqGenTipoClienteKey.ToString() }).ToList();
            ViewBag.DsTipoCliente = (from x in db.prGetTipoCliente(null).ToList()  select new { x.unqGenTipoClienteKey,x.vchDescripcion }).ToList();
            //ViewData["DsTipoCliente"] = (from x in db.prGetTipoCliente(null).ToList() select new { x.unqGenTipoClienteKey, x.vchDescripcion }).ToList();

        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View(GetCliente(null));
        }

        // GET: Cliente/Details/5
        public ActionResult Details(Guid id)
        {
            return View(GetCliente(id).First());
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            InitControls();
            return View();
        }


        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente Cliente, FormCollection TipoClienteC)
        {
            try
            {
                var ClienteKey = db.prMantenimientoCliente(Guid.Empty, Guid.Parse(TipoClienteC["cboTipoCliente"].ToString()), Cliente.Nombre,Cliente.ApellidoP,Cliente.ApellidoM,Cliente.RFC,Cliente.Calle,Cliente.NoExterior,Cliente.Colonia,Cliente.CodigoPostal,Cliente.Telefono,Cliente.Correo, 1).First();
                db.SetComentarioXML(ClienteKey, "Gen", "Cliente", Session["Usuario"].ToString(), "Agrego", "El usuario " + Session["Usuario"].ToString() + " agrego el registro " + Cliente.Nombre);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + Cliente.Nombre + " ha sido capturado correctamente!";
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

        // GET: Cliente/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            { 
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var dbCliente = db.prGetCliente(id);

            if (dbCliente == null)
            { 
            return HttpNotFound();
            }
            InitControls();

            var objCliente = dbCliente.First();

            Cliente C = new Cliente();
            C = GetCliente(id).First();
            return View(C);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Cliente Cliente, FormCollection TipoClienteC)
        {
            try
            {
                //Cliente.GenTipoClienteLink
                // TODO: Add update logic here
                db.prMantenimientoCliente(id, Guid.Parse(TipoClienteC["cboTipoCliente"].ToString()), Cliente.Nombre, Cliente.ApellidoP, Cliente.ApellidoM, Cliente.RFC, Cliente.Calle, Cliente.NoExterior, Cliente.Colonia, Cliente.CodigoPostal, Cliente.Telefono, Cliente.Correo, 2).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "Cliente", Session["Usuario"].ToString(), "Cambio", "El usuario " + Session["Usuario"].ToString() + " cambio el registro " + Cliente.Nombre);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + Cliente.Nombre + " ha sido modificado correctamente!";
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

        // GET: Cliente/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var dbCliente = db.prGetCliente(id);

            if (dbCliente == null)
                return HttpNotFound();

            var objCliente = dbCliente.First();

            Cliente C = new Cliente();
            C = GetCliente(id).First();
            return View(C);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid  id, Cliente Cliente)
        {
            try
            {

                // TODO: Add update logic here
                db.prMantenimientoCliente(id, Cliente.GenTipoClienteLink, Cliente.Nombre, Cliente.ApellidoP, Cliente.ApellidoM, Cliente.RFC, Cliente.Calle, Cliente.NoExterior, Cliente.Colonia, Cliente.CodigoPostal, Cliente.Telefono, Cliente.Correo, 3).First();
                db.SaveChanges();
                db.SetComentarioXML(id, "Gen", "Cliente", Session["Usuario"].ToString(), "Elimino", "El usuario " + Session["Usuario"].ToString() + " elimino el registro " + Cliente.Nombre);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + Cliente.Nombre + " ha sido eliminado correctamente!";
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
            var Comentarios = db.prGetComentarioXML(id.ToString(), "Gen", "Cliente").ToList().Select(x => new Comentario() { Accion = x.vchAccion, Coment = x.vchComentario, Fecha = x.dtmFecha, Usuario = x.vchUsuario }).ToList();

            return View(Comentarios);
        }


    }
}
