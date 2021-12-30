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

        List<Usuario> resultado;

        public CreateAccount()
        {
            this.InitializeComponent();
            inizializarAPIAsync();


        }

        private async void inizializarAPIAsync()
        {
            resultado = await UsuarioController.getUsuario();

        }


        public void Button_Click(object sender, RoutedEventArgs e)
        {

            if (validarPassword(textboxPassword.Password) && validarEmail(textboxEmail.Text) && validarUsername(textboxUsername.Text) && validarConfirmarPassword(textBoxConfirmPassword.Password) && validarPasswords(textBoxConfirmPassword.Password) && validarExistenciaUsername())
            {

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
                textboxErrorUsername.Text = "Username field is required";
                return false;
            }

            textboxErrorUsername.Text = "";
            return true;
        }

        private Boolean validarPassword(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorPassword.Text = "The password field is requiredo";
                return false;
            }

            textboxErrorPassword.Text = "";
            return true;
        }

        private Boolean validarEmail(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorEmail.Text = "The email field is required";
                return false;
            }

            textboxErrorEmail.Text = "";
            return true;
        }

        private Boolean validarConfirmarPassword(string a)
        {
            if (a == null || a.Equals(""))
            {
                textBoxErrorConfirmPassword.Text = "The confirm password field is required";
                return false;
            }

            textBoxErrorConfirmPassword.Text = "";
            return true;
        }

        private Boolean validarPasswords(string a)
        {
            if (!textBoxConfirmPassword.Password.Equals(textboxPassword.Password))
            {
                textBoxErrorPasswords.Text = "The passwords doesn't match";
                return false;
            }

            textBoxErrorPasswords.Text = "";
            return true;
        }

        private Boolean validarExistenciaUsername()
        {

            Usuario UsuarioValidacion = resultado.Find(x => x.Correo.Equals(textboxUsername.Text));

            if (UsuarioValidacion != null)
            {
                textboxErrorUsername.Text = "The username already exists";
                return false;
            }

            textboxErrorUsername.Text = "";
            return true;
        }
    }
}
