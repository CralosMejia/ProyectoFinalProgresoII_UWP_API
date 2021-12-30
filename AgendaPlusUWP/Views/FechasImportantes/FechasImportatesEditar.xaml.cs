using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views.FechasImportantes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FechasImportatesEditar : Page
    {
        private static int userID;
        private static int fechaID;
        private static FechasImportante fecha;

        public FechasImportatesEditar()
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
            fechaID = Int32.Parse(lista[1]);

            inizializarAPI();

            base.OnNavigatedTo(e);
        }

        private async void inizializarAPI()
        {
            List<FechasImportante> resultado = await FechasImportantesController.getFecha(userID);

            fecha = resultado.Find(x => x.FechasImportantesID == fechaID);

            textBoxTitle.Text = fecha.Titulo.ToString();
            textBoxDescription.Text = fecha.Descripcion.ToString();
            DatePickerFecha.Date = fecha.FechaLimite;

        }

        private void editarFecha(object sender, RoutedEventArgs e)
        {
            if (validarTitulo(textBoxTitle.Text) && validarDescripcion(textBoxDescription.Text))
            {

                fecha.Titulo = textBoxTitle.Text;
                fecha.Descripcion = textBoxDescription.Text;
                fecha.FechaLimite = DatePickerFecha.Date.Value.DateTime;
                fecha.Usuario = null;

                FechasImportantesController.putNota(fecha);

                Frame.Content = null;
                Frame.Navigate(typeof(FechasImportantesMain), userID);
            }
            else
            {
                validarDescripcion(textBoxDescription.Text);
            }

        }

        private Boolean validarTitulo(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorTitle.Text = "The title field is required.";
                return false;
            }
            textBoxErrorTitle.Text = "";
            return true;
        }

        private Boolean validarDescripcion(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorDescription.Text = "The description field is required.";
                return false;
            }
            textBoxErrorDescription.Text = "";
            return true;
        }
    }
}
