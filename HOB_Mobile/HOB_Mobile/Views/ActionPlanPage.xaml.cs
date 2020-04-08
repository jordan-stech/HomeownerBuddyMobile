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
        public ActionPlan(String urlEndpoint)
        {
            InitializeComponent();

            // Call function to perform a web request with the passed URL endpoint as parameter
            GetActionPlans(urlEndpoint);
        }

        /*
        * Get all action plans, available in the database, for the respective category selected by the user
        */
        public async void GetActionPlans(String category)
        {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/ActionPlanAPI/" + category;

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Get web request response and store it
            var response = await httpClient.GetAsync(uri);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON object returned from the web request
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON object. In other words, convert the returned string back to its original object form (JSON)
                var actionPlans = JsonConvert.DeserializeObject<List<ContentModel>>(content);

                // Add JSON object returned from the web request to the ListView in the ActionPlanPage.xaml file
                ListActionPlan.ItemsSource = actionPlans;
            }
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

        /*
        *  Listener for action plan click.
        */
        private void HandleShowStepsClick(object sender, SelectedItemChangedEventArgs e)
        {
            // Get the object that triggered the function, cast it to a ListView and then get its selected item
            var actionPlanList = (ListView)sender;
            var actionPlanItem = (actionPlanList.SelectedItem as ContentModel);

            // Go to the ShowActionItemPage passing the selected item's title, link and steps as parameters
            Navigation.PushAsync(new ShowActionItemPage(actionPlanItem.title, actionPlanItem.link, actionPlanItem.steps));

            // Unselect item.
            actionPlanList.SelectedItem = null;
        }
    }
}