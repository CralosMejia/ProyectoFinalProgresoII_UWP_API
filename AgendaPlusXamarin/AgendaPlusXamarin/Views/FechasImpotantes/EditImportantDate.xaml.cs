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
    public partial class EditImportantDate : ContentPage
    {
        private int userID;
        private int fechaID;
        private FechaImportante fecha;

        public EditImportantDate(int userIDParam, int fechaIDParam)
        {
            fechaID = fechaIDParam;
            userID = userIDParam;

            InitializeComponent();

            datePi.MinimumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            InitPage();

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

        private async void editDate(object sender, EventArgs e)
        {
            if (dateTitle.Text != "" && dateDescription.Text != "")
            {
                fecha.Titulo = dateTitle.Text;
                fecha.Descripcion = dateDescription.Text;
                fecha.Usuario = null;
                fecha.FechaLimite = datePi.Date;

                FechaImportanteController.putfecha(fecha);

                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed.", "OK");
            }
        }

        private async void closedButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}