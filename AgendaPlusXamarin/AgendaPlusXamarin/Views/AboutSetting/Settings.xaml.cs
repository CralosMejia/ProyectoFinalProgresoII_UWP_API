using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaPlusXamarin.Views.AboutSetting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private int userID;
        private static Usuario user;

        public Settings(int userIDParam = 1)
        {
            userID = userIDParam;
            InitializeComponent();
            InitAPI();

        }

        private async void InitAPI()
        {
            List<Usuario> resultado = await UsuarioController.getUsuario();

            user = resultado.FirstOrDefault(x => x.UsuarioID == userID);
        }

        private async void changePassword(object sender, EventArgs e)
        {
            if (textPassword.Text != null && textConfirmPassword.Text != null)
            {
                if (validarIgualdadContraseñas(textPassword.Text, textConfirmPassword.Text))
                {
                    if (validarContenidoPassword(textPassword.Text))
                    {
                        user.Contrasena = textPassword.Text;
                        user.ConfirmarContrasena = textConfirmPassword.Text;
                        user.FechasImportantes = null;
                        user.Contactos = null;
                        user.Notas = null;
                        user.Pendientes = null;

                        UsuarioController.putTask(user);
                        await DisplayAlert("Alert", "password changed successfully", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Alert", @"Error.Password must have one capital, one special character and one numerical character.It can not start with a special character or a digit.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Alert", "The passwords doesn't match.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed.", "OK");
            }
        }


        private Boolean validarIgualdadContraseñas(string a, string b)
        {
            if (!a.Equals(b))
            {
                return false;
            }
            return true;
        }

        private Boolean validarContenidoPassword(string a)
        {
            var expresionRegular = new Regex(@"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}");
            if (!expresionRegular.IsMatch(a))
            {
                return false;
            }
            return true;
        }

        private async void cambiarAvatar(object sender, EventArgs e)
        {
            if (rb1.IsChecked)
            {
                enviarAvatarAPI("https://i.ibb.co/185gsr0/profile2.png");

            }
            else if (rb2.IsChecked)
            {
                enviarAvatarAPI("https://i.ibb.co/R7FzpbR/profile1.png");
            }
            else if (rb3.IsChecked)
            {
                enviarAvatarAPI("https://i.ibb.co/tbcSZhH/profile5.png");
            }
            else if (rb4.IsChecked)
            {
                enviarAvatarAPI("https://i.ibb.co/2vv7GwK/profile4.png");
            }
            else if (rb5.IsChecked)
            {
                enviarAvatarAPI("https://i.ibb.co/v1QQ7Kd/profile.png");
            }
            else if (rb6.IsChecked)
            {
                enviarAvatarAPI("https://i.ibb.co/r3nC5qX/profile3.png");
            }
            else
            {
                await DisplayAlert("Alert", "You must be select an avatar", "OK");
            }

        }

        private async void enviarAvatarAPI(string avatar)
        {
            await DisplayAlert("Alert", "Successfully changed avatar", "OK");

            user.FechasImportantes = null;
            user.Contactos = null;
            user.Notas = null;
            user.Pendientes = null;
            user.Avatar = avatar;

            UsuarioController.putTask(user);
        }
    }
}