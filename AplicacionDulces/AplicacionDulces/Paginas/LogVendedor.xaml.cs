using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class LogVendedor : ContentPage
    {
        public string usuario = "";
        private RestService<Vendedores> _serviciovendedor;
        private List<Vendedores> vendedoreslst;
        public LogVendedor()
        {
            InitializeComponent();
            _serviciovendedor = new RestService<Vendedores>("http://appdulceria-movil.somee.com/", "api/vendedors/");
        }
        private async void btnLogin_Clicked(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UVendedor.Text) || string.IsNullOrEmpty(CVendedor.Text))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                UVendedor.Focus();
                return;
            }
            try
            {
                waitActivityIndicator.IsRunning = true;
                btnLoginVendedor.IsEnabled = false;
                vendedoreslst = await _serviciovendedor.ObtenerDatos();
                bool logincorrecto = false;
                foreach (var item in vendedoreslst)
                {
                    if (item.usuario == UVendedor.Text && item.contrasena == CVendedor.Text)
                    {
                        usuario = item.nombre;
                        logincorrecto = true;
                        break;
                    }
                }
                if (logincorrecto == true)
                {
                    await DisplayAlert("Conexión Exitosa", "Bienvenido Vendedor " + usuario, "Aceptar");
                    waitActivityIndicator.IsRunning = false;
                    btnLoginVendedor.IsEnabled = true;
                    await Navigation.PushAsync(new BVendedor());
                }
                else
                {
                    await DisplayAlert("Error de Autenticación", "Los datos ingresados no son correctos o no coinciden", "Aceptar");
                    waitActivityIndicator.IsRunning = false;
                    btnLoginVendedor.IsEnabled = true;
                    return;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error de la red", "No hay conexión con el sistema", "Aceptar");
                throw;
            }
        }
    }
}
