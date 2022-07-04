using System;
using System.IO;
using System.Linq;
using User06.DataBase;
using User06.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace User06
{
    public partial class App : Application
    {
        private static DataAccess db;
        private const string DATABASE_NAME = "User06.db";

        public static DataAccess Db
        {
            get
            {
                if (db == null)
                {
                    db = new DataAccess(Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            var users = Db.GetUsers();
            try
            {
                var currentUser = users.First();
                MainPage = new NavigationPage(new AuthPage(currentUser))
                {
                    BarBackgroundColor = Color.Black
                };
            }
            catch
            {
                MainPage = new NavigationPage(new RegistrationPage())
                {
                    BarBackgroundColor = Color.Black
                };
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
