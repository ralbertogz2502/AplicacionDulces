using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class Catalogo : ContentPage
    {
        public int idcatego = 0;
        private List<Categorias> categolst;
        private List<Producto> productolst;
        private List<Producto> productolst1;
        private RestService<Producto> _servicioproducto;
        private RestService<Categorias> _serviciocategoria;
        public Catalogo()
        {
            
            InitializeComponent();
            _servicioproducto = new RestService<Producto>("http://appdulceria-movil.somee.com/", "api/productos/");
            _serviciocategoria = new RestService<Categorias>("http://appdulceria-movil.somee.com/", "api/categorias/");
            CargarCategorias();
            categoria.SelectedIndexChanged += Categoria_SelectedIndexChanged;
            productop.SelectedIndexChanged += Productop_SelectedIndexChanged;
        }

        private async void Productop_SelectedIndexChanged(object sender, EventArgs e)
        {
            DescripcionP.Text = string.Empty;
            disponibles.Text = string.Empty;
            PrecioP.Text = string.Empty;
            try
            {
                productolst1 = await _servicioproducto.ObtenerDatos();
                var selected = productop.Items[productop.SelectedIndex];
                foreach (var item in productolst1)
                {
                    if (item.nombre == selected)
                    {
                        DescripcionP.Text = item.descripcion;
                        disponibles.Text = item.cantidad.ToString();
                        PrecioP.Text = item.precio.ToString();
                    }

                }
            }
            catch (Exception)
            {

                
            }

        }

        private async void Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productop.SelectedIndex != -1)
            {
                productop.Items.Clear();
                DescripcionP.Text = string.Empty;
                disponibles.Text = string.Empty;
                PrecioP.Text = string.Empty;
            }
            try
            {
                categolst = await _serviciocategoria.ObtenerDatos();
                int idcatego = this.categolst[categoria.SelectedIndex].id_categoria;
                productolst = await _servicioproducto.ObtenerDatos();
                foreach (var item in productolst)
                {
                    if (item.id_categoria == idcatego)
                    {
                        productop.Items.Add(item.nombre);
                    }
                    
                }
            }
            catch (Exception)
            {

                
            }
        }

        private async void CargarCategorias()
        {
            try
            {
                categolst = await _serviciocategoria.ObtenerDatos();
                foreach (var item in categolst)
                {
                    categoria.Items.Add(item.nombre);
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error", "Por el momento el servicio no está en funcionamiento, intenta más tarde", "Aceptar");
            }
        }
    }
}
