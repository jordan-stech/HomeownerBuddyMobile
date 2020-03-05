using System.Collections.Generic;

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
            GetServiceProviders();
        }

        public async void GetServiceProviders()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = null;
            if (Device.RuntimePlatform == Device.Android) apiUrl = "https://10.0.2.2:5001/api/ServiceProviderAPI";
            else if (Device.RuntimePlatform == Device.iOS) apiUrl = "https://localhost:5001/api/ServiceProviderAPI";

            var uri = new Uri(string.Format(apiUrl, string.Empty));
            var response = await httpClient.GetAsync(uri);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var serviceProviders = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(content);
                ListServiceProvider.ItemsSource = serviceProviders;
            } else
            {
                Debug.WriteLine("Response not successful");
            }
        }
    }
}