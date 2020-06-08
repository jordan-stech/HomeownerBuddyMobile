using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace HOB_Mobile.Views

{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    [DesignTimeVisible(false)]

    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            // Call function that adds the logo to the register page
            SetUpLoginPageHabitatHumanityLogo();
        }

        /*
         * Handle the display of the Habitat For Humanity Mid-Ohio logo
         */
        private void SetUpLoginPageHabitatHumanityLogo()
        {
            // Add the Humanity Mid-Ohio logo stored in the Resources folder to its respective Image in the RegisterPage.xaml file
            habitat_humanity_logo.Source = ImageSource.FromResource("HOB_Mobile.Resources.habitat_midohio_logo.jpg");
        }

        /*
         * Listener for "Register" button
         */
        private void HandleRegisterButtonClick(object sender, EventArgs e)
        {
            // Get the text entered by the user in each of the input forms (Home Code, First Name and Last Name)
            // by it's x:Name followed by .Text
            string userHomeCode = homeowner_buddy_home_code.Text;
            string userFirstName = homeowner_buddy_first_name.Text;
            string userLastName = homeowner_buddy_last_name.Text;

            // Check if any of the input forms are empty when the user clicks the "Register" button
            if (userHomeCode == null || userFirstName == null || userLastName == null)
            { 
                DisplayAlert("All Fields are Required", "Make sure you fill out all of the fields", "OK");

            } else
            {
                //POST
                PostUserInfo(userHomeCode, userFirstName, userLastName); 
            }
        }

        public async void PostUserInfo(String userHomeCode, String userFirstName, String userLastName)
        {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/MobileUsersAPI";

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            MobileUsers user = new MobileUsers();
            user.FName = userFirstName;
            user.Lname = userLastName;
            user.Code = userHomeCode;

            string JSONresult = JsonConvert.SerializeObject(user);
            var content = new StringContent(JSONresult, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            // Keep track of id from the database - will be used to unregister home
            if (response.IsSuccessStatusCode)
            {
                // Get response from POST request
                var tokenJson = await response.Content.ReadAsStringAsync();
                var array = tokenJson.Split('"');
                String id = array[2];
                id = id.Substring(1);
                id = id.TrimEnd(',');
                if (id != "-1")
                {
                    String address = array[17];
                    // Save the id in preferences
                    //This is where we store the home code and name, we are going to use Preferences to see if a user is logged in
                    Preferences.Set("user_home_code", userHomeCode);
                    Preferences.Set("user_first_name", userFirstName);
                    Preferences.Set("user_last_name", userLastName);
                    Preferences.Set("user_id", id);
                    Preferences.Set("user_address", address);

                    //await Navigation.PushAsync(new HomePage(Preferences.Get("user_first_name", "")));
                    
                    // Sets the Home Page as the MainPage so when the physical back button is pressed immediately after registering, the app closes instead of returning to the Register Page 
                    Application.Current.MainPage = new NavigationPage(new Views.HomePage(Preferences.Get("user_first_name", "")));
                } else
                {
                    await DisplayAlert("User Already Exists", "This user has already been registered on another phone, please unregister before continuing", "OK");
                }
            } else
            {
                await DisplayAlert("Home Code does not exist", "Please double check your home code", "OK");
            }
        }
    }
}
