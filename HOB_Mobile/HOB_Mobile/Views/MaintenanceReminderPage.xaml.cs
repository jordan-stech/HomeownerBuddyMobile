using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}