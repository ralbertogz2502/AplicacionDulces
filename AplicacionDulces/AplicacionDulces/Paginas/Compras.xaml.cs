using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionDulces.Paginas;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class Compras : ContentPage
    {
        private List<Categorias> categolst;
        private List<Producto> productolst;
        private List<Producto> productolst1;
        private List<Pedidos> pedidolst;
        private List<Clientes> clientelst;
        private RestService<Producto> _servicioproducto;
        private RestService<Categorias> _serviciocategoria;
        private RestService<Pedidos> _serviciopedido;
        private RestService<Salidas> _serviciosalida;
        private RestService<Clientes> _serviciocliente;
        RestService<Producto> servicioproducto;
        private RestService<Producto> _servproducto;
        public static decimal montototalg = 0;
        Producto prod = new Producto();

        Pedidos pedido = new Pedidos();
        Salidas salida = new Salidas();
        int contador_pedido_in = 0;
        public static int idpedidocurrent = 0;
        public Compras()
        {
            InitializeComponent();
            _servicioproducto = new RestService<Producto>("http://appdulceria-movil.somee.com/", "api/productos/");
            _serviciocategoria = new RestService<Categorias>("http://appdulceria-movil.somee.com/", "api/categorias/");
            _serviciopedido = new RestService<Pedidos>("http://appdulceria-movil.somee.com/", "api/pedidos/");
            _serviciosalida = new RestService<Salidas>("http://appdulceria-movil.somee.com/", "api/salidas/");
            _serviciocliente = new RestService<Clientes>("http://appdulceria-movil.somee.com/", "api/clientes/");
            CargarCategorias();
            categoria.SelectedIndexChanged += Categoria_SelectedIndexChanged;
            productop.SelectedIndexChanged += Productop_SelectedIndexChanged;
            montototalg = 0;
            idpedidocurrent = 0;
            contador_pedido_in = 0;

        }
        private async void btnAgregar_Clicked(Object sender, EventArgs e)
        {
            if (categoria.SelectedIndex == -1 || productop.SelectedIndex == -1 || disponibles.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                return;
            }
            try
            {
                categolst = await _serviciocategoria.ObtenerDatos();
                int idcat = this.categolst[categoria.SelectedIndex].id_categoria;
                var seleceted = productop.Items[productop.SelectedIndex];
                
                int idprod=0;
                productolst = await _servicioproducto.ObtenerDatos();
                foreach (var item in productolst)
                {
                    if (item.nombre == seleceted)
                    {
                        idprod = item.id_producto;
                        break;
                    }
                }
                var disp = disponibles.Items[disponibles.SelectedIndex];
                int cantidad_pro = Convert.ToInt32(disp);
                decimal monto = (Convert.ToDecimal(PrecioP.Text)) * (Convert.ToDecimal(cantidad_pro));
                if (contador_pedido_in == 0)
                {
                    
                    pedido.status = 0;
                    pedido.direccion = "sin asignar";
                    await _serviciopedido.Guardar(pedido);
                    pedidolst = await _serviciopedido.ObtenerDatos();
                    idpedidocurrent = pedidolst[pedidolst.Count - 1].id_pedido;
                }
                contador_pedido_in++;
                clientelst = await _serviciocliente.ObtenerDatos();
                foreach (var item in clientelst)
                {
                    if (item.usuario == LogCliente.current_cliente)
                    {
                        salida.id_cliente = item.id_cliente;
                        break;
                    }
                }
                
                salida.id_pedido = idpedidocurrent;
                salida.id_producto = idprod;
                salida.monto_total = monto;
                salida.cantidad_prod = cantidad_pro;
                salida.fecha = DateTime.Now.ToLocalTime();
                var resultado = await _serviciosalida.Guardar(salida);
                if (resultado != false)
                {
                    montototalg = montototalg + monto;
                    Producto prod = productolst.SingleOrDefault(p => p.id_producto == idprod);
                    prod.cantidad -= cantidad_pro;
                    await _servicioproducto.Actualizar(prod, prod.id_producto.ToString());
                    await DisplayAlert("Compra de Productos", "Producto agregado al carrito", "Aceptar");
                    
                }
                else
                {
                    await DisplayAlert("Compra de Productos", "Error del sistema", "Aceptar");
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error", "Por el momento el servicio no está en funcionamiento, intenta más tarde", "Aceptar");
            }
            DescripcionP.Text = string.Empty;
            disponibles.Items.Clear();
            PrecioP.Text = string.Empty;
            productop.Items.Clear();
        }

        private async void Productop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productop.SelectedIndex != -1)
            {
                DescripcionP.Text = string.Empty;
                disponibles.Items.Clear();
                PrecioP.Text = string.Empty;
            }
            try
            {
                productolst = await _servicioproducto.ObtenerDatos();
                var selected = productop.Items[productop.SelectedIndex];
                foreach (var item in productolst)
                {
                    if (item.nombre == selected)
                    {
                        DescripcionP.Text = item.descripcion;
                        PrecioP.Text = item.precio.ToString();
                        for (int i = 1; i <= item.cantidad; i++)
                        {
                            disponibles.Items.Add(i.ToString());
                        }
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
                disponibles.Items.Clear();
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

        private async void btnFin_Clicked(Object sender, EventArgs e)
        {
            if (contador_pedido_in == 0 || Direccion.Text == string.Empty)
            {
                await DisplayAlert("Error", "No se ha realizado ninguna carga al carrito de compras", "Aceptar");
                return;
            }
            pedido.direccion = Direccion.Text;
            pedido.status = 0;
            await _serviciopedido.Actualizar(pedido, idpedidocurrent.ToString());
            await Navigation.PushAsync(new Factura());

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
