using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class ActCategoria : ContentPage
    {
        RestService<Categorias> _serviciocategoria;
        private Categorias _catego;
        public ActCategoria(Categorias c, RestService<Categorias> serviciocategoria)
        {
            InitializeComponent();
            _catego = c;
            entryNombre.Text = _catego.nombre;
            _serviciocategoria = serviciocategoria;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente deseas modificar la categoría?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            if (string.IsNullOrEmpty(entryNombre.Text))
            {
                await DisplayAlert("Error", "Falta el nombre", "Aceptar");
                entryNombre.Focus();
                return;
            }
            _catego.nombre = entryNombre.Text;
            var r = await _serviciocategoria.Actualizar(_catego, _catego.id_categoria.ToString());
            await DisplayAlert("Administración de Categorías", "Categoría Actualizada, ya puede volver al menú anterior", "Aceptar");
            await Navigation.PopAsync();
        }


    }
}
