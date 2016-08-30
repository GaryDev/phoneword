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
using Phoneword.Factory;
using Phoneword.Util;
using Phoneword.DataModel;
using Phoneword.Command;
using Android.Support.V7.App;

namespace Phoneword_Droid
{
    [Activity(Label = "BaseActivity")]
    public abstract class BaseActivity : AppCompatActivity
    {
        protected Dialog mDialog;
        protected ISharedPreferences iSharedPref;

        private OnFailureHandler _errorHandler;
        protected OnFailureHandler ErrorHandler
        {
            get
            {
                if (_errorHandler == null)
                    _errorHandler = new OnFailureHandler(HandleErrors);
                return _errorHandler;
            }
        }

        private AppCommand _appCommand;
        protected AppCommand AppCommand
        {
            get
            {
                if (_appCommand == null)
                    _appCommand = new AppCommand();
                return _appCommand;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);

            int layoutId = GetLayoutId();
            SetContentView(layoutId);

            iSharedPref = GetSharedPreferences("loginInfo", FileCreationMode.Private);

            InitView();
        }

        protected virtual void InitView() { }
        protected abstract int GetLayoutId();

        protected void ShowLoading(string tip = null)
        {
            if (mDialog != null)
            {
                mDialog.Dismiss();
                mDialog = null;
            }
            //mDialog = DialogFactory.CreateDialog(this, tip);
            mDialog = DialogFactory.CreateProgressDialog(this, tip);
            mDialog.Show();
        }

        protected void HideLoading()
        {
            if (mDialog != null)
            {
                mDialog.Hide();
            }
        }

        protected virtual void HandleErrors(string resContent)
        {
            HideLoading();
            ApiError error = JSONUtil.ToObject<ApiError>(resContent);

            string text;
            if (error != null)
                text = string.Format("({0}) - {1}", error.ErrorCode, error.ErrorMessage);
            else
                text = GetString(Resource.String.msg_api_fail);

            Toast.MakeText(this, text, ToastLength.Long).Show();
        }

    }
}