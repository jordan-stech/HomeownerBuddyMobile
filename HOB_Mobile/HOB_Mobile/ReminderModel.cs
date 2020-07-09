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
        public int reminderId { get; set; }

        public string reminderDescription { get; set; }
        public string reminderItem { get; set; }
        public int userId { get; set; }
        public string notificationInterval { get; set; }
        public string seasonSpring { get; set; }
        public string seasonSummer { get; set; }
        public string seasonFall { get; set; }
        public string seasonWinter { get; set; }
        public int actionPlanId { get; set; }
        public string actionPlanTitle { get; set; }
        public string actionPlanCategory { get; set; }
        public string actionPlanLink { get; set; }
        public string actionPlanSteps { get; set; }

        // Check to see if a homeowner completed a maintenance task
        public string completed { get; set; }
        public string dueDate { get; set; }
        public string lastCompleted { get; set; }

        public ImageSource icon { get; set; }

    }
}
