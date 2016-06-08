using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class BAdmin : ContentPage
    {
        public BAdmin()
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
        private async void btnCategorias_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdmCategoria());
        }
        private async void btnPedidos_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdmPedidos());
        }
        private async void btnVendedores_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AVendedor());
        }
    }
}
