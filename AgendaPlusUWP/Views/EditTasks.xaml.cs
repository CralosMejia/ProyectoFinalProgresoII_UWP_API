using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EditTasks : Page
    {
        private static int userID;
        private static int pendienteID;
        private static Pendiente task;
        private static int i;
        public EditTasks()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<string> lista = new List<string>();

            string paramStr = e.Parameter.ToString();

            paramStr = paramStr.Replace("(", string.Empty);
            paramStr = paramStr.Replace(")", string.Empty);

            lista = paramStr.Split(",").ToList();

            userID = Int32.Parse(lista[0]);
            pendienteID = Int32.Parse(lista[1]);

            llenarDatos();

            base.OnNavigatedTo(e);
        }


        private async void llenarDatos()
        {
            List<Pendiente> resultado = await PendientesController.getTasks(userID); ;

            task = resultado.Find(x => x.PendienteID == pendienteID);

            txtTitle.Text = task.Titulo.ToString();
            txtDesc.Text = task.Descripcion.ToString();
            
            calendarioDT.SelectedDate = task.FechaLimite;

            if (task.Prioridad == 1)
            {
                cB_Priority.SelectedIndex = 0;
            }
            else if (task.Prioridad == 2)
            {
                cB_Priority.SelectedIndex = 1;

            }
            else if (task.Prioridad == 3)
            {
                cB_Priority.SelectedIndex = 2;
            }

        }

        private bool validarCampos()
        {
            if (!txtTitle.ToString().Equals(null) && !txtDesc.ToString().Equals(null) && !(cB_Priority.SelectedIndex == -1) && calendarioDT.SelectedDate != null)
                return true;
            else
                return false;
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

            if (validarCampos())
            {
                task.Titulo = txtTitle.Text;
                task.Descripcion = txtDesc.Text;
                task.Prioridad = i;
                task.FechaLimite = calendarioDT.Date.DateTime;
                task.Estado = false;
                task.Usuarios = null;

                PendientesController.putTask(task);

                Frame.Content = null;
                Frame.Navigate(typeof(MainTasks), userID);
            }
            else
            {
                mostrarCuadroDeDialogoError();
            }
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
