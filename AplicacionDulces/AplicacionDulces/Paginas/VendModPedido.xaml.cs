using AplicacionDulces.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AplicacionDulces.Paginas
{
    public partial class VendModPedido : ContentPage
    {
        private RestService<Pedidos> _serviciopedido;
        private Pedidos _pedidos;
        public VendModPedido(Pedidos p, RestService<Pedidos> serviciopedido)
        {
            InitializeComponent();
            _pedidos = p;
            entryPedido.Text = _pedidos.direccion;
            _serviciopedido = serviciopedido;
            cargarpicker();
            btnActualizar.Clicked += BtnActualizar_Clicked;
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Confirmación", "¿Realmente deseas modificar el pedido?", "Sí", "No");
            if (!respuesta)
            {
                return;
            }
            if (entryStatus.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Faltan campos por llenar", "Aceptar");
                return;
            }
            string stat = entryStatus.Items[entryStatus.SelectedIndex];
            int statusin = 0;
            if (stat == "Entregado")
            {
                statusin = 1;
            }
            if (stat == "No Entregado")
            {
                statusin = 0;
            }
            _pedidos.direccion = entryPedido.Text;
            _pedidos.status = statusin;
            await _serviciopedido.Actualizar(_pedidos, _pedidos.id_pedido.ToString());
            await DisplayAlert("Administración de Pedidos", "Pedido Actualizado, ya puede volver al menú anterior", "Aceptar");
            await Navigation.PopAsync();
        }

        private void cargarpicker()
        {
            entryStatus.Items.Add("Entregado");
            entryStatus.Items.Add("No entregado");
        }
    }
}

