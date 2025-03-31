using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common.Response
{
    public class ComprasDtResponse
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
        public decimal CostoUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoTotal { get; set; }
    }
}
