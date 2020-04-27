using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionPlan : ContentPage
    {
        List<ContentModel> globalActionPlans;

        Dictionary<string, List<ContentModel>> tagToJsonMap = new Dictionary<string, List<ContentModel>>();

        public ActionPlan(string category)
        {
            InitializeComponent();

            // Call function to perform a web request with the passed category as parameter
            GetActionPlans(category);

            // Convert category's first letter to uppercase and store in a new string.
            string uppercaseCategory = char.ToUpper(category[0]) + category.Substring(1);

            // Set title to category selected by the user.
            category_label.Text = uppercaseCategory;
        }

        /*
         * Get all action plans, available in the database, for the respective category selected by the user
         */
        public async void GetActionPlans(string category)
        {
            // Set up new HttpClientHandler and its credentials so we can perform the web request
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Create new httpClient using our client handler created above
            HttpClient httpClient = new HttpClient(clientHandler);

            String apiUrl = "https://habitathomeownerbuddy.azurewebsites.net/api/ActionPlanAPI/" + category;

            // Create new URI with the API url so we can perform the web request
            var uri = new Uri(string.Format(apiUrl, string.Empty));

            // Get web request response and store it
            var response = await httpClient.GetAsync(uri);

            // Check if the web request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the raw JSON object returned from the web request and read it as a string
                var content = await response.Content.ReadAsStringAsync();

                // Store converted JSON object in global variable for future use
                globalActionPlans = JsonConvert.DeserializeObject<List<ContentModel>>(content);

                // Call function that helps parse the raw JSON string returned from web request
                tagToJsonMap = ParseRawJsonObjectHelper(content);

                // Deserialize the object. In other words, convert the returned string back to the format set by the ContentModel
                var actionPlans = JsonConvert.DeserializeObject<List<ContentModel>>(content);

                // Set the list of ContentModel to the ListView in the ActionPlanPage.xaml file
                ListActionPlan.ItemsSource = actionPlans;
            }
            else
            {
                // This prints to the Visual Studio Output window
                Debug.WriteLine("Response not successful");
            }
        }

        /*
         *  Helper function that parses the returned web request response into a Map that maps tags
         *  to every JSON object that has the respective tag.
         *  
         *  Returns a map of format IDictionary<string, List<ContentModel>>
         */
        private Dictionary<string, List<ContentModel>> ParseRawJsonObjectHelper(string rawJson)
        {
            // Map to be returned
            Dictionary<string, List<ContentModel>> tagsMap = new Dictionary<string, List<ContentModel>>();

            // Convert the web request response returned as a raw JSON to a list of ContentModel objects
            List<ContentModel> contentModelJsonObject = JsonConvert.DeserializeObject<List<ContentModel>>(rawJson);

            // Loop through the array of JSON objects and map tags to the JSON object that has the respective tag
            foreach (ContentModel currentContentModel in contentModelJsonObject)
            {
                // Declare tag array
                string[] tagArray;

                // Check if the tag key has only one element
                if (currentContentModel.tags.IndexOf(',') == -1)
                {
                    // Convert the string into an array
                    tagArray = new string[1];
                    tagArray[0] = currentContentModel.tags;
                }
                else
                {
                    // Else, split tag string by comma and store it in an array
                    tagArray = currentContentModel.tags.Split(',');
                }

                // Loop through the tag array
                foreach (String tag in tagArray)
                {
                    // Removed all spaces aroung tag so we can make sure that ContainsKey() compares the right strings
                    string trimmedTag = tag.Trim();

                    // Check if the tag map already have the current tag as key
                    if (tagsMap.ContainsKey(trimmedTag))
                    {
                        // Add the current contentModel object to the end of the list of ContentModel in which the key is the current tag
                        List<ContentModel> list = tagsMap[trimmedTag];
                        list.Add(currentContentModel);

                        tagsMap[trimmedTag] = list;
                    } 
                    else
                    {
                        // Create a list of ContentModel objects so we can add the current ContentModel object to the tag map
                        List<ContentModel> contentModelList = new List<ContentModel>();
                        contentModelList.Add(currentContentModel);

                        // Else, add the current tag as a new key and its respective list of ContentModel objects
                        tagsMap.Add(trimmedTag, contentModelList);
                    }

                    trimmedTag = null;
                }

                // Clear tag array
                Array.Clear(tagArray, 0, tagArray.Length);
            }

            /* 
             * The following code is used for debugging purposes and might help you understand what you're dealing with.
             * It loops through the tag map and print to the Output console each title, link and tags.
             */

            /*foreach (string key in tagsMap.Keys)
            {
                Debug.Write(key);
                foreach (ContentModel item in tagsMap[key])
                {
                    Debug.Write(item.title);
                    Debug.Write(item.link);
                    Debug.Write(item.tags);
                    Debug.WriteLine("\n");
                }
                Console.WriteLine();
            }*/

            return tagsMap;
        }

        /*
         *  Listener for search bar
         */
        private void HandleTextChange(object sender, EventArgs e)
        {
            // Get the object that triggered the function and cast it to a SearchBar
            SearchBar searchBar = (SearchBar)sender;

            // Remove all leading and trailing spaces from the text entered by the user
            string trimmedText = "";
            if (searchBar.Text != null)
            {
                trimmedText = searchBar.Text.Trim();
            }
            // If the search bar is empty, then clear the ListView
            if (trimmedText.Equals("") || trimmedText == null)
            {
                actionPlanSearchResults.ItemsSource = null;

                // Hide search results ScrollView
                actionPlanPageScrollView.IsVisible = false;
            } else
            {
                // Hide search results ScrollView
                actionPlanPageScrollView.IsVisible = false;

                // Get the searched text and put it in lowercase
                var normalizedQuery = trimmedText?.ToLower() ?? "";

                // Loop through the keys of the tag Map
                foreach (string key in tagToJsonMap.Keys)
                {
                    // If the key starts with the text entered by the user, then proceed
                    if (key.StartsWith(normalizedQuery))
                    {
                        // Show search results ScrollView
                        actionPlanPageScrollView.IsVisible = true;

                        // Create a new list of ContentModel to store JSON objects that match the tag searched
                        List<ContentModel> jsonThatMatchesTagSearched = new List<ContentModel>();

                        // If the key exists in the tag map, then remove the value from the key value pair and store it in the list we created above
                        if (tagToJsonMap.TryGetValue(key, out jsonThatMatchesTagSearched))
                        {
                            // Create new list of strings to store the title of the JSON objects that have the searched tag
                            List<string> searchResults = new List<string>();

                            // Loop through the list of ContentModel that have the JSON objects that matched the searched tag
                            foreach (ContentModel actionPlan in jsonThatMatchesTagSearched)
                            {
                                // Add the action plan title to the search results list
                                searchResults.Add(actionPlan.title);
                            }

                            // Set the list of search results to the ListView in the ActionPlanPage.xaml file
                            actionPlanSearchResults.ItemsSource = searchResults;
                        }
                    }
                }
            }
        }

        /*
         *  Listener for selected action plan
         */
        private void HandleShowStepsTap(object sender, ItemTappedEventArgs e)
        {
            // Get the object that triggered the function, cast it to a ListView and then get its selected item
            var actionPlanList = (ListView)sender;
            var actionPlanItem = (actionPlanList.SelectedItem as ContentModel);

            try
            {
                // Go to the ShowActionItemPage passing the selected item's title, link and steps as parameters
                Navigation.PushAsync(new ShowActionItemPage(actionPlanItem.title, actionPlanItem.link, actionPlanItem.steps));

                // Unselect item.
                actionPlanList.SelectedItem = null;

                // Clear search results ListView
                actionPlanSearchBar.Text = "";
                actionPlanSearchResults.ItemsSource = null;

                // Hide search results ScrollView
                actionPlanPageScrollView.IsVisible = false;
            } catch(Exception error)
            {
                Debug.WriteLine(error);
            }
        }

       /*
        *  Listener for selected action plan searched
        */
        private void HandleTappedActionPlan(object sender, ItemTappedEventArgs e)
        {
            // Get the object that triggered the function, cast it to a ListView and then get its selected item
            var searchResultList = (ListView)sender;
            var selectedActionPlan = searchResultList.SelectedItem.ToString();

            // Loop through the JSON object that stores all the available action plans
            foreach (ContentModel actionPlan in globalActionPlans)
            {
                // If the action plan matches the selected title, then go to ShowActionItemPage passing
                // the selected action plan's title, link and steps as parameters
                if (actionPlan.title.Equals(selectedActionPlan))
                {
                    Navigation.PushAsync(new ShowActionItemPage(actionPlan.title, actionPlan.link, actionPlan.steps));

                    // Clear search results ListView
                    actionPlanSearchBar.Text = "";
                    actionPlanSearchResults.ItemsSource = null;

                    // Hide search results ScrollView
                    actionPlanPageScrollView.IsVisible = false;
                }
            }
        }
    }
}