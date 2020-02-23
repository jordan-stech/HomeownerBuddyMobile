﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOB_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DiagnoseIssuePage : ContentPage
    {
        public DiagnoseIssuePage()
        {
            InitializeComponent();

            SetUpHomeIssueImages();
        }

        /*
         * Listener for "Home Issue Location" button
         */
        private void HandleLocationOfHomeIssueImageClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ActionPlan());
        }

        /*
         * Handle the display of images in the home page
         */
        private void SetUpHomeIssueImages()
        {
            kitchen_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.kitchen.png");
            plumbing_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.plumbing.png");
            bedroom_home_issue_page.Source = ImageSource.FromResource("HOB_Mobile.Resources.bedroom.png");
            bathroom_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.bathroom.png");
            laundry_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.laundry.png");
            exterior_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.exterior.png");
            closet_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.closet.png");
            hallway_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.hallway.png");
            living_room_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.living_room.png");
            misc_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.misc.png");
            hvac_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.HVAC.png");
            electrical_home_issue_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.electric.png");
            //.Source = ImageSource.FromResource("HOB_Mobile.Resources..png");
            //.Source = ImageSource.FromResource("HOB_Mobile.Resources..png");
            //.Source = ImageSource.FromResource("HOB_Mobile.Resources..png");
            //.Source = ImageSource.FromResource("HOB_Mobile.Resources..png");
        }
    }
}