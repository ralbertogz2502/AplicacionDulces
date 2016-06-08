using AplicacionDulces.Cells;
using AplicacionDulces.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class Productos : ContentPage
    {
        private List<Categorias> categolst;
        private RestService<Producto> _servicioproducto;
        private RestService<Categorias> _serviciocategoria;

        public Productos()
        {
            InitializeComponent();
            _servicioproducto = new RestService<Producto>("http://appdulceria-movil.somee.com/", "api/productos/");
            _serviciocategoria = new RestService<Categorias>("http://appdulceria-movil.somee.com/", "api/categorias/");
            CargarCategorias();
        }
        private async void btnActualizar_Click(Object sender, EventArgs e)
        {
            lstProductos.ItemsSource = await _servicioproducto.ObtenerDatos();
        }
        private void lstProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ActualizarProd(e.Item as Producto, _servicioproducto));
        }
        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Producto producto = new Producto();
            if (string.IsNullOrEmpty(entryNombre.Text) || string.IsNullOrEmpty(entryCantidad.Text) || string.IsNullOrEmpty(entryDescripcion.Text) || string.IsNullOrEmpty(entryPrecio.Text) || string.IsNullOrEmpty(entryCategoria.ToString()))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            try
            {
                categolst = await _serviciocategoria.ObtenerDatos();
                int idigual = this.categolst[entryCategoria.SelectedIndex].id_categoria;
                producto.nombre = entryNombre.Text;
                producto.precio = Convert.ToDecimal(entryPrecio.Text);
                producto.descripcion = entryDescripcion.Text;
                producto.cantidad = Convert.ToInt32(entryCantidad.Text);
                producto.id_categoria = idigual;
                
                var resultado = await _servicioproducto.Guardar(producto);
                if (resultado != false)
                {
                    await DisplayAlert("Administración de Productos", "Producto Insertado", "Aceptar");
                    lstProductos.ItemsSource = await _servicioproducto.ObtenerDatos();
                }
                else
                {
                    await DisplayAlert("Administración de Productos", "Error del sistema", "Aceptar");
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error", "Por el momento el servicio no está en funcionamiento, intenta más tarde", "Aceptar");
            }
            entryNombre.Text = string.Empty;
            entryCantidad.Text = string.Empty;
            entryPrecio.Text = string.Empty;
            entryDescripcion.Text = string.Empty;
            

        }

        private async void CargarCategorias()
        {
            try
            {
                var categorias = await _serviciocategoria.ObtenerDatos();
                foreach (var item in categorias)
                {
                    entryCategoria.Items.Add(item.nombre);
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error","Por el momento el servicio no está en funcionamiento, intenta más tarde","Aceptar");
            }
        }
    }
}

