using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaPlusXamarin.Views.FechasImpotantes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainImportantDate : ContentPage
    {
        private List<FechaImportante> resultadoAPI;
        private int userID;

        public MainImportantDate(int userIDParam = 1)
        {
            userID = userIDParam;
            InitializeComponent();
            InitApi();
        }

        private async void InitApi()
        {
            if (FechaImportanteController.getFecha(userID) != null)
            {
                resultadoAPI = await FechaImportanteController.getFecha(userID);
                ListImportantDate.ItemsSource = resultadoAPI;
            }
        }

        private async void createImportantDate(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CreateImportantDate(userID));
        }

        private async void seeImportantDate(object sender, EventArgs e)
        {

            FechaImportante fecha = (FechaImportante)ListImportantDate.SelectedItem;

            if (fecha != null)
            {
                await Navigation.PushModalAsync(new SeeImportantDate(userID, fecha.FechasImportantesID));
            }
        }

        private void searchEvent(object sender, TextChangedEventArgs e)
        {
            string word = textSearch.Text.ToUpper();

            List<FechaImportante> resultado = resultadoAPI.Where(x => x.Titulo.ToUpper().Contains(word)).ToList();
            ListImportantDate.ItemsSource = resultado;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitApi();
        }
    }
}