using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactServiceProvider : ContentPage
    {
        public ContactServiceProvider()
        {
            InitializeComponent();

            // Call function to perform a web request
            GetServiceProviders();
        }

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

                // Set the list of ServiceProviderModel to the ListView in the ContactServiceProviderPage.xaml file
                ListServiceProvider.ItemsSource = serviceProviders;
            } else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

        /*
        *  Listener for phone number click.
        */
        private async void HandlePhoneNumberClick(object sender, SelectedItemChangedEventArgs e)
        {
            // Get the object that triggered the function, cast it to a ListView and then get its selected item
            var trustedServiceProviderList = (ListView)sender;
            var trustedServiceProvider = (trustedServiceProviderList.SelectedItem as ServiceProviderModel);

            // Get the phone number associated with the selected item in the ListView
            var trustedServiceProvierPhoneNumber = trustedServiceProvider.phone_number;

            // Display alert to confirm if user wants to call the selected number or not
            bool answer = await DisplayAlert("", "Would you like to call this number?", "Call", "Cancel");

            // If the user selected "Call", then proceed
            if (answer == true)
            {
                // If the clicked phone number is not null or empty, then call respective trusted service provider
                if (!string.IsNullOrEmpty(trustedServiceProvierPhoneNumber))
                {
                    // Call function that calls the trusted service provider's phone number clicked by the user
                    Call(trustedServiceProvierPhoneNumber);
                }
            }

            // Unselect item.
            trustedServiceProviderList.SelectedItem = null;
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