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

        private void inizializarPanel(String a= "")
        {
            switch (a)
            {
                case "NotesCreate":
                    ContentFrame.Navigate(typeof(NotesCreate));
                    break;
                case "NotesEdit":
                    ContentFrame.Navigate(typeof(NotesEdit));
                    break;
                default:
                    ContentFrame.Navigate(typeof(NotesMain));
                    break;
            }
        }
    }
}
