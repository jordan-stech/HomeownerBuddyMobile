using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowReminderPage : ContentPage
    {
        private string actionPlanName;
        public ShowReminderPage(String reminder)
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
        }
    }
}