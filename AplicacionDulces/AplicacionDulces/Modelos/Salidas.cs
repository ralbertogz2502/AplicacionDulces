using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDulces.Modelos
{
    public class Salidas
    {
        public int id_compra { get; set; }
        public int id_cliente { get; set; }
        public int id_producto { get; set; }
        public DateTime fecha { get; set; }
        public int cantidad_prod { get; set; }
        public decimal monto_total { get; set; }
        public int id_pedido { get; set; }
        public override string ToString()
        {
            return string.Format("{0}.- {1} {2} {3}", id_compra, id_producto, cantidad_prod, monto_total);
        }
    }
}
