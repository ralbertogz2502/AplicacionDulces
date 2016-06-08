using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class BCliente : ContentPage
    {
        public BCliente()
        {
            InitializeComponent();
        }
        private async void btnCatalogo_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Catalogo());
        }
        private async void btnCompra_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Compras());
        }
    }
}
