using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGAIJE.Models
{
    public class CatalogoArticulo
    {
        public Guid GenArticuloKey { get; set; }
        public Guid? GenTipoArticuloLink { get; set; }
        public string TipoArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal? Descuento { get; set; }
        public int? Existencia { get; set; }
        public int? CantidadMaxima { get; set; }
        public int? CantidadMinima { get; set; }
    }
}