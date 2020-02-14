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
        private void handleRegisterButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
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
