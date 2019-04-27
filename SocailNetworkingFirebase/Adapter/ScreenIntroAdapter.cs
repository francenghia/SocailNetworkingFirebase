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
using Android.Support.V4.View;
using Java.Lang;
using SocailNetworkingFirebase.Models;

namespace SocailNetworkingFirebase.Adapter
{
    public class ScreenIntroAdapter : PagerAdapter

    {
        Context context;
        List<ItemScreenIntro> listScreen;



        public ScreenIntroAdapter(Context context, List<ItemScreenIntro> listScreen)
        {
            this.context = context;
            this.listScreen = listScreen;
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            View layoutScreen = inflater.Inflate(Resource.Layout.layout_screen, null);
            ImageView img = layoutScreen.FindViewById<ImageView>(Resource.Id.intro_img);
            TextView txtTitle = layoutScreen.FindViewById<TextView>(Resource.Id.intro_title);
            TextView txtDescription = layoutScreen.FindViewById<TextView>(Resource.Id.intro_description);

            txtTitle.Text = listScreen[position].title;
            txtDescription.Text = listScreen[position].description;
            img.SetImageResource(listScreen[position].img);

            var viewPager = container.JavaCast<ViewPager>();
            viewPager.AddView(layoutScreen);
            return layoutScreen;
        }

        public override int Count
        {
           get { return listScreen.Count; }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == @object;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }

    }
}