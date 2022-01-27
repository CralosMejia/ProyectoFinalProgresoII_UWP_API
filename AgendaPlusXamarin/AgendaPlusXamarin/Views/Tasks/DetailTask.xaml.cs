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
    public partial class DetailTask : ContentPage
    {
        #region Variables
        private int userID;
        private int pendienteID;
        private Pendiente task; //este es el pendiente que se maneja actualmente
        private Pendiente editedTask;
        #endregion

        #region Constructor
        public DetailTask(int userID, int pendienteID)
        {
            this.userID = userID;
            this.pendienteID = pendienteID;
            InitializeComponent();
            CargarDatos();
        }
        #endregion


        #region Handlers
        private async void CargarDatos()
        {
            if (PendienteController.getTasks(userID) != null)
            {
                List<Pendiente> resultado = await PendienteController.getTasks(userID);
                task = resultado.Find(x => x.PendienteID == pendienteID);

                task.calcularEstado();
                task.calcularPrioridad();

                taskTitle.Text = task.Titulo;
                taskDesc.Text = task.Descripcion;
                taskPriority.Text = task.PrioridadString;
                taskDeadline.Date = task.FechaLimite;
                taskState.Text = task.EstadoString;

            }
        }

        private async void deleteBtn_Clicked(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("Question", "Would you like to delete the current task?", "Yes", "No");

            if (answer)
            {
                task.Usuarios = null;

                PendienteController.deleteTask(task);

                await Navigation.PopModalAsync();
            }
            else
            {
                mostrarCuadroDeDialogo();
            }


        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditTask(userID, pendienteID));
        }

        private async void doneBtn_Clicked(object sender, EventArgs e)
        {


            if (task != null && !task.Estado)
            {
                List<Pendiente> resultado = await PendienteController.getTasks(userID);

                editedTask = resultado.Find(x => x.PendienteID == task.PendienteID);

                editedTask.Titulo = task.Titulo;
                editedTask.Descripcion = task.Descripcion;
                editedTask.Prioridad = task.Prioridad;
                editedTask.FechaLimite = task.FechaLimite;
                editedTask.Usuarios = null;
                editedTask.Estado = true;

                PendienteController.putTask(editedTask);
                CargarDatos();

            }
            else
            {
                mostrarCuadroDeDialogo2();
            }
        }
        private async void dispose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        #endregion


        #region Cuadros de dialogo
        private async void mostrarCuadroDeDialogo()
        {
            await DisplayAlert("Alert", "Current task has not been deleted", "Ok");
        }
        private async void mostrarCuadroDeDialogo2()
        {
            await DisplayAlert("Alert", "Task already done", "Ok");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarDatos();
        }
        #endregion
    }
}