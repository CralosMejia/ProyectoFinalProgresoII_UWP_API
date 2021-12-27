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

        private static List<Usuarios> resultadoAPI;

        public async Task crearPendienteAsync(int UsuarioID, string Titulo, string Descripcion, DateTime FechaLimite, string ColorPrioridad, string StringPrioridad, string StringEstado, bool Estado, int Prioridad)
        {
            Pendientes pendiente = new Pendientes() { Titulo = Titulo, Descripcion = Descripcion, UsuarioID = UsuarioID, ColorPrioridad = ColorPrioridad , Prioridad= Prioridad
            , Estado = Estado, EstadoString = StringEstado, FechaLimite = FechaLimite, PrioridadString = StringPrioridad};

            var json = JsonConvert.SerializeObject(pendiente);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44386/api/pendientes", content);
        }

        public async void borrarNota(ListView ListaPendientes)
        {
            Pendientes pendiente = (Pendientes)ListaPendientes.SelectedItem;

            if (pendiente != null)
            {
                pendiente.Usuarios = null;

                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Delete task permanently?",
                    Content = "If you delete this fitask, you won't be able to recover it. Do you want to delete it?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
                string resultSTR = result.ToString();

                if (resultSTR.Equals("Primary"))
                {
                    var httpHandler = new HttpClientHandler();
                    var client = new HttpClient(httpHandler);
                    var json = JsonConvert.SerializeObject(pendiente);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.DeleteAsync($"https://localhost:44304/api/notas/{pendiente.PendienteID}");
                    llenarAsync(pendiente.UsuarioID,ListaPendientes);
                }
            }
        }

        public async void llenarAsync(int userID, ListView ListaPendientes)
        {

            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();

            //definir todos los atributos del request

            request.RequestUri = new Uri("https://localhost:44386/api/usuarios");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            //asignar cliente
            var client = new HttpClient(httpHandler);

            //usar palabra claver await con funciones async 

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            //se necesita instalar por NuGet JSON
            //se deserializa el contenido para formatear de acuerdo a la interfaz
            var resultado = JsonConvert.DeserializeObject<List<Usuarios>>(content);

            // resultadoAPI = resultado.FirstOrDefault(x => x.UsuarioID == userID).Pendientes.ToList();


            //PUEDE SERVIR PARA CUANDO SE SELECCIONA EL PENDIENTE
            //foreach(Pendientes item in ListaPendientes.Items)
            //{
            //    ListViewItem lvi = ListaPendientes.ContainerFromItem(item) as ListViewItem;
            //    if(lvi != null && lvi.IsSelected)
            //    {

            //    }
            //}
            ListaPendientes.ItemsSource = resultado;

            ActualizarLista(ListaPendientes);


            resultadoAPI = resultado;

            //ListaPendientes.ItemsSource = resultadoAPI;

        }

        public void ActualizarLista(ListView ListaPendientes)
        {

            var pendientes = ListaPendientes.Items;

            foreach (Pendientes item in pendientes)
            {
                item.calcularEstado();
                item.calcularPrioridad();
            }
        }


        public async void editarPendiente(Pendientes pendiente,int UserId,string Titulo, string Descripcion, DateTime FechaLimite, 
            string ColorPrioridad, string StringPrioridad, string StringEstado, bool Estado, int Prioridad
            ,bool validarTitulo, bool validarDescripcion, bool validarFecha, bool validarPrioridad, bool validarEstado, Frame frame)
        {
            if (validarTitulo && validarDescripcion && validarFecha && validarPrioridad && validarEstado)
            {

                pendiente.Titulo =Titulo;
                pendiente.Descripcion = Descripcion;
                pendiente.Usuarios = null;
                pendiente.FechaLimite = FechaLimite;
                pendiente.Prioridad = Prioridad;
                pendiente.Estado = Estado;

                var httpHandler = new HttpClientHandler();
                var client = new HttpClient(httpHandler);
                var json = JsonConvert.SerializeObject(pendiente);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://localhost:44304/api/notas/{pendiente.PendienteID}", content);

                frame.Content = null;
                frame.Navigate(typeof(MainTasks), UserId);

            }
           

        }


    }
}
