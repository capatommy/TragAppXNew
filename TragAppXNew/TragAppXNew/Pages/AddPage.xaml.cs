using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TragAppXNew.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TragAppXNew.Helpers;

namespace TragAppXNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public string date;
        static public int month;
        static public int day;
        static public int year;

        static string andata, ritorno;

        static public int oraAnd;
        static public int oraRit;
        static public int minAnd;
        static public int minRit;

        static public int counter;

        readonly INterfaceAuth auth;
        //DateTime dt;

        public AddPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<INterfaceAuth>();
            counter = 0;
            DatePicker.SetValue(DatePicker.MinimumDateProperty, DateTime.Today);
            year = DatePicker.Date.Year;
            month = DatePicker.Date.Month;
            day = DatePicker.Date.Day;
            date = DatePicker.Date.ToLongDateString();
        }

        private void DatePickerDateSelected(object sender, EventArgs e)
        {
            year = DatePicker.Date.Year;
            month = DatePicker.Date.Month;
            day = DatePicker.Date.Day;
            date = DatePicker.Date.ToLongDateString();
        }

        private void AndataSelectedIndexChanged(object sender, EventArgs e)
        {
            andata = AndataPicker.Items[AndataPicker.SelectedIndex];
            oraAnd = Convert.ToInt32(andata.Substring(0, 2));
            minAnd = Convert.ToInt32(andata.Substring(3, 2));


        }

        private void RitornoSelectedIndexChanged(object sender, EventArgs e)
        {
            ritorno = RitornoPicker.Items[RitornoPicker.SelectedIndex];
            oraRit = Convert.ToInt32(ritorno.Substring(0, 2));
            minRit = Convert.ToInt32(ritorno.Substring(3, 2));

        }

        private void Btn_Plus_Clicked(object sender, EventArgs e)
        {
            counter++;
            lblcounter.Text = $"{ counter}";
        }

        private void Btn_Minus_Clicked(object sender, EventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                lblcounter.Text = $"{ counter}";
            }

        }

        private async void Btn_Reserve_Clicked(object sender, EventArgs e)
        {
            var email = auth.GetEmail();

            if (check(oraAnd, oraRit, date, counter))
            {

                var query = await CrossCloudFirestore.Current
                    .Instance
                    .GetCollection("prenotazioni")
                    .GetDocumentsAsync();

                var reserves = query.ToObjects<Prenotazione>();
                int count = 0;
                if (query.Count != 0)
                {
                    foreach (Prenotazione doc in reserves)
                    {
                        if (String.Equals(doc.data, date) && oraAnd == doc.oraAnd &&
                            minAnd == doc.minutiAnd)
                        {
                            count += doc.numAd;
                        }
                    }
                }

                if (counter + count <= 10)
                {
                    Prenotazione pren = new Prenotazione(oraAnd, minAnd, oraRit, minRit, date, counter);
                    await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("utenti")
                        .GetDocument(email)
                        .GetCollection("prenotazioni")
                        .AddDocumentAsync(pren);

                    await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("prenotazioni")
                        .AddDocumentAsync(pren);

                    await DisplayAlert("Successo", "Prenotazione effettuata", "OK");

                    //await Navigation.PushAsync(new TabPage());
                    Navigation.RemovePage(this);
                }
                else
                {
                    await DisplayAlert("Full", "Barche piene, seleziona un altra data", "OK");
                }
            }

        }

        public bool check(int oraAnd, int oraRit, string date, int counter)
        {
            if (date == null)
            {
                DisplayAlert("ErrorDate", "Seleziona data", "OK");
                return false;
            }

            if (counter == 0)
            {
                DisplayAlert("ErrorNum", "Inserisci numero persone", "OK");
                return false;
            }

            if (oraAnd > oraRit)
            {
                DisplayAlert("ErroreOra", "Seleziona orario corretto", "OK");
                return false;
            }

            if (oraAnd == 0 || oraRit == 0)
            {
                DisplayAlert("ErroreOra", "Seleziona orario corretto", "OK");
                return false;
            }

            return true;
        }
    }
}