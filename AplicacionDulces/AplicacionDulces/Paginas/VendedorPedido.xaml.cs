using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class VendedorPedido : ContentPage
    {
        private RestService<Pedidos> _serviciopedido;
        private List<Pedidos> pedidoslst;
        public VendedorPedido()
        {
            InitializeComponent();
            _serviciopedido = new RestService<Pedidos>("http://appdulceria-movil.somee.com/", "api/pedidos/");
            CargarPedidosnoEntregados();
            btnActualizar.Clicked += BtnActualizar_Clicked;
            lstPedidos.ItemTapped += LstPedidos_ItemTapped;
        }

        private void LstPedidos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new VendModPedido(e.Item as Pedidos, _serviciopedido));
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            Pedidos ped = new Pedidos();
            pedidoslst = await _serviciopedido.ObtenerDatos();

            var detalles = pedidoslst.Where(d => d.status == 0);
            List<Pedidos> pedidos = new List<Pedidos>();
            foreach (var item in detalles)
            {
                ped = pedidoslst.SingleOrDefault(p => p.id_pedido == item.id_pedido);
                if (ped != null)
                {
                    pedidos.Add(ped);
                }
            }

            lstPedidos.ItemsSource = pedidos;
        }

        private async void CargarPedidosnoEntregados()
        {
            Pedidos ped = new Pedidos();
            pedidoslst = await _serviciopedido.ObtenerDatos();

            var detalles = pedidoslst.Where(d => d.status == 0);
            List<Pedidos> pedidos = new List<Pedidos>();
            foreach (var item in detalles)
            {
                ped = pedidoslst.SingleOrDefault(p => p.id_pedido == item.id_pedido);
                if (ped != null)
                {
                    pedidos.Add(ped);
                }
            }

            lstPedidos.ItemsSource = pedidos;
        }
    }
}
