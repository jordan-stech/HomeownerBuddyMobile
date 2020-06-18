using System;
using System.Collections.Generic;
using System.Text;

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

        public string sent { get; set; }

        public string completed { get; set; }
    }
}
