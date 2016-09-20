using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using Phoneword_Droid.Model;
using ActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Phoneword_Droid
{
    public class NavDrawerFragment : Fragment
    {
        private static string TAG = typeof(NavDrawerFragment).Name;

        private RecyclerView recyclerView;
        private NavigationDrawerAdapter adapter;
        private ActionBarDrawerToggle mDrawerToggle;
        private View containerView;

        private static string[] titles = null;
        private DrawerLayout mDrawerLayout;

        public event EventHandler<NavDrawerFragmentEventArgs> OnDrawerItemSelected;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            titles = Activity.Resources.GetStringArray(Resource.Array.nav_drawer_labels);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View layout = inflater.Inflate(Resource.Layout.fragment_navigation_drawer, container, false);
            recyclerView = layout.FindViewById<RecyclerView>(Resource.Id.drawerList);

            adapter = new NavigationDrawerAdapter(Activity, GetData());
            adapter.ItemClick += OnItemClicked;
            recyclerView.SetAdapter(adapter);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            return layout; 
        }

        private void OnItemClicked(object sender, int position)
        {
            OnDrawerItemSelected?.Invoke(sender, new NavDrawerFragmentEventArgs(position));
            mDrawerLayout.CloseDrawer(containerView);
        }

        public void InitFragment(int fragmentId, DrawerLayout drawerLayout, Toolbar toolbar)
        {
            containerView = Activity.FindViewById<View>(fragmentId);
            mDrawerLayout = drawerLayout;
            mDrawerToggle = new NavDrawerActionBarDrawerToggle(Activity, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            mDrawerLayout.Post(() => {
                mDrawerToggle.SyncState();
            });
        }

        public static List<NavDrawerItem> GetData()
        {
            List<NavDrawerItem> data = new List<NavDrawerItem>();
            foreach (string title in titles)
            {
                data.Add(new NavDrawerItem { Title = title });
            }
            return data;
        }
    }

    public class NavDrawerFragmentEventArgs : EventArgs
    {
        public NavDrawerFragmentEventArgs(int position)
        {
            Position = position;
        }

        public int Position { get; }
    }

    public class NavDrawerActionBarDrawerToggle : ActionBarDrawerToggle
    {
        private Android.App.Activity _activity;
        private Toolbar _toolbar;

        public NavDrawerActionBarDrawerToggle(Android.App.Activity activity, DrawerLayout drawerLayout, Toolbar toolbar, int openDrawerContentDescRes, int closeDrawerContentDescRes)
            : base(activity, drawerLayout, toolbar, openDrawerContentDescRes, closeDrawerContentDescRes)
        {
            _activity = activity;
            _toolbar = toolbar;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            _activity.InvalidateOptionsMenu();
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            _activity.InvalidateOptionsMenu();
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
            _toolbar.Alpha = 1 - slideOffset / 2;
        }
    }
}