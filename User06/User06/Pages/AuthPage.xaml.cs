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
    public partial class AuthPage : ContentPage
    {
        private User _currentUser;
        private string _inputPassword;

        public AuthPage(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            WelcomeText.Text = $"ПРИВЕТ, {currentUser.Name}";
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "1";
        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "2";
        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "3";
        }

        private void Btn4_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "4";
        }

        private void Btn5_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "5";
        }

        private void Btn6_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "6";
        }

        private void Btn7_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "7";
        }

        private void Btn8_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "8";
        }

        private void Btn9_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "9";
        }

        private void Btn0_Clicked(object sender, EventArgs e)
        {
            EField.Text += "*";
            _inputPassword += "0";
        }

        private async void BtnOkay_Clicked(object sender, EventArgs e)
        {
            if (_inputPassword == _currentUser.Password.ToString())
                await Navigation.PushAsync(new MainPage(_currentUser));
            else
                await DisplayAlert("Внимание!", "Неверный пароль!", "OK");

            EField.Text = "";
            _inputPassword = "";
        }
    }
}