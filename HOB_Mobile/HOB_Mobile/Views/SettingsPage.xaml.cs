using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

            // Call function that sets up the phone image
            SetUpPhoneImage();

            // Call function that steps up the emergency phone number listener
            SetUpEmergencyPhoneNumberHandler();

            string vers = VersionTracking.CurrentVersion;

            Version_Number.Text = "Version: " + vers;
            Version_Date.Text = "Release Date: " + DateTime.Today.Month + "/" + DateTime.Today.Day + "/" + DateTime.Today.Year;
        }

        /*
         * Handle the display of the unregister home image
         */
        private void SetUpUnregisterHomeImage()
        {
            unregister_device.Source = ImageSource.FromResource("HOB_Mobile.Resources.logout_icon.png");
        }

        /*
         * Handle the display of the phone image
         */
        private void SetUpPhoneImage()
        {
            emergency_phone_number_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.phone.png");
        }

        /*
         * Handle the emergency phone number click
         */
        private void SetUpEmergencyPhoneNumberHandler() {
            // Create new tap gesture recognizer so we know when the user tried to click on the emergency phone number
            var callEmergencyPhoneNumber = new TapGestureRecognizer();

            // Set up the tap handler
            callEmergencyPhoneNumber.Tapped += async (sender, e) =>
            {
                // Display alert to confirm if user wants to call the emergency phone number or not
                bool answer = await DisplayAlert("Emergency Only", "Would you like to call " + emergency_phone_number.Text + "?", "Call", "Cancel");

                // If the user selected "Call", then proceed
                if (answer == true)
                {
                    // If the clicked emergency phone number is not null or empty, then call respective trusted service provider
                    if (!string.IsNullOrEmpty(emergency_phone_number.Text))
                    {
                        // Call function that calls the emergency phone number
                        Call(emergency_phone_number.Text);
                    }
                }
            };

            // Add the gesture recognizer to the respective emergency phone number label from SettingsPage.xaml
            emergency_phone_number.GestureRecognizers.Add(callEmergencyPhoneNumber);
        }

        /*
         *  Listener that performs the call.
         */
        public void Call(string phoneNumber)
        {
            try
            {
                // Open the phone dialer in the user's phone
                PhoneDialer.Open(phoneNumber);
            }

            catch (FeatureNotSupportedException ex)
            {
                // If the PhoneDialer is not supported on the user's device, then display an alert
                DisplayAlert("", "Phone Dialer is not supported on this device.", "", "Close");
                Debug.Write(ex);
            }
            catch (Exception ex)
            {
                // Other error has occurred
                Debug.Write(ex);
            }
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
                Preferences.Remove("user_register_date");

                DeleteMobileUser(Preferences.Get("user_id", "default"));


                // await Navigation.PushAsync(new RegisterPage());

                // Return to register page and set it as  the MainPage so the back button doesn't return to previous pages 
                Application.Current.MainPage = new NavigationPage(new Views.RegisterPage());
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