using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace AplicacionDulces.Modelos
{
    public class Categorias
    {
        public int id_categoria { get; set; }
        public string nombre { get; set; }
        public override string ToString()
        {
            return string.Format("{0}.- {1}", id_categoria, nombre);
        }
    }
}
