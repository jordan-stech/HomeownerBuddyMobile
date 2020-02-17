using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    //homeowner_buddy_username
    //homeowner_buddy_user_address

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public HomePage(string userFirstName)
        {
            InitializeComponent();

            // Display user's first name in the home page
            homeowner_buddy_username.Text = userFirstName;         
        }

        /*
         * Listener for "Help Me Diagnose an Issue" button
         */
        private void HandleHelpMeDiagnoseAnIssueButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DiagnoseIssuePage());
        }

        /*
         * Listener for "Maintenance Reminders" button
         */
        private void HandleMaintenanceRemindersButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MaintenanceReminder());
        }

        /*
         * Listener for "Contact Service Providers" button
         */
        private void HandleContactServiceProvidersButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactServiceProvider());
        }

        /*
        * Listener for "Settings" button
        */
        private void HandleSettingsButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }
    }
}