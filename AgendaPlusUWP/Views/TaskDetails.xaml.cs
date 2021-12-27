using AgendaPlusUWP.Models;
using AgendaPlusUWP.Controllers;
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
    public sealed partial class TaskDetails : Page
    {
        private static int userID;
        private static int pendienteID;
        private static Pendientes task;
        public TaskDetails()
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
            List<Pendientes> resultado = await PendientesController.getTasks(userID); ;

            task = resultado.Find(x => x.PendienteID == pendienteID);

            txtTitle.Text = task.Titulo.ToString();
            txtDesc.Text = task.Titulo.ToString();

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

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = null;
            Frame.Navigate(typeof(MainTasks), userID);
        }
    }
}
