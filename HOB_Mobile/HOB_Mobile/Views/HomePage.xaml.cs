using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        /*
         * Listener for "Help Me Diagnose an Issue" button
         */
        private void handleHelpMeDiagnoseAnIssueButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DiagnoseIssuePage());
        }

        /*
         * Listener for "Maintenance Reminders" button
         */
        private void handleMaintenanceRemindersButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MaintenanceReminder());
        }

        /*
         * Listener for "Contact Service Providers" button
         */
        private void handleContactServiceProvidersButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactServiceProvider());
        }

        /*
        * Listener for "Settings" button
        */
        private void handleSettingsButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings());
        }
    }
}