using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class BVendedor : ContentPage
    {
        public BVendedor()
        {
            InitializeComponent();
        }
        private async void btnClientes_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AClientes());
        }
        private async void btnProductos_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }
        private async void btnPedidos_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VendedorPedido());
        }
    }
}
