using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class ActVendedor : ContentPage
    {
        private List<Admins> adminlst;
        private List<Vendedores> vendorslst;
        private RestService<Vendedores> _serviciovendedor;
        private RestService<Vendedores> _serviciovendedor1;
        private RestService<Admins> _servicioadmin;
        private Vendedores _vendedor;
        public ActVendedor(Vendedores v, RestService<Vendedores> serviciovendedor)
        {
            InitializeComponent();
            _vendedor = v;
            entryNombre.Text = _vendedor.nombre;
            entryPaterno.Text = _vendedor.apellidop;
            entryMaterno.Text = _vendedor.apellidom;
            entryTelefono.Text = _vendedor.telefono;
            entryUsuario.Text = _vendedor.usuario;
            entryContrasena.Text = _vendedor.contrasena;
            _serviciovendedor = serviciovendedor;
            _serviciovendedor1 = new RestService<Vendedores>("http://appdulceria-movil.somee.com/", "api/vendedors/");
            _servicioadmin = new RestService<Admins>("http://appdulceria-movil.somee.com/", "api/admins/");

        }
        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente deseas modificar el vendedor?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            if (string.IsNullOrEmpty(entryNombre.Text) || string.IsNullOrEmpty(entryPaterno.Text) || string.IsNullOrEmpty(entryMaterno.Text) || string.IsNullOrEmpty(entryTelefono.Text)  || string.IsNullOrEmpty(entryUsuario.Text) || string.IsNullOrEmpty(entryContrasena.ToString()))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            _vendedor.nombre = entryNombre.Text;
            _vendedor.apellidop = entryPaterno.Text;
            _vendedor.apellidom = entryMaterno.Text;
            _vendedor.telefono = entryTelefono.Text;
            _vendedor.usuario = entryUsuario.Text;
            _vendedor.contrasena = entryContrasena.Text;

            var r = await _serviciovendedor.Actualizar(_vendedor, _vendedor.id_vendedor.ToString());
            await DisplayAlert("Administración de Vendedores", "Vendedor Actualizado, ya puede volver al menú anterior", "Aceptar");
            await Navigation.PopAsync();
        }
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente desea eliminar el vendedor?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            try
            {
                adminlst = await _servicioadmin.ObtenerDatos();
                int adminid = 0;
                foreach (var item in adminlst)
                {
                    adminid = item.id_admin;
                    break;
                }
                if (_vendedor.id_vendedor == adminid)
                {
                    await DisplayAlert("Error", "El vendedor es un administrador, no se puede eliminar", "Aceptar");
                    await Navigation.PopAsync();

                }
                else
                {
                    var r = await _serviciovendedor.Eliminar(_vendedor, _vendedor.id_vendedor.ToString());
                    await DisplayAlert("Administración de Vendedores", "Vendedor Eliminado, ya puede volver al menú anterior", "Aceptar");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Administración de Vendedores", "El vendedor no se puede eliminar por cuestiones del sistema.", "Aceptar");
            }
        }
    }
}
