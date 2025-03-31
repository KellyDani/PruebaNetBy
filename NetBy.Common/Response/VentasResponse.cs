using NetBy.Common.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common.Response
{
    public class VentasResponse
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Anulado { get; set; }
        public List<VentasDtResponse> Detalles { get; set; }
    }
}
