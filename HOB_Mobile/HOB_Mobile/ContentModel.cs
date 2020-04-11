using System.Collections.Generic;
using System.Linq;

namespace HOB_Mobile
{
    /*
      * Model that handles each Action Plan information.
      * It pulls the information stored in the Azure database through
      * the REST API call implemented on the web app side.
      */
    class ContentModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string steps { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
    }
}
