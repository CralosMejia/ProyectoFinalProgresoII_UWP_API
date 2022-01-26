using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaPlusXamarin.Views.Notas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNote : ContentPage
    {
        private Nota nota;
        private int userID;

        public CreateNote(int userIDParam)
        {
            userID = userIDParam;
            InitializeComponent();
        }

        private async void closedButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void createNote(object sender, EventArgs e)
        {

            if (noteDescription.Text != null && noteTitle.Text != null)
            {
                nota = new Nota { Titulo = noteTitle.Text, Descripcion = noteDescription.Text, UsuarioID = userID };

                NotaController.postNota(nota);

                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed.", "OK");
            }
        }
    }
}