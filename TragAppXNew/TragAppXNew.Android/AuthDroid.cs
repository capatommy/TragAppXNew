﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Firebase.Auth;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Firebase.Firestore;
using Firebase;
using Xamarin.Forms.Xaml;
using Java.Util;
using Android.Preferences;
using TragAppXNew.Helpers;

[assembly: Dependency(typeof(TragAppXNew.Droid.AuthDroid))]
namespace TragAppXNew.Droid
{
    public class AuthDroid : INterfaceAuth
    {

        public AuthDroid()
        {

        }

        public async Task<bool> AuthenticateUser(string email, string password)
        {

            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                //shared preferences
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);
                ap.SaveAccessKey(true);
                ap.SaveAccessEmail(email);
                ap.SaveAccessPass(password);

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetCurrentUserId()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public string GetNome()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.DisplayName;
        }

        public bool GetSharedPreferences()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            return ap.GetAccessKey();
        }

        public string GetEmail()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            return ap.GetAccessEmail();
        }

        public string GetPass()
        {
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            return ap.GetAccessPass();
        }
        public bool IsAuthenticated()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null;
        }

        public async Task<bool> LogOut()
        {
            
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            ap.SaveAccessKey(false);
            ap.SaveAccessPass("");
            ap.SaveAccessEmail("");
            FirebaseAuth.Instance.SignOut();
            return true;

        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

                var profileUpdates = new Firebase.Auth.UserProfileChangeRequest.Builder();
                profileUpdates.SetDisplayName(email);
                var build = profileUpdates.Build();
                var user = FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(build);

                //shared preferences
                Context mContext = Android.App.Application.Context;
                AppPreferences ap = new AppPreferences(mContext);
                ap.SaveAccessKey(true);
                ap.SaveAccessEmail(email);
                ap.SaveAccessPass(password);

                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("An unknown error occurred, please try again.");
            }

        }

       

        public async Task<bool> ResetPassword(string email)
        {
            try
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}