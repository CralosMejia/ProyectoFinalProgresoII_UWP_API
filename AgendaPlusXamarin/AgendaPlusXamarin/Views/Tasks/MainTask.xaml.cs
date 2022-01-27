using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaPlusXamarin.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTask : ContentPage
    {
        #region Variables
        private int userID;
        private List<Pendiente> resulAPI;
        public string show = "Default";
        public string orderBy = "Default";

        #endregion

        #region Constructor
        public MainTask(int userID = 1)
        {
            this.userID = userID;
            InitializeComponent();

            if (this.show != "Default" || this.orderBy != "Default")
            {
                HandleOrderBySettings();
                HandleShowSettings();
            }
            else
            {
                llamarAPI();
            }
        }
        #endregion

        #region API
        private async void llamarAPI()
        {
            if (PendienteController.getTasks(userID) != null)
            {
                resulAPI = await PendienteController.getTasks(userID);
                ListaPendientes.ItemsSource = resulAPI;
            }
        }

        #endregion

        #region Handlers
        private void buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = buscar.Text.ToUpper();

            List<Pendiente> resultado = resulAPI.Where(x => x.Titulo.ToUpper().Contains(word)).ToList();
            ListaPendientes.ItemsSource = resultado;
        }

        private async void btnFiltrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalMainTaskSettings(this));
        }

        private async void ListaPendientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Pendiente pendienteSeleccionado = (Pendiente)ListaPendientes.SelectedItem;

            if (pendienteSeleccionado != null)
            {

                var pendiente = (Pendiente)ListaPendientes.SelectedItem;

                bool answer = await DisplayAlert("Question", "Would you like to see " +
                    $"{pendiente.Titulo.ToUpper()}" + " details?", "Yes", "No");

                if (answer)
                    await Navigation.PushModalAsync(new DetailTask(pendienteSeleccionado.UsuarioID, pendienteSeleccionado.PendienteID));
            }

        }

        private async void btn_AddPendiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTask(userID));
        }

        public void SetSettings(string show, string orderBy)
        {

            switch (show)
            {
                case "Default":
                    this.show = "Default";
                    break;
                case "Pending tasks":
                    this.show = "Pending tasks";
                    break;
                case "Done tasks":
                    this.show = "Done tasks";
                    break;
            }

            switch (orderBy)
            {
                case "Default":
                    this.orderBy = "Default";
                    break;
                case "Priority":

                    this.orderBy = "Priority";
                    break;

            }
        }

        private void HandleOrderBySettings()
        {

            if (this.orderBy.Equals("Priority"))
            {
                List<Pendiente> resultado = resulAPI.OrderBy(x => x.Prioridad).ToList();

                ListaPendientes.ItemsSource = resultado;
            }
            else if (this.orderBy.Equals("Default") && this.show.Equals("Pending tasks"))
            {
                List<Pendiente> resultado = resulAPI.Where(x => !x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;

            }
            else if (this.orderBy.Equals("Default") && this.show.Equals("Done tasks"))
            {
                List<Pendiente> resultado2 = resulAPI.Where(x => x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado2;
            }
            else if (this.orderBy.Equals("Priority") && this.show.Equals("Pending tasks"))
            {
                List<Pendiente> resultado = resulAPI.OrderBy(x => x.Prioridad).ToList();

                resultado = resulAPI.Where(x => !x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;

            }
            else if (this.orderBy.Equals("Priority") && this.show.Equals("Done tasks"))
            {
                List<Pendiente> resultado = resulAPI.OrderBy(x => x.Prioridad).ToList();

                resultado = resulAPI.Where(x => x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;
            }


        }
        private void HandleShowSettings()
        {
            if (this.show.Equals("Pending tasks"))
            {
                List<Pendiente> resultado = resulAPI.Where(x => !x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado;
            }
            else if (this.show.Equals("Done tasks"))
            {
                List<Pendiente> resultado2 = resulAPI.Where(x => x.Estado).ToList();

                ListaPendientes.ItemsSource = resultado2;
            }
            else if (this.show.Equals("Default") && this.orderBy.Equals("Priority"))
            {
                List<Pendiente> resultado = resulAPI.OrderBy(x => x.Prioridad).ToList();
                ListaPendientes.ItemsSource = resultado;
            }
            else if (this.show.Equals("Pending") && this.orderBy.Equals("Priority"))
            {
                List<Pendiente> resultado = resulAPI.Where(x => !x.Estado).ToList();

                resultado = resulAPI.OrderBy(x => x.Prioridad).ToList();


                ListaPendientes.ItemsSource = resultado;
            }

        }



        #endregion
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.show != "Default" || this.orderBy != "Default")
            {
                HandleOrderBySettings();
                HandleShowSettings();
            }
            else
            {
                llamarAPI();
            }


        }


    }
}