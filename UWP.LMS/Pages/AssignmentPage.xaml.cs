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
    public sealed partial class AssignmentPage : Page
    {
        public AssignmentPage()
        {
            InitializeComponent();
            DataContext = new CourseDetailViewModel();
            CourseDetailViewModel.selectedOption = 2;
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text != null)
            {
                ItemBox.ItemsSource = (DataContext as CourseDetailViewModel).Search(SearchBox.Text);
            }
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else
            {
                var diag = new AssignmentTypeDialog(DataContext as CourseDetailViewModel);
                await diag.ShowAsync();
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else
            {
                (DataContext as CourseDetailViewModel).Remove();
                (DataContext as CourseDetailViewModel).RefreshAssignments();
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else if (!((DataContext as CourseDetailViewModel).selectedCourseItem is null))
            {
                var selectedItem = (DataContext as CourseDetailViewModel).selectedCourseItem;
                if ((DataContext as CourseDetailViewModel).selectedCourseItem is AssignmentGroup)
                {
                    var diag = new AssignmentGroupDialog((DataContext as CourseDetailViewModel).AssignmentGroupsOnly, (DataContext as CourseDetailViewModel).selectedCourseItem as AssignmentGroup);
                    await diag.ShowAsync();
                }
                else if ((DataContext as CourseDetailViewModel).selectedCourseItem is Assignment)
                {
                    var diag = new AssignmentDialog((DataContext as CourseDetailViewModel).CurrentAg, (DataContext as CourseDetailViewModel).selectedCourseItem as Assignment);
                    await diag.ShowAsync();
                }

                (DataContext as CourseDetailViewModel).RefreshAssignments();
                ItemBox.SelectedItem = selectedItem;
            }
        }

        private async void Grade_Click(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.loggedInPerson is Student)
            {
                var diag = new NoPermissionDialog();
                await diag.ShowAsync();
            }
            else if ((DataContext as CourseDetailViewModel).selectedCourseItem is Assignment)
            {
                CourseDetailViewModel.selectedAssignment = (DataContext as CourseDetailViewModel).selectedCourseItem as Assignment;
                Frame.Navigate(typeof(AssignmentGradePage), null, new SuppressNavigationTransitionInfo());
            }
        }
    }
}