using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace AgendaPlusUWP.Views.Contactos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactoMain : Page
    {

        private static List<Contacto> resultadoAPI;
        private static int userID;

        public ContactoMain()
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
            resultadoAPI = await ContactoController.getContacto(userID);

            ListaContacto.ItemsSource = resultadoAPI;
        }

        private void textBoxBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string palabra = textBoxBuscar.Text.ToUpper();

            if (palabra.Equals(""))
            {
                inizializarAPI();
            }
            else
            {
                List<Contacto> resultado = resultadoAPI.Where(x => x.NombreContacto.ToUpper().Contains(palabra)).ToList();

                ListaContacto.ItemsSource = resultado;
            }

        }

        private void BotonCrear_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Frame.Navigate(typeof(ContactoCrear), userID);
        }

        private void editarContacto(object sender, RoutedEventArgs e)
        {
            Contacto contacto = (Contacto)ListaContacto.SelectedItem;

            if (contacto != null)
            {
                Frame.Content = null;
                Frame.Navigate(typeof(ContactoEditar), (userID, contacto.ContactoID));
            }
        }

        private async void borrarContacto(object sender, RoutedEventArgs e)
        {
            Contacto contacto = (Contacto)ListaContacto.SelectedItem;

            if (contacto != null)
            {

                contacto.Usuario = null;

                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Warning",
                    Content = "Are you sure you want to delete the contact?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Delete"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
                string resultSTR = result.ToString();

                if (resultSTR.Equals("Primary"))
                {
                    ContactoController.deleteContacto(contacto);

                    ListaContacto.ItemsSource = null;

                    resultadoAPI.Remove(contacto);

                    ListaContacto.ItemsSource = resultadoAPI;
                }
            }
        }

      
    }
}
