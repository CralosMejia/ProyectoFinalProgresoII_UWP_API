using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;

namespace AgendaPlusXamarin.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class editContact : ContentPage
    {
        private int userID;
        private int ContactoID;
        private Contacto contacto;

        public editContact(int userIDParam, int contactoIDParam)
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

        private async void closedButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void editarContacto(object sender, EventArgs e)
        {
            if (Validacion())
            {
                contacto.CorreoContacto = txtEmail_Contacts.Text;
                contacto.NombreContacto = txtName_Contact.Text;
                contacto.TelefonoContacto = txtNumber_Contact.Text;
                contacto.Usuario = null;

                ContactoController.putContacto(contacto);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed", "OK");
            }
        }

        private bool Validacion()
        {
            if (!txtEmail_Contacts.Text.Equals("") && !txtName_Contact.Text.Equals("") && !txtNumber_Contact.Text.Equals(""))
            {
                return true;
            }
            return false;
        }


    }
}