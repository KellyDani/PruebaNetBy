using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Productos Producto { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal UnidadesStock { get; set; }
        public DateTime UltimaEdicion { get; set; }
    }
}
