using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common.Response
{
    public class StockResponse
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }

        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }

        public decimal UnidadesStock { get; set; }
        public DateTime UltimaEdicion { get; set; }
    }
}
