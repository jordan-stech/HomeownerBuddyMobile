using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private int updaedReminderID;
        public ShowReminderPage(int reminderID, string reminderName)
        {
            InitializeComponent();
            actionPlanName = reminderName;
            updaedReminderID = reminderID;

            SetButtonImages();          
            setVideoButtonText(reminderName);
        }

        private void setVideoButtonText(string reminderName)
        {
            VideoButton.Text = reminderName + " Video";
        }

        private void SetButtonImages()
        {
            show_video_button.Source = ImageSource.FromResource("HOB_Mobile.Resources.video_button.png");
        }

        private void videoButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ActionPlan(actionPlanName));
        }

        private void UpdateReminderStatus(object sender, EventArgs e) {
            UpdateReminderInDB();
        }

        private async void UpdateReminderInDB()
        {
            // Id of the current reminder that the user clicked on
            string reminderId = updaedReminderID.ToString();
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI/" + reminderId;

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            var content = new StringContent(reminderId, Encoding.UTF8, "application/json");

            // Get web request response and store it
            var response = await httpClient.PutAsync(uri, content);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                Preferences.Set("completed", "done");
                await Navigation.PopAsync();
                await Navigation.PushAsync(new MaintenanceReminder());
            }
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }


    }
}