using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HOB_Mobile.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public HomePage(string userFirstName)
        {
            InitializeComponent();

            // Get user's first name passed as a parameter to HomePage.xaml.cs and display it in the home page
            homeowner_buddy_username.Text = userFirstName;
            homeowner_buddy_user_address.Text = Preferences.Get("user_address", "no address found");
        }

        /*
         * Listener for "Help Me Diagnose an Issue" button
         */
        private void HandleHelpMeDiagnoseAnIssueButtonClick(object sender, EventArgs e)
        {
            // Go to the DiagnoseIssuePage
            Navigation.PushAsync(new DiagnoseIssuePage());
        }

        /*
         * Listener for "Maintenance Reminders" button
         */
        private void HandleMaintenanceRemindersButtonClick(object sender, EventArgs e)
        {
            // Go to the MaintenanceReminderPage
            Navigation.PushAsync(new MaintenanceReminder());
        }

        /*
         * Listener for "Contact Service Providers" button
         */
        private void HandleContactServiceProvidersButtonClick(object sender, EventArgs e)
        {
            // Go to the ContactServiceProviderPage
            Navigation.PushAsync(new ContactServiceProvider());
        }

        /*
        * Listener for "Settings" button
        */
        private void HandleSettingsButtonClick(object sender, EventArgs e)
        {
            // Go to the SettingsPage
            Navigation.PushAsync(new Settings());
        }
    }
}