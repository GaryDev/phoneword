using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V7.Widget;

namespace Phoneword_Droid
{
    public class HomeTabFragment : Fragment
    {
        private const string TAB_INDEX = "tab_index";

        public static HomeTabFragment CreateInstance(int tabIndex)
        {
            HomeTabFragment fragment = new HomeTabFragment();
            Bundle args = new Bundle();
            args.PutInt(TAB_INDEX, tabIndex);
            fragment.Arguments = args;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            int tabIndex = Arguments.GetInt(TAB_INDEX);

            //TextView view = new TextView(Activity);
            //view.Gravity = GravityFlags.Center;
            //view.Text = string.Format("Text in tab # {0}", tabIndex);

            List<string> items = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                items.Add(string.Format("Tab # {0} item # {1}", tabIndex, i));
            }

            View view = inflater.Inflate(Resource.Layout.recycler_view, container, false);
            RecyclerView recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerview);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            recyclerView.SetAdapter(new HomeRecyclerAdapter(items));

            return view;
        }
    }
}