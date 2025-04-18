﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos
{
    public class VentasDt
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public Ventas Venta { get; set; }

        public int ProductoId { get; set; }
        public Productos Producto { get; set; }

        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
