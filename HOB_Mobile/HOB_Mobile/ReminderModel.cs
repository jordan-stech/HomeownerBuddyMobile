using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HOB_Mobile
{
    /*
      * Model that handles each Reminder information.
      * It pulls the information stored in the Azure database through
      * the REST API call implemented on the web app side.
      */
    class ReminderModel
    {
        public int id { get; set; }

        public string reminder { get; set; }

        public string description { get; set; }

        public string notificationInterval { get; set; }

        public ImageSource icon { get; set; }
    }
}
