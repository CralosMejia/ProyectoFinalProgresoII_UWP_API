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
using AgendaPlusUWP.Controllers;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainTasks : Page
    {

        private PendientesController controlador = new PendientesController();

        private static List<Pendientes> resultadoAPI;

        private static int userID;

        public MainTasks()
        {
           
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            llenarAsync();
            base.OnNavigatedTo(e);
        }

        private async void llenarAsync()
        {
            resultadoAPI = await controlador.getTasks(userID);

            ListaPendientes.ItemsSource = resultadoAPI;
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

        private void btn_AddPendiente_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Content = null;
            Frame.Navigate(typeof(AddTasks), userID);
        }
    }
    
}
