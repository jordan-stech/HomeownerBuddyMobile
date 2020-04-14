using Xamarin.Forms;

namespace HOB_Mobile
{
    /*
     * Model that handles each Trusted Service Provider information.
     * It pulls the information stored in the Azure database through
     * the REST API call implemented on the web app side.
     */
    class ServiceProviderModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string service { get; set; }
        public string phone_number { get; set; }
        public string url { get; set; }
        public ImageSource phone_icon { get; set; }
        public ImageSource website_icon { get; set; }
    }
}
