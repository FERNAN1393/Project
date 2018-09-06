using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGAIJE.Models
{
    public class MastCotizacion
    {
        public Guid CtzMastCotizacionKey { get; set; }
        public Guid GenClienteLink { get; set; }
        public string Cliente { get; set; }
        public Guid GenPlantillaLink { get; set; }
        public string Plantilla { get; set; }
        public string NoFolio { get; set; }
        public string Comentarios { get; set; }
        public DateTime ?Fecha { get; set; }
        public decimal ?ImporteTotal { get; set; }
        public Boolean Aceptada { get; set; }
        public Boolean IVA { get; set; }
        public List<DetCotizacion> DetalleCotizacion = new List<DetCotizacion>();

    }
}