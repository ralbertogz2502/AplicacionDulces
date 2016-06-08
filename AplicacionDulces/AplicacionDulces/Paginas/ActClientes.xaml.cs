using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class ActClientes : ContentPage
    {
        private RestService<Clientes> _serviciocliente;
        private Clientes _cliente;
        public ActClientes(Clientes c,RestService<Clientes> serviciocliente)
        {
            InitializeComponent();
            _cliente = c;
            entryNombre.Text = _cliente.nombre;
            entryPaterno.Text = _cliente.apellidop;
            entryMaterno.Text = _cliente.apellidom;
            entryCorreo.Text = _cliente.correo;
            entryTelefono.Text = _cliente.telefono;
            entryUsuario.Text = _cliente.usuario;
            entryContrasena.Text = _cliente.contrasena;
            _serviciocliente = serviciocliente;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente deseas modificar el cliente?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            if (string.IsNullOrEmpty(entryNombre.Text) || string.IsNullOrEmpty(entryPaterno.Text) || string.IsNullOrEmpty(entryMaterno.Text) || string.IsNullOrEmpty(entryTelefono.Text) || string.IsNullOrEmpty(entryCorreo.ToString()) || string.IsNullOrEmpty(entryUsuario.Text) || string.IsNullOrEmpty(entryContrasena.ToString()))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            _cliente.nombre = entryNombre.Text;
            _cliente.apellidop = entryPaterno.Text;
            _cliente.apellidom = entryMaterno.Text;
            _cliente.correo = entryCorreo.Text;
            _cliente.telefono = entryTelefono.Text;
            _cliente.usuario = entryUsuario.Text;
            _cliente.contrasena = entryContrasena.Text;

            var r = await _serviciocliente.Actualizar(_cliente, _cliente.id_cliente.ToString());
            await DisplayAlert("Administración de Clientes", "Cliente Actualizado, ya puede volver al menú anterior", "Aceptar");
            await Navigation.PopAsync();
        }
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente desea eliminar el cliente?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            try
            {
                var r = await _serviciocliente.Eliminar(_cliente, _cliente.id_cliente.ToString());
                await DisplayAlert("Administración de Clientes", "Cliente Eliminado, ya puede volver al menú anterior", "Aceptar");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {

                await DisplayAlert("Administración de Clientes", "El cliente no se puede eliminar por cuestiones del sistema.", "Aceptar");
            }
        }
    }
}
