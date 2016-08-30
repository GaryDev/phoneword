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
using Phoneword.Command;
using Phoneword.DataModel;
using Phoneword.Util;
using Phoneword_Droid;

namespace Phoneword
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : BaseActivity
    {
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private TextView mTxvSignup;
        private Button mBtnLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override int GetLayoutId()
        {
            return Resource.Layout.Login3;
        }

        protected override void InitView()
        {
            mTxtEmail = FindViewById<EditText>(Resource.Id.input_email);
            mTxtPassword = FindViewById<EditText>(Resource.Id.input_password);
            mTxvSignup = FindViewById<TextView>(Resource.Id.link_signup);

            mBtnLogin = FindViewById<Button>(Resource.Id.btn_login);
            mBtnLogin.Click += OnLoginClicked;
        }

        protected override void HandleErrors(string resContent)
        {
            mBtnLogin.Enabled = true;
            base.HandleErrors(resContent);
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(true);
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string email = mTxtEmail.Text.Trim();
            string password = mTxtPassword.Text.Trim();

            if (!Validate(email, password))
                return;

            mBtnLogin.Enabled = false;
            LoginApp(email, password);
        }

        private bool Validate(string email, string password)
        {
            bool valid = true;

            mTxtEmail.Error = null;
            if (string.IsNullOrWhiteSpace(email) || !Android.Util.Patterns.EmailAddress.Matcher(email).Matches())
            {
                mTxtEmail.Error = GetString(Resource.String.msg_invalid_email);
                valid = false;
            }

            mTxtPassword.Error = null;
            if (string.IsNullOrWhiteSpace(password) || password.Length < 4 || password.Length > 10)
            {
                mTxtPassword.Error = GetString(Resource.String.msg_invalid_password);
                valid = false;
            }

            return valid;
        }

        private void LoginApp(string email, string password)
        {
            UserLogin user = new UserLogin { Email = email, Password = password };

            ShowLoading(GetString(Resource.String.msg_loading_auth));
            AppCommand.DoLogin(user,
                new OnSuccessHandler(
                    delegate (string resContent)
                    {
                        HideLoading();
                        UserLoginData userData = JSONUtil.ToObject<UserLoginData>(resContent);
                        if (userData != null)
                        {
                            iSharedPref.Edit().PutString("Username", userData.Username).Apply();
                            iSharedPref.Edit().PutString("Token", userData.SessionToken).Apply();

                            Toast.MakeText(this, GetString(Resource.String.msg_login_success), ToastLength.Long).Show();
                            //var intent = new Intent(this, typeof(MainActivity));
                            var intent = new Intent(this, typeof(HomeActivity));
                            StartActivity(intent);
                            Finish();
                        }
                        else
                        {
                            Toast.MakeText(this, GetString(Resource.String.msg_login_fail), ToastLength.Long).Show();
                            mTxtEmail.Text = string.Empty;
                            mTxtPassword.Text = string.Empty;
                            mTxtEmail.RequestFocus();
                        }
                    }
                ), ErrorHandler);
        }
        
    }

    
}