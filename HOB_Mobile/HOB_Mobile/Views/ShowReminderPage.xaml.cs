using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowReminderPage : ContentPage
    {
        private string actionPlanName;

        public ShowReminderPage(string reminder)
        {
            InitializeComponent();

            SetButtonImages();

            actionPlanName = reminder;
            setVideoButtonText(reminder);
        }

        private void setVideoButtonText(string reminder)
        {
            VideoButton.Text = "Click for " + reminder + " Action Plan Video";
        }

        private void SetButtonImages() {
            show_video_button.Source = ImageSource.FromResource("HOB_Mobile.Resources.video_button.png");
        }

        private void videoButtonClicked(object sender, EventArgs e) { 
            Navigation.PushAsync(new ActionPlan(actionPlanName));
        }

        private void UpdateReminderStatus(object sender, EventArgs e) {
            //update the status of the reminder in database
            UpdateReminderInDB();
        }

        private async void UpdateReminderInDB() {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI";

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Create new user object to pass into the POST request
            MobileUsers user = new MobileUsers();
            int tempId = Int32.Parse(Preferences.Get("user_id", "no user id"));
            user.Id = tempId;
            user.FName = Preferences.Get("user_first_name", "no first name found");
            user.Lname = Preferences.Get("user_last_name", "no last name found");
            user.Code = Preferences.Get("user_home_code", "no home code found");
            user.date = Preferences.Get("user_register_code", "no register date found");

            // Get web request response and store it
            var response = await httpClient.GetAsync(uri);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the JSON object returned from the web request
                var content = await response.Content.ReadAsStringAsync();

                var reminders = JsonConvert.DeserializeObject<List<ReminderModel>>(content);

                foreach (ReminderModel reminder in reminders)
                {
                    string taskName = reminder.reminder;
                    if (taskName.Equals(actionPlanName)) {

                        string JSONresult = JsonConvert.SerializeObject(user);
                        Console.WriteLine(JSONresult);
                        var newContent = new StringContent(JSONresult, Encoding.UTF8, "application/json");
                        HttpResponseMessage newResponse = await httpClient.PostAsync(apiUrl, newContent);

                        if (newResponse.IsSuccessStatusCode)
                        {
                            var tokenJson = await newResponse.Content.ReadAsStringAsync();
                            Console.WriteLine(tokenJson);
                            var array = tokenJson.Split('"');
                            String completed = array[14];
                            Preferences.Set("completed", "done");
                           

                            String putApiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI/" + Preferences.Get("user_id", "no user id");
                            var putResponse = await httpClient.GetAsync(uri);

                            if (putResponse.IsSuccessStatusCode)
                            {
                                await DisplayAlert(completed, " result ", "OK");
                                Application.Current.MainPage = new NavigationPage(new Views.MaintenanceReminder());
                            }
                        
                        }
                    }
                }

            }
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }
    }
}