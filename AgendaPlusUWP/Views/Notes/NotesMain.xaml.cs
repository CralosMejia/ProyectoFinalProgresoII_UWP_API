using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views.Notes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesMain : Page
    {
        private static List<Nota> resultadoAPI;
        private static int userID;


        public  NotesMain()
        {
            userID = 1;
            this.InitializeComponent();
            inizializarAPI();
        }

        private async void inizializarAPI()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:44304/api/usuarios");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            resultadoAPI = resultado.FirstOrDefault(x => x.UsuarioID == userID).Notas.ToList();

            ListaNota.ItemsSource = resultadoAPI;
        }

        private void TextBox_Buscar(object sender, TextChangedEventArgs e)
        {
            string palabra = textBoxBuscar.Text.ToUpper();

            if (palabra.Equals(""))
            {
                inizializarAPI();
            }
            else
            {
                List<Nota> resultado = resultadoAPI.Where(x => x.Titulo.ToUpper().Contains(palabra)).ToList();

                ListaNota.ItemsSource = resultado;
            }

        }

        private  void crearNota(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
        }

        private async void borrarNota(object sender, RoutedEventArgs e)
        {
            Nota nota = (Nota)ListaNota.SelectedItem;

            if (nota != null)
            {
                nota.Usuario = null;

                var httpHandler = new HttpClientHandler();
                var client = new HttpClient(httpHandler);
                var json = JsonConvert.SerializeObject(nota);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44304/api/notas/{nota.NotaID}");
                inizializarAPI();
            }
        }
    }
}
