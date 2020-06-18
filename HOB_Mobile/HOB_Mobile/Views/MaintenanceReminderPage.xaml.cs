using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * This file is where the Notification System should take place.
 * 
 * As far as we know, even though xamarin forms is cross-platform, the notification system
 * will have to be implemented separately in the HOB_Mobile.Android and HOB_Mobile.iOS
 * projects (they are located in the Solution Explorer).
 * 
 */

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaintenanceReminder : ContentPage
    {
        public MaintenanceReminder()
        {
            InitializeComponent();

            getReminder();
        }

        public async void getReminder() {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI";

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Get web request response and store it
            var response = await httpClient.GetAsync(uri);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON object returned from the web request
                var content = await response.Content.ReadAsStringAsync();
   
                var reminders = JsonConvert.DeserializeObject<List<ReminderModel>>(content);

                ListReminders.ItemsSource = reminders;
            }
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

        private void HandleReminderClicked(object sender, EventArgs e) {
            ViewCell box = (ViewCell)sender;
            var label = (ReminderModel)box.BindingContext;

            var reminder = label.reminder;
            var status = label.completed;

            //Navigation.PushAsync(new ShowReminderPage(reminder, status));
            Navigation.PushAsync(new ActionPlan(reminder));
        }

    }
}