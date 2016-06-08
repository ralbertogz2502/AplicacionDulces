using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class AClientes : ContentPage
    {
        
        private RestService<Clientes> _serviciocliente;
        public AClientes()
        {
            InitializeComponent();
            _serviciocliente = new RestService<Clientes>("http://appdulceria-movil.somee.com/", "api/clientes/");
        }
        private async void btnActualizar_Click(Object sender, EventArgs e)
        {
            lstClientes.ItemsSource = await _serviciocliente.ObtenerDatos();
        }
        private void lstClientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ActClientes(e.Item as Clientes, _serviciocliente));
        }
        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Clientes cliente = new Clientes();
            if (string.IsNullOrEmpty(entryNombre.Text) || string.IsNullOrEmpty(entryPaterno.Text) || string.IsNullOrEmpty(entryMaterno.Text) || string.IsNullOrEmpty(entryTelefono.Text) || string.IsNullOrEmpty(entryCorreo.ToString()) || string.IsNullOrEmpty(entryUsuario.Text) || string.IsNullOrEmpty(entryContrasena.ToString()))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            try
            {
                cliente.nombre = entryNombre.Text;
                cliente.apellidop = entryPaterno.Text;
                cliente.apellidom = entryMaterno.Text;
                cliente.telefono = entryTelefono.Text;
                cliente.correo = entryCorreo.Text;
                cliente.usuario = entryUsuario.Text;
                cliente.contrasena = entryContrasena.Text;

                var resultado = await _serviciocliente.Guardar(cliente);
                if (resultado != false)
                {
                    await DisplayAlert("Administración de Clientes", "Cliente Insertado", "Aceptar");
                    lstClientes.ItemsSource = await _serviciocliente.ObtenerDatos();
                }
                else
                {
                    await DisplayAlert("Administración de Clientes", "Error del sistema", "Aceptar");
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error", "Por el momento el servicio no está en funcionamiento, intenta más tarde", "Aceptar");
            }
            entryNombre.Text = string.Empty;
            entryPaterno.Text = string.Empty;
            entryMaterno.Text = string.Empty;
            entryCorreo.Text = string.Empty;
            entryTelefono.Text = string.Empty;
            entryUsuario.Text = string.Empty;
            entryContrasena.Text = string.Empty;
        }
    }
}
