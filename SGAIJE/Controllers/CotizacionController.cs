using CrystalDecisions.CrystalReports.Engine;
using SGAIJE.DataContext;
using SGAIJE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGAIJE.Controllers
{
    public class CotizacionController : Controller
    {
        SGAIJEEntities db = new SGAIJEEntities();

        public List<MastCotizacion> GetCotizacion(Guid? Id)
        {
            List<MastCotizacion> Cotizacion = new List<MastCotizacion>();
            Cotizacion = db.prGetMastCotizacion(Id).ToList().Select(x => new MastCotizacion()
            {
                CtzMastCotizacionKey = x.unqCtzMastCotizacionKey,
                GenClienteLink = x.unqGenClienteLink,
                Cliente = db.prGetCliente(x.unqGenClienteLink).First().vchNombre,
                GenPlantillaLink = x.unqGenPlantillaLink,
                Plantilla = db.prGetPlantilla(x.unqGenPlantillaLink).First().vchDescripcion,
                NoFolio = x.NoFolio,
                Comentarios = x.vchComentarios,
                Fecha = x.dtmFecha,
                ImporteTotal = x.decImporteTotal,
                Aceptada = Convert.ToBoolean(x.bitAceptada),
                IVA = Convert.ToBoolean(x.bitIVA),
                DetalleCotizacion = db.prGetDetalleCotizacion(null, Id).ToList().Select(o => new DetCotizacion { CtzDetalleCotizacionKey = o.unqCtzDetalleCotizacionKey, Articulo = db.prGetArticulo(o.unqGenArticuloLink).First().vchDescripcion, Codigo = db.prGetArticulo(o.unqGenArticuloLink).First().vchCodigoArticulo, Cantidad = Convert.ToInt32(o.intCantidad), GenArticuloLink = o.unqGenArticuloLink, Total = Convert.ToDecimal(o.decMontoTotal), Descuento = Convert.ToDecimal(o.decDescuento), ValorUnitario = Convert.ToDecimal(o.decValorUnitario), MontoTotal = Convert.ToDecimal(o.decMontoTotal), CtzMastCotizacionLink = o.unqCtzMastCotizacionLink }).ToList()
            }).ToList();
            return Cotizacion;
        }

      



        private void InitControls()
        {
            ViewBag.DsCotizacion = (from x in db.prGetMastCotizacion(null).ToList() select new { x.unqCtzMastCotizacionKey, x.unqGenClienteLink, x.unqGenPlantillaLink, x.vchComentarios, x.NoFolio, x.decImporteTotal, x.bitIVA, x.bitAceptada, x.dtmFecha }).ToList();
            ViewBag.DsCliente = (from x in db.prGetCliente(null).ToList() select new { x.unqGenClienteKey, x.vchNombre, x.unqGenTipoClienteLink, x.vchApellidoP, x.vchApellidoM, x.vchCalle, x.vchCodigoPostal, x.vchColonia, x.vchCorreo, x.vchTelefono, x.vchRFC }).ToList();
            ViewBag.DsPlantilla = (from x in db.prGetPlantilla(null).ToList() select new { x.unqGenPlantillaKey, x.vchDescripcion, x.vchPathPlantilla }).ToList();

        }

        // GET: Cotizacion
        public ActionResult Index()
        {
            return View(GetCotizacion(null));
        }

        // GET: Cotizacion/Details/5
        public ActionResult Details(Guid id)
        {
            return View(GetCotizacion(id).First());
        }

        // GET: Cotizacion/Create
        public ActionResult Create()
        {
            Session["MontoTotal"] = null;
            Session["DetalleCotizacion"] = null;
            InitControls();
            MastCotizacion FolioCotizacion = new MastCotizacion()
            {
                NoFolio = db.GetLastFolio(null).First()
            };

            return View(FolioCotizacion);
        }

        // POST: Cotizacion/Create
        [HttpPost]
        public ActionResult Create(FormCollection Cotizacion, MastCotizacion CatalogoMCotizacion)
        {
            List<DetCotizacion> ListCotizacion = new List<DetCotizacion>();
            ListCotizacion = (List<DetCotizacion>)Convert.ChangeType(Session["DetalleCotizacion"], typeof(List<DetCotizacion>));
            
            //            ListCotizacion = CatalogoMCotizacion.DetalleCotizacion.ToList().Select(x => new DetCotizacion { Cantidad = x.Cantidad, GenArticuloLink = x.GenArticuloLink, MontoTotal = x.MontoTotal, Total = x.Total }).ToList();
            try
            {
                var CotizacionKey = db.prMantenimientoMastCotizacion(Guid.Empty,
                                                                     Guid.Parse(Cotizacion["cboCliente"].ToString()),
                                                                     Guid.Parse(Cotizacion["cboPlantilla"].ToString()),
                                                                     CatalogoMCotizacion.Comentarios,
                                                                     CatalogoMCotizacion.Fecha,
                                                                     null,
                                                                     CatalogoMCotizacion.ImporteTotal,
                                                                     Cotizacion["Aceptada"].ToString() == "false" ? false :true,
                                                                     CatalogoMCotizacion.IVA = false ? false : true,
                                                                     1).First();

                foreach (var articulos in ListCotizacion)
                {
                    var DetCotizacionKey = db.prMantenimientoDetalleCotizacion(Guid.Empty,
                                                                               CotizacionKey,
                                                                               articulos.GenArticuloLink,
                                                                               articulos.Cantidad,
                                                                               articulos.Total,
                                                                               articulos.Descuento,
                                                                               articulos.MontoTotal,
                                                                               1).First();
                }

      
                


                db.SetComentarioXML(CotizacionKey, "CTZ", "MastCotizacion", Session["Usuario"].ToString(), "Agrego", "El usuario " + Session["Usuario"].ToString() + " agrego el registro " + CatalogoMCotizacion.Comentarios);
                Session["Message"] = "Registro Correcto";
                Session["MessageDatos"] = "El Registro  " + CatalogoMCotizacion.Comentarios + " ha sido capturado correctamente!";
                Session["MessageTipo"] = "success";



                // TODO: Add insert logic here

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

        // GET: Cotizacion/Edit/5
        public ActionResult Edit(Guid id)
        {
            Session["DetalleCotizacion"] = null;
            InitControls();

            MastCotizacion Cotizacion = new MastCotizacion();

            Cotizacion = GetCotizacion(id).First();
            Session["DetalleCotizacion"] = Cotizacion.DetalleCotizacion;

            return View(Cotizacion);

            
        }

        // POST: Cotizacion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cotizacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cotizacion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        [Route("CreateAsync")]
        public ActionResult CreateAsync()
        {
            InitControls();
            return View();

        }

        [HttpPost]
        [Route("GetOrdenDetalleOS")]
        public ActionResult GetOrdenDetalleOS(DetCotizacion detalleCotizacion)
        {

            List<DetCotizacion> ListCotizacion = new List<DetCotizacion>();

            if (Session["DetalleCotizacion"] == null)

            {
                Session["DetalleCotizacion"] = new List<DetCotizacion>();

            }

            ListCotizacion = (List<DetCotizacion>)Convert.ChangeType(Session["DetalleCotizacion"], typeof(List<DetCotizacion>));
            //ListCotizacion = (List<ListCotizacion>)Session["DetalleOS"];
            if (ModelState.IsValid)
            {
                var Articulo = db.prGetArticulo(detalleCotizacion.GenArticuloLink).First();
                Session["Articulo"] = detalleCotizacion.GenArticuloLink;
                if (ListCotizacion.Count == 0)
                {
                    ListCotizacion.Add(detalleCotizacion);

                    ListCotizacion = ListCotizacion.Select(x => new DetCotizacion
                    {
                        Descripcion = x.Descripcion,
                        GenArticuloLink = x.GenArticuloLink,
                        Cantidad = x.Cantidad,
                        Total = x.Total,
                        Codigo = x.Codigo,
                        Descuento = x.Descuento,
                        MontoTotal = x.Descuento == 0? (x.Total * x.Cantidad): x.Total - ((x.Cantidad * x.Total) * x.Descuento)/100
                    }).ToList();

                }

                else
                {
                    //aumentamos la cantidad si existe el el articulo en la lista
                    if (ListCotizacion.Where(c => c.GenArticuloLink == detalleCotizacion.GenArticuloLink).ToList().Count > 0)
                    {
                        ListCotizacion.Where(c => c.GenArticuloLink == detalleCotizacion.GenArticuloLink).First().Cantidad += detalleCotizacion.Cantidad;

                        //OrdenServicio.Where(Function(c) c.InvArticuloLinkLink = DetalleOrdenServicio.InvArticuloLinkLink).First.Cantidad += DetalleOrdenServicio.Cantidad

                    }
                    else
                    {
                        //ListCotizacion.AddRange(ListCotizacion);
                        detalleCotizacion.Codigo = Articulo.vchCodigoArticulo;
                        detalleCotizacion.Descripcion = Articulo.vchDescripcion;
                        detalleCotizacion.Cantidad = detalleCotizacion.Cantidad;
                        detalleCotizacion.Total = Articulo.decPrecio;
                        detalleCotizacion.Descuento = Convert.ToDecimal(Articulo.decDescuento);
                        detalleCotizacion.MontoTotal = Articulo.decPrecio - ((detalleCotizacion.Cantidad * Articulo.decPrecio) * Articulo.decDescuento) / 100;

                        ListCotizacion.Add(detalleCotizacion);
                    }
                }
            }


            ListCotizacion = ListCotizacion.Select(x => new DetCotizacion
            {
                Descripcion = x.Descripcion,
                GenArticuloLink = x.GenArticuloLink,
                Cantidad = x.Cantidad,
                Total = x.Total,
                Codigo = x.Codigo,
                Descuento = x.Descuento,
                MontoTotal = x.Descuento == 0 ? (x.Total * x.Cantidad) : x.Total - ((x.Cantidad * x.Total) * x.Descuento) / 100
            }).ToList();

            Session["DetalleCotizacion"] = ListCotizacion;
            Session["MontoTotal"] = detalleCotizacion.MontoTotal;



            return Json(ListCotizacion, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [Route("DeleteAsync")]
        public ActionResult DeleteAsync(Guid id)
        {
            List<DetCotizacion> ListCotizacion = new List<DetCotizacion>();
            ListCotizacion = (List<DetCotizacion>)Convert.ChangeType(Session["DetalleCotizacion"], typeof(List<DetCotizacion>));
            ListCotizacion = ListCotizacion.Where(x => x.GenArticuloLink != id).ToList();
            Session["DetalleCotizacion"] = ListCotizacion;
            return Json(ListCotizacion, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExportarPlantilla()
        {
            string key = "7984D80F-37AB-E811-BFDC-1867B0A5505B";
            List<MastCotizacion> allCustomer = new List<MastCotizacion>();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("/Reportes"), "Plantilla_1.rpt"));
            //rd.SetDataSource();
            rd.SetParameterValue("@unqCtzMastCotizacionKey", key);
            rd.SetParameterValue("@unqCtzDetalleCotizacionKey", null);
            rd.SetParameterValue("@unqCtzMastCotizacionLink", key);
            rd.SetParameterValue("@unqGenClienteKey", null);
            rd.SetParameterValue("@unqGenArticuloKey", null);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Plantilla.pdf");
        }

    }

}
