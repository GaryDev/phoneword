using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using Android.Support.V4.Widget;
using Android.Util;

namespace Phoneword_Droid
{
    [Activity(Label = "Home2Activity")]
    public class Home2Activity : BaseActivity
    {
        private static string TAG = typeof(Home2Activity).Name;

        private Toolbar mToolbar;
        private NavDrawerFragment drawerFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override int GetLayoutId()
        {
            return Resource.Layout.Home2;
        }

        protected override void InitView()
        {
            mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(mToolbar);
            SupportActionBar.Title = string.Empty;
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            DrawerLayout drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout2);

            drawerFragment = (NavDrawerFragment)SupportFragmentManager.FindFragmentById(Resource.Id.fragment_navigation_drawer);
            drawerFragment.InitFragment(Resource.Id.fragment_navigation_drawer, drawerLayout, mToolbar);
            drawerFragment.OnDrawerItemSelected += OnNavDrawerItemSelected;

            DisplayView(0);
        }

        private void OnNavDrawerItemSelected(object sender, NavDrawerFragmentEventArgs e)
        {
            //Log.Debug(TAG, "sender => {0}", sender.GetType().Name);
            //Log.Debug(TAG, "args => position #{0}", e.Position);
            DisplayView(e.Position);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu2, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (id)
            {
                case Resource.Id.menu_action_search:
                    break;
                case Resource.Id.menu_action_setting:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void DisplayView(int position)
        {
            Fragment fragment = null;
            string title = GetString(Resource.String.ApplicationName);
            switch (position)
            {
                case 0:
                    fragment = new HomeFragment();
                    title = GetString(Resource.String.title_home);
                    break;
            }
            if (fragment != null)
            {
                FragmentTransaction fragmentTransaction = SupportFragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.container_body, fragment);
                fragmentTransaction.Commit();

                // set the toolbar title
                SupportActionBar.Title = title;
            }
        }
    }
}