using AgendaPlusXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusXamarin.Controllers
{
    class FechaImportanteController
    {
        public static async Task<List<FechaImportante>> getFecha(int userID)
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://10.0.2.2:44304/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            return resultado.FirstOrDefault(x => x.UsuarioID == userID).FechasImportantes.ToList();
        }

        public static async void postFecha(FechaImportante fecha)
        {
            var json = JsonConvert.SerializeObject(fecha);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("http://10.0.2.2:44304/api/fechaimportante", content);
        }

        public static async void putfecha(FechaImportante fecha)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(fecha);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"http://10.0.2.2:44304/api/fechaimportante/{fecha.FechasImportantesID}", content);
        }

        public static async void deleteFecha(FechaImportante fecha)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(fecha);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.DeleteAsync($"http://10.0.2.2:44304/api/fechaimportante/{fecha.FechasImportantesID}");
        }
    }
}
