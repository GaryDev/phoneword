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
using Android.Support.V7.Widget;
using static Android.Support.V7.Widget.RecyclerView;
using Phoneword_Droid.Model;

namespace Phoneword_Droid
{
    public class NavigationDrawerAdapter : RecyclerView.Adapter
    {
        private List<NavDrawerItem> navItems = new List<NavDrawerItem>();
        private Context ctx;
        private LayoutInflater inflater;

        public NavigationDrawerAdapter(Context context, List<NavDrawerItem> items)
        {
            ctx = context;
            inflater = LayoutInflater.From(context);
            navItems = items;
        }

        public event EventHandler<int> ItemClick;

        public override int ItemCount
        {
            get { return navItems.Count; }
        }

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            NavDrawerItem currentItem = navItems[position];
            if (holder is NavigationDrawerAdapterViewHolder)
                (holder as NavigationDrawerAdapterViewHolder).TextView.Text = currentItem.Title;
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = inflater.Inflate(Resource.Layout.nav_drawer_row, parent, false);
            NavigationDrawerAdapterViewHolder viewHolder = new NavigationDrawerAdapterViewHolder(view, OnClick);
            return viewHolder;
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }

    public class NavigationDrawerAdapterViewHolder : ViewHolder
    {
        public TextView TextView { get; private set; }

        public NavigationDrawerAdapterViewHolder(View v, Action<int> listener) 
            : base(v)
        {
            TextView = v.FindViewById<TextView>(Resource.Id.title);
            TextView.Click += (sender, e) => listener(LayoutPosition);
        }
    }

}