using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TragAppXNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageInfo : ContentPage
    {
        public PageInfo()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}