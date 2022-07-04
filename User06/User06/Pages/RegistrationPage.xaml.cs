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
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void RegistrationButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (NameEntry.Text == null || PasswordEntry1.Text == null || PasswordEntry2.Text == null)
                {
                    await DisplayAlert("Внимание!", "Введите в поля данные!", "OK");
                    return;
                }

                if (PasswordEntry1.Text != PasswordEntry2.Text)
                {
                    await DisplayAlert("Внимание!", "Введеные пароли не совпадают!", "OK");
                    return;
                }

                var isNumeric = int.TryParse(PasswordEntry2.Text, out int passwordInt);

                if (!isNumeric)
                {
                    await DisplayAlert("Внимание!", "Пароль должен содержать от 4 до 6 цифр!", "OK");
                    return;
                }

                int digitCount = (int)Math.Log10(passwordInt) + 1;

                if (digitCount < 4 || digitCount > 6)
                {
                    await DisplayAlert("Внимание!", "Пароль должен содержать от 4 до 6 цифр!", "OK");
                    return;
                }

                var user = new User
                {
                    Name = NameEntry.Text,
                    Password = passwordInt,
                };

                App.Db.SaveUser(user);
                await Navigation.PushAsync(new AuthPage(user));
            }
            catch
            {
                await DisplayAlert("Внимание!", "Проверьте введённые данные", "OK");
            }
        }

        private async void Eye1_Clicked(object sender, EventArgs e)
        {
            PasswordEntry1.IsPassword = false;

            await Task.Delay(1000);

            PasswordEntry1.IsPassword = true;
        }

        private async void Eye2_Clicked(object sender, EventArgs e)
        {
            PasswordEntry2.IsPassword = false;

            await Task.Delay(1000);

            PasswordEntry2.IsPassword = true;
        }
    }
}