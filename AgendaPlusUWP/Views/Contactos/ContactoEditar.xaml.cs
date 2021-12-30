using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class ContactoEditar : Page
    {
        private static int userID;
        private static int contactoID;
        private static Contacto contacto
            ;

        public ContactoEditar()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<string> lista = new List<string>();

            string paramStr = e.Parameter.ToString();

            paramStr = paramStr.Replace("(", string.Empty);
            paramStr = paramStr.Replace(")", string.Empty);

            lista = paramStr.Split(",").ToList();

            userID = Int32.Parse(lista[0]);
            contactoID = Int32.Parse(lista[1]);

            inizializarAPI();

            base.OnNavigatedTo(e);
        }

        private async void inizializarAPI()
        {
            List<Contacto> resultado = await ContactoController.getContacto(userID); ;

            contacto = resultado.Find(x=> x.ContactoID== contactoID);

            textBoxNombre.Text = contacto.NombreContacto.ToString();
            textBoxCorreo.Text = contacto.CorreoContacto.ToString();
            textBoxNumero.Text = contacto.TelefonoContacto.ToString();

        }

        private void editarContacto(object sender, RoutedEventArgs e)
        {
            if (validarNombre(textBoxNombre.Text) && validarCorreo(textBoxCorreo.Text) && validarNumero(textBoxNumero.Text))
            {

                contacto.NombreContacto = textBoxNombre.Text;
                contacto.CorreoContacto = textBoxCorreo.Text;
                contacto.TelefonoContacto = textBoxNumero.Text;
                contacto.Usuario = null;

                ContactoController.putContacto(contacto);

                Frame.Content = null;
                Frame.Navigate(typeof(ContactoMain), userID);

            }
            

        }



        private Boolean validarNombre(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorNombre.Text = "The name field is required";
                return false;
            }
            textBoxErrorNombre.Text = "";
            return true;
        }

        private Boolean validarCorreo(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorCorreo.Text = "The email field is required";
                return false;
            }
            textBoxErrorCorreo.Text = "";
            return true;
        }

        private Boolean validarNumero(string a)
        {

            var expresionRegular = new Regex(@"(\+34|0034|34)?[ -]*(6|7)[ -]*([0-9][ -]*){8}");
            if (a.Equals("") && !expresionRegular.IsMatch(a))
            {
                textBoxErrorCorreo.Text = "The phone number field is required or the number is incorrect.";
                return false;
            }
            textBoxErrorCorreo.Text = "";
            return true;
        }
    }
}
