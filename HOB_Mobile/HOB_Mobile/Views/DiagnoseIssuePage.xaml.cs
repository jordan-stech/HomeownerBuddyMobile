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

            SetUpExteriorAndInteriorHomeImages();
        }

        /*
         * Handle the display of exterior and interior images
         */
        private void SetUpExteriorAndInteriorHomeImages()
        {
            exterior_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.house_outside.png");
            interior_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.house_inside.png");
        }

        /*
         * Listener for "Interior Issue" button
         */
        private void HandleInteriorIssueButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InteriorIssue());
        }

        /*
         * Listener for "Interior Issue" button
         */
        private void HandleExteriorIssueButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExteriorIssue());
        }
    }
}