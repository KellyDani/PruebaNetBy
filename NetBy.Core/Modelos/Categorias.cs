using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class Categorias
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
        public bool Anulado { get; set; }
        public DateTime FechaUltimaEdicion { get; set; }
    }
}
