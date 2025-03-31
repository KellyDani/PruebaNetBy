using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class Ventas
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Estado { get; set; }

        [MaxLength(200)]
        public string Detalle { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEdicion { get; set; }
        public bool Anulado { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public List<VentasDt> Detalles { get; set; }
    }
}
