using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainTasks : Page
    {


        private static List<Pendientes> resultadoAPI;

        public MainTasks()
        {
            llenarAsync();
            this.InitializeComponent();
        }


        public async void llenarAsync(int userID = 1)
        {
       
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();

            //definir todos los atributos del request

            request.RequestUri = new Uri("https://localhost:44386/api/pendientes");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            //asignar cliente
            var client = new HttpClient(httpHandler);

            //usar palabra claver await con funciones async 

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            //se necesita instalar por NuGet JSON
            //se deserializa el contenido para formatear de acuerdo a la interfaz
            var resultado = JsonConvert.DeserializeObject<List<Pendientes>>(content);

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

            ActualizarLista();


            resultadoAPI = resultado;

            //ListaPendientes.ItemsSource = resultadoAPI;

        }

        private void ActualizarLista() {

           var pendientes = ListaPendientes.Items;

            foreach(Pendientes item in pendientes)
            {
                item.calcularEstado();
                item.calcularPrioridad();
            }
        }

        private async void DisplayDeleteFileDialog()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Delete task permanently?",
                Content = "If you delete this fitask, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                // Delete the file.
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private void buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string palabra = buscar.Text.ToUpper();

         
                if (palabra.Equals(""))
                {
                    llenarAsync();
                }
                else
                {


                    List<Pendientes> resultado = resultadoAPI.Where(x => x.Titulo.ToUpper().Contains(palabra)).ToList();

                    ListaPendientes.ItemsSource = resultado;

                }
            }

        private void cB_TipoTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;

            if (comboBoxItem == null) return;

            var content = comboBoxItem.Content as string;

            
            if(content != null && content.Equals("Pending tasks"))
            {
                List<Pendientes> resultado = resultadoAPI.Where(x => !x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;

            }
            else if (content != null && content.Equals("Done tasks"))
            {

                List<Pendientes> resultado = resultadoAPI.Where(x => x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;

            }else if (content.Equals("No filter"))
            {
                llenarAsync();
            }
            else
            {
                llenarAsync();
            }
        }
    }
    
}
