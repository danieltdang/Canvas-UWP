using Canvas.Library.Models;
using System;
using Canvas.UWP.Dialogs;
using Canvas.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RosterPage : Page
    {
        public RosterPage()
        {
            InitializeComponent();
            DataContext = new CourseDetailViewModel();
            CourseDetailViewModel.selectedOption = 3;
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
                var diag = new RosterDialog(CourseViewModel.selectedCourse);
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
            }
        }
    }
}
