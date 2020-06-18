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

        public ShowReminderPage(string category, string reminderStatus)
        {
            InitializeComponent();

            GetActionPlans(category);
        }

        public void GetActionPlans(string category)
        {
            // this function is depended on whether the video will be played on this page
            // if the video is not necessaryly to be played here just call getActionPlans(category)
            // from ActionPlan page
        }
    }
}