using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowActionItemPage : ContentPage
    {
        public ShowActionItemPage(String title, String link, String steps)
        {
            InitializeComponent();

            SetUpActionItemDisplay(title, link, steps);
        }

        /*
         * Function that sets up the information about the item to be displayed.
         */
        private void SetUpActionItemDisplay(String title, String link, String steps)
        {
            youtubeUrl.Source = link;
            youtubeTitle.Text = title;
            youtubeSteps.Text = steps;
        }
    }
}