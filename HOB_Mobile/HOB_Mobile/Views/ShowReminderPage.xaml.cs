using Android.Widget;
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
        private string actionTitle;
        private string actionLink;
        private string actionSteps;
        private int updaedReminderID;
        public ShowReminderPage(int reminderID, string reminderItem, string reminderName, string reminderDescription, string actionPlanTitle, string actionPlanLink, string actionPlanSteps)
        {
            InitializeComponent();
            actionTitle = actionPlanTitle;
            actionLink = actionPlanLink;
            actionSteps = actionPlanSteps;
            updaedReminderID = reminderID;

            if (actionPlanLink==null || actionPlanLink == " ") {
                video_task.IsVisible = false;
            }

            SetItem(reminderItem);
            SetTitle(reminderName);
            SetDescription(reminderDescription);
            SetButtonImages();          
            setVideoButtonText(reminderName);

        }

        private void SetItem(string reminderItem)
        {
            Item.Text = reminderItem;
        }

        private void SetTitle(string reminderName)
        {
            Title.Text = reminderName;
        }

        private void SetDescription(string reminderDescription)
        {
            Description.Text = reminderDescription;
        }

        private void setVideoButtonText(string reminderName)
        {
            VideoButton.Text = "Action Plan Video";
        }

        private void SetButtonImages()
        {
            show_video_button.Source = ImageSource.FromResource("HOB_Mobile.Resources.video_button.png");
            task_finished.Source = ImageSource.FromResource("HOB_Mobile.Resources.task_done.png");
        }

        public void videoButtonClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ActionPlan(actionPlanName));
            if (actionTitle != "None")
            {
                Navigation.PushAsync(new ShowActionItemPage(actionTitle, actionLink, actionSteps));
            }
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
                Toast.MakeText(Android.App.Application.Context, "Task Completed!", ToastLength.Short).Show();
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
                await Navigation.PushAsync(new MaintenanceReminder());
                Navigation.RemovePage(this);


            }
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

    }
}