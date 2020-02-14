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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            SetUpImages();
        }

        /*
         * Handle the display of images
         */
        private void SetUpImages()
        {
            unregister_device.Source = ImageSource.FromResource("HOB_Mobile.Resources.logout_icon.png");
        }

        /*
         * Listener for "Unregister" button
         */
        private void handleUnregisterDeviceButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}