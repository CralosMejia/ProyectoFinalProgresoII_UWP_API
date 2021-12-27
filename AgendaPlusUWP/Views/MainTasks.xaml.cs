using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
          
        }

        private void cB_SortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;

            if (comboBoxItem == null) return;

            var content = comboBoxItem.Content as string;


            if (content != null && content.Equals("Importance") && cB_TipoTask.SelectedIndex==2)
            {
                List<Pendientes> resultado = resultadoAPI.OrderBy(x => x.Prioridad).ToList();

                ListaPendientes.ItemsSource = resultado;

            }else if(content != null && content.Equals("Importance") && cB_TipoTask.SelectedIndex == 0)
            {
                List<Pendientes> resultado = resultadoAPI.Where(x => !x.Estado).ToList();
                resultado.OrderBy(x => x.Prioridad);

                ListaPendientes.ItemsSource = resultado;

            }else if (content != null && content.Equals("Importance") && cB_TipoTask.SelectedIndex == 1)
            {
                List<Pendientes> resultado = resultadoAPI.Where(x => x.Estado).ToList();
                resultado.OrderBy(x => x.Prioridad);

                ListaPendientes.ItemsSource = resultado;
            }
            else if (content.Equals("No filter"))
            {
                llenarAsync();
            }
      
        }

   
        private void ListaPendientes_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var item = (((Pendientes)args.Item));

             args.ItemContainer.Background = new SolidColorBrush(HexToColor(item.ColorPrioridad));
                                
        }

        //metodo que convierte un string a hexadecimal 
        public static Color HexToColor(string hexColor)
        {
           
            if (hexColor.IndexOf('#') != -1)
            {
                hexColor = hexColor.Replace("#", "");
            }

            if (hexColor.Length == 6)
            {
                hexColor = "FF" + hexColor;
            }

            //100 % — FF  //50 % — 80
            //95 % — F2  //45 % — 73
            //90 % — E6  //40 % — 66
            //85 % — D9  //30 % — 4D
            //80 % — CC     //25 % — 40
            //75 % — BF  //20 % — 33
            //70 % — B3  //15 % — 26
            //65 % — A6  //10 % — 1A
            //60 % — 99  //5 % — 0D
            //55 % — 8C  //0 % — 00

            byte alpha = 0;
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            if (hexColor.Length == 8)
            {
                alpha = byte.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                red = byte.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                green = byte.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                blue = byte.Parse(hexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);
            }
            return Color.FromArgb(alpha, red, green, blue);
        }

        //private void mantenerFiltrado (object sender, object sender2, SelectionChangedEventArgs e, SelectionChangedEventArgs e1)
        //{
        //    var comboBoxItem = e.AddedItems[0] as ComboBoxItem;

        //    if (comboBoxItem == null) return;

        //    var content = comboBoxItem.Content as string;


        //    var comboBoxItem1 = e1.AddedItems[0] as ComboBoxItem;

        //    if (comboBoxItem1 == null) return;

        //    var content1 = comboBoxItem1.Content as string;

        //    switch (content + content1)
        //    {
        //        case ("Pending tasks" + "Importance"):

        //            List<Pendientes> resultado = resultadoAPI.Where(x => !x.Estado) .ToList();

        //            resultado.OrderBy(x => x.Prioridad);

        //            ListaPendientes.ItemsSource = resultado;
        //            break;
        //        case ("Done tasks" + "Importance"):

        //            List<Pendientes> resultado2 = resultadoAPI.Where(x => !x.Estado).ToList();

        //            resultado2.OrderBy(x => x.Prioridad);

        //            ListaPendientes.ItemsSource = resultado2;
        //            break;
        //        case ("No filter" + "No filter"):

        //            llenarAsync();

        //            break;
        //        case ("No filter" + "Importance"):

        //            List<Pendientes> resultado3 = resultadoAPI.OrderBy(x => x.Prioridad).ToList();

        //            ListaPendientes.ItemsSource = resultado3;

        //            break;



        //    }

        //}

    }
    
}
