using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class Kardex
    {
        public int Id { get; set; }
        public int TransaccionId { get; set; }
        public bool Ingreso { get; set; }

        public int ProductoId { get; set; }
        public Productos Producto { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal PrecioCosto { get; set; }//Si es compra costo, si es venta precio
        public DateTime FechaEliminacion { get; set; }
        public bool Anulado { get; set; }
    }
}
