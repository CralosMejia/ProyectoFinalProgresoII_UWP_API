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
        private static Pendientes task;
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

            //inizializarAPI();

            base.OnNavigatedTo(e);
        }


        //private async void inizializarAPI()
        //{
        //    List<Pendientes> resultado = await NotasController.getNota(userID); ;

        //    nota = resultado.Find(x => x.NotaID == notaID);

        //    textBoxTitle.Text = nota.Titulo.ToString();
        //    textBoxDescription.Text = nota.Descripcion.ToString();

        //}
    }
}
