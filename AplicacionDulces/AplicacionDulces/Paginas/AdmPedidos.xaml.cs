using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class AdmPedidos : ContentPage
    {
        private RestService<Salidas> _serviciosalida;
        private List<Salidas> salidalst;
        private RestService<Pedidos> _serviciopedido;
        private List<Pedidos> pedidolst;
        private List<Pedidos> pedidolst1;
        public IEnumerable<Salidas> Salidaie;
        public IEnumerable<Pedidos> Pedidoie;
        public AdmPedidos()
        {
            InitializeComponent();
            
            _serviciopedido = new RestService<Pedidos>("http://appdulceria-movil.somee.com/", "api/pedidos/");
            _serviciosalida = new RestService<Salidas>("http://appdulceria-movil.somee.com/", "api/salidas/");
            cargarmesyano();
            diaobtener.DateSelected += Diaobtener_DateSelected;
            Mes.SelectedIndexChanged += Mes_SelectedIndexChanged;
            Ano.SelectedIndexChanged += Ano_SelectedIndexChanged;  
            
        }

        private async void Ano_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pedidos ped = new Pedidos();
            pedidolst = await _serviciopedido.ObtenerDatos();
            salidalst = await _serviciosalida.ObtenerDatos();
            var seleceted = Ano.Items[Ano.SelectedIndex];


            var detalles = salidalst.Where(d => d.fecha.ToString().Substring(6, 4) == seleceted).OrderBy(d => d.id_pedido);
            
            List<int> ids = new List<int>();
            int igual = 0;
            foreach (var item in detalles)
            {
                if (item.id_pedido != igual)
                {
                    igual = item.id_pedido;
                    ids.Add(igual);
                }
            }
            List<Pedidos> pedidos = new List<Pedidos>();
            foreach (var item in ids)
            {
                ped = pedidolst.SingleOrDefault(p => p.id_pedido == item);
                if (ped != null)
                {
                    pedidos.Add(ped);
                }
            }
            
            lstPedidos.ItemsSource = pedidos;
        }

        private void cargarmesyano()
        {

            try
            {
                Mes.Items.Add("01");
                Mes.Items.Add("02");
                Mes.Items.Add("03");
                Mes.Items.Add("04");
                Mes.Items.Add("05");
                Mes.Items.Add("06");
                Mes.Items.Add("07");
                Mes.Items.Add("08");
                Mes.Items.Add("09");
                Mes.Items.Add("10");
                Mes.Items.Add("11");
                Mes.Items.Add("12");
                Ano.Items.Add("2016");

            }
            catch (Exception)
            {

                
            }
        }

        private async void Mes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pedidos ped = new Pedidos();
            pedidolst = await _serviciopedido.ObtenerDatos();
            salidalst = await _serviciosalida.ObtenerDatos();
            var seleceted = Mes.Items[Mes.SelectedIndex];
            

            var detalles = salidalst.Where(d => d.fecha.ToString().Substring(3, 2) == seleceted).OrderBy(d => d.id_pedido);
            //crear una lista de strings,y buscar los id diferentes
            List<int> ids = new List<int>();
            int igual = 0;
            foreach (var item in detalles)
            {
                if (item.id_pedido != igual)
                {
                    igual = item.id_pedido;
                    ids.Add(igual);
                }
            }
            List<Pedidos> pedidos = new List<Pedidos>();
            foreach (var item in ids)
            {
                ped = pedidolst.SingleOrDefault(p => p.id_pedido == item);
                if (ped != null)
                {
                    pedidos.Add(ped);
                }
            }
            ///var pedidlst1 = pedidolst.Where(p => p.id_pedido == (salidalst.Single(s => s.fecha.Date.ToString().Substring(0, 10) == fechaseleccionada).id_pedido));
            lstPedidos.ItemsSource = pedidos;
        }

        private async void Diaobtener_DateSelected(object sender, DateChangedEventArgs e)
        {
            Pedidos ped = new Pedidos();
            pedidolst = await _serviciopedido.ObtenerDatos();
            salidalst = await _serviciosalida.ObtenerDatos();
            string fechaseleccionada = diaobtener.Date.ToString().Substring(0,10);

            var detalles = salidalst.Where(d => d.fecha.ToString().Substring(0, 10) == fechaseleccionada).OrderBy(d => d.id_pedido);
            //crear una lista de strings,y buscar los id diferentes
            List<int> ids = new List<int>();
            int igual = 0;
            foreach (var item in detalles)
            {
                if (item.id_pedido != igual)
                {
                    igual = item.id_pedido;
                    ids.Add(igual);
                }
            }
            List<Pedidos> pedidos = new List<Pedidos>();
            foreach (var item in ids)
            {
                ped = pedidolst.SingleOrDefault(p => p.id_pedido == item);
                if(ped!=null)
                {
                    pedidos.Add(ped);
                }
            }
            ///var pedidlst1 = pedidolst.Where(p => p.id_pedido == (salidalst.Single(s => s.fecha.Date.ToString().Substring(0, 10) == fechaseleccionada).id_pedido));
            lstPedidos.ItemsSource = pedidos;
            
            
        }
    }
}
