using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPlusUWP.Controllers
{
    class ContactoController
    {
        public static async Task<List<Contacto>> getContacto(int userID)
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:44304/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            return resultado.FirstOrDefault(x => x.UsuarioID == userID).Contactos.ToList();
        }

        public static async void postContacto(Contacto contacto)
        {
            var json = JsonConvert.SerializeObject(contacto);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44304/api/contacto", content);
        }

        public static async void putContacto(Contacto contacto)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(contacto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/api/contacto/{contacto.ContactoID}", content);
        }


        public static async void deleteContacto(Contacto contacto)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(contacto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.DeleteAsync($"https://localhost:44304/api/contacto/{contacto.ContactoID}");
        }
    }
}
