using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class AddTasks : Page
    {
        private static int userID;

        private static int i;

        public AddTasks()
        {
            this.InitializeComponent();
            setearValoresPorDefecto();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// metodo que aplica restricciones a los componentes de date picker y time picker
        /// </summary>
        /// <returns> bool </returns>
        private void setearValoresPorDefecto()
        {
            calendarioDL.SelectedDate = new DateTimeOffset(new DateTime(2022, 1, 1));
            calendarioDL.MinYear = new DateTimeOffset(new DateTime(2022, 1, 1));
            calendarioDL.MaxYear = new DateTimeOffset(new DateTime(2030, 1, 1));

        }

        private  void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {
            //DateTime tiempo = Convert.ToDateTime(tiempoDL.ToString());

            if (validarCampos())
            {
                Pendiente newTask = new Pendiente()
                {
                    Titulo = txt_Title.Text,
                    Descripcion = txt_Description.Text,
                    UsuarioID = userID,
                    Estado = false,
                    Prioridad = i,
                    FechaLimite = calendarioDL.Date.DateTime
                };

                newTask.calcularPrioridad();
                newTask.calcularEstado();

                PendientesController.postTask(newTask);


                Frame.Content = null;
                Frame.Navigate(typeof(MainTasks), userID);

            }else if (!validarTitulo())
            {
                mostrarCuadroDeDialogoErrorTitulo();
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

        /// <summary>
        /// metodo que valida los campos de la pagina AddTasks
        /// </summary>
        /// <returns> bool </returns>
        private bool validarCampos()
        {
            if (validarTitulo() && validarDescripcion() && !(cB_Priority.SelectedIndex == -1) && calendarioDL.SelectedDate != null)
                return true;
            else
                return false;
        }

        private async void mostrarCuadroDeDialogoError()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Error",
                Content = "Some fields may have not been filled",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private async void mostrarCuadroDeDialogoErrorTitulo()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Error",
                Content = "Please fill title field",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private async void mostrarCuadroDeDialogoErrorDesc()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Error",
                Content = "Please fill description field",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        private bool validarTitulo()
        {
            if (txt_Title.Text.ToString().Equals(""))
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
            if (txt_Description.Text.ToString().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void cB_Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBoxItem = e.AddedItems[0] as ComboBoxItem;

            if (comboBoxItem == null) return;

            var content = comboBoxItem.Content as string;

            if (content != null && content.Equals("Severe"))
            {
                i = 1;
            }
            else if (content != null && content.Equals("Important"))
            {
                i = 2;
            }
            else if (content != null && content.Equals("Normal"))
            {
                i = 3;
            }


        }

    }
}
