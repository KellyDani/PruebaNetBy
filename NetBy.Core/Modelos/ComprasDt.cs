using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class ComprasDt
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public Compras Compra { get; set; }

        public int ProductoId { get; set; }
        public Productos Producto { get; set; }

        public decimal CostoUnitario { get; set; }
        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal CostoTotal { get; set; }

    }
}
