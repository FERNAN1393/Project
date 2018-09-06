using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGAIJE.Models
{
    public class TipoCliente
    {
        public Guid GenTipoClienteKey { get; set; }
        public String Descripcion { get; set; }
        public Decimal? Descuento { get; set; }
        public ICollection<Cliente> Cliente { get; set; }

    }
}