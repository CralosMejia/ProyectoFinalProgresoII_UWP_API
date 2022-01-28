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
    public partial class createContact : ContentPage
    {
        private Contacto contacto;
        private int userID;
        public createContact(int userIDParam)
        {
            userID = userIDParam;
            InitializeComponent();
        }


        private async void btn_crear_Clicked(object sender, EventArgs e)
        {
            String txtemail = txtEmail_Contacts.Text;
            String txtname = txtName_Contact.Text;
            String txtnumber = txtNumber_Contact.Text;


            if (txtEmail_Contacts.Text != null && txtName_Contact.Text != null && txtNumber_Contact.Text != null)
            {
                contacto = new Contacto { NombreContacto = txtName_Contact.Text, CorreoContacto = txtEmail_Contacts.Text, TelefonoContacto = txtNumber_Contact.Text, UsuarioID = userID };
                ContactoController.postContacto(contacto);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed", "OK");
            }
        }
    }
}