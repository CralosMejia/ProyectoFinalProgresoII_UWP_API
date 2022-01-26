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
    public partial class EditNote : ContentPage
    {

        private int userID;
        private int notaID;
        private Nota nota;

        public EditNote(int userIDParam, int notaIDParam)
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

        private async void editNote(object sender, EventArgs e)
        {
            if (fieldValidation())
            {
                nota.Titulo = noteTitle.Text;
                nota.Descripcion = noteDescription.Text;
                nota.Usuario = null;

                NotaController.putNota(nota);

                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed.", "OK");
            }
        }

        private bool fieldValidation()
        {
            if (!noteTitle.Text.Equals("") && !noteDescription.Text.Equals(""))
            {
                return true;
            }
            return false;
        }
    }