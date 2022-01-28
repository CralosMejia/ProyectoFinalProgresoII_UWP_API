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
    public partial class mainContact : ContentPage
    {

        private int userID;
        private List<Contacto> resultadoAPI;

        public mainContact(int userIDParam)
        {
            string IDstr = userIDParam.ToString();

            userID = Int32.Parse(IDstr);
            InitializeComponent();
            IniciarApi();
        }

        private async void IniciarApi()
        {
            if (ContactoController.getContacto(userID) != null)
            {
                resultadoAPI = await ContactoController.getContacto(userID);
                ListContacts.ItemsSource = resultadoAPI;
            }
        }

        private async void newContact(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new createContact(userID));
        }

        private void searchContactEvent(object sender, TextChangedEventArgs e)
        {
            string word = SearchContact.Text.ToUpper();

            List<Contacto> resultado = resultadoAPI.Where(x => x.NombreContacto.ToUpper().Contains(word)).ToList();
            ListContacts.ItemsSource = resultado;
        }

        private async void seeContact(object sender, EventArgs e)
        {
            Contacto contacto = (Contacto)ListContacts.SelectedItem;
            if (contacto != null)
            {
                await Navigation.PushModalAsync(new seeContact(contacto.UsuarioID, contacto.ContactoID));
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IniciarApi();
        }


    }
}