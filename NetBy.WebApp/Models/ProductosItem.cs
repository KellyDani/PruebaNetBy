using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetBy.WebApp.Models
{
    public class ProductosItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Anulado { get; set; }
        public string Estado { get; set; }
        public int CategoriaId { get; set; }
        public string RutaImagen { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal CostoUltimaCompra { get; set; }

        public ProductosItem Clone()
        {
            return new ProductosItem
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                Anulado = this.Anulado,
                Estado = this.Estado,
                CategoriaId = this.CategoriaId,
                CostoUltimaCompra = this.CostoUltimaCompra,
                PrecioVenta = this.PrecioVenta,
                RutaImagen = this.RutaImagen
            };
        }
    }
}
