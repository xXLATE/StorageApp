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
        private IEnumerable<Project> _projects;
        private IEnumerable<DataBase.Type> _types;

        public MainPage(User currentUser)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            _currentUser = currentUser;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _types = App.Db.GetTypesByUser(_currentUser.Id);
            _projects = App.Db.GetProjectsByUser(_currentUser.Id);

            List<string> filterList = new List<string> { "All Types" };
            filterList.AddRange(_types.Select(type => type.Name).ToList());

            FilterPicker.ItemsSource = filterList;

            List<ProjectForListView> projectForListViews = new List<ProjectForListView>();

            foreach (var project in _projects)
            {
                var projectTypeName = _types.First(type => type.Id == project.Type_Id).Name;
                var projectForListView = new ProjectForListView(project.Id, project.URL, projectTypeName);
                projectForListViews.Add(projectForListView);
            }

            ProjectsLV.ItemsSource = projectForListViews;
        }

        private async void NewProject_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPasswordPage(_currentUser));
        }

        private async void ProjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedProjectForListView = (ProjectForListView)e.Item;
            var selectedProject = _projects.First(project => project.Id == selectedProjectForListView.ProjectID);

            await Navigation.PushAsync(new AddPasswordPage(_currentUser, selectedProject));
        }

        private void FilterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<Project> projectListForFilter = new List<Project>();
                List<ProjectForListView> projectForListViews = new List<ProjectForListView>();

                var selectedTypeString = FilterPicker.Items[FilterPicker.SelectedIndex];

                if (selectedTypeString == "All Types")
                    projectListForFilter = _projects.ToList();
                else
                {
                    var selectedTypeId = _types.First(x => x.Name == selectedTypeString).Id;
                    projectListForFilter = _projects.Where(x => x.Type_Id == selectedTypeId).ToList();
                }

                foreach (var project in projectListForFilter)
                {
                    var projectTypeName = _types.First(type => type.Id == project.Type_Id).Name;
                    var projectForListView = new ProjectForListView(project.Id, project.URL, projectTypeName);
                    projectForListViews.Add(projectForListView);
                }

                ProjectsLV.ItemsSource = projectForListViews;
            }
            catch { }
        }
    }

    public struct ProjectForListView
    {
        public ProjectForListView(int projectId, string url, string typeName)
        {
            ProjectID = projectId;
            URL = url;
            TypeName = typeName;
        }

        public int ProjectID { get; }
        public string URL { get; }
        public string TypeName { get; }
    }
}