using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AgendaPlusXamarin.Models;
using Newtonsoft.Json;

namespace AgendaPlusXamarin.Controllers
{
    class ContactoController
    {
        public static async Task<List<Contacto>> getContacto(int userID)
        {
            //Artista
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://10.0.2.2:44304/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();

                var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

                return resultado.FirstOrDefault(x => x.UsuarioID == userID).Contactos.ToList();

            }
            return null;
        }

        public static async void postContacto(Contacto contacto)
        {
            var json = JsonConvert.SerializeObject(contacto);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("http://10.0.2.2:44304/api/contacto", content);
        }

        public static async void putContacto(Contacto contacto)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(contacto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync($"http://10.0.2.2:44304/api/contacto/{contacto.ContactoID}", content);
        }


        public static async void deleteContacto(Contacto contacto)
        {
            var httpHandler = new HttpClientHandler();
            var client = new HttpClient(httpHandler);
            var json = JsonConvert.SerializeObject(contacto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.DeleteAsync($"http://10.0.2.2:44304/api/contacto/{contacto.ContactoID}");
        }
    }
}
