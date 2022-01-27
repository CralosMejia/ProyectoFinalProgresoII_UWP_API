using AgendaPlusXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusXamarin.Controllers
{
    class UsuarioController
    {
        public static async void postUsuario(Usuario usuario)
        {

            var json = JsonConvert.SerializeObject(usuario);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await httpClient.PostAsync("http://10.0.2.2:44304/api/usuario", content);
        }



        public static async void putTask(Usuario usuario)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync($"http://10.0.2.2:44304/api/usuario/{usuario.UsuarioID}", content);

        }

        public static async Task<List<Usuario>> getUsuario()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://10.0.2.2:44304/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Usuario>>(content);

        }
    }
}
