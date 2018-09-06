using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGAIJE.Models
{
    public class DetCotizacion
    {
       public Guid CtzDetalleCotizacionKey { get; set; }
       public Guid CtzMastCotizacionLink { get; set; }
       public Guid GenArticuloLink { get; set; }
       public string Articulo { get; set; }
       public int Cantidad { get; set; }
       public decimal ValorUnitario { get; set; }
       public decimal Descuento { get; set; }
       public decimal ?MontoTotal { get; set; }
       public string Descripcion { get; set; }
       public decimal Total { get; set; }
       public string Codigo { get; set; }
    }
}