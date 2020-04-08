using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

/* 
 * Main program which runs first on app startup
 */

namespace HOB_Mobile
{
    public partial class App : Application
    {

        public App()
        {
            // Component must be initialized
            InitializeComponent();

            // Launch register page
            MainPage = new NavigationPage(new Views.RegisterPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
