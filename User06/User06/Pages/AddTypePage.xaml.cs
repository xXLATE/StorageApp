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
    public partial class AddTypePage : ContentPage
    {
        private User _currentUser;

        public AddTypePage(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var newType = new DataBase.Type()
            {
                Name = TypeEntry.Text,
                User_Id = _currentUser.Id,
            };

            App.Db.SaveType(newType);

            await Navigation.PopAsync();
        }
    }
}