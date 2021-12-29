using AgendaPlusUWP.Controllers;
using AgendaPlusUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace AgendaPlusUWP.Views.Notes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesEdit : Page
    {
        private static int userID;
        private static int notaID;
        private static Nota nota;


        public NotesEdit()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<string> lista = new List<string>();

            string paramStr =e.Parameter.ToString();

            paramStr = paramStr.Replace("(", string.Empty);
            paramStr = paramStr.Replace(")", string.Empty);

            lista = paramStr.Split(",").ToList();

            userID = Int32.Parse(lista[0]);
            notaID = Int32.Parse(lista[1]);

            inizializarAPI();

            base.OnNavigatedTo(e);
        }

        private async void inizializarAPI()
        {
            List<Nota> resultado= await NotasController.getNota(userID); ;

            nota = resultado.Find(x=>x.NotaID == notaID);

            textBoxTitle.Text = nota.Titulo.ToString();
            textBoxDescription.Text=nota.Descripcion.ToString();

        }

        private void editarNota(object sender, RoutedEventArgs e)
        {
            if (validarTitulo(textBoxTitle.Text) && validarDescripcion(textBoxDescription.Text))
            {
                
                nota.Titulo = textBoxTitle.Text;
                nota.Descripcion = textBoxDescription.Text;
                nota.Usuario = null;

                NotasController.putNota(nota);

                Frame.Content = null;
                Frame.Navigate(typeof(NotesMain), userID);

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
                textBoxErrorTitle.Text = "El campo titulo es requerido";
                return false;
            }
            textBoxErrorTitle.Text = "";
            return true;
        }

        private Boolean validarDescripcion(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorDescription.Text = "El campo decripcion es requerido";
                return false;
            }
            textBoxErrorDescription.Text = "";
            return true;
        }
    }
}
