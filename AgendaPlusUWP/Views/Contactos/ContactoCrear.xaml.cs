using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;
using AgendaPlusUWP.Views.Notes;
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
    public sealed partial class ContactoCrear : Page
    {
        private static int userID;

        public ContactoCrear()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            base.OnNavigatedTo(e);
        }

        private void crearContacto(object sender, RoutedEventArgs e)
        {
            if (validarNombre(textBoxNombre.Text) && validarCorreo(textBoxCorreo.Text) && validarNumero(textBoxNumero.Text))
            {
                Contacto contacto = new Contacto() { NombreContacto= textBoxNombre.Text,CorreoContacto= textBoxCorreo.Text,TelefonoContacto= textBoxNumero.Text, UsuarioID = userID };

                ContactoController.postContacto(contacto);

                Frame.Content = null;
                Frame.Navigate(typeof(ContactoMain), userID);
            }
        }


        //Validaciones.
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

