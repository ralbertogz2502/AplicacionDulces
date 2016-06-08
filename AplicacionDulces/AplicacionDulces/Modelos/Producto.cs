using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace AplicacionDulces.Modelos
{
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public int id_categoria { get; set; }
        public override string ToString()
        {
            return string.Format("{0}.- {1} Prec.U.:${2} Disp:{3}", id_producto, nombre, precio, cantidad);
        }
    }
}
