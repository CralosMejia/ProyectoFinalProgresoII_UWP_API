﻿using AgendaPlusUWP.Models;
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
            userID = 1;
            this.InitializeComponent();
        }

        private async void crearNota(object sender, RoutedEventArgs e)
        {
            if (validarTitulo(textBoxTitle.Text) && validarDescripcion(textBoxDescription.Text))
            {
                Nota nota = new Nota() { Titulo= textBoxTitle.Text , Descripcion= textBoxDescription.Text, UsuarioID=userID};

                var json = JsonConvert.SerializeObject(nota);
                HttpClient httpClient = new HttpClient();
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                await httpClient.PostAsync("https://localhost:44304/api/notas", content);              
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