using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using TragAppXNew.Helpers;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using TragAppXNew.Entities;
using System.IO;
using Firebase.Storage;

namespace TragAppXNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageProfile : ContentPage
    {
        readonly INterfaceAuth auth;
        
        public PageProfile()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            auth = DependencyService.Get<INterfaceAuth>();
            
            setUserName(auth.GetEmail());
            

        }

        async void setUserName(string email)
        {
            var doc = await CrossCloudFirestore.Current
                .Instance
                .GetCollection("utenti")
                .GetDocument(email)
                .GetCollection("Info")
                .GetDocumentsAsync();

            var group = doc.ToObjects<Utente>();

            foreach (Utente a in group)
            {
                StringBuilder myStringBuilder = new StringBuilder();
                myStringBuilder.Append(a.Nome + " " + a.Cognome);
                lblname.Text = myStringBuilder.ToString();
            }
            
        }

        private async void Propic_Clicked(object sender, EventArgs e)
        {
            try
            {

                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    Directory = "Xamarin",
                    SaveToAlbum = true
                });

                if (photo != null)
                    propic.Source = ImageSource.FromStream(() => {  var imageStream = photo.GetStream();
                        return imageStream;
                    });
               // await StoreImages(photo.GetStream());



            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Errore", "Ok");
            }
        }

        /*public async Task<string> StoreImages(Stream imageStream)
        {
            var stroageImage = await new FirebaseStorage("tragappx.appspot.com")
                .Child("XamarinMonkeys")
                .Child("image.jpg")
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;
        }*/

        private void Btn_Reserve_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPage());
            //Navigation.RemovePage(this);
        }

        private async void Btn_LogOut_Clicked(object sender, EventArgs e)
        {
            bool logout = await DisplayAlert("LOGOUT", "Vuoi davvero fare il logout?", "OK", "ANNULLA");

            if (logout)
            {
                bool result = await auth.LogOut();
                if (result)
                {

                    await Navigation.PushAsync(new MainPage());
                    
                }
                else
                {
                    await DisplayAlert("Logout Failed", "Errore nel logout", "OK");
                }
            }

        }
    }
}