using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;

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
                // Delete the saved user data
                Preferences.Remove("user_home_code");
                Preferences.Remove("user_first_name");
                Preferences.Remove("user_last_name");
                Preferences.Remove("user_address");

                DeleteMobileUser(Preferences.Get("user_id", "default"));
                // Return to register page

                await Navigation.PushAsync(new RegisterPage());
            }
        }

        public async void DeleteMobileUser(String id)
        {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MobileUsersAPI/" + id;

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            var response = await httpClient.DeleteAsync(uri);
        }
    }
}