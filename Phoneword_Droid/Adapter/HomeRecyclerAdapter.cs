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

namespace Phoneword_Droid
{
    public class HomeRecyclerAdapter : RecyclerView.Adapter
    {
        private List<string> itemList;

        public HomeRecyclerAdapter(List<string> items)
        {
            itemList = items;
        }

        public override int ItemCount
        {
            get { return itemList.Count; }
        }

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            if (holder is HomeRecyclerViewHolder)
            {
                (holder as HomeRecyclerViewHolder).TextView.Text = itemList[position];
            }
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row_simplelist, parent, false);
            return new HomeRecyclerViewHolder(v);
        }
    }

    public class HomeRecyclerViewHolder : ViewHolder
    {
        public TextView TextView { get; private set; }

        public HomeRecyclerViewHolder(View v) : base(v)
        {
            TextView = v.FindViewById<TextView>(Resource.Id.list_item);
        }
    }
}