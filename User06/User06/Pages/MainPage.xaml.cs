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
    public partial class MainPage : ContentPage
    {
        private User _currentUser;

        public MainPage(User currentUser)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            _currentUser = currentUser;
        }

        protected override void OnAppearing()
        {
            ProjectsLV.ItemsSource = App.Db.GetProjectsByUser(_currentUser.Id);
            base.OnAppearing();
        }

        private async void NewProject_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_currentUser));
        }

        private async void ProjectsLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Project selectedProject = (Project)e.SelectedItem;
            //await Navigation.PushAsync(new Project(selectedProject, _currentUser)
            //{
            //    BindingContext = selectedProject
            //});
        }
    }
}