using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgendaPlusXamarin.Views.Login;
using AgendaPlusXamarin.Views.Contacts;
using AgendaPlusXamarin.Models;
using AgendaPlusXamarin.Controllers;

namespace AgendaPlusXamarin.Views.CreateAccount
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccounts : ContentPage
    {
        private static int userID;
        public CreateAccounts(int userIDParam)
        {
            string IDstr = userIDParam.ToString();

            userID = Int32.Parse(IDstr);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        


        private async void Register(object sender, EventArgs e)
        {
            if (txtUsername.Text != null && txtEmail.Text != null && txtPassword.Text != null && txtConfirmPassword.Text != null && Validacion() == true)
            {
                Usuario usuario = new Usuario()
                { NombreUsuario = txtUsername.Text, Correo = txtEmail.Text, Contrasena = txtPassword.Text, ConfirmarContrasena = txtConfirmPassword.Text, Avatar = "https://i.ibb.co/v1QQ7Kd/profile.png" };

                UsuarioController.postUsuario(usuario);

                await DisplayAlert("Alert", "User created successfully", "OK");

                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed or the Passwords doesn't match", "OK");
            }
        }

        private bool Validacion()
        {
            if (txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}