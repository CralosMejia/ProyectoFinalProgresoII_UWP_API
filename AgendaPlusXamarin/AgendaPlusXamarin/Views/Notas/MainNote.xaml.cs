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
    public partial class MainNote : ContentPage
    {
        private int userID;
        private List<Nota> resultadoAPI;


        public MainNote(int userIDParam = 1)
        {
            userID = userIDParam;
            InitializeComponent();
            InitApi();
        }


        private async void InitApi()
        {
            if (NotaController.getNota(userID) != null)
            {
                resultadoAPI = await NotaController.getNota(userID);
                ListNotes.ItemsSource = resultadoAPI;
            }
        }



        private async void createNote(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateNote(userID));
        }



        private async void seeNote(object sender, EventArgs e)
        {
            Nota nota = (Nota)ListNotes.SelectedItem;

            if (nota != null)
            {
                await Navigation.PushModalAsync(new SeeNote(nota.UsuarioID, nota.NotaID));
            }
        }

        private void searchEvent(object sender, TextChangedEventArgs e)
        {
            string word = textSearch.Text.ToUpper();

            List<Nota> resultado = resultadoAPI.Where(x => x.Titulo.ToUpper().Contains(word)).ToList();
            ListNotes.ItemsSource = resultado;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitApi();
        }
    }