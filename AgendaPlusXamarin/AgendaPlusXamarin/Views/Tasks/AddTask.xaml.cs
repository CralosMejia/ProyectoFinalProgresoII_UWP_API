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
    public partial class AddTask : ContentPage
    {
        #region Variables
        private static int i = 0;
        private Pendiente pendiente;
        private int userID;
        #endregion

        #region Constructor
        public AddTask(int userID)
        {
            this.userID = userID;
            InitializeComponent();
            setearValoresPorDefecto();
            generarPrioridad();
        }
        #endregion

        #region Valores por defecto
        /// <summary>
        /// metodo que aplica restricciones a los componentes de date picker y time picker
        /// </summary>
        /// <returns> bool </returns>
        private void setearValoresPorDefecto()
        {
            calendarioDL.Date = System.DateTime.Now;
            calendarioDL.MaximumDate = new DateTime(2050, 1, 1);
            calendarioDL.MinimumDate = System.DateTime.Now;

        }

        private void generarPrioridad()
        {
            if (rb_Severe.IsChecked)
            {
                var content = rb_Severe.Content as string;

                if (content != null && content.Equals("Severe"))
                {
                    i = 1;
                }
            }

            if (rb_Important.IsChecked)
            {
                var content = rb_Important.Content as string;

                if (content != null && content.Equals("Important"))
                {
                    i = 2;
                }
            }

            if (rb_Normal.IsChecked)
            {
                var content = rb_Normal.Content as string;

                if (content != null && content.Equals("Normal"))
                {
                    i = 3;
                }
            }
        }
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
            if (txt_Title.Text == null)
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
            if (txt_Description.Text == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        #endregion

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

        private async void btn_AddTask_Clicked(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                pendiente = new Pendiente
                {
                    Titulo = txt_Title.Text,
                    Descripcion = txt_Description.Text,
                    Estado = false,
                    Prioridad = i,
                    FechaLimite =
                 calendarioDL.Date,
                    UsuarioID = userID
                };

                pendiente.calcularPrioridad();
                pendiente.calcularEstado();

                PendienteController.postTask(pendiente);

                await Navigation.PopModalAsync();
            }
            else if (!validarTitulo())
            {
                mostrarCuadroDeDialogoErrorTitle();
            }
            else if (!validarDescripcion())
            {
                mostrarCuadroDeDialogoErrorDesc();
            }
            else
            {
                mostrarCuadroDeDialogoError();
            }
        }

        private async void dispose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #region Cuadros de dialogo

        private async void mostrarCuadroDeDialogoError()
        {
            await DisplayAlert("Error", "Some fields may have not been filled", "Ok");
        }

        private async void mostrarCuadroDeDialogoErrorTitle()
        {
            await DisplayAlert("Error", "Please fill title field", "Ok");
        }

        private async void mostrarCuadroDeDialogoErrorDesc()
        {
            await DisplayAlert("Error", "Please fill description field", "Ok");
        }


        #endregion
    }
}