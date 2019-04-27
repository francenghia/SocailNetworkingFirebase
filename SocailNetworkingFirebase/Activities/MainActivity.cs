using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.View;
using Android.Views.Animations;
using Android.Support.Design.Widget;
using SocailNetworkingFirebase.Adapter;
using Android.Views;
using SocailNetworkingFirebase.Models;
using System.Collections;
using System.Collections.Generic;
using System;
using Android.Content;
using SocailNetworkingFirebase.Activities;
using Android.Preferences;

namespace SocailNetworkingFirebase
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        private ViewPager screenPager;
        //private ScreenIntroAdapter adapter;
        private TabLayout tabIndicator;
        private Button btnNext, btnGetStarted;
        int position = 0;
        Animation animation;
        List<ItemScreenIntro> getDataScreen;




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            if (restoreData())
            {
                var m_intent = new Intent(this, typeof(LoginAndRegisterActivity));
                StartActivity(m_intent);
                Finish();
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

      

            btnNext = FindViewById<Button>(Resource.Id.btn_next);
            btnGetStarted = FindViewById<Button>(Resource.Id.btn_get_started);
            screenPager = FindViewById<ViewPager>(Resource.Id.screen_viewpager);
            tabIndicator = FindViewById<TabLayout>(Resource.Id.tab_indicator);
            animation = AnimationUtils.LoadAnimation(this, Resource.Animator.button_anim);

            getDataScreen = new List<ItemScreenIntro>();
            getDataScreen.Add(new ItemScreenIntro(GetString(Resource.String.title_1), 
                GetString(Resource.String.description_text_1),Resource.Drawable.img1));
            getDataScreen.Add(new ItemScreenIntro(GetString(Resource.String.title_2),
                GetString(Resource.String.description_text_2), Resource.Drawable.img2));
            getDataScreen.Add(new ItemScreenIntro(GetString(Resource.String.title_3),
                GetString(Resource.String.description_text_3), Resource.Drawable.img3));

            
            screenPager.Adapter = new ScreenIntroAdapter(this,getDataScreen);
            tabIndicator.SetupWithViewPager(screenPager);
            btnNext.Click += delegate {
                LoadScreenToClick();
            };

            tabIndicator.TabSelected += (object sender, TabLayout.TabSelectedEventArgs e) =>
            {
                var tab = e.Tab;
                if (tab.Position == getDataScreen.Count - 1)
                {
                    loadLastScreen();
                }
            };
            btnGetStarted.Click += delegate {
                startActivityOnceTimeIntro();
            };
        }

        private void startActivityOnceTimeIntro()
        {
            var m_intent = new Intent(this,typeof(LoginAndRegisterActivity));
            StartActivity(m_intent);
            savePrefeDataOnceTime();
            Finish();
        }

        private void LoadScreenToClick()
        {
            position = screenPager.CurrentItem;
            if (position < getDataScreen.Count)
            {
                position++;
                screenPager.CurrentItem = position;
            }

            if (position == getDataScreen.Count)
            {
                loadLastScreen();
            }
        }

        private Boolean restoreData()
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            Boolean isOnceTime = preferences.GetBoolean("isIntroOnceTime", false);
            return isOnceTime;
        }

        private void savePrefeDataOnceTime()
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = preferences.Edit();
            editor.PutBoolean("isIntroOnceTime", true);
            editor.Apply();
        }

        private void loadLastScreen()
        {
            btnNext.Visibility = ViewStates.Invisible;
            btnGetStarted.Visibility = ViewStates.Visible;
            tabIndicator.Visibility = ViewStates.Invisible;
            btnGetStarted.Animation = animation;
        }
    }
   
}