using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            SetUpImages();
        }

        /*
         * Handle the display of images
         */
        private void SetUpImages()
        {
            unregister_device.Source = ImageSource.FromResource("HOB_Mobile.Resources.logout_icon.png");
        }

        /*
         * Listener for "Unregister" button
         */
        private async void HandleUnregisterDeviceButtonClick(object sender, EventArgs e)
        {
            // Display alert to confirm if user wants to unregister the device
            bool answer = await DisplayAlert("Unregister Device", "Are you sure you want to unregister this device?", "Yes", "No");

            if (answer == true)
            {
                Preferences.Clear();
                // Return to register page

                Navigation.PushAsync(new RegisterPage());
            }
        }
    }
}