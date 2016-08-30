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
using Java.Lang;

namespace Phoneword_Droid
{
    public class HomePagerAdapter : FragmentStatePagerAdapter
    {
        private const int TAB_COUNT = 3;

        public HomePagerAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count
        {
            get { return TAB_COUNT; }
        }

        public override Fragment GetItem(int position)
        {
            return HomeTabFragment.CreateInstance(position);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            string title = string.Format("TAB {0}", position);
            return new Java.Lang.String(title);
        }
    }
}