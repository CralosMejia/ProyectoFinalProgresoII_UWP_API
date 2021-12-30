using AgendaPlusUWP.Controlers;
using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace AgendaPlusUWP.Views.ConfiguracionAbout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Configurations : Page
    {
        private static int userID;
        private static Usuario user;

        public Configurations()
        {
            this.InitializeComponent();
            inizializarAPI();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            base.OnNavigatedTo(e);
        }

        private async void inizializarAPI()
        {


            List<Usuario> resultado =await UsuarioController.getUsuario();

            user = resultado.FirstOrDefault(x => x.UsuarioID == userID);


        }

        private  void cambiarContraseña(object sender, RoutedEventArgs e)
        {
            if (validarPassword(textBoxPassword.Password) && validarConfirmPassword(textBoxConfirmPassword.Password))
            {
                if (validarContenidoPassword(textBoxPassword.Password) && validarContenidoPassword(textBoxConfirmPassword.Password))
                {
                    if (validarIgualdadContraseñas(textBoxPassword.Password, textBoxConfirmPassword.Password))
                    {
                        user.Contrasena = textBoxPassword.Password;
                        user.ConfirmarContrasena = textBoxConfirmPassword.Password;
                        user.FechasImportantes = null;
                        user.Contactos = null;
                        user.Notas = null;
                        user.Pendientes = null;

                        UsuarioController.putTask(user);
                        textBoxErrorGeneral.Text = "la contraseña se modifico con exito";
                    }

                }
                
            }
            else
            {
                validarConfirmPassword(textBoxConfirmPassword.Password);
            }

        }

        //Validaciones.
        private Boolean validarPassword(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorPassword.Text = "El campo password es requerido";
                return false;
            }
            textBoxErrorPassword.Text = "";
            return true;
        }

        private Boolean validarConfirmPassword(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorConfirmPassword.Text = "El campo confirm password es requerido";
                return false;
            }
            textBoxErrorConfirmPassword.Text = "";
            return true;
        }

        private Boolean validarContenidoPassword(string a)
        {
            var expresionRegular = new Regex(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}");
            if (!expresionRegular.IsMatch(a))
            {
                textBoxErrorGeneral.Text = @"Error. Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.";
                return false;
            }
            textBoxErrorGeneral.Text = "";
            return true;
        }

        private Boolean validarIgualdadContraseñas(string a, string b)
        {
            if (!a.Equals(b))
            {
                textBoxErrorGeneral.Text = "las Contraseñas no coinciden";
                return false;
            }
            textBoxErrorGeneral.Text = "";
            return true;
        }

        private void cambiarAvatar(object sender, RoutedEventArgs e)
        {
            if (rb1.IsChecked.Value)
            {
                enviarAvatarAPI("https://i.ibb.co/185gsr0/profile2.png");
                
            }
            else if (rb2.IsChecked.Value)
            {
                enviarAvatarAPI("https://i.ibb.co/R7FzpbR/profile1.png");
            }
            else if (rb3.IsChecked.Value)
            {
                enviarAvatarAPI("https://i.ibb.co/tbcSZhH/profile5.png");
            }
            else if (rb4.IsChecked.Value)
            {
                enviarAvatarAPI("https://i.ibb.co/2vv7GwK/profile4.png");
            }
            else if (rb5.IsChecked.Value)
            {
                enviarAvatarAPI("https://i.ibb.co/v1QQ7Kd/profile.png");
            }
            else if (rb6.IsChecked.Value)
            {
                enviarAvatarAPI("https://i.ibb.co/r3nC5qX/profile3.png");
            }
            else
            {
                textBoxCambiarAvatar.Text = "Seleciona un avatar";
            }

        }

        private void enviarAvatarAPI(string avatar)
        {
            textBoxCambiarAvatar.Text= "Se cambio con exito el avatar";

            user.FechasImportantes = null;
            user.Contactos = null;
            user.Notas = null;
            user.Pendientes = null;
            user.Avatar = avatar;

            UsuarioController.putTask(user);
        }

        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            textBoxCambiarAvatar.Text = "";
        }
    }
}
