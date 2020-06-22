using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
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

            // Create new user object to send the current user data to the Maintenance Reminder API
            MobileUsers user = new MobileUsers();
            user.FName = Preferences.Get("user_first_name", "no first name found");
            user.Lname = Preferences.Get("user_last_name", "no last name found");
            user.Code = Preferences.Get("user_home_code", "no home code found");
            user.address = Preferences.Get("user_address", "no address found");
            user.date = Preferences.Get("user_register_date", "no date found");

            string JSONresult = JsonConvert.SerializeObject(user);
            Console.WriteLine(JSONresult);
            var userId = new StringContent(JSONresult, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await httpClient.PostAsync(apiUrl, userId);
          
            // Check if the web POST request was successful
            if (postResponse.IsSuccessStatusCode)
            {
                // Get web request response and store it
                var response = await httpClient.GetAsync(uri);

                // Check if the web GET request was successful
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