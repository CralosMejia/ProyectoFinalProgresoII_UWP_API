using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;
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
    public partial class SeeNote : ContentPage
    {
        private int userID;
        private int notaID;
        private Nota nota;

        public SeeNote(int userIDParam, int notaIDParam)
        {
            notaID = notaIDParam;
            userID = userIDParam;

            InitializeComponent();

            InitPage();
        }

        private async void InitPage()
        {

            if (NotaController.getNota(userID) != null)
            {
                List<Nota> resultado = await NotaController.getNota(userID);
                nota = resultado.Find(x => x.NotaID == notaID);

                noteTitle.Text = nota.Titulo;
                noteDescription.Text = nota.Descripcion;
            }
        }

        private async void closedButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void deleteNote(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Are you sure you want to delete the note?", "Yes", "No");

            if (answer)
            {
                nota.Usuario = null;

                NotaController.deleteNota(nota);

                await Navigation.PopModalAsync();
            }
        }

        private async void editNote(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditNote(userID, notaID));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitPage();
        }
    }
}