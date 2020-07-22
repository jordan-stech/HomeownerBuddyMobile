using System;
using System.Collections.Generic;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactServiceProvider : ContentPage
    {
        public ContactServiceProvider()
        {
            InitializeComponent();

            // Disable item tapped from ListView so we can handle phone number click and website click separately
            ListServiceProvider.ItemTapped += HandleServiceProviderItemTapped;

            // Call function to perform a web request
            GetServiceProviders();
        }

        /*
         * Listener that disables the item tapped trigger for the service provider ListView
         */
        private void HandleServiceProviderItemTapped (object sender, ItemTappedEventArgs e) => ListServiceProvider.SelectedItem = null;

        /*
        * Get all service providers available in the database
        */
        public async void GetServiceProviders()
        {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/ServiceProviderAPI";

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Get web request response and store it
            var response = await httpClient.GetAsync(uri);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON object returned from the web request
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the object. In other words, convert the returned string back to
                // the format set by the ServiceProviderModel
                var serviceProviders = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(content);

                // Loop through every service provider and add the phone and website icon to their display
                foreach(ServiceProviderModel model in serviceProviders)
                {
                    model.phone_icon = ImageSource.FromResource("HOB_Mobile.Resources.phone.png");
                    if (model.url != null)
                    {
                        model.website_icon = ImageSource.FromResource("HOB_Mobile.Resources.website.png");
                    }
                }

                // Set the list of ServiceProviderModel to the ListView in the ContactServiceProviderPage.xaml file
                ListServiceProvider.ItemsSource = serviceProviders;
            } else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

        /*
         * Listener for clicked service provider website
         */
        private async void HandleServiceProviderWebsiteClick(object sender, EventArgs e)
        {
            // Get the object that triggered the function and cast it to a Label
            var selectedServiceProvider = (Label)sender;

            // Open selected service provider's website on the default browser
            Uri uri = new Uri(selectedServiceProvider.Text);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        /*
         * Listener for clicked service provider phone number
         */
        private async void HandleServiceProviderPhoneNumberClick(object sender, EventArgs e)
        {
            // Get the object that triggered the function and cast it to a Label
            var selectedServiceProvider = (Label)sender;

            // Display alert to confirm if user wants to call the service provider phone number or not
            bool answer = await DisplayAlert("Service Provider", "Would you like to call " + selectedServiceProvider.Text + "?", "Call", "Cancel");

            // If the user selected "Call", then proceed
            if (answer == true)
            {
                // If the clicked service provider phone number is not null or empty, then call respective service provider
                if (!string.IsNullOrEmpty(selectedServiceProvider.Text))
                {
                    // Call function that calls the service provider phone number
                    Call(selectedServiceProvider.Text);
                }
            }
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
    }
}