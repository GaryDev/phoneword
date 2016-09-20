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
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using static Android.Support.Design.Widget.NavigationView;
using Android.Widget;
using Android.Util;

namespace Phoneword_Droid
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : BaseActivity
    {
        private DrawerLayout mDrawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override int GetLayoutId()
        {
            return Resource.Layout.Home;
        }

        protected override void InitView()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //SupportActionBar.Title = iSharedPref.GetString("Username", string.Empty);
            SupportActionBar.Title = string.Empty;
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetHomeButtonEnabled(true);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            NavigationView navView = FindViewById<NavigationView>(Resource.Id.navigation_view);
            navView.NavigationItemSelected += OnNavViewItemSelected;

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += OnFabButtonClicked;

            HomePagerAdapter homePagerApt = new HomePagerAdapter(SupportFragmentManager);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = homePagerApt;
            TabLayout tab = FindViewById<TabLayout>(Resource.Id.tablayout);
            tab.SetupWithViewPager(viewPager);
        }

        private void OnFabButtonClicked(object sender, EventArgs e)
        {
            Snackbar bar = Snackbar.Make(FindViewById(Resource.Id.coordinator), "I'm a Snack bar.", Snackbar.LengthLong);
            bar.SetAction("Action", (View v) => {
                Toast.MakeText(this, "Snaker Action", ToastLength.Long).Show();
            });
            bar.Show();
        }

        private void OnNavViewItemSelected(object sender, NavigationItemSelectedEventArgs e)
        {
            IMenuItem menuItem = e.MenuItem;

            menuItem.SetChecked(true);
            mDrawerLayout.CloseDrawers();
            Toast.MakeText(this, menuItem.ToString(), ToastLength.Long).Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_search, menu);

            IMenuItem searchMenu = menu.FindItem(Resource.Id.action_search);
            searchMenu.ExpandActionView();

            SearchView searchView = (SearchView)searchMenu.ActionView;
            searchView.Iconified = false;
            searchView.SearchClick += OnSearchViewIconClicked;

            return true;
        }

        private void OnSearchViewIconClicked(object sender, EventArgs e)
        {
            GoToSearchView();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (id)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer(GravityCompat.Start);
                    break;
                case Resource.Id.action_search:
                    //GoToSearchView();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void GoToSearchView()
        {
            Intent intent = new Intent(this, typeof(SearchActivity));
            StartActivity(intent);
        }
    }
}