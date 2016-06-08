using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    
    public partial class ActualizarProd : ContentPage
    {
        private List<Categorias> categolst;
        private RestService<Producto> _servicioproducto;
        private RestService<Categorias> _serviciocategoria;
        private Producto _producto;
        public ActualizarProd(Producto p, RestService<Producto> servicioproducto)
        {
            InitializeComponent();
            _producto = p;
            entryNombre.Text = _producto.nombre;
            entryPrecio.Text = _producto.precio.ToString();
            entryCantidad.Text = _producto.cantidad.ToString();
            entryDescripcion.Text = _producto.descripcion;
            _serviciocategoria = new RestService<Categorias>("http://appdulceria-movil.somee.com/", "api/categorias/");
            _servicioproducto = servicioproducto;
            CargarCategorias();
        }
        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente deseas modificar el producto?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            if (string.IsNullOrEmpty(entryNombre.Text) || string.IsNullOrEmpty(entryCantidad.Text) || string.IsNullOrEmpty(entryDescripcion.Text) || string.IsNullOrEmpty(entryPrecio.Text) || string.IsNullOrEmpty(entryCategoria.ToString()))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            categolst = await _serviciocategoria.ObtenerDatos();
            int idigual = this.categolst[entryCategoria.SelectedIndex].id_categoria;

            _producto.nombre = entryNombre.Text;
            _producto.precio = Convert.ToDecimal(entryPrecio.Text);
            _producto.descripcion = entryDescripcion.Text;
            _producto.cantidad = Convert.ToInt32(entryCantidad.Text);
            _producto.id_categoria = idigual;
            var r = await _servicioproducto.Actualizar(_producto, _producto.id_producto.ToString());
            await DisplayAlert("Administración de Productos", "Producto Actualizado, ya puede volver al menú anterior", "Aceptar");
            await Navigation.PopAsync();
        }
        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente desea eliminar el producto?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            try
            {
                var r = await _servicioproducto.Eliminar(_producto, _producto.id_producto.ToString());
                await DisplayAlert("Administración de Productos", "Producto Eliminado, ya puede volver al menú anterior", "Aceptar");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {

                await DisplayAlert("Administración de Productos", "El producto se está utilizando, no se puede eliminar", "Aceptar");
            }
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

                await DisplayAlert("Error", "Por el momento el servicio no está en funcionamiento, intenta más tarde", "Aceptar");
            }
        }
    }
}
