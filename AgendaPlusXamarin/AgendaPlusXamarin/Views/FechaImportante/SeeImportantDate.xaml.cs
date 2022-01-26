using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaPlusXamarin.Views.FechaImportante
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeImportantDate : ContentPage
    {
        private int userID;
        private int fechaID;
        private FechaImportante fecha;

        public SeeImportantDate(int userIDParam, int fechaIDParam)
        {
            fechaID = fechaIDParam;
            userID = userIDParam;

            InitializeComponent();

            InitPage();

            datePi.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        }

        private async void InitPage()
        {
            if (FechaImportanteController.getFecha(userID) != null)
            {
                List<FechaImportante> resultado = await FechaImportanteController.getFecha(userID);
                fecha = resultado.Find(x => x.FechasImportantesID == fechaID);

                dateTitle.Text = fecha.Titulo;
                dateDescription.Text = fecha.Descripcion;
                datePi.Date = fecha.FechaLimite;
            }
        }

        private async void closedButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void editImportantDate(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditImportantDate(userID, fecha.FechasImportantesID));
        }

        private async void deleteDate(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("Question?", "Are you sure you want to delete the date?", "Yes", "No");

            if (answer)
            {
                fecha.Usuario = null;

                FechaImportanteController.deleteFecha(fecha);

                await Navigation.PopModalAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitPage();
        }
    }
}