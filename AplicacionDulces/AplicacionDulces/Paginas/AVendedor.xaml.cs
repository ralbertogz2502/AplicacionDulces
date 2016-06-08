using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class AVendedor : ContentPage
    {
        private RestService<Vendedores> _serviciovendedor;
        public AVendedor()
        {
            InitializeComponent();
            _serviciovendedor = new RestService<Vendedores>("http://appdulceria-movil.somee.com/", "api/vendedors/");
        }

        private async void btnActualizar_Click(Object sender, EventArgs e)
        {
            lstVendedores.ItemsSource = await _serviciovendedor.ObtenerDatos();
        }
        private void lstVendedores_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ActVendedor(e.Item as Vendedores, _serviciovendedor));
        }
        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Vendedores vendedor = new Vendedores();
            if (string.IsNullOrEmpty(entryNombre.Text) || string.IsNullOrEmpty(entryPaterno.Text) || string.IsNullOrEmpty(entryMaterno.Text) || string.IsNullOrEmpty(entryTelefono.Text) ||  string.IsNullOrEmpty(entryUsuario.Text) || string.IsNullOrEmpty(entryContrasena.ToString()))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            try
            {
                vendedor.nombre = entryNombre.Text;
                vendedor.apellidop = entryPaterno.Text;
                vendedor.apellidom = entryMaterno.Text;
                vendedor.telefono = entryTelefono.Text;
                vendedor.usuario = entryUsuario.Text;
                vendedor.contrasena = entryContrasena.Text;
                var resultado = await _serviciovendedor.Guardar(vendedor);
                if (resultado != false)
                {
                    await DisplayAlert("Administración de Vendedores", "Vendedor Insertado", "Aceptar");
                    lstVendedores.ItemsSource = await _serviciovendedor.ObtenerDatos();
                }
                else
                {
                    await DisplayAlert("Administración de Vendedores", "Error del sistema", "Aceptar");
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error", "Por el momento el servicio no está en funcionamiento, intenta más tarde", "Aceptar");
            }
            entryNombre.Text = string.Empty;
            entryPaterno.Text = string.Empty;
            entryMaterno.Text = string.Empty;
            entryTelefono.Text = string.Empty;
            entryUsuario.Text = string.Empty;
            entryContrasena.Text = string.Empty;
        }
    }
}
