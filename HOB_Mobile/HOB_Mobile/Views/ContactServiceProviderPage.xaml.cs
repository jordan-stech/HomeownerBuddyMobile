using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://localhost:44362/api/ServiceProviderModel");
            var serviceProviders = JsonConvert.DeserializeObject<List<ServiceProviderModel>>(response);
            ListServiceProvider.ItemsSource = serviceProviders;
            
        }
    }
}