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
    public partial class AddPasswordPage : ContentPage
    {
        private User _currentUser;

        public AddPasswordPage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Project project = new Project
            {
                User_Id = _currentUser.Id,
                TypeName = TypeEntry.Text,
                Login = LoginEntry.Text,
                Password = PasswordEntry.Text,
                URL = UrlEntry.Text,
            };

            App.Db.SaveProject(project);

            await Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}