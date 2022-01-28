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
    class PendienteController
    {
        public static async Task<List<Pendiente>> getTasks(int userID)
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();

            //cambiar uri a usuario OJO
            request.RequestUri = new Uri("http://10.0.2.2:44304/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            //usar palabra claver await con funciones async 

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();


            // var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            return resultado.FirstOrDefault(x => x.UsuarioID == userID).Pendientes.ToList();

        }

        public static async void postTask(Pendiente pendiente)
        {

            var json = JsonConvert.SerializeObject(pendiente);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("http://10.0.2.2:44304/api/pendientes", content);
        }



        public static async void putTask(Pendiente pendiente)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(pendiente);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"http://10.0.2.2:44304/api/pendientes/{pendiente.PendienteID}", content);

        }

        public static async void deleteTask(Pendiente pendiente)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(pendiente);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.DeleteAsync($"http://10.0.2.2:44304/api/pendientes/{pendiente.PendienteID}");
        }
    }
}
