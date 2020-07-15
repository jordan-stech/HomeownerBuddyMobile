using Humanizer;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaintenanceReminder : ContentPage
    {

        private double OverdueFramBound;
        private double ToDoFrameBound;
        private double UISpacingError = 60.0;

        public MaintenanceReminder()
        {
            InitializeComponent();
            getReminder();
        }

        public async void getReminder()
        {
            // Grab user ID to send as a get message for user reminders
            string userId = Preferences.Get("user_id", "no address found");

            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI/" + userId;
            String postApiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MaintenanceReminderAPI";

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Grab the current users information to call the post method for checking all user due dates
            MobileUsers user = new MobileUsers();
            user.FName = Preferences.Get("user_first_name", "no first name found");
            user.Lname = Preferences.Get("user_last_name", "no last name found");
            user.Code = Preferences.Get("user_home_code", "no home code found");
            string regDate = DateTime.Today.ToString("MM/dd/yyyy");
            user.date = regDate;

            string JSONresult = JsonConvert.SerializeObject(user);
            Console.WriteLine(JSONresult);
            var content = new StringContent(JSONresult, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await httpClient.PostAsync(postApiUrl, content);

            // Check if the POST web request was successful
            if (postResponse.IsSuccessStatusCode)
            {
                // Get web request response and store it
                var getResponse = await httpClient.GetAsync(uri);

                // Check if the GET web request was successful
                if (getResponse.IsSuccessStatusCode)
                {
                    // Get the JSON object returned from the web request
                    var userContent = await getResponse.Content.ReadAsStringAsync();

                    var reminders = JsonConvert.DeserializeObject<List<ReminderModel>>(userContent);

                    ImageSource OverDueIcon = ImageSource.FromResource("HOB_Mobile.Resources.over_due.png");
                    ImageSource ToDoIcon = ImageSource.FromResource("HOB_Mobile.Resources.to_do_icon.png");
                    ImageSource DoneIcon = ImageSource.FromResource("HOB_Mobile.Resources.done_icon.png");

                    var OverDues = new List<ReminderModel>();
                    var ToDos = new List<ReminderModel>();
                    var Dones = new List<ReminderModel>();

                    foreach (ReminderModel reminder in reminders)
                    {


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
                        pastdues.Text = "You have no overdue tasks";
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
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

        private void ChangeHeaderText(object sender, EventArgs e) {
            ScrollView scroll = (ScrollView)sender;
            if (scroll.ScrollY < OverdueFramBound + UISpacingError) {
                headerText.Text = "Overdue Maintenance";
            } else if (scroll.ScrollY > OverdueFramBound + UISpacingError && scroll.ScrollY < ToDoFrameBound + UISpacingError) {
                headerText.Text = "To Do List";
            } else if (scroll.ScrollY > ToDoFrameBound + UISpacingError) {
                headerText.Text = "Completed Maintenance";
            } else if (scroll.ScrollY <= 55.0) {
                headerText.Text = "Maintenance Reminders";
            }
        }

        private void HandleOverDueHeight(object sender, EventArgs e)
        {
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

        private void UpdateOverDueHeight(ViewCell viewCell)
        {
            double height = 0;

            foreach (var cell in OverDue.ItemsSource)
            {
                height += viewCell.View.Bounds.Height + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom;
            }
            OverDue.HeightRequest = height;
            OverdueFramBound = height;
        }

        private void UpdateToDoHeight(ViewCell viewCell)
        {
            double height = 0;
            foreach (var cell in ToDo.ItemsSource)
            {
                height += viewCell.View.Bounds.Height + viewCell.View.Margin.Top + viewCell.View.Margin.Bottom;
            }
            ToDo.HeightRequest = height;
            ToDoFrameBound = height;
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

        private void HandleReminderClicked(object sender, EventArgs e)
        {
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





    }
}