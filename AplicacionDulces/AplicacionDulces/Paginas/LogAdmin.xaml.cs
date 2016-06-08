using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class LogAdmin : ContentPage
    {
        public string usuario = "";
        public int id_usuario_current = 0;
        private RestService<Vendedores> _serviciovendedor;
        private RestService<Admins> _servicioadmin;
        private List<Vendedores> vendedoreslst;
        private List<Admins> adminslst;
        public LogAdmin()
        {
            InitializeComponent();
            _serviciovendedor = new RestService<Vendedores>("http://appdulceria-movil.somee.com/", "api/vendedors/");
            _servicioadmin = new RestService<Admins>("http://appdulceria-movil.somee.com/", "api/admins/");
        }
        private async void btnAdmin_Clicked(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UAdmin.Text) || string.IsNullOrEmpty(CAdmin.Text))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                UAdmin.Focus();
                return;
            }
            try
            {
                waitActivityIndicator.IsRunning = true;
                btnLoginAdmin.IsEnabled = false;
                adminslst = await _servicioadmin.ObtenerDatos();
                vendedoreslst = await _serviciovendedor.ObtenerDatos();
                bool logincorrecto = false;
                int idadmin = 0;
                int idvend = 0;
                foreach (var item in adminslst)
                {
                    idadmin = item.id_vendedor;
                    break;
                }

                foreach (var item in vendedoreslst)
                {
                    if (item.usuario == UAdmin.Text && item.contrasena == CAdmin.Text)
                    {
                        idvend = item.id_vendedor;
                        if (idadmin == idvend)
                        {
                            usuario = item.nombre;
                            id_usuario_current = item.id_vendedor;
                            logincorrecto = true;
                            break;
                        }
                    }
                }
                if (logincorrecto == true)
                {
                    await DisplayAlert("Conexión Exitosa", "Bienvenido Administrador " + usuario, "Aceptar");
                    waitActivityIndicator.IsRunning = false;
                    btnLoginAdmin.IsEnabled = true;
                    await Navigation.PushAsync(new BAdmin());
                }
                else
                {
                    await DisplayAlert("Error de Autenticación", "Los datos ingresados no son correctos o no coinciden", "Aceptar");
                    waitActivityIndicator.IsRunning = false;
                    btnLoginAdmin.IsEnabled = true;
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
