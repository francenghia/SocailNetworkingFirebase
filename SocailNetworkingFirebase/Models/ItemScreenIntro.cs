using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SocailNetworkingFirebase.Models
{
    public class ItemScreenIntro
    {

        public String title { get; set; }
        public String description { get; set; }
        public int img { get; set; }


        public ItemScreenIntro()
        {

        }

        public ItemScreenIntro(String title, String description, int img)
        {
            this.title = title;
            this.description = description;
            this.img = img;
        }
    }
}