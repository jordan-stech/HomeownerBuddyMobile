using HOB_Mobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

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


            //Check and see if user registered previously, if they have, redirect them to the HomePage
            //set the testing boolean to true to see register page for testing purposes
            var testing = false;
            if (!Preferences.Get("user_home_code", "default_value").Equals("default_value") && !testing)
            {
                // Launch Home Page
                MainPage = new NavigationPage(new Views.HomePage(Preferences.Get("user_first_name", "")));
            }
            else
            {
                // Launch register page
                MainPage = new NavigationPage(new Views.RegisterPage());
            }

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
