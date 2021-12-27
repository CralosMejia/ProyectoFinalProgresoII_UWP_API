using AgendaPlusUWP.Models;
using AgendaPlusUWP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AgendaPlusUWP.Controllers
{
    class PendientesController
    {
        public static async Task<List<Pendientes>> getTasks(int userID)
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri("https://localhost:44386/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            //usar palabra claver await con funciones async 

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuarios>>(content);

            return resultado.FirstOrDefault(x => x.UsuarioID == userID).Pendientes.ToList();
        }

        public static async void postTask(Pendientes pendiente)
        {
  
            var json = JsonConvert.SerializeObject(pendiente);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44386/api/pendientes", content);
        }



        public static async void putTask(Pendientes pendiente)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(pendiente);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"https://localhost:44386/api/pendientes/{pendiente.PendienteID}", content);

        }

        public static async void deleteTask(Pendientes pendiente)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(pendiente);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.DeleteAsync($"https://localhost:44386/api/pendientes/{pendiente.PendienteID}");
        }
    }
}
