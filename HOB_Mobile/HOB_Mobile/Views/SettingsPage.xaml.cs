using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            // Call function that sets up the unregiser home image
            SetUpUnregisterHomeImage();
        }

        /*
         * Handle the display of the unregister home image
         */
        private void SetUpUnregisterHomeImage()
        {
            // Add the logout image stored in the Resources folder to its respective ImageButton in the SettingsPage.xaml file
            unregister_device.Source = ImageSource.FromResource("HOB_Mobile.Resources.logout_icon.png");
        }

        /*
         * Listener for "Unregister" button
         */
        private async void HandleUnregisterDeviceButtonClick(object sender, EventArgs e)
        {
            // Display alert to confirm if user wants to unregister the device
            bool answer = await DisplayAlert("Unregister Device", "Are you sure you want to unregister this device?", "Yes", "No");

            // If the user clicked "Yes", then proceed
            if (answer == true)
            {
                // Unregister device and then return to register page
                await Navigation.PushAsync(new RegisterPage());
            }
        }
    }
}