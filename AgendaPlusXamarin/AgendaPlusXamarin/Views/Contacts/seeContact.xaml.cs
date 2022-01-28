using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgendaPlusXamarin.Models;
using AgendaPlusXamarin.Controllers;

namespace AgendaPlusXamarin.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class seeContact : ContentPage
    {
        private int userID;
        private int ContactoID;
        private Contacto contacto;

        public seeContact(int userIDParam, int contactoIDParam)
        {
            ContactoID = contactoIDParam;
            userID = userIDParam;


            InitializeComponent();
            IniciarPage();
        }

        private async void IniciarPage()
        {
            if (ContactoController.getContacto(userID) != null)
            {
                List<Contacto> resultado = await ContactoController.getContacto(userID);
                contacto = resultado.Find(x => x.ContactoID == ContactoID);

                txtName_Contact.Text = contacto.NombreContacto;
                txtEmail_Contacts.Text = contacto.CorreoContacto;
                txtNumber_Contact.Text = contacto.TelefonoContacto;
            }
        }

        private async void closeButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void borrarContacto(object sender, EventArgs e)
        {
            bool alerta = await DisplayAlert("Question?", "Are you sure you want to delete this Contact", "Yes", "No");

            if (alerta)
            {
                contacto.Usuario = null;
                ContactoController.deleteContacto(contacto);
                await Navigation.PopModalAsync();
            }
        }

        private async void editarContacto(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new editContact(userID, ContactoID));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IniciarPage();
        }

    }
}