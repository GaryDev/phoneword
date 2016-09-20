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
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Phoneword_Droid
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : BaseActivity
    {
        private SearchView mSearchView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override int GetLayoutId()
        {
            return Resource.Layout.Search;
        }

        protected override void InitView()
        {
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = string.Empty;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_search, menu);

            mSearchView = (SearchView)menu.FindItem(Resource.Id.action_search).ActionView;
            mSearchView.SetIconifiedByDefault(false);

            return true;
        }
    }
}