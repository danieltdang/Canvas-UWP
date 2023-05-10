using NetStandard.LMS.Models;
using System;
using UWP.LMS.Dialogs;
using UWP.LMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.LMS.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AssignmentGradePage : Page
    {
        public AssignmentGradePage()
        {
            InitializeComponent();
            DataContext = new CourseDetailViewModel();
            CourseDetailViewModel.selectedOption = 2;
            CourseDetailViewModel.selectedSubOption = 1;
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                ItemBox.ItemsSource = (DataContext as CourseDetailViewModel).Search(SearchBox.Text);
            }
        }

        private async void Grade_Click(object sender, RoutedEventArgs e)
        {
            var diag = new AssignmentGradeDialog(CourseDetailViewModel.selectedAssignment, (DataContext as CourseDetailViewModel).selectedPerson as Student);
            await diag.ShowAsync();
            (DataContext as CourseDetailViewModel).UpdateGrade((DataContext as CourseDetailViewModel).selectedPerson as Student);
            (DataContext as CourseDetailViewModel).RefreshStudents();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AssignmentPage), null, new SuppressNavigationTransitionInfo());
        }
    }
}