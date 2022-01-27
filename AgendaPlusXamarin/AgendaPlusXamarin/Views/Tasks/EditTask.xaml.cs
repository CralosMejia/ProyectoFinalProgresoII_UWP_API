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
    public partial class EditTask : ContentPage
    {
        #region Variables
        private int userID;
        private int pendienteID;
        private Pendiente task;
        private static int i;
        #endregion

        #region Constructor
        public EditTask(int userID, int pendienteID)
        {
            this.userID = userID;
            this.pendienteID = pendienteID;

            InitializeComponent();

            llenarDatos();
        }
        #endregion

        #region Handlers
        private async void llenarDatos()
        {
            List<Pendiente> resultado = await PendienteController.getTasks(userID); ;

            task = resultado.Find(x => x.PendienteID == pendienteID);

            txtTitle.Text = task.Titulo.ToString();
            txtDescription.Text = task.Descripcion.ToString();

            calendarioDL.Date = task.FechaLimite;

            if (task.Prioridad == 1)
            {
                rb_Severe.IsChecked = true;
            }
            else if (task.Prioridad == 2)
            {
                rb_Important.IsChecked = true;

            }
            else if (task.Prioridad == 3)
            {
                rb_Normal.IsChecked = true;
            }

            if (task.Estado)
            {
                rb_Done.IsChecked = true;
            }
            else if (!task.Estado)
            {
                rb_Pending.IsChecked = true;
            }
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                task.Titulo = txtTitle.Text;
                task.Descripcion = txtDescription.Text;
                task.Prioridad = i;
                task.FechaLimite = calendarioDL.Date;
                task.Usuarios = null;
                task.calcularPrioridad();

                //task.Estado = false;

                if (rb_Done.IsChecked)
                {
                    task.Estado = true;
                }
                else if (rb_Pending.IsChecked)
                {
                    task.Estado = false;
                }
                else
                {
                    task.Estado = false;
                }

                task.Usuarios = null;

                PendienteController.putTask(task);

                await Navigation.PopModalAsync();
                //Frame.Content = null;
                //Frame.Navigate(typeof(MainTasks), userID);
            }
            else
            {
                await DisplayAlert("Alert", "All fields must be completed.", "OK");
            }
        }
        #region Handlers
        private void rb_Severe_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (rb_Severe.IsChecked)
            {
                var content = rb_Severe.Content as string;

                if (content != null && content.Equals("Severe"))
                {
                    i = 1;
                }
            }

        }



        private void rb_Important_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (rb_Important.IsChecked)
            {
                var content = rb_Important.Content as string;

                if (content != null && content.Equals("Important"))
                {
                    i = 2;
                }
            }
        }

        private void rb_Normal_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (rb_Normal.IsChecked)
            {
                var content = rb_Normal.Content as string;

                if (content != null && content.Equals("Normal"))
                {
                    i = 3;
                }
            }
        }

        private async void dispose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #endregion

        #region Validacion de campos
        /// <summary>
        /// metodo que valida los campos de la pagina AddTasks
        /// </summary>
        /// <returns> bool </returns>
        private bool validarCampos()
        {
            if (validarTitulo() && validarDescripcion() && calendarioDL.Date != null && i != 0)
            {
                if (rb_Important.IsChecked || rb_Severe.IsChecked || rb_Normal.IsChecked)
                {


                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
                return false;
        }
        private bool validarTitulo()
        {
            if (txtTitle.Text == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool validarDescripcion()
        {
            if (txtDescription.Text == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        #endregion
    }
}