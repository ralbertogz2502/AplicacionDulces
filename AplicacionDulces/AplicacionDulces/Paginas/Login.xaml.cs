using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnClientes_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogCliente());
        }
        private async void btnVendedores_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogVendedor());
        }
        private async void btnAdministrador_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogAdmin());
        }
    }
}
