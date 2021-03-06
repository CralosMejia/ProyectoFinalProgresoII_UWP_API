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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private int userID;
        private static Usuario user;

        public MainPage()
        {
            userID = 1;
            this.InitializeComponent();
            inizializarAPI();
        }

        private async void inizializarAPI()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:44386/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            user = resultado.FirstOrDefault(x => x.UsuarioID == userID);

            userName.Text = user.NombreUsuario.ToString();
            //rutaAvatar();
        }


        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            //aqui se cambiaria segun la pagina a la que se desee navegar
            ("pendientes", typeof(MainTasks)),
            ("contactos", null),
            ("notas", null),
            ("fechas", null),
        };

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, userID);

            }
        }

        //private void NavView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //navegar por defecto a pagina principal pendientes
        //    NavView.SelectedItem = NavView.MenuItems[0];

        //    NavView_Navigate("pendientes", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        //}

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Error al cargar la página" + e.SourcePageType.FullName);
        }



        private void NavView_Navigate(string navItemTag, int userID)
        {
            Type _page = null;


            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            _page = item.Page;

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null) && !Type.Equals(preNavPageType, _page))
            {
                ContentFrame.Navigate(_page, userID);
            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            //navegar por defecto a pagina principal pendientes
            //NavView.SelectedItem = NavView.MenuItems[0];
            
           // ContentFrame.Navigate(typeof(MainTasks), userID);
                       
        }

        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(MainTasks), userID);
        }
    }
}
