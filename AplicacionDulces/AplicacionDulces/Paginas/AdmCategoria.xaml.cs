using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class AdmCategoria : ContentPage
    {
        private RestService<Categorias> _serviciocategoria;
        public AdmCategoria()
        {
            InitializeComponent();
            _serviciocategoria = new RestService<Categorias>("http://appdulceria-movil.somee.com/", "api/categorias/");
        }
        private async void btnActualizar_Click(Object sender, EventArgs e)
        {
            lstCategorias.ItemsSource = await _serviciocategoria.ObtenerDatos();
        }
        private void lstCategorias_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ActCategoria(e.Item as Categorias, _serviciocategoria));
        }
        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            Categorias catego = new Categorias();
            if (string.IsNullOrEmpty(entryNombre.Text))
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                entryNombre.Focus();
                return;
            }
            catego.nombre = entryNombre.Text;
            var resultado = await _serviciocategoria.Guardar(catego);
            if (resultado != false)
            {

                await DisplayAlert("Administración de Categorías", "Categoría Insertada", "Aceptar");
                lstCategorias.ItemsSource = await _serviciocategoria.ObtenerDatos();
            }
            else
            {
                await DisplayAlert("Administración de Categorías", "Error del sistema", "Aceptar");
            }
            entryNombre.Text = string.Empty;
        }

    }
}
