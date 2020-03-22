using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HOB_Mobile.Views

{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    [DesignTimeVisible(false)]

    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            //Check and see if user registered previously, if they have, redirect them to the HomePage
            if (!Preferences.Get("user_home_code", "default_value").Equals("default_value"))
            {
                Navigation.PushAsync(new HomePage(Preferences.Get("user_first_name", "")));
            }

            SetUpLoginPageHabitatHumanityLogo();
        }

        /*
         * Listener for "Register" button
         */
        private void HandleRegisterButtonClick(object sender, EventArgs e)
        {
            string userHomeCode = homeowner_buddy_home_code.Text;
            string userFirstName = homeowner_buddy_first_name.Text;
            string userLastName = homeowner_buddy_last_name.Text;

            var myValue = Preferences.Get("user_home_code", "default_value");

            if (userHomeCode == null || userFirstName == null || userLastName == null)
            { 
                DisplayAlert("", "All fields are required", "OK");

            } else
            {
                //This is where we store the home code, we are going to use Preferences to see if a user is logged in
                Preferences.Set("user_home_code", userHomeCode);
                Preferences.Set("user_first_name", userFirstName);
                Preferences.Set("user_last_name", userLastName);
                var myValue1 = Preferences.Get("user_home_code", "default_value");
                Navigation.PushAsync(new HomePage(userFirstName));
            }
        }

        /*
         * Handle logo display
         */
        private void SetUpLoginPageHabitatHumanityLogo()
        {
            habitat_humanity_logo.Source = ImageSource.FromResource("HOB_Mobile.Resources.habitat_midohio_logo.jpg");
        }
    }
}
