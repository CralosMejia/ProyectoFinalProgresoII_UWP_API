using System;
using AgendaPlusUWP.Controlers;
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
using AgendaPlusUWP.Models;
using System.Threading.Tasks;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        public int userID;

        List<Usuario> resultado;

        Usuario user;

        

        public Login()
        {
            this.InitializeComponent();
            inizializarAPIAsync();
        }

        private async void inizializarAPIAsync()
        {
            resultado = await UsuarioController.getUsuario();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (validarCorreo(textboxCorreo.Text) && validarContrasena(textboxContrasena.Password) && validarUsuario())
            {

                Frame.Navigate(typeof(Menu),userID);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccount));
        }


        //Validaciones

        private Boolean validarCorreo(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorCorreo.Text = "The email field is required";
                return false;
            }

            textboxErrorCorreo.Text = " ";
            return true;
        }

        private Boolean validarContrasena(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorContrasena.Text = "The password field is required";
                return false;
            }

            textboxErrorContrasena.Text = "";
            return true;
        }


        private  Boolean validarUsuario()
        {
            user = resultado.Find(x => x.Correo.Equals(textboxCorreo.Text) && x.Contrasena.Equals(textboxContrasena.Password));
            
            if (user == null)
            {
                textBoxErrorLogin.Text = "The credentials doesn't match";
                return false;
            }

            textBoxErrorLogin.Text = "";
            userID = user.UsuarioID;
            return true;
        }

        
    }
}
