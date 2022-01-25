using AgendaPlusUWP.Models;
using AgendaPlusUWP.Controllers;
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
            resultadoAPI = await NotasController.getNota(userID);

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
            Frame.Navigate(typeof(NotesCreate), userID);
        }

        private void editarNota(object sender, RoutedEventArgs e)
        {
            Nota nota = (Nota)ListaNota.SelectedItem;

            if (nota != null)
            {
                Frame.Content = null;
                Frame.Navigate(typeof(NotesEdit), (userID, nota.NotaID));
            }
        }

        private async void borrarNota(object sender, RoutedEventArgs e)
        {
            Nota nota = (Nota)ListaNota.SelectedItem;

            if (nota != null)
            {

                nota.Usuario = null;

                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Warning",
                    Content = "Are you sure you want to delete the note?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Delete"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
                string resultSTR = result.ToString();

                if (resultSTR.Equals("Primary"))
                {
                    NotasController.deleteNota(nota);

                    ListaNota.ItemsSource = null;

                    resultadoAPI.Remove(nota);

                    ListaNota.ItemsSource = resultadoAPI;
                }
            }
        }

    }
}
