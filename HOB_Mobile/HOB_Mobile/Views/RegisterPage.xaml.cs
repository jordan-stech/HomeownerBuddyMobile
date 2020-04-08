using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views

{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    [DesignTimeVisible(false)]

    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            // Call function that adds the logo to the register page
            SetUpLoginPageHabitatHumanityLogo();
        }

        /*
         * Handle the display of the Habitat For Humanity Mid-Ohio logo
         */
        private void SetUpLoginPageHabitatHumanityLogo()
        {
            // Add the Humanity Mid-Ohio logo stored in the Resources folder to its respective Image in the RegisterPage.xaml file
            habitat_humanity_logo.Source = ImageSource.FromResource("HOB_Mobile.Resources.habitat_midohio_logo.jpg");
        }

        /*
         * Listener for "Register" button
         */
        private void HandleRegisterButtonClick(object sender, EventArgs e)
        {
            // Get the text entered by the user in each of the input forms (Home Code, First Name and Last Name)
            // by it's x:Name followed by .Text
            string userHomeCode = homeowner_buddy_home_code.Text;
            string userFirstName = homeowner_buddy_first_name.Text;
            string userLastName = homeowner_buddy_last_name.Text;

            // Check if any of the input forms are empty when the user clicks the "Register" button
            if (userHomeCode == null || userFirstName == null || userLastName == null)
            {
                // If any of the input forms are empty, then display alert
                DisplayAlert("", "All fields are required", "OK");
            }
            else
            {
                // If all the input forms are filled, then go to the HomePage and pass the user's first name as parameter
                Navigation.PushAsync(new HomePage(userFirstName));
            }
        }
    }
}
