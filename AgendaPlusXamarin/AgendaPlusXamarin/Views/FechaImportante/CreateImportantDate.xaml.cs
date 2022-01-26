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
    public partial class CreateImportantDate : ContentPage
    {
        private FechaImportante fecha;
        private int userID;

        public CreateImportantDate(int userIDParam)
        {
            userID = userIDParam;
            InitializeComponent();
            datePi.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        private async void closedButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void createDate(object sender, EventArgs e)
        {

            if (dateDescription.Text != null && dateTitle.Text != null)
            {
                fecha = new FechaImportante { Titulo = dateTitle.Text, Descripcion = dateDescription.Text, UsuarioID = userID, FechaLimite = datePi.Date };

                FechaImportanteController.postFecha(fecha);

                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed.", "OK");
            }
        }
    }
}