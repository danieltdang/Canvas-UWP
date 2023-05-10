using Canvas.Library.Models;
using System;
using Canvas.UWP.Dialogs;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentCoursePage : Page
    {
        public StudentCoursePage()
        {
            InitializeComponent();
            DataContext = new CourseViewModel();

            ViewType.Text = $"Student" +
                            $"\n{MainViewModel.loggedInPerson.Name} (GPA: {(MainViewModel.loggedInPerson as Student).GPA:0.00})";
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                CourseBox.ItemsSource = (DataContext as CourseViewModel).Search(SearchBox.Text, false);
            }
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new StudentCourseDialog(MainViewModel.loggedInPerson as Student);
                await diag.ShowAsync();
                Frame.Navigate(typeof(StudentCoursePage), null, new SuppressNavigationTransitionInfo()); // need better implementation of updating listbox
            }
            else
            {
                var diag = new CourseDialog((DataContext as CourseViewModel).Courses);
                await diag.ShowAsync();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseViewModel).Remove();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else if (!(CourseViewModel.selectedCourse is null))
            {
                var diag = new CourseDialog((DataContext as CourseViewModel).Courses, CourseViewModel.selectedCourse);
                await diag.ShowAsync();
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseDetailPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CoursePage));
        }
    }
}
