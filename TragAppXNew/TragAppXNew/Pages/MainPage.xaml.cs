﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TragAppXNew.Pages;
using TragAppXNew.Helpers;

namespace TragAppXNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        readonly INterfaceAuth auth;
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<INterfaceAuth>();

            bool firstrun = auth.GetSharedPreferences();
            if (firstrun)
            {
                Btn_signin_Clicked(auth.GetEmail(), auth.GetPass());
            }
        }
        async void Btn_signin_Clicked(object sender, EventArgs e)
        {
            bool result = await auth.AuthenticateUser(Entry_email.Text, Entry_Password.Text);
            if (result)
            {

                await Navigation.PushAsync(new PageProfile());
                Navigation.RemovePage(this);
            }
            else
            {
                ShowError();
            }

        }
        async void Btn_signin_Clicked(string a, string b)
        {
            bool result = await auth.AuthenticateUser(a, b);
            if (result)
            {
                await Navigation.PushAsync(new TabPage());
                Navigation.RemovePage(this);
            }
            else
            {
                ShowError();
            }
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        async void Btn_registration_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
            Navigation.RemovePage(this);
        }

        async void Btn_reset_Clicked(object sender, EventArgs e)
        {
            if (Entry_email.Text == null)
            {
                await DisplayAlert("Errore", "Inserisci l'email se vuoi resettare la password", "OK");
                return;
            }


            bool result = await auth.ResetPassword(Entry_email.Text);
            if (result)
            {
                await DisplayAlert("CHECK BOX MAIL", "Ti è stata inviata un'email", "OK");

            }
            else
            {
                await DisplayAlert("CHECK BOX MAIL", "Qualcosa è andato storto, l'email inserita è errata", "OK");
            }
        }

    }
}