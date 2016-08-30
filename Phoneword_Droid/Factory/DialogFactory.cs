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
using Phoneword_Droid;
using Android.Content.Res;

namespace Phoneword.Factory
{
    public class DialogFactory
    {
        public static Dialog CreateDialog(Context context, string tip = null)
        {
            Dialog dialog = new Dialog(context, Resource.Style.dialog);
            dialog.SetContentView(Resource.Layout.Dialog);
            dialog.SetCanceledOnTouchOutside(false);

            WindowManagerLayoutParams wlp = dialog.Window.Attributes;
            int width = (int) (Resources.System.DisplayMetrics.WidthPixels / Resources.System.DisplayMetrics.Density);
            wlp.Width = (int) (0.6 * width);

            TextView tv = dialog.FindViewById<TextView>(Resource.Id.tvLoad);
            if (string.IsNullOrWhiteSpace(tip))
                tv.Text = context.GetString(Resource.String.msg_loading_default);
            else
                tv.Text = tip;

            return dialog;
        }

        public static ProgressDialog CreateProgressDialog(Context context, string tip = null)
        {
            ProgressDialog dialog = new ProgressDialog(context, Resource.Style.dialog);
            dialog.Indeterminate = true;
            if (string.IsNullOrWhiteSpace(tip))
                dialog.SetMessage(context.GetString(Resource.String.msg_loading_default));
            else
                dialog.SetMessage(tip);
            
            return dialog;
        }
    }
}