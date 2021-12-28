﻿using System;
using AgendaPlusUWP.Controlers;
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
using AgendaPlusUWP.Models;
using System.Threading.Tasks;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        public int userID = 1;

        string resultado;

        

        public Login()
        {
            this.InitializeComponent();
        }

        private async void inizializarAPIAsync()
        {
            resultado = await UsuarioController.getUsuario(userID);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (validarCorreo(textboxCorreo.Text) && validarCorreo(textboxContrasena.Password) && validarUsuario())
            {

                Frame.Navigate(typeof(MainPage));

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateAccount));
        }


        //Validaciones

        private Boolean validarCorreo(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorCorreo.Text = "El campo Email es requerido";
                return false;
            }

            textboxErrorCorreo.Text = " ";
            return true;
        }

        private Boolean validarContrasena(string a)
        {
            if (a == null || a.Equals(""))
            {
                textboxErrorContrasena.Text = "El campo Password es requerido";
                return false;
            }

            textboxErrorContrasena.Text = "";
            return true;
        }


        private Boolean validarUsuario()
        {
            inizializarAPIAsync();
            if(resultado == null || resultado.Equals(""))
            {
                textBoxErrorLogin.Text = "Las creedenciales no coinciden";
                return false;
            }

            textBoxErrorLogin.Text = "";
            return true;
        }

        
    }
}