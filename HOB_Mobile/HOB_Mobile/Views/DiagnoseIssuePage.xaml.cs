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
            // Remove navigation bar from top of the screen
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            // Call function to set up the home locations icons
            SetUpHomeIssueImages();
        }

        /*
         * Handle the display of images in the home page
         */
        private void SetUpHomeIssueImages()
        {
            // Add each image stored in the Resources folder to its respective ImageButton in the DiagnoseIssuePage.xaml file
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

        /*
        * Listener for "Home Issue Location" button
        */
        private void HandleLocationOfHomeIssueImageClick(object sender, EventArgs e)
        {
            // Get the object that triggered the function and cast it to an ImageButton
            var issueImage = (ImageButton)sender;

            // Get the ImageButton style ID (x:Name in the DiagnoseIssuePage.xaml file), which in this case we set up to be the category
            var category = issueImage.StyleId;

            // Go to the ActionPlanPage passing the selected image button's category as parameter.
            Navigation.PushAsync(new ActionPlan(category));
        }
    }
}