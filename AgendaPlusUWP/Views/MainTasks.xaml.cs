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

       

        private static List<Pendiente> resultadoAPI;

        private static int userID=1;

        private static Pendiente t;

        public MainTasks()
        {
           this.InitializeComponent();

           llenarAsync();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string IDstr = e.Parameter.ToString();

            //userID = Int32.Parse(IDstr);
              

            base.OnNavigatedTo(e);
        }

        private async void llenarAsync()
        {
            if(userID != 0) { 
            resultadoAPI = await PendientesController.getTasks(userID);

            ListaPendientes.ItemsSource = resultadoAPI;
            ActualizarLista();
            }
        }



        private void ActualizarLista()
        {

            var pendientes = ListaPendientes.Items;

            foreach (Pendiente item in pendientes)
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

                    List<Pendiente> resultado = resultadoAPI.Where(x => x.Titulo.ToUpper().Contains(palabra)).ToList();

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
                List<Pendiente> resultado = resultadoAPI.Where(x => !x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;

            }
            else if (content != null && content.Equals("Done tasks"))
            {

                List<Pendiente> resultado = resultadoAPI.Where(x => x.Estado).ToList();

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
                List<Pendiente> resultado = resultadoAPI.OrderBy(x => x.Prioridad).ToList();

                ListaPendientes.ItemsSource = resultado;

            }else if(content != null && content.Equals("Importance") && cB_TipoTask.SelectedIndex == 0)
            {
                List<Pendiente> resultado = resultadoAPI.Where(x => !x.Estado).ToList();
                resultado.OrderBy(x => x.Prioridad);

                ListaPendientes.ItemsSource = resultado;

            }else if (content != null && content.Equals("Importance") && cB_TipoTask.SelectedIndex == 1)
            {
                List<Pendiente> resultado = resultadoAPI.Where(x => x.Estado).ToList();
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
            var item = (((Pendiente)args.Item));

            if(item.ColorPrioridad != null)
                args.ItemContainer.Background = new SolidColorBrush(HexToColor(item.ColorPrioridad));
                                
        }

        /// <summary>
        /// metodo que permite convertir un string a hexadecimal
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns>Color</returns>
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

        private void btn_EditPendiente_Click(object sender, RoutedEventArgs e)
        {
            Pendiente task = (Pendiente)ListaPendientes.SelectedItem;

            if(task != null)
            {
                Frame.Content = null;
                Frame.Navigate(typeof(EditTasks), (userID, task.PendienteID));
            }
            else
            {
                mostrarCuadroDeDialogo();
            }
        }


        private async void mostrarCuadroDeDialogo()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "No selection",
                Content = "You have not selected any task, try again.",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

          private async void mostrarCuadroDeDialogoCambiarEstado()
            {
                ContentDialog dialogo = new ContentDialog
                {
                    Title = "Task already done",
                    Content = "You have already done this task.",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await dialogo.ShowAsync();
            }

            private void btn_verPendiente_Click(object sender, RoutedEventArgs e)
        {
            Pendiente task = (Pendiente)ListaPendientes.SelectedItem;
            

            if (task != null)
            {
                Frame.Content = null;
                Frame.Navigate(typeof(TaskDetails), (userID, task.PendienteID));
            }
            else
            {
                mostrarCuadroDeDialogo();
            }
        }

        private async void btn_DelPendiente_Click(object sender, RoutedEventArgs e)
        {
            Pendiente task = (Pendiente)ListaPendientes.SelectedItem;

            if (task != null)
            {
                task.Usuarios = null;

                ContentDialog dialogo = new ContentDialog
                {
                    Title = "Delete task permanently?",
                    Content = "If you delete this task, you won't be able to recover it. Do you want to delete it?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Delete"
                };

                ContentDialogResult result = await dialogo.ShowAsync();
                string resultSTR = result.ToString();

                if (resultSTR.Equals("Primary"))
                {
                    PendientesController.deleteTask(task);

                    ListaPendientes.ItemsSource = null;

                    resultadoAPI.Remove(task);

                    ListaPendientes.ItemsSource = resultadoAPI;
                }
            }
            else
            {
                mostrarCuadroDeDialogo();
            }
        }

        //cambiar estado del pendiente a realizado (se realiza aqui o en editarTask)
        private  async void btn_PendienteRealizado_Click(object sender, RoutedEventArgs e)
        {
            Pendiente editedTask = (Pendiente)ListaPendientes.SelectedItem;

            if (editedTask != null && !editedTask.Estado)
            {
                List<Pendiente> resultado =  await PendientesController.getTasks(userID);

                t = resultado.Find(x => x.PendienteID == editedTask.PendienteID);

                t.Titulo = editedTask.Titulo;
                t.Descripcion = editedTask.Descripcion;
                t.Prioridad = editedTask.Prioridad;
                t.FechaLimite = editedTask.FechaLimite;
                t.Usuarios = null;
                t.Estado = true;
                                    
                              
                PendientesController.putTask(t);

                llenarAsync();
                llenarAsync();
            }
            else if(editedTask == null)
            {
                mostrarCuadroDeDialogo();
            }
            else
            {
                mostrarCuadroDeDialogoCambiarEstado();
            }
        }
    }
        
}
