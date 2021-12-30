using AgendaPlusUWP.Models;
using AgendaPlusUWP.Views.ConfiguracionAbout;
using AgendaPlusUWP.Views.Contactos;
using AgendaPlusUWP.Views.FechasImportantes;
using AgendaPlusUWP.Views.Notes;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        private static int userID;
        private static Usuario user;

        public Menu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string IDstr = e.Parameter.ToString();

            userID = Int32.Parse(IDstr);

            inizializarAPI();

            base.OnNavigatedTo(e);
        }

        private async void inizializarAPI()
        {
            var httpHandler = new HttpClientHandler();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://localhost:44304/api/usuario");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");

            var client = new HttpClient(httpHandler);

            HttpResponseMessage response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<List<Usuario>>(content);

            user = resultado.FirstOrDefault(x => x.UsuarioID == userID);

            userName.Text = user.NombreUsuario.ToString();
            rutaAvatar();
        }

        private void rutaAvatar()
        {
            switch (user.Avatar)
            {
                case "https://i.ibb.co/185gsr0/profile2.png":
                    AvatarIMG.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar.png"));
                    break;
                case "https://i.ibb.co/R7FzpbR/profile1.png":
                    AvatarIMG.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar1.png"));
                    break;
                case "https://i.ibb.co/tbcSZhH/profile5.png":
                    AvatarIMG.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar2.png"));
                    break;
                case "https://i.ibb.co/2vv7GwK/profile4.png":
                    AvatarIMG.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar3.png"));
                    break;
                case "https://i.ibb.co/v1QQ7Kd/profile.png":
                    AvatarIMG.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar4.png"));
                    break;
                case "https://i.ibb.co/r3nC5qX/profile3.png":
                    AvatarIMG.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar5.png"));
                    break;
                default:
                    AvatarIMG.Source= new BitmapImage(new Uri(base.BaseUri, @"/Assets/Avatar/avatar4.png"));
                    break;
            }
        }
       

        private void Button_About(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = null;
            ContentFrame.Navigate(typeof(About));
        }

        private void Button_Configurations(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = null;
            ContentFrame.Navigate(typeof(Configurations), userID);
        }

        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(MainTasks), userID);
        }


        //------------------------------------navegacion
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Error al cargar la página" + e.SourcePageType.FullName);
        }


        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("pendientes", typeof(MainTasks)),
            ("contactos", typeof(ContactoMain)),
            ("notas", typeof(NotesMain)),
            ("fechas", typeof(FechasImportantesMain))
        };

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {


            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);

            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            //navegar por defecto a pagina principal pendientes
            NavView.SelectedItem = NavView.MenuItems[0];

            NavView_Navigate("pendientes", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
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
                ContentFrame.Navigate(_page, userID, transitionInfo);
            }
        }

        private void salir(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
