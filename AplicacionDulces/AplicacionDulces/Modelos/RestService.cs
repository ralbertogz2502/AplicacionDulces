using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDulces.Modelos
{
    public class RestService<T> where T : class
    {
        private HttpClient _cliente;
        private string _uriApi;

        public RestService(string uriBase, string uriApi)
        {
            _uriApi = uriApi;
            _cliente = new HttpClient();
            //http://tienda-movil-dulces.somee.com
            _cliente.BaseAddress = new Uri(uriBase);
            _cliente.DefaultRequestHeaders.Accept.Clear();
            _cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _cliente.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<T>> ObtenerDatos()
        {
            HttpResponseMessage response = await _cliente.GetAsync(_uriApi);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<T>>(content);
                return items;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Guardar(T c)
        {
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _cliente.PostAsync(_uriApi, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Actualizar(T c, string id)
        {
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _cliente.PutAsync(string.Format("{0}{1}", _uriApi, id), content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Eliminar(T c, string id)
        {
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _cliente.DeleteAsync(string.Format("{0}{1}", _uriApi, id));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
