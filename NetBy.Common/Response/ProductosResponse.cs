using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common.Response
{
    public class ProductosResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }     
        public string RutaImagen { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal CostoUltimaCompra { get; set; }
        public bool Anulado { get; set; }
    }
}
