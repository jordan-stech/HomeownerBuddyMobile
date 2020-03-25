using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionPlan : ContentPage
    {
        public ActionPlan()
        {
            InitializeComponent();

            // Perform web request
            GetActionPlans();
        }

        /*
        * Get all action plans available in the database
        */
        public async void GetActionPlans()
        {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = null;
            if (Device.RuntimePlatform == Device.Android) apiUrl = "https://10.0.2.2:5001/api/ActionPlanAPI";
            else if (Device.RuntimePlatform == Device.iOS) apiUrl = "https://localhost:5001/api/ActionPlanAPI";

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Get web request response and store it
            var response = await httpClient.GetAsync(uri);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON object returned from the web request
                var content = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.Write("JSON Response is: " + content);

                // Deserialize the JSON object. In other words, convert the returned string back to its original object form (JSON)
                var actionPlans = JsonConvert.DeserializeObject<List<ContentModel>>(content);

                ListActionPlan.ItemsSource = actionPlans;
            }
            else
            {
                Debug.WriteLine("Response not successful");
            }
        }

        /*
        *  Listener for action plan click.
        */
        private void HandleListActionPlanItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var actionPlanListView = (ListView)sender;
            var actionPlanItem = actionPlanListView.SelectedItem;

            if (actionPlanItem != null)
            {
                if (IsVisible)
                {
                    actionPlanItem.IsVisible = false;
                }
                else
                {
                    actionPlanItem.IsVisible = true;
                }
            }
        }
    }
}