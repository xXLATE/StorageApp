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
        private Project _selectedProject;
        private DataBase.Type _selectedType;
        private IEnumerable<DataBase.Type> _types;

        private bool _isChange = false;

        public AddPasswordPage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            TrashButton.IsVisible = false;
            SaveButton.Text = "Сохранить";
            TitleLabel.Text = "Сохранить аккаунт";
        }

        public AddPasswordPage(User currentUser, Project selectedProject)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _selectedProject = selectedProject;

            TrashButton.IsVisible = true;
            SaveButton.Text = "Изменить";
            TitleLabel.Text = "Изменить аккаунт";
            TitleLabel.Margin = new Thickness(-15, 0, 0, 0);

            _isChange = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _types = App.Db.GetTypes();

            TypePicker.ItemsSource = _types.Select(type => type.Name).ToList();

            if (_isChange == true)
            {
                LoginEntry.Text = _selectedProject.Login;
                PasswordEntry.Text = _selectedProject.Password;
                UrlEntry.Text = _selectedProject.URL;
                TypePicker.SelectedItem = _types.First(type => type.Id == _selectedProject.Type_Id).Name;
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (_isChange)
            {
                _selectedProject.Login = LoginEntry.Text;
                _selectedProject.Password = PasswordEntry.Text;
                _selectedProject.URL = UrlEntry.Text;
                _selectedProject.Type_Id = _selectedType.Id;

                App.Db.SaveProject(_selectedProject);
            }
            else
            {
                Project project = new Project
                {
                    Login = LoginEntry.Text,
                    Password = PasswordEntry.Text,
                    URL = UrlEntry.Text,
                    User_Id = _currentUser.Id,
                    Type_Id = _selectedType.Id,
                };

                App.Db.SaveProject(project);
            }

            await Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void TrashButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Удаление", $"Вы действительно хотите удалить {_selectedProject.URL}?", "Да", "Нет"))
            {
                App.Db.DeleteProject(_selectedProject);
                await Navigation.PopAsync();
            }
        }

        private async void AddType_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTypePage(_currentUser));
        }

        private async void Eye_Clicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = false;
            await Task.Delay(1000);
            PasswordEntry.IsPassword = true;
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedType = _types.First(type => type.Name == TypePicker.Items[TypePicker.SelectedIndex]);
            }
            catch { }
        }
    }
}