using Humanizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
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
            // Grab user ID to send as a get message for user reminders
            string userId = Preferences.Get("user_id", "no address found");

            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI/" + userId;

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

                ImageSource OverDueIcon = ImageSource.FromResource("HOB_Mobile.Resources.over_due.png");
                ImageSource ToDoIcon = ImageSource.FromResource("HOB_Mobile.Resources.to_do_icon.png");
                ImageSource DoneIcon = ImageSource.FromResource("HOB_Mobile.Resources.done_icon.png");

                var OverDues = new List<ReminderModel>();
                var ToDos = new List<ReminderModel>();
                var Dones = new List<ReminderModel>();

                foreach (ReminderModel reminder in reminders) {

                    
                    if (reminder.completed.Equals("Due"))
                    {
                        reminder.icon = ToDoIcon;
                        ToDos.Add(reminder);
                    }
                    else if (reminder.completed.Equals("Completed"))
                    {
                        reminder.icon = DoneIcon;
                        Dones.Add(reminder);
                    }
                    else if (reminder.completed.Equals("Overdue")) 
                    {
                        reminder.icon = OverDueIcon;
                        OverDues.Add(reminder);
                    }
                    else
                    {
                        // Not in season so don't display
                    }
                }

                if (OverDues.Count.Equals(0))
                {
                    pastdues.Text = "You have no overdues";
                    OverDueFrame.HeightRequest = 10;
                    
                }
                else
                {
                    OverDue.ItemsSource = OverDues;
                }

                if (ToDos.Count.Equals(0))
                {
                    todos.Text = "You have no maintenance tasks to do";
                    ToDoFrame.HeightRequest = 10;
                }
                else
                {
                    ToDo.ItemsSource = ToDos;
                }

                if (Dones.Count.Equals(0))
                {
                    finished.Text = "You haven't done any maintenance tasks yet";
                    DoneFrame.HeightRequest = 10;
                }
                else
                {
                    Done.ItemsSource = Dones;
                }
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

            var reminderID = label.id;
            var reminderItem = label.reminderItem;
            var reminderName = label.reminder;
            var reminderDescription = label.reminderDescription;
            var actionPlanTitle = label.actionPlanTitle;
            var actionPlanLink = label.actionPlanLink;
            var actionPlanSteps = label.actionPlanSteps;

            Navigation.PushAsync(new ShowReminderPage(reminderID, reminderItem, reminderName, reminderDescription, actionPlanTitle, actionPlanLink, actionPlanSteps));
        }

        private void HandleOverDueHeight(object sender, EventArgs e) {
            ViewCell cell = (ViewCell)sender;
            UpdateOverDueHeight(cell);
        }

        private void HandleToDoHeight(object sender, EventArgs e)
        {
            ViewCell cell = (ViewCell)sender;
            UpdateToDoHeight(cell);
        }

        private void HandleDoneHeight(object sender, EventArgs e)
        {
            ViewCell cell = (ViewCell)sender;
            UpdateDoneHeight(cell);
        }

        private void UpdateOverDueHeight(ViewCell viewCell) {
            double height = 0;

            foreach (var cell in OverDue.ItemsSource)
            {
                height += viewCell.View.Bounds.Height + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom;
            }
            OverDue.HeightRequest = height; 
        }

        private void UpdateToDoHeight(ViewCell viewCell)
        {
            double height = 0;
            foreach (var cell in ToDo.ItemsSource)
            {
                height += viewCell.View.Bounds.Height + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom;
            }
            ToDo.HeightRequest = height;
        }

        private void UpdateDoneHeight(ViewCell viewCell)
        {
            double height = 0;
            foreach (var cell in Done.ItemsSource)
            {
                height += viewCell.View.Bounds.Height + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom;     
            }
            Done.HeightRequest = height;
        }

    }
}