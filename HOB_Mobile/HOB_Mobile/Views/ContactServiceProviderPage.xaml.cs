using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;

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
            string apiUrl = null;
            if (Device.RuntimePlatform == Device.Android) apiUrl = "http://10.0.2.2:5001/api/ServiceProviderAPI";
            else if (Device.RuntimePlatform == Device.iOS) apiUrl = "http://localhost:5001/api/ServiceProviderAPI";

            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            var serviceProviders = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(response);
            ListServiceProvider.ItemsSource = serviceProviders;
        }
    }
}