using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common.Request
{
    public class ComprasRequest
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Detalle { get; set; }
        public List<ComprasDtRequest> Detalles { get; set; }
    }
}
