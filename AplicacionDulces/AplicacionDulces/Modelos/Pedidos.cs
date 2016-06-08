using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDulces.Modelos
{
    public class Pedidos
    {
        public int id_pedido { get; set; }
        public string direccion { get; set; }
        public int status { get; set; }
        public override string ToString()
        {
            string entrega = "";
            if (status == 0)
            {
                entrega = "No entregado";
            }
            else
            {
                entrega = "Entregado";
            }
            return string.Format("Pedido No.:{0} Dir.:{1} Stat:{2}", id_pedido, direccion, entrega);
        }
    }
}
