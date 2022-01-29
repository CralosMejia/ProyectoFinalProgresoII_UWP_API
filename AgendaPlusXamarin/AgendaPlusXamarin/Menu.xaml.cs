using AgendaPlusXamarin.Controllers;
using AgendaPlusXamarin.Models;
using AgendaPlusXamarin.Views.AboutSetting;
using AgendaPlusXamarin.Views.FechasImpotantes;
using AgendaPlusXamarin.Views.Notas;
using AgendaPlusXamarin.Views.Contacts;
using AgendaPlusXamarin.Views.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgendaPlusXamarin
{
    public partial class Menu : MasterDetailPage
    {

        private int userID;
        private static Usuario user;

        public Menu(int userIDParam = 1)
        {
            userID = userIDParam;

            InitializeComponent();
            Init();
            InitAPI();
        }

        private async void InitAPI()
        {
            List<Usuario> resultado = await UsuarioController.getUsuario();

            user = resultado.FirstOrDefault(x => x.UsuarioID == userID);

            InitIMG();
            userName.Text = user.NombreUsuario;
        }

        private void InitIMG()
        {
            switch (user.Avatar)
            {
                case "https://i.ibb.co/185gsr0/profile2.png":
                    ImgUser.Source = "avatar";
                    break;
                case "https://i.ibb.co/R7FzpbR/profile1.png":
                    ImgUser.Source = "avatar1";
                    break;
                case "https://i.ibb.co/tbcSZhH/profile5.png":
                    ImgUser.Source = "avatar2";
                    break;
                case "https://i.ibb.co/2vv7GwK/profile4.png":
                    ImgUser.Source = "avatar3";
                    break;
                case "https://i.ibb.co/v1QQ7Kd/profile.png":
                    ImgUser.Source = "avatar4";
                    break;
                case "https://i.ibb.co/r3nC5qX/profile3.png":
                    ImgUser.Source = "avatar5";
                    break;
                default:
                    ImgUser.Source = "avatar4";
                    break;
            }
        }


        ///------------METODOS CAMBIAR PAGINAS

        public void Init()
        {
            Detail = new NavigationPage(new MainTask(userID)) { BarBackgroundColor = Color.FromHex("#393A3D") };

        }

        private void openPageTasks(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new MainTask(userID)) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }

        private void openPageContacts(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new mainContact(userID)) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }


        private void openPageNotes(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new MainNote(userID)) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }

        private void openImportantDates(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new MainImportantDate(userID)) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }

        private void openSettings(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new Settings(userID)) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }

        private void openAbout(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new About()) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }

        private void logOut(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new About()) { BarBackgroundColor = Color.FromHex("#393A3D") };
        }

        private void closeMenu(object sender, EventArgs e)
        {
            IsPresented = false;
            InitAPI();
        }
    }
}
