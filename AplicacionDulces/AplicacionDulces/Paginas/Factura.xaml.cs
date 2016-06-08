using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class Factura : ContentPage
    {
        public Factura()
        {
            InitializeComponent();
            Pedido.Text = Compras.idpedidocurrent.ToString();
            Nombre.Text = LogCliente.current_cliente;
            Monto.Text = Compras.montototalg.ToString();
        }
    }
}
