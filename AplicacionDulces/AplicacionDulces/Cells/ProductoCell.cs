using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AplicacionDulces.Cells
{
    class ProductoCell : ViewCell
    {
        public ProductoCell()
        {
            var lblidproducto = new Label
            {
                HorizontalOptions = LayoutOptions.End,
                FontSize = 15
            };
            lblidproducto.SetBinding(Label.TextProperty, new Binding("id_producto"));
            var lblnombre = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 15
            };
            lblnombre.SetBinding(Label.TextProperty, new Binding("nombre"));
            var lblprecio = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 15
            };
            lblprecio.SetBinding(Label.TextProperty, new Binding("precio"));
            var lblcantidad = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 10
            };
            lblcantidad.SetBinding(Label.TextProperty, new Binding("cantidad"));
            var lbl2 = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 10
            };
            
            var linea1 = new StackLayout
            {
                Children =
                {
                    lblidproducto, lblnombre,lblprecio
                },
                Orientation = StackOrientation.Horizontal
            };

            var linea2 = new StackLayout
            {
                Children =
                {
                    lbl2,lblcantidad
                },
                Orientation = StackOrientation.Horizontal
            };

            View = new StackLayout
            {
                Children =
                {
                    linea1,linea2
                }
            };
        }
    }
}
