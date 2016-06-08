using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDulces.Modelos
{
    public class Vendedores
    {
        public int id_vendedor { get; set; }
        public string nombre { get; set; }
        public string apellidop { get; set; }
        public string apellidom { get; set; }
        public string telefono { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public override string ToString()
        {
            return string.Format("{0}.- {1} {2} {3}", id_vendedor, nombre, apellidop, telefono);
        }
    }
}
