using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class Productos
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }
        public Categorias Categoria { get; set; }

        [MaxLength(200)]
        public string RutaImagen { get; set; }


        [Column(TypeName = "decimal(10,6)")]
        public decimal PrecioVenta { get; set; }


        [Column(TypeName = "decimal(10,6)")]
        public decimal CostoUltimaCompra { get; set; }

        public DateTime FechaCreacion { get; set; }
        public bool Anulado { get; set; }
        public DateTime FechaUltimaEdicion { get; set; }

    }
}
