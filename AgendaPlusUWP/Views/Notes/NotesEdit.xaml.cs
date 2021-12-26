using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    public sealed partial class NotesEdit : Page
    {
        private static int userID;
        private static int notaID;
        private static Nota nota;
        public NotesEdit()
        {
            userID = 1;
            notaID = 1;
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

            nota = resultado.FirstOrDefault(x => x.UsuarioID == userID).Notas.ToList().Find(x=>x.NotaID == notaID);

            textBoxTitle.Text = nota.Titulo.ToString();
            textBoxDescription.Text=nota.Descripcion.ToString();

        }

        private async void editarNota(object sender, RoutedEventArgs e)
        {
            if (validarTitulo(textBoxTitle.Text) && validarDescripcion(textBoxDescription.Text))
            {
                
                nota.Titulo = textBoxTitle.Text;
                nota.Descripcion = textBoxDescription.Text;
                nota.Usuario = null;

                var httpHandler = new HttpClientHandler();
                var client = new HttpClient(httpHandler);
                var json = JsonConvert.SerializeObject(nota);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/api/notas/{nota.NotaID}",content);



            }
            else
            {
                validarDescripcion(textBoxDescription.Text);
            }

        }



        private Boolean validarTitulo(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorTitle.Text = "El campo titulo es requerido";
                return false;
            }
            textBoxErrorTitle.Text = "";
            return true;
        }

        private Boolean validarDescripcion(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorDescription.Text = "El campo decripcion es requerido";
                return false;
            }
            textBoxErrorDescription.Text = "";
            return true;
        }
    }
}
