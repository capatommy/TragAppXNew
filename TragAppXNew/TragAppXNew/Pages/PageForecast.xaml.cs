using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TragAppXNew.Entities;
using TragAppXNew.Forecast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TragAppXNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageForecast : ContentPage
    { 

        public PageForecast()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            GetForecast();
        }


        private async void GetForecast()
        {
            var url = GenerateRequestUri(Constants.OpenWeatherMapEndpoint);
            var result = await ApiCaller.Get(url);
            var converter = new ColorTypeConverter();

            if (result.Successful)
            {
                try
                {
                    var forecastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forecastInfo.list)
                    {
                        
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }
                    //Sl.BackgroundColor = (Color)converter.ConvertFromInvariantString(UpdateColor(allList[0].weather[0].icon));

                    lbl1prev.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    Sl1.BackgroundColor = (Color)converter.ConvertFromInvariantString( UpdateColor(allList[0].weather[0].icon));
                    img1.Source = UpdateTab(allList[0].weather[0].icon);
                    lbl1temp.Text = $"{allList[0].main.temp.ToString("0")} C";

                    lbl2prev.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    Sl2.BackgroundColor = (Color)converter.ConvertFromInvariantString(UpdateColor(allList[1].weather[0].icon));
                    img2.Source = UpdateTab(allList[1].weather[0].icon);
                    lbl2temp.Text = $"{allList[1].main.temp.ToString("0")} C";

                    lbl3prev.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    Sl3.BackgroundColor = (Color)converter.ConvertFromInvariantString(UpdateColor(allList[2].weather[0].icon));
                    img3.Source = UpdateTab(allList[2].weather[0].icon);
                    lbl3temp.Text = $"{allList[2].main.temp.ToString("0")} C";

                    lbl4prev.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    Sl4.BackgroundColor = (Color)converter.ConvertFromInvariantString(UpdateColor(allList[3].weather[0].icon));
                    img4.Source = UpdateTab(allList[3].weather[0].icon);
                    lbl4temp.Text = $"{allList[3].main.temp.ToString("0")} C";

                    lbl5prev.Text = DateTime.Parse(allList[4].dt_txt).ToString("dd MMM");
                    Sl5.BackgroundColor = (Color)converter.ConvertFromInvariantString(UpdateColor(allList[4].weather[0].icon));
                    img5.Source = UpdateTab(allList[4].weather[0].icon);
                    lbl5temp.Text = $"{allList[4].main.temp.ToString("0")} C";

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q=Sirolo";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&appid={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

         string UpdateTab(string icon)
        {
            string uri;
            switch (icon)
            {
                case "01d":
                    uri = "ic_weather_clear_sky.png";
                    return uri;

                case "01n":
                    uri = "ic_weather_clear_sky.png";
                    return uri;

                case "02d":
                    uri = "ic_weather_few_clouds.png";
                    return uri;

                case "02n":
                    uri = "ic_weather_few_clouds.png";
                    return uri;

                case "03d":
                    uri = "ic_weather_scattered_clouds.png";
                    return uri;

                case "03n":
                    uri = "ic_weather_scattered_clouds.png";
                    return uri;

                case "04d":
                    uri = "ic_weather_broken_clouds.png";
                    return uri;

                case "04n":
                    uri = "ic_weather_broken_clouds.png";
                    return uri;

                case "09d":
                    uri = "ic_weather_shower_rain.png";
                    return uri;

                case "09n":
                    uri = "ic_weather_shower_rain.png";
                    return uri;

                case "10d":
                    uri = "ic_weather_rain.png";
                    return uri;

                case "10n":
                    uri = "ic_weather_rain.png";
                    return uri;

                case "11d":
                    uri = "ic_weather_thunderstorm.png";
                    return uri;

                case "11n":
                    uri = "ic_weather_thunderstorm.png";
                    return uri;

                case "13d":
                    uri = "ic_weather_snow.png";
                    return uri;

                case "13n":
                    uri = "ic_weather_snow.png";
                    return uri;

                case "15d":
                    uri = "ic_weather_mist.png";
                    return uri;


                case "15n":
                    uri = "ic_weather_mist.png";
                    return uri;

                default:
                    return null;

            }
        }

        string UpdateColor(string icon)
        {
            string uri;
            switch (icon)
            {
                case "01d":
                    uri = "#F3644F";
                    return uri;

                case "01n":
                    uri = "#F3644F";
                    return uri;

                case "02d":
                    uri = "#9ABA71";
                    return uri;

                case "02n":
                    uri = "#9ABA71";
                    return uri;

                case "03d":
                    uri = "#8EB7EB";
                    return uri;

                case "03n":
                    uri = "#8EB7EB";
                    return uri;

                case "04d":
                    uri = "#856799";
                    return uri;

                case "04n":
                    uri = "#856799";
                    return uri;

                case "09d":
                    uri = "#A46B58";
                    return uri;

                case "09n":
                    uri = "#A46B58";
                    return uri;

                case "10d":
                    uri = "#67BCB7";
                    return uri;

                case "10n":
                    uri = "#67BCB7";
                    return uri;

                case "11d":
                    uri = "#B4A699";
                    return uri;

                case "11n":
                    uri = "#B4A699";
                    return uri;

                case "13d":
                    uri = "#8D9C9F";
                    return uri;

                case "13n":
                    uri = "#8D9C9F";
                    return uri;

                case "15d":
                    uri = "#D870A5";
                    return uri;


                case "15n":
                    uri = "#D870A5";
                    return uri;

                default:
                    return null;

            }
        }

    }
}