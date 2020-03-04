using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactServiceProvider : ContentPage
    {
        public ContactServiceProvider()
        {
            InitializeComponent();
            GetProviders();
        }

        public async void GetProviders()
        {
            
            

            
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            
            HttpClient httpClient = new HttpClient(clientHandler);
            var uri = new Uri(string.Format("https://10.0.2.2:5001/api/ServiceProviderAPI", string.Empty));
            var response = await httpClient.GetAsync(uri);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var serviceProviders = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(content);
                ListServiceProvider.ItemsSource = serviceProviders;
            } else
            {
                Debug.WriteLine("sad");
            }
           
            

            //String apiUrl = null;
            //if (Device.RuntimePlatform == Device.Android) apiUrl = "http://10.0.2.2:5001/api/ServiceProviderAPI";
            //else if (Device.RuntimePlatform == Device.iOS) apiUrl = "http://localhost:5001/api/ServiceProviderAPI";

            //var httpClient = new ();
            //var response = await httpClient.GetStringAsync(apiUrl);
            //var serviceProviders = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(response);
            //ListServiceProvider.ItemsSource = serviceProviders;
        }
    }
}