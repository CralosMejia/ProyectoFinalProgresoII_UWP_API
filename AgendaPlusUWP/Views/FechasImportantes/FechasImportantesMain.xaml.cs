using AgendaPlusUWP.Controllers;
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

namespace AgendaPlusUWP.Views.FechasImportantes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FechasImportantesMain : Page
    {

        private static List<FechasImportante> resultadoAPI;
        private static int userID;

        public FechasImportantesMain()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            inizializarAPI();

            base.OnNavigatedTo(e);
        }

        private async void inizializarAPI()
        {
            resultadoAPI = await FechasImportantesController.getFecha(userID);

            ListaFechasImportantes.ItemsSource = resultadoAPI;
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
                List<FechasImportante> resultado = resultadoAPI.Where(x => x.Titulo.ToUpper().Contains(palabra)).ToList();

                ListaFechasImportantes.ItemsSource = resultado;
            }
        }

        private void crearFecha(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Frame.Navigate(typeof(FechasImportantesCreate), userID);
        }

        private void editarFecha(object sender, RoutedEventArgs e)
        {
            FechasImportante fecha = (FechasImportante)ListaFechasImportantes.SelectedItem;

            if (fecha != null)
            {
                Frame.Content = null;
                Frame.Navigate(typeof(FechasImportatesEditar), (userID, fecha.FechasImportantesID));
                inizializarAPI();

            }
        }

        private async void borrarNota(object sender, RoutedEventArgs e)
        {
            FechasImportante fecha = (FechasImportante)ListaFechasImportantes.SelectedItem;

            if (fecha != null)
            {
                fecha.Usuario = null;

                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Warning",
                    Content = "Are you sure you want to delete the date?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Delete"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
                string resultSTR = result.ToString();

                if (resultSTR.Equals("Primary"))
                {
                    FechasImportantesController.deleteFecha(fecha);


                    ListaFechasImportantes.ItemsSource = null;

                    resultadoAPI.Remove(fecha);

                    ListaFechasImportantes.ItemsSource = resultadoAPI;
                }
            }
        }
    }
}
