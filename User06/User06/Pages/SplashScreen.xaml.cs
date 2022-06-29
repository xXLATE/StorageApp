using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User06.DataBase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace User06.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var users = App.Db.GetUsers();
            try
            {
                var currentUser = users.First();
                await Navigation.PushAsync(new AuthPage(currentUser));
            }
            catch
            {
                await Navigation.PushAsync(new RegistrationPage());
            }
        }
    }
}