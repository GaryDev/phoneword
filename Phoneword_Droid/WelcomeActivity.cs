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
using Phoneword;

namespace Phoneword_Droid
{
    [Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
    public class WelcomeActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override int GetLayoutId()
        {
            return Resource.Layout.Welcome;
        }

        protected override void InitView()
        {
            new Handler().PostDelayed(GotoLogin, 3000);
        }

        public void GotoLogin()
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
            Finish();
        }
    }
}