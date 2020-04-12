using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

            // Call function that steps up the emergency phone number listener
            SetUpEmergencyPhoneNumberHandler();
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
         * Handle the emergency phone number click
         */
        private void SetUpEmergencyPhoneNumberHandler() {
            // Create new tap gesture recognizer so we know when the user tried to click on the emergency phone number
            var callEmergencyPhoneNumber = new TapGestureRecognizer();

            // Set up the tap handler
            callEmergencyPhoneNumber.Tapped += async (sender, e) =>
            {
                // Display alert to confirm if user wants to call the emergency phone number or not
                bool answer = await DisplayAlert("", "Would you like to call this number?", "Call", "Cancel");

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
                // Unregister device and then return to register page
                await Navigation.PushAsync(new RegisterPage());
            }
        }
    }
}