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
    public sealed partial class NotesCreate : Page
    {
        private static int userID;

        public NotesCreate()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            base.OnNavigatedTo(e);
        }

        private  void crearNota(object sender, RoutedEventArgs e)
        {
            if (validarTitulo(textBoxTitle.Text) && validarDescripcion(textBoxDescription.Text))
            {
                Nota nota = new Nota() { Titulo= textBoxTitle.Text , Descripcion= textBoxDescription.Text, UsuarioID=userID};

                NotasController.postNota(nota);

                Frame.Content = null;
                Frame.Navigate(typeof(NotesMain), userID);
            }
            else
            {
                validarDescripcion(textBoxDescription.Text);
            }

        }


        //Validaciones.
        private Boolean validarTitulo(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorTitle.Text = "The title field is required";
                return false;
            }
            textBoxErrorTitle.Text = "";
            return true;
        }

        private Boolean validarDescripcion(string a)
        {
            if (a.Equals(""))
            {
                textBoxErrorDescription.Text = "The description field is required";
                return false;
            }
            textBoxErrorDescription.Text = "";
            return true;
        }
    }
}
