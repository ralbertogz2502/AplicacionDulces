using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class LogCliente : ContentPage
    {
        public static string current_cliente = "";
        private RestService<Clientes> _serviciocliente;
        private List<Clientes> clienteslst;
        public LogCliente()
        {
            InitializeComponent();
            _serviciocliente = new RestService<Clientes>("http://appdulceria-movil.somee.com/", "api/clientes/");
        }
        private async void btnLogin_Clicked(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UCliente.Text) || string.IsNullOrEmpty(CCliente.Text))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                UCliente.Focus();
                return;
            }
            try
            {
                waitActivityIndicator.IsRunning = true;
                btnLoginCliente.IsEnabled = false;
                clienteslst = await _serviciocliente.ObtenerDatos();
                bool logincorrecto = false;
                string usuarionom="";
                foreach (var item in clienteslst)
                {
                    if (item.usuario == UCliente.Text && item.contrasena == CCliente.Text)
                    {
                        current_cliente = item.usuario;
                        usuarionom = item.nombre;
                        logincorrecto = true;
                        break;
                    }
                }
                if (logincorrecto == true)
                {
                    await DisplayAlert("Conexión Exitosa", "Bienvenido Cliente " + usuarionom, "Aceptar");
                    waitActivityIndicator.IsRunning = false;
                    btnLoginCliente.IsEnabled = true;
                    await Navigation.PushAsync(new BCliente());
                }
                else
                {
                    await DisplayAlert("Error de Autenticación", "Los datos ingresados no son correctos o no coinciden", "Aceptar");
                    waitActivityIndicator.IsRunning = false;
                    btnLoginCliente.IsEnabled = true;
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
