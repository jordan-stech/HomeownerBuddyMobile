using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DiagnoseIssuePage : ContentPage
    {
        public DiagnoseIssuePage()
        {
            InitializeComponent();

            SetUpHomeIssueImages();
        }

        /*
         * Listener for "Home Issue Location" button
         */
        private void HandleLocationOfHomeIssueImageClick(object sender, EventArgs e)
        {
            var issueImage = (ImageButton)sender;
            var category = issueImage.StyleId;

            Navigation.PushAsync(new ActionPlan(category));
        }

        /*
         * Handle the display of images in the home page
         */
        private void SetUpHomeIssueImages()
        {
            kitchen.Source = ImageSource.FromResource("HOB_Mobile.Resources.kitchen.png");
            plumbing.Source = ImageSource.FromResource("HOB_Mobile.Resources.plumbing.png");
            bedroom.Source = ImageSource.FromResource("HOB_Mobile.Resources.bedroom.png");
            bathroom.Source = ImageSource.FromResource("HOB_Mobile.Resources.bathroom.png");
            laundry.Source = ImageSource.FromResource("HOB_Mobile.Resources.laundry.png");
            exterior.Source = ImageSource.FromResource("HOB_Mobile.Resources.exterior.png");
            closet.Source = ImageSource.FromResource("HOB_Mobile.Resources.closet.png");
            hallway.Source = ImageSource.FromResource("HOB_Mobile.Resources.hallway.png");
            livingRoom.Source = ImageSource.FromResource("HOB_Mobile.Resources.living_room.png");
            misc.Source = ImageSource.FromResource("HOB_Mobile.Resources.misc.png");
            hvac.Source = ImageSource.FromResource("HOB_Mobile.Resources.HVAC.png");
            electrical.Source = ImageSource.FromResource("HOB_Mobile.Resources.electric.png");
            basement.Source = ImageSource.FromResource("HOB_Mobile.Resources.basement.png");
        }
    }
}