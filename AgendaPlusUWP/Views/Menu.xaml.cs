using AgendaPlusUWP.Views.ConfiguracionAbout;
using AgendaPlusUWP.Views.FechasImportantes;
using AgendaPlusUWP.Views.Notes;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPlusUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {

        public Menu()
        {
            this.InitializeComponent();
            inizializarPanel();
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Error al cargar la página" + e.SourcePageType.FullName);
        }

        private void inizializarPanel(String a= "Configurations")
        {
            switch (a)
            {
                case "NotesCreate":
                    ContentFrame.Navigate(typeof(NotesCreate));
                    break;
                case "NotesEdit":
                    ContentFrame.Navigate(typeof(NotesEdit));
                    break;
                case "FechasImportantesMain":
                    ContentFrame.Navigate(typeof(FechasImportantesMain));
                    break;
                case "FechasImportantesCreate":
                    ContentFrame.Navigate(typeof(FechasImportantesCreate));
                    break;
                case "FechasImportatesEditar":
                    ContentFrame.Navigate(typeof(FechasImportatesEditar));
                    break;
                case "Configurations":
                    ContentFrame.Navigate(typeof(Configurations));
                    break;
                default:
                    ContentFrame.Navigate(typeof(NotesMain));
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
            ContentFrame.Navigate(typeof(Configurations));
        }
    }
}
