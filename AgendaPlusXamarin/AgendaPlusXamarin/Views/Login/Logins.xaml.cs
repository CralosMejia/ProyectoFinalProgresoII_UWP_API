using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgendaPlusXamarin.Views.CreateAccount;
using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;
using AgendaPlusXamarin.Views.Contacts;

namespace AgendaPlusXamarin.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logins : ContentPage
    {
        public int userID;

        List<Usuario> resultado;

        Usuario user;

        public Logins()
        {

            InitializeComponent();
            iniciarApi();
        }

        private async void crearCuenta(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccounts(userID));

        }


        private async void iniciarApi()
        {
            resultado = await UsuarioController.getUsuario();
        }

        private async void Login(object sender, EventArgs e)
        {
            if (txtEmail.Text != null && txtPassword.Text != null && validarUsuario())
            {
                await Navigation.PushAsync(new mainContact(userID));
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed", "OK");
            }
        }


        private bool validarUsuario()
        {
            user = resultado.Find(x => x.Correo.Equals(txtEmail.Text) && x.Contrasena.Equals(txtPassword.Text));
            if (user == null)
            {

                return false;
            }
            userID = user.UsuarioID;
            return true;
        }


    }
}