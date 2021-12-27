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

        private static int userID;
        private static Usuarios user;

        public MainPage()
        {
            userID = 1;
            
            this.InitializeComponent();
        }

        private async void inizializarAPI()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:44304/api/usuarios");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuarios>>(content);

            user = resultado.FirstOrDefault(x => x.UsuarioID == userID);

            userName.Text = user.NombreUsuario.ToString();
            //rutaAvatar();
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("pendientes", typeof(MainTasks)),
            ("contactos", typeof(AddTasks)),
            ("notas", typeof(EditTasks)),
            ("fechas", typeof(TaskDetails)),
        };

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {


            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);

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



        private void NavView_Navigate(string navItemTag, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
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
                ContentFrame.Navigate(_page, null, transitionInfo);
            }
        }

        private void btn_Info_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Navigate(typeof(ejemplo), null);

        }
    }
}
