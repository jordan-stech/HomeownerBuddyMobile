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

            // Call function that sets up and display the selected ActionPlan item
            SetUpActionItemDisplay(title, link, steps);
        }

        /*
         * Handle the set up of the information about the ActionPlan item to be displayed
         */
        private void SetUpActionItemDisplay(String title, String link, String steps)
        {
            // Get the YouTube link passed as a parameter and added it to its respective
            // WebView in the ShowActionItemPlan.xaml file
            youtubeUrl.Source = link;

            // Get the YouTube video's title and steps passed as parameters and set them to
            // their respective Label's text in the ShowActionItemPlan.xaml file
            youtubeTitle.Text = title;
            youtubeSteps.Text = steps;
        }
    }
}