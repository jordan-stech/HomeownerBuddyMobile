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
            logout_image.Source = ImageSource.FromResource("HOB_Mobile.Resources.logout_icon.png");
        }
    }
}