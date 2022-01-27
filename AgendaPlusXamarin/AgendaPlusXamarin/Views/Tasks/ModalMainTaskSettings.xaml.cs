using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaPlusXamarin.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalMainTaskSettings : ContentPage
    {
        MainTask parent;
        string show = "Default";
        string orderBy = "Default";
        public ModalMainTaskSettings(MainTask parent)
        {
            InitializeComponent();
            this.parent = parent;
            MantenerSettings();
        }
        private async void dispose_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void saveAndExit_Clicked(object sender, EventArgs e)
        {
            verificacionOrderBy();
            verificacionShow();
            parent.SetSettings(show, orderBy);
            await Navigation.PopModalAsync();
        }

        private void MantenerSettings()
        {
            string actualShow = parent.show;
            string actualOrder = parent.orderBy;

            if (actualShow == "Default")
            {
                showDefault.IsChecked = true;
            }
            else if (actualShow == "Pending tasks")
            {
                showPending.IsChecked = true;
            }
            else if (actualShow == "Done tasks")
            {
                showDone.IsChecked = true;
            }

            if (actualOrder == "Default")
            {
                noOrder.IsChecked = true;
            }
            else if (actualOrder == "Priority")
            {
                orderPriority.IsChecked = true;

            }
        }
        private void verificacionOrderBy()
        {
            if (noOrder.IsChecked)
            {
                orderBy = "Default";
            }
            else if (orderPriority.IsChecked)
            {
                orderBy = "Priority";
            }

        }

        private void verificacionShow()
        {
            if (showPending.IsChecked)
            {
                show = "Pending tasks";
            }
            else if (showDone.IsChecked)
            {
                show = "Done tasks";
            }
            else if (showDefault.IsChecked)
            {
                show = "Default";
            }

        }

        private void orderPriority_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (orderPriority.IsChecked)
            {
                orderBy = "Priority";
            }
        }


        private void showDone_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (showDone.IsChecked)
            {
                show = "Done tasks";
            }
        }

        private void showPending_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (showPending.IsChecked)
            {
                show = "Pending tasks";
            }
        }

        private void showDefault_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (showDefault.IsChecked)
            {
                show = "Default";
            }

        }

        private void noOrder_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (noOrder.IsChecked)
            {
                show = "Default";
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MantenerSettings();
        }
    }
}