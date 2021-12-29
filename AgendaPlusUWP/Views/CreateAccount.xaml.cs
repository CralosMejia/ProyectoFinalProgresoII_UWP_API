using AgendaPlusUWP.Models;
using AgendaPlusUWP.Controlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class CreateAccount : Page
    {
        public CreateAccount()
        {
            this.InitializeComponent();

            
        }


        public void Button_Click(object sender, RoutedEventArgs e)
        {

            if (validarPassword(textboxPassword.Password) && validarEmail(textboxEmail.Text) && validarUsername(textboxUsername.Text) && validarConfirmarPassword(textBoxConfirmPassword.Password) && validarPasswords(textBoxConfirmPassword.Password)){

                Usuario usuario = new Usuario()
                { NombreUsuario = textboxUsername.Text, Correo = textboxEmail.Text,
                    Contrasena = textboxPassword.Password,
                    ConfirmarContrasena = textBoxConfirmPassword.Password,
                    Avatar = "https://i.ibb.co/v1QQ7Kd/profile.png"
                };

                UsuarioController.postUsuario(usuario);

                Frame.Navigate(typeof(Login));

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }


        // Validaciones 
        private Boolean validarUsername (string a)
        {
            if(a==null || a.Equals(""))
            {
                textboxErrorUsername.Text = "El campo username es requerido";
                return false;
            }

            textboxErrorUsername.Text = "";
            return true;
        }

        private Boolean validarPassword(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorPassword.Text = "El campo password es requerido";
                return false;
            }

            textboxErrorPassword.Text = "";
            return true;
        }

        private Boolean validarEmail(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorEmail.Text = "El campo Email es requerido";
                return false;
            }

            textboxErrorEmail.Text = "";
            return true;
        }

        private Boolean validarConfirmarPassword(string a)
        {
            if (a == null || a.Equals(""))
            {
                textBoxErrorConfirmPassword.Text = "El campo Confirm Password es requerido";
                return false;
            }

            textBoxErrorConfirmPassword.Text = "";
            return true;
        }

        private Boolean validarPasswords(string a)
        {
            if (!textBoxConfirmPassword.Password.Equals(textboxPassword.Password))
            {
                textBoxErrorPasswords.Text = "Las contrasenias no coinciden";
                return false;
            }

            textBoxErrorPasswords.Text = "";
            return true;
        }
    }
}
