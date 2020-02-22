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

            if (userHomeCode == null || userFirstName == null || userLastName == null)
            {
                DisplayAlert("", "All fields are required", "OK");
            } else
            {
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
